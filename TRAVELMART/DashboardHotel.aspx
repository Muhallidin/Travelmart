<%@ Page Title="" Language="C#" MasterPageFile="~/TravelmartMaster.Master" AutoEventWireup="true" CodeBehind="DashboardHotel.aspx.cs" Inherits="TRAVELMART.DashboardHotel" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<%--    <script language ="javascript" type="text/javascript">
        $(document).ready(function() {
            Settings();
        });

        function pageLoad(sender, args) {
            var isAsyncPostback = Sys.WebForms.PageRequestManager.getInstance().get_isInAsyncPostBack();
            if (isAsyncPostback) {
                Settings();
            }
        }

        function Settings() {
            //Variables
            var imgDashboard = $("#<%=uoImageHotl.ClientID %>");
//            var ViewDashboard = $("#<%=uoHiddenFieldDashboard.ClientID %>")
            var ViewRoomType = $("#<%=ucLabelDetails.ClientID %>")

            ViewRoomType

            imgDashboard.click(function(ev) {
                if (ViewDashboard.val() == "0") {
                    ViewDashboard.val("1");
                }
                else {
                    ViewDashboard.val("0");
                }
            });
         
        }        
   </script>--%>
    
<%--    <div class="ViewTitlePadding">
    <table  width="100%">
        <tr>
            <td class="PageTitle">
                Dashboard Hotel
            </td>
        </tr>
    </table>
</div>--%>
<div class="PageTitle">
    <asp:Panel ID="Panel3" runat="server">
        Dashboard Hotel
    </asp:Panel>
</div>
<br />
<div class="PageTitle">
    <asp:Panel ID="PanelHead" runat="server">
        Room Type Details
        <asp:Label ID="ucLabelDetails" runat="server" Text="" Font-Size="Small" />
    </asp:Panel>
</div>

<asp:Panel ID="Panel1" runat="server">
<table width="100%" cellspacing="0" border="0">
<tr>
<td>
<asp:ListView runat="server" ID="uoListViewDashboard"
        onprerender="uoListViewDashboard_PreRender" 
        onitemcommand="uoListViewDashboard_ItemCommand" >
                    <LayoutTemplate>
                        <table border="0" cellpadding="0" cellspacing="0" class="listViewTable" width="100%">
                            <tr>

                                <th>
                                    Status
                                </th>
                                <th>
                                    Block/s
                                </th>

                            </tr>
                            <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                        </table>
                    </LayoutTemplate>
                    <ItemTemplate>
                     <%# DashboardAddGroup()%>  
                        <tr>

                            <td class="leftAligned">
                                <%# Eval("colHotelStatusVarchar")%>
                            </td>
                            <td class="RightAligned">
                                <%# Eval("countr")%>
                            </td>

                        </tr>
                    </ItemTemplate>
                    <EmptyDataTemplate>
                        <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                            <tr>
                                <th>
                                    Status
                                </th>
                                <th>
                                    Block/s
                                </th>
                            </tr>
                            <tr>
                                <td colspan="2" class="leftAligned">
                                    No Record
                                </td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                </asp:ListView>
                <asp:DataPager ID="uoListViewDashboardPager" runat="server" PagedControlID="uoListViewDashboard"
                    PageSize="20" OnPreRender="uoListViewDashboard_PreRender">
                    <Fields>
                        <asp:NumericPagerField ButtonType="link" NumericButtonCssClass="PagerClass" />
                    </Fields>
                </asp:DataPager>
           </td>        
</tr>
</table>
</asp:Panel>
                <cc1:CollapsiblePanelExtender ID="CollapsiblePanelExtender1" runat="server" TargetControlID="Panel1"
                    ExpandControlID="PanelHead" CollapseControlID="PanelHead" TextLabelID="ucLabelDetails"
                    CollapsedText="(Show List)" ExpandedText="(Hide List)" Collapsed="false" SuppressPostBack="true">
                </cc1:CollapsiblePanelExtender>

    <hr />
<div class="PageTitle">
    <asp:Panel ID="uopanelroomhead" runat="server">
        Weekly Room Allocation
        <asp:Label ID="uolabelRoom" runat="server" Text="" Font-Size="Small" />
    </asp:Panel>
</div>

 <asp:Panel ID="Panel2" runat="server" CssClass="CollapsiblePanel">
 <table width="100%" cellspacing="0" border="0">
 <tr >
 <td >
 <asp:ListView runat="server" ID="uoListViewDashboard2" 
        onprerender="uoListViewDashboard2_PreRender" 
        onitemcommand="uoListViewDashboard2_ItemCommand" >
                    <LayoutTemplate>
                        <table border="0" cellpadding="0" cellspacing="0" class="listViewTable" width="100%">
                            <tr>
                                <th>
                                    Room Type
                                </th>
                                <th>
                                    Monday
                                </th>
                                <th>
                                    Tuesday
                                </th>
                                <th>
                                    Wednesday
                                </th>
                                <th>
                                    Thursday
                                </th>
                                <th>
                                    Friday
                                </th>
                                <th>
                                    Saturday
                                </th>
                                <th>
                                    Sunday
                                </th>
                            </tr>
                            <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                        </table>
                    </LayoutTemplate>
                    <ItemTemplate>
                   <%--  <%# DashboardAddGroup()%>  --%>
                        <tr>
                            <td class="leftAligned">
                                <%# Eval("colRoomNameVarchar")%>
                            </td>
                            <td class="RightAligned">
                                <%# Eval("colMonInt")%>
                            </td>
                            <td class="RightAligned">
                                <%# Eval("colTuesInt")%>
                            </td>
                            <td class="RightAligned">
                                <%# Eval("colWedInt")%>
                            </td>
                            <td class="RightAligned">
                                <%# Eval("colThursInt")%>
                            </td>                            
                            <td class="RightAligned">
                                <%# Eval("colFriInt")%>
                            </td>
                            <td class="RightAligned">
                                <%# Eval("colSatInt")%>
                            </td>
                            <td class="RightAligned">
                                <%# Eval("colSunInt")%>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <EmptyDataTemplate>
                        <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                            <tr>
                                <th>
                                    Room Type
                                </th>
                                <th>
                                    Monday
                                </th>
                                <th>
                                    Tuesday
                                </th>
                                <th>
                                    Wednesday
                                </th>
                                <th>
                                    Thursday
                                </th>
                                <th>
                                    Friday
                                </th>
                                <th>
                                    Saturday
                                </th>
                                <th>
                                    Sunday
                                </th>
                            </tr>
                            <tr>
                                <td colspan="8" class="leftAligned">
                                    No Record
                                </td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                </asp:ListView>
                <asp:DataPager ID="DataPager1" runat="server" PagedControlID="uoListViewDashboard2"
                    PageSize="5" OnPreRender="uoListViewDashboard2_PreRender">
                    <Fields>
                        <asp:NumericPagerField ButtonType="link" NumericButtonCssClass="PagerClass" />
                    </Fields>
                </asp:DataPager>
 </td>
 </tr>
 </table>
     </asp:Panel>
                     <cc1:CollapsiblePanelExtender ID="uocollapsibleRoom" runat="server" TargetControlID="Panel2"
            ExpandControlID="uopanelroomhead" CollapseControlID="uopanelroomhead" TextLabelID="uolabelRoom"
            CollapsedText="(Show List)" ExpandedText="(Hide List)" Collapsed="false" SuppressPostBack="true">
                </cc1:CollapsiblePanelExtender> 
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/TravelmartMaster.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="TRAVELMART.Dashboard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="ViewTitlePadding">
    <table  width="100%">
        <tr>
            <td class="PageTitle">
                Dashboard
            </td>
        </tr>
    </table>
</div>
<table>
<tr>
<td >
    <asp:DropDownList ID="uoDropDownListGroup" runat="server" Visible =false  >
        <asp:ListItem Value="PortName">Port Name</asp:ListItem>
        <asp:ListItem>Ship</asp:ListItem>
    </asp:DropDownList>
</td>
</tr>
</table>
<table width="100%" align="left">
<tr>
<td>
<asp:ListView runat="server" ID="uoListViewDashboard" 
        onprerender="uoListViewDashboard_PreRender" 
        onitemcommand="uoListViewDashboard_ItemCommand" >
                    <LayoutTemplate>
                        <table border="0" cellpadding="0" cellspacing="0" class="listViewTable" width="100%">
                            <tr>
                                <th>
                                    Date
                                </th>

                                <th>
                                    Day
                                </th>
                                <th>
                                    SignOn
                                </th>
                                <th>
                                    SignOff
                                </th>
                                <th>
                                    Departure
                                </th>
                                <th>
                                    Arrival
                                </th>

                            </tr>
                            <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                        </table>
                    </LayoutTemplate>
                    <ItemTemplate>
                     <%# DashboardAddGroup()%>  
                        <tr>
                             <td class="leftAligned">
                                <%# String.Format("{0:dd-MMM-yyyy}", Eval("cDate"))%>
                            </td>
                            <td class="leftAligned">
                                <%# Eval("cDay")%>
                            </td>
                            <td class="RightAligned">
                                <%# Eval("SignOn")%>
                            </td>
                            
                            <td class="RightAligned">
                                <%# Eval("SignOff")%>
                            </td>
                            <td class="RightAligned">
                                <%# Eval("Departure")%>
                            </td>
                            <td class="RightAligned">
                                <%# Eval("Arrival")%>
                            </td>

                        </tr>
                    </ItemTemplate>
                    <EmptyDataTemplate>
                        <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                            <tr>
                                <th>
                                    Date
                                </th>
                                <th>
                                    Day
                                </th>
                                <th>
                                    SignOn
                                </th>
                                <th>
                                    SignOff
                                </th>
                                <th>
                                    Departure
                                </th>
                                <th>
                                    Arrival
                                </th>

                            </tr>
                            <tr>
                                <td colspan="6" class="leftAligned">
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
</asp:Content>

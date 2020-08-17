<%@ Page Title="" Language="C#" MasterPageFile="~/TravelmartMaster.Master" AutoEventWireup="true" CodeBehind="DashboardVehicle.aspx.cs" Inherits="TRAVELMART.DashboardVehicle" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="PageTitle">
        <asp:Panel ID="Panel1" runat="server">
            Dashboard Vehicle
        </asp:Panel>
    </div>
    
        <br />
        
    <div class="PageTitle">
        <asp:Panel ID="PanelHead" runat="server">
            Vehicle Type Details
                <asp:Label ID="ucLabelVehicleTypeDetails" runat="server" Text="" Font-Size="Small" />
        </asp:Panel>
    </div>
    
    <asp:Panel ID="uoPanelVehicle" runat="server" CssClass="CollapsiblePanel">
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
                                    Unit/s
                                </th>

                            </tr>
                            <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                        </table>
                    </LayoutTemplate>
                    <ItemTemplate>
                     <%# DashboardAddGroup()%>  
                        <tr>

                            <td class="leftAligned">
                                <%# Eval("colVehicleStatusVarchar")%>
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
                                    Unit/s
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
    <cc1:CollapsiblePanelExtender ID="CollapsiblePanelExtender1" runat="server" TargetControlID="uoPanelVehicle"
    ExpandControlID="PanelHead" CollapseControlID="PanelHead" TextLabelID="ucLabelVehicleTypeDetails"
    CollapsedText="(Show List)" ExpandedText="(Hide List)" Collapsed="false" SuppressPostBack="true">
    </cc1:CollapsiblePanelExtender>
</asp:Content>

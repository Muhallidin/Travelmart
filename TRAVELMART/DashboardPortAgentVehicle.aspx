<%@ Page Title="" Language="C#" MasterPageFile="~/TravelmartMaster.Master" AutoEventWireup="true" CodeBehind="DashboardPortAgentVehicle.aspx.cs" Inherits="TRAVELMART.DashboardPortAgentVehicle" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<table width="100%">
    <tr>
        <td class="PageTitle">Service Provider Vehicle Dashboard</td>
    </tr>
    <tr><td></td></tr>
    <tr>
        <td align="left">
            <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Panel runat="server" ID="panelHeader">
                        <asp:Label runat="server" ID="uoLabelSearch" Font-Underline="true"></asp:Label>
                    </asp:Panel>
                    <asp:Panel runat="server" ID="panelSearch">
                        <cc1:CollapsiblePanelExtender runat="server"
                            ID="collapsiblePanelSearch" Enabled="true"
                            Collapsed="true"
                            CollapseControlID="panelHeader"
                            ExpandControlID="panelHeader"
                            TargetControlID="panelSearch"
                            CollapsedText="Advanced Search"
                            ExpandedText="Advanced Search"
                            ExpandDirection="Vertical"
                            TextLabelID="uoLabelSearch"
                            SuppressPostBack="true"></cc1:CollapsiblePanelExtender>
                            <table width="100%">
                                <tr>
                                    <td class="LeftClass">&nbsp;Vehicle Brand :</td>
                                    <td class="LeftClass">
                                        <asp:DropDownList runat="server" ID="uoDropDownListBrand"
                                            AppendDataBoundItems="true" AutoPostBack="true"
                                            Width="200px" 
                                            onselectedindexchanged="uoDropDownListBrand_SelectedIndexChanged"></asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="LeftClass">&nbsp;Vehicle Branch :</td>
                                    <td class="LeftClass">
                                        <asp:DropDownList runat="server" ID="uoDropDownListBranch"
                                            AppendDataBoundItems="true" Width="200px">
                                            <asp:ListItem Text="--Select Vehicle Branch--" Value="0" Enabled="true"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                    </asp:Panel>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="uoButtonViewAll" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="uoDropDownListBrand" 
                        EventName="SelectedIndexChanged" />
                </Triggers>
            </asp:UpdatePanel>
        </td>
    </tr>
      <tr>
        <td></td>
    </tr>
    <tr>
        <td align="left">
            <asp:Button runat="server" ID="uoButtonView" CssClass="SmallButton" Text="View" 
                onclick="uoButtonView_Click" /> &nbsp;
            <asp:Button runat="server" ID="uoButtonViewAll" CssClass="SmallButton" 
                Text="View All" onclick="uoButtonViewAll_Click" />
        </td>
    </tr>
    <tr>
        <td class="Module">
            <asp:UpdatePanel runat="server" ID="UpdatePanelVehicle" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:ListView runat="server" ID="uoVehicleDashboardList">
                        <LayoutTemplate>
                            <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                                <tr>
                                    <th>Date</th>
                                    <th>Day</th>
                                    <th>Arrived</th>
                                    <th>In Transit</th>
                                    <th>Cancelled</th>
                                    <th>Unused</th>
                                </tr>
                                <asp:PlaceHolder runat="server" ID="itemPlaceHolder"></asp:PlaceHolder>
                            </table>
                        </LayoutTemplate>
                        <ItemTemplate>
                        <%# DashboardAddGroup()%>
                            <tr>
                                <td class="leftAligned">
                                    <%# String.Format("{0:dd-MMM-yyyy}", Eval("CDate")) %>
                                </td>
                                <td class="leftAligned">
                                    <%# Eval("CDay") %>
                                </td>
                                <td class="leftAligned">
                                    <%# Eval("Arrived") %>
                                </td>
                                <td class="leftAligned">
                                    <%# Eval("InTransit") %>
                                </td>
                                <td class="leftAligned">
                                    <%# Eval("Cancelled") %>
                                </td>
                                <td class="leftAligned">
                                    <%# Eval("Unused") %>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <EmptyDataTemplate>
                            <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                                <tr>
                                    <th>Date</th>
                                    <th>Day</th>
                                    <th>Arrived</th>
                                    <th>In Transit</th>
                                    <th>Cancelled</th>
                                    <th>Unused</th>
                                </tr>
                                <tr>
                                    <td class="leftAligned" colspan="8">No Records</td>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
                    </asp:ListView>
                    <asp:DataPager ID="uoVehicleDashboardListPager" runat="server" 
                        PagedControlID="uoVehicleDashboardList" 
                        onprerender="uoVehicleDashboardListPager_PreRender" 
                        PageSize="20"
                        >
                        <Fields>
                            <asp:NumericPagerField ButtonType="link" NumericButtonCssClass="PagerClass" />
                        </Fields>
                    </asp:DataPager>
                    
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="uoVehicleDashboardListPager" 
                        EventName="PreRender" />
                    <asp:AsyncPostBackTrigger ControlID="uoButtonView" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="uoButtonViewAll" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
            
        </td>
    </tr>
    <asp:ObjectDataSource ID="ObjectDataSource1" 
        runat="server" EnablePaging="True"
        OldValuesParameterFormatString="oldCount_{0}" 
        SelectCountMethod="PortAgentVehicleDashboardDetailsCount" 
        SelectMethod="PortAgentVehicleDashboardDetails" 
        TypeName="TRAVELMART.BLL.DashboardBLL"  EnableViewState="False" 
        ConflictDetection="CompareAllValues">
        <SelectParameters>
            <asp:ControlParameter ControlID="uoHiddenFieldPortAgentId" DbType="Int32" 
                Name="portAgentId" PropertyName="Value" />
            <asp:ControlParameter ControlID="uoHiddenFieldBranchId" DbType="Int32" 
                Name="BranchId" PropertyName="Value" />
            <asp:ControlParameter ControlID="uoHiddenFieldStartDate" DbType="Date" 
                Name="startDate" PropertyName="Value" />
            <asp:ControlParameter ControlID="uoHiddenFieldEndDate" DbType="Date" 
                Name="endDate" PropertyName="Value" />
            <asp:ControlParameter ControlID="uoHiddenFieldBrandId" DbType="Int32" 
                Name="BrandId" PropertyName="Value" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:HiddenField runat="server" ID="uoHiddenFieldPortAgentId" Value="0" />
    <asp:HiddenField runat="server" ID="uoHiddenFieldStartDate" Value="0" />
    <asp:HiddenField runat="server" ID="uoHiddenFieldEndDate" Value="0" />
    <asp:HiddenField runat="server" ID="uoHiddenFieldBranchId" Value="0" />
    <asp:HiddenField runat="server" ID="uoHiddenFieldBrandId" Value="0" />
</table>
</asp:Content>

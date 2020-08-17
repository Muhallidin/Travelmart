<%@ Page Title="" Language="C#" MasterPageFile="~/Popup.Master" AutoEventWireup="true" CodeBehind="PortAgentContractVehicleView.aspx.cs" Inherits="TRAVELMART.ContractManagement.PortAgentContractVehicleView" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<table width="100%">
    <tr>
        <td class="PageTitle" colspan="2">Vehicle Service</td>
    </tr>
    <tr><td colspan="2"><hr /></td></tr>
    <tr>
        <td class="LeftClass" style="width:150px;">&nbsp;Vehicle Brand Name :</td>
        <td class="LeftClass">
            <asp:Label runat="server" ID="uoLabelBrandName"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="LeftClass" style="width:150px;">&nbsp;Vehicle Branch Name :</td>
        <td class="LeftClass">
            <asp:Label runat="server" ID="uoLabelBranchName"></asp:Label>
        </td>
    </tr>
    <tr><td colspan="2"></td></tr>
    <tr runat="server" id="uoTRVehicles"><td class="PageTitle" colspan="2">Vehicles</td></tr>
    <tr>
        <td colspan="2" class="Module">
            <asp:ListView runat="server" ID="uoVehicleList" >
                <LayoutTemplate>
                    <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                        <tr>
                            <th>Vehicle Type</th>
                            <th>Capacity</th>
                            <th>Date from</th>
                            <th>Date To</th>
                            <th>Origin</th>
                            <th>Destination</th>
                            <th>Rate</th>
                            <th>Currency</th>
                          
                        </tr>
                        <asp:panel runat="server" ID="itemPlaceHolder"></asp:panel>
                    </table>
                </LayoutTemplate>
                <ItemTemplate>
                    <tr>
                        <td class="leftAligned"><%# Eval("colVehicleNameVarchar")%></td>
                        <td class="leftAligned"><%# Eval("colVehicleCapacity")%></td>
                        <td class="leftAligned"><%# String.Format("{0:dd-MMM-yyyy}", Eval("colDateFromDatetime")) %></td>
                        <td class="leftAligned"><%# String.Format("{0:dd-MMM-yyyy}", Eval("colDateToDatetime")) %></td>
                        <td class="leftAligned"><%# Eval("colOriginVarchar")%></td>
                        <td class="leftAligned"><%# Eval("colDestinationVarchar")%></td>
                        <td class="leftAligned"><%# Eval("colServiceRateMoney") %></td>
                        <td class="leftAligned"><%# Eval("colCurrencyNameVarchar") %></td>
                       
                    </tr>
                </ItemTemplate>
            </asp:ListView>
        </td>
    </tr>
   
</table>
</asp:Content>

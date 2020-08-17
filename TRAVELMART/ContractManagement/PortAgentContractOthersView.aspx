<%@ Page Title="" Language="C#" MasterPageFile="~/Popup.Master" AutoEventWireup="true" CodeBehind="PortAgentContractOthersView.aspx.cs" Inherits="TRAVELMART.ContractManagement.PortAgentContractOthersView" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<table width="100%">
    <tr>
        <td colspan="2" class="PageTitle"><%= Request.QueryString["vTypeName"].ToString()%></td>
    </tr>
    <tr><td colspan="2"></td></tr>
    <tr>
        <td class="LeftClass">&nbsp;Currency :</td>
        <td class="LeftClass">
            <asp:Label runat="server" ID="uoLabelCurrency"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="LeftClass">&nbsp;Service Rate :</td>
        <td class="LeftClass">
            <asp:Label runat="server" ID="uoLabelRate"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="LeftClass">&nbsp;Remarks :</td>
        <td class="LeftClass">
            <asp:TextBox runat="server" ID="uoTextBoxRemarks"
                Width="400px" Height="50px" TextMode="MultiLine" ReadOnly="true"></asp:TextBox>            
        </td>
    </tr>
</table>
</asp:Content>

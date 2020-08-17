<%@ Page Title="" Language="C#" MasterPageFile="~/Popup.Master" AutoEventWireup="true" CodeBehind="PortAgentContractOthers.aspx.cs" Inherits="TRAVELMART.ContractManagement.PortAgentContractOthers" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
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
            <asp:DropDownList runat="server" ID="uoDropDownListCurrency"
                AppendDataBoundItems="true" Width="200px"></asp:DropDownList>
            <asp:RequiredFieldValidator runat="server" ID="rfvCurrency"
                ErrorMessage="Currency required." ValidationGroup="Save"
                ControlToValidate="uoDropDownListCurrency"
                InitialValue="0">*</asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td class="LeftClass">&nbsp;Service Rate :</td>
        <td class="LeftClass">
            <asp:TextBox runat="server" ID="uoTextBoxRate"
                Width="200px"></asp:TextBox>
            <cc1:MaskedEditExtender runat="server"
                ID="uoTextBoxRate_MaskedEditExtender" TargetControlID="uoTextBoxRate"
                Mask="999,999.99" MaskType="Number" Enabled="true">
            </cc1:MaskedEditExtender>
            <asp:RequiredFieldValidator runat="server" ID="rfvRate"
                ErrorMessage="Service Rate required."
                ValidationGroup="Save"
                ControlToValidate="uoTextBoxRate">*</asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td class="LeftClass">&nbsp;Remarks :</td>
        <td class="LeftClass">
            <asp:TextBox runat="server" ID="uoTextBoxRemarks"
                Width="400px" Height="50px" TextMode="MultiLine"></asp:TextBox>            
        </td>
    </tr>
    <tr><td colspan="2"></td></tr>
    <tr>
        <td colspan="2" align="center">
            <asp:Button runat="server" ID="uoButtonSave" ValidationGroup="Save" Text="Save" 
                onclick="uoButtonSave_Click" />
        </td>
    </tr>
</table>
<asp:HiddenField runat="server" ID="uoHiddenFieldDetailId" Value="0" />
</asp:Content>

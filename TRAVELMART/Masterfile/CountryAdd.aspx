<%@ Page Title="" Language="C#" MasterPageFile="~/Popup.Master" AutoEventWireup="true" CodeBehind="CountryAdd.aspx.cs" Inherits="TRAVELMART.CountryAdd" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <table width="100%">
        <tr class="PageTitle">
            <td colspan="2">Country</td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Save" />
            </td>
        </tr>
        <%--<tr>
            <td style="width:25%" class="LeftClass">&nbsp Region:</td>
            <td class="LeftClass">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                         <asp:TextBox ID="uoTextBoxRegion" runat="server" Width="300px" Enabled="false" Visible="false"></asp:TextBox>
                         <asp:DropDownList ID="uoDropDownListRegion" runat="server" Width="96%" Visible="false"
                            AppendDataBoundItems="true"></asp:DropDownList>&nbsp;
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                            ControlToValidate="uoDropDownListRegion" ErrorMessage="Select region." InitialValue="0"
                            ValidationGroup="Save">*</asp:RequiredFieldValidator>
                    </ContentTemplate>
                </asp:UpdatePanel>
              
            </td>
        </tr>--%>
        <tr>
            <td width=20%>&nbsp Country Name:</td>
            <td class="LeftClass">
                <asp:TextBox ID="uoTextBoxCountryName" runat="server" Width="95%"></asp:TextBox>
                &nbsp;
                <asp:RequiredFieldValidator ID="uorfvRegionName" runat="server" 
                    ControlToValidate="uoTextBoxCountryName" ErrorMessage="Country Name is required."
                    ValidationGroup="Save">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td width=20%>&nbsp Country Code: </td>
            <td class="LeftClass">
                <asp:TextBox runat="server" ID="uoTextBoxCountryCode" Width="95%" MaxLength="10"></asp:TextBox>
                &nbsp;
                <asp:RequiredFieldValidator ID="uorfvCCode" runat="server" 
                    ControlToValidate="uoTextBoxCountryCode" ErrorMessage="Country Code is required."
                    ValidationGroup="Save">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr><td colspan="2"></td></tr>
        <tr>
            <td colspan="2" align="center">
                <asp:Button ID="uoButtonSave" runat="server" Text="Save" Width="80"
                    ValidationGroup="Save" onclick="uoButtonSave_Click" />
            </td>
        </tr>
    </table>
</asp:Content>

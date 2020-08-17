<%@ Page Title="" Language="C#" MasterPageFile="~/Popup.Master" AutoEventWireup="true"
    CodeBehind="PortCompanyMaintenance.aspx.cs" Inherits="TRAVELMART.PortCompanyMaintenance" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="PageTitle">
        Port Company
    </div>
    <hr />
    <table>
        <tr>
            <td style="width:26%">
                Company Name:
            </td>
            <td>
                <asp:TextBox ID="uoTextBoxCompanyName" runat="server" Width="300px"></asp:TextBox>
                &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="uoTextBoxCompanyName"
                    ErrorMessage="Company name required." ValidationGroup="Save">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                Company Address:
            </td>
            <td>
                <asp:TextBox ID="uoTextBoxAddress" runat="server" Width="300px" TextMode="MultiLine"
                    CssClass="TextBoxInput"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Company Country:
            </td>
            <td>
                <asp:DropDownList ID="uoDropDownListCountry" runat="server" Width="305px" AutoPostBack="true">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="uoDropDownListCountry"
                    ErrorMessage="Country required." InitialValue="" ValidationGroup="Save">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                Contact Person:
            </td>
            <td>
                <asp:TextBox ID="uoTextBoxPerson" runat="server" Width="300px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
              Contact No:
            </td>
            <td>
                <asp:TextBox ID="uoTextBoxContactNo" runat="server" Width="300px"></asp:TextBox>
                <cc1:MaskedEditExtender ID="uoTextBoxContactNo_MaskedEditExtender" runat="server"
                    CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                    CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                    CultureTimePlaceholder="" Enabled="True" Mask="(999) 999-9999" MaskType="Number"
                    TargetControlID="uoTextBoxContactNo">
                </cc1:MaskedEditExtender>
            </td>
        </tr>      
        <tr>
            <td colspan="2">
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <asp:Button ID="uoButtonSave" runat="server" Text="Save" Width="80" OnClick="uoButtonSave_Click"
                    ValidationGroup="Save" />
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <asp:HiddenField ID="uoHiddenFieldVendorIdInt" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <asp:HiddenField ID="uoHiddenFieldVendorType" runat="server" />
            </td>
        </tr>
    </table>
</asp:Content>

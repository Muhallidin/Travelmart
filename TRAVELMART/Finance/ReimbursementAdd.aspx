<%@ Page Title="" Language="C#" MasterPageFile="~/Popup.Master" AutoEventWireup="true" CodeBehind="ReimbursementAdd.aspx.cs" Inherits="TRAVELMART.Finance.ReimbursementAdd" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<table width ="100%">
    <tr>
        <td colspan="4" class="PageTitle">
               <%= PageTitle %>
        </td>
    </tr>
    <tr>
        <td colspan="4"></td>
    </tr>
    <tr>
        <td colspan="4" class="LeftClass">
            <asp:ValidationSummary runat="server" ID="uoValidationSummary" ValidationGroup="Save" />
        </td>
    </tr>
    <tr><td colspan="4"></td></tr>
    <tr>
        <td class="LeftClass">
            &nbsp; Reimbursement:
        </td>
        <td class="LeftClass" colspan="3">
        
            <asp:TextBox runat="server" ID="uoTextBoxReimbursementName" Width="500px"
                CssClass="TextBoxInput"></asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ID="rfvrName"
                ErrorMessage="Reimbursement Name required." ValidationGroup="Save"
                ControlToValidate="uoTextBoxReimbursementName">*</asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td class="LeftClass">
            &nbsp; Remarks :
        </td>
        <td class="LeftClass" colspan="3">
        
            <asp:TextBox runat="server" ID="uoTextBoxRemarks" Width="500px" 
                Height="50px" TextMode="MultiLine" CssClass="TextBoxInput"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="LeftClass">
            &nbsp; Amount :
        </td>
        <td class="LeftClass">
            <asp:TextBox runat="server" ID="uoTextBoxAmount" Width ="180px"
                CssClass="TextBoxInput"></asp:TextBox>
            
            <cc1:MaskedEditExtender ID="uoTextBoxAmount_MaskedEditExtender" 
                runat="server" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                Mask="9,999,999.99" MaskType="Number" TargetControlID="uoTextBoxAmount"
                InputDirection="RightToLeft">
            </cc1:MaskedEditExtender>
            <asp:RequiredFieldValidator runat="server" ID="rfvAmount"
                ErrorMessage="Amount required." ValidationGroup="Save"
                ControlToValidate="uoTextBoxAmount">*</asp:RequiredFieldValidator>
        </td>
        <td class="LeftClass">
            &nbsp; Currency :
        </td>
        <td class="LeftClass">
            <asp:TextBox runat="server" ID="uoTextBoxCurrency" Width ="180px" 
               CssClass="TextBoxInputReadOnly" ReadOnly="true"></asp:TextBox>
            <asp:HiddenField runat="server" ID = "uoHiddenFieldCurrencyId" Value="0" />
        </td>
    </tr>
    <tr><td colspan="4"></td></tr>
    <tr>
        <td colspan="4" align="center">
            <asp:Button runat="server" ID="uoButtonSave"
                Text ="Save" ValidationGroup="Save" onclick="uoButtonSave_Click" />
        </td>
    </tr>
</table>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/Popup.Master" AutoEventWireup="true" CodeBehind="ReimbursementView.aspx.cs" Inherits="TRAVELMART.Finance.ReimbursementView" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width ="100%">
        <tr>
            <td colspan="4" class="PageTitle">
                   Reimbursement
            </td>
        </tr>
        <tr>
            <td colspan="4"></td>
        </tr>
       
        <tr><td colspan="4"></td></tr>
        <tr>
            <td class="LeftClass">
                &nbsp; Reimbursement:
            </td>
            <td class="LeftClass" colspan="3">
                <asp:Label runat="server" ID="uoLabelReimbursementName" ></asp:Label>                
            </td>
        </tr>
        <tr>
            <td class="LeftClass">
                &nbsp; Remarks :
            </td>
            <td class="LeftClass" colspan="3">
                <asp:TextBox runat="server" ID="uoTextBoxRemarks" Width="95%" 
                Height="50px" TextMode="MultiLine" ReadOnly="true" CssClass="TextBoxInput"></asp:TextBox>
                
            </td>
        </tr>
        <tr>
            <td class="LeftClass">
                &nbsp; Amount :
            </td>
            <td class="LeftClass">
                
                <asp:Label runat="server" ID="uoLabelAmount"></asp:Label>
                
            </td>
            
        </tr>
        <tr>
            <td class="LeftClass">
                &nbsp; Currency :
            </td>
            <td class="LeftClass">
                <asp:Label runat="server" ID="uoLabelCurrency"></asp:Label>
            </td>
        </tr>
       <tr>
            <td>
                <asp:HiddenField runat="server" ID="uoHiddenFieldCurrencyId" />
            </td>
       </tr>
    </table>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/Popup.Master" AutoEventWireup="true" CodeBehind="AddConfirmation.aspx.cs" Inherits="TRAVELMART.Hotel.AddConfirmation" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server"> 
    <table width="100%" style="text-align:left">
        <tr class="PageTitle">
            <td colspan="2">Confirmation No.</td>
        </tr>
            <tr>
                <td class="LeftClass">
                    Confirmation No:
                </td>
                <td lass="LeftClass">
                    <asp:TextBox ID="uoTxtConfirmation" MaxLength="20" runat="server" Width="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="2">                  
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td align="left">
                    <asp:Button ID="uoBtnSave" runat="server" Text="Save" Width="100px" OnClick="uoBtnSave_Click" />
                </td>
            </tr>
        </table>
</asp:Content>
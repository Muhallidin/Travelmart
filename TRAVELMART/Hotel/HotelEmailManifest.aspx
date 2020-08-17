<%@ Page Title="" Language="C#" MasterPageFile="~/Popup.Master" AutoEventWireup="true" CodeBehind="HotelEmailManifest.aspx.cs" Inherits="TRAVELMART.Hotel.HotelEmailManifest" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server"> 
<script type="text/javascript" language="javascript">
    function validateEmail(field) { 
        var regex = /^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$/;
        return (regex.test(field)) ? true : false;
    }
    function validateMultipleEmailsSemiColonSeparated(email, seperator) {
        var value = email.value;
        if (value != '') {
            var result = value.split(seperator);
            for (var i = 0; i < result.length; i++) {
                if (result[i] != '') {
                    if (!validateEmail(result[i])) {
                        email.focus();                        
                        alert('Please check, `' + result[i] + '` email address not valid!');                        
                        return false;
                    }
                }                
            }            
        }        
        return true;
    }

    function confirmSaveAndSend() {
        if (confirm("Saving will completely change your recipient and/or carbon copy list. Are you sure?") == true)
            return true;
        else
            return false;
    }

//    function insertDelimeter(key) {        
//        if (event.which || event.keycode) {
//            if ((event.which == 32) || (event.keycode == 32)) {
//                    return false;
//                    var delimeter = ';';
//                    return email.value += delimeter;            
//            }
//       }              
//    }
</script>

    <table width="100%">
        <tr class="PageTitle">
            <td colspan="2">Hotel Manifest E-Mail</td>
        </tr>
        <tr>
            <td colspan="2" class="LeftClass">
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
                    ValidationGroup="Search" />
            </td>
        </tr>        
        <tr>
            <td><br /></td>
        </tr>
        <tr>
            <td class="LeftClass" style="width:15%;">&nbsp To:</td>
            <td class="LeftClass">
                <asp:TextBox onblur="validateMultipleEmailsSemiColonSeparated(this,';');" ID="uoTextBoxTo" runat="server" Width="80%" MaxLength="500" Height="50px"
                    TextMode="MultiLine" ToolTip="Use a semicolon (;) with no spaces to separate multiple e-mail addresses."></asp:TextBox>                                                
            </td>
        </tr>
        <tr>
            <td class="LeftClass" style="width:15%;">&nbsp Cc:</td>
            <td class="LeftClass">
                <asp:TextBox onblur="validateMultipleEmailsSemiColonSeparated(this,';');" ID="uoTextBoxCc" runat="server" Width="80%" MaxLength="500" Height="50px"
                    TextMode="MultiLine" ToolTip="Use a semicolon (;) with no spaces to separate multiple e-mail addresses."></asp:TextBox>                
            </td>
        </tr>                
        <tr>
            <td></td>
            <td>
                <a id="uoHyperLinkSend" runat="server">
                    <asp:Button ID="uoButtonSend" runat="server" Text="Send" CssClass="SmallButton"
                    onclick="uoButtonSend_Click"/>
                </a> &nbsp;
                <a id="uoHyperLinkSaveAndSend" runat="server">
                    <asp:Button ID="uoButtonSaveAndSend" runat="server" Text="Save & Send" CssClass="SmallButton"
                    CommandName="SaveAndSend" OnClientClick="return confirmSaveAndSend();" OnCommand="uoButtonSaveAndSend_OnCommand"/>
                </a>
                <%--<asp:Button ID="uoButtonCancel" runat="server" Text="Cancel" Width="35%"
                OnClick="uoButtonCancel_Click"/>--%>
            </td>
        </tr>
    </table>    
</asp:Content>




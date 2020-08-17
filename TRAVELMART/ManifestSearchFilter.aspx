<%@ Page Title="" Language="C#" MasterPageFile="~/Popup.Master" AutoEventWireup="true" CodeBehind="ManifestSearchFilter.aspx.cs" Inherits="TRAVELMART.ManifestSearchFilter" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server"> 
    <script type="text/javascript" src="../FBox/jquery-1.6.1.min.js"></script>  
    <script type="text/javascript">
        function validateID(key) {
            //getting key code of pressed key
            var keycode = (key.which) ? key.which : key.keyCode;

            if (keycode >= 48 && keycode <= 57) {
                return true;
            }
            else {
                return false;
            }
        }

        function validateName(key) {
            //getting key code of pressed key
            var keycode = (key.which) ? key.which : key.keyCode;

            if ((keycode >= 65 && keycode <= 90) || (keycode >= 97 && keycode <= 122) ||
                 (keycode >= 44 && keycode <= 46) || keycode == 8 || keycode == 32 || keycode == 127) {
                return true;
            }
            else {
                return false;
            }
        }
    </script>
    
              
    <table  width="100%">
        <tr class="PageTitle">
            <td colspan="2">Manifest Search Filter</td>
        </tr>
        <tr>
            <td colspan="2" class="LeftClass">
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
                    ValidationGroup="Search" />
            </td>
        </tr>      
        
        <tr>
            <td class="LeftClass" style="width:15%;">Filter by (Seafarer)</td>
        </tr>
        <tr>
            <td class="LeftClass" style="width:15%;">&nbsp ID:</td>
            <td class="LeftClass">
                <asp:TextBox ID="uoTextBoxSeafarerID" runat="server" Width="95%" MaxLength=8 onkeypress="return validateID(event);"></asp:TextBox>                
            </td>
        </tr>
        <tr>
            <td class="LeftClass" style="width:15%;">&nbsp Last Name:</td>
            <td class="LeftClass">
                <asp:TextBox ID="uoTextBoxSeafarerLN" runat="server" Width="95%" MaxLength=100 onkeypress="return validateName(event);"></asp:TextBox>                
            </td>
        </tr>
        <tr>
            <td class="LeftClass" style="width:15%;">&nbsp First Name:</td>
            <td class="LeftClass">
                <asp:TextBox ID="uoTextBoxSeafarerFN" runat="server" Width="95%" MaxLength=100 onkeypress="return validateName(event);"></asp:TextBox>                              
            </td>
        </tr>
        <tr>
            <td class="LeftClass" style="width:15%;">&nbsp Record Locator:</td>
            <td class="LeftClass">
                <asp:TextBox ID="uoTextBoxRecLoc" runat="server" Width="95%" MaxLength=10 ></asp:TextBox>                               
            </td>
        </tr>        
        <tr><td colspan="2"><hr/></td></tr>
        <tr>
            <td class="LeftClass" style="width:15%;">Filter by (Ship)</td>
        </tr>
        <tr>
            <td class="LeftClass" style="width:15%;">&nbsp Code:</td>
            <td class="LeftClass">
                <asp:TextBox ID="uoTextBoxVesselCode" runat="server" Width="95%" MaxLength=10 onkeypress="return validateName(event);"></asp:TextBox>                
            </td>
        </tr>
        <tr>
            <td class="LeftClass" style="width:15%;">&nbsp Name:</td>
            <td class="LeftClass">
                <asp:TextBox ID="uoTextBoxVesselName" runat="server" Width="95%" MaxLength=50 onkeypress="return validateName(event);"></asp:TextBox>                
            </td>
        </tr>
        <tr><td colspan="2"></td></tr>
        <tr><td colspan="2"></td></tr>
        <tr>
            <td></td>
            <td >
                <a id="uoHyperLinkManifestSearchView" runat="server">
                    <asp:Button ID="uoButtonSearch" runat="server" Text="Search" CssClass="SmallButton"
                    onclick="uoButtonSearch_Click"/>
                </a>
            </td>
        </tr>
    </table>    
</asp:Content>


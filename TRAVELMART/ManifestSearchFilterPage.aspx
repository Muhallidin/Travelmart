<%@ Page Title="" Language="C#" MasterPageFile="~/TravelMartMaintenanceMaster.Master" AutoEventWireup="true" CodeBehind="ManifestSearchFilterPage.aspx.cs" Inherits="TRAVELMART.ManifestSearchFilterPage" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

    
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style1
        {
            height: 19px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" runat="server">   
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="NaviPlaceHolder" runat="server">       
        
    <script type="text/javascript" src="FBox/jquery-1.6.1.min.js"></script>
    <script type="text/javascript" src="Menu/jquery-1.3.1.min.js"></script>
    <script type="text/javascript" src="FBox/fancybox/jquery.mousewheel-3.0.4.pack.js"></script>
    <script type="text/javascript" src="FBox/fancybox/jquery.fancybox-1.3.4.pack.js"></script>
        
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
                <asp:TextBox ID="uoTextBoxSeafarerID" runat="server" Width="300px" MaxLength=8  onkeypress="return validateID(event);"></asp:TextBox>                
            </td>
        </tr>
        <tr>
            <td class="LeftClass" style="width:15%;">&nbsp Last Name:</td>
            <td class="LeftClass">
                <asp:TextBox ID="uoTextBoxSeafarerLN" runat="server" Width="300px" MaxLength=100 onkeypress="return validateName(event);"></asp:TextBox>                
            </td>
        </tr>
        <tr>
            <td class="LeftClass" style="width:15%;">&nbsp First Name:</td>
            <td class="LeftClass">
                <asp:TextBox ID="uoTextBoxSeafarerFN" runat="server" Width="300px" MaxLength=100 onkeypress="return validateName(event);"></asp:TextBox>                              
            </td>
        </tr>
        <tr>
            <td class="LeftClass" style="width:15%;">&nbsp Record Locator:</td>
            <td class="LeftClass">
                <asp:TextBox ID="uoTextBoxRecLoc" runat="server" Width="300px" MaxLength=10 ></asp:TextBox>                               
            </td>
        </tr>        
        <tr><td colspan="2"><hr/></td></tr>
        <tr>
            <td class="LeftClass" style="width:15%;">Filter by (Ship)</td>
        </tr>
        <tr>
            <td class="LeftClass" style="width:15%;">&nbsp Code:</td>
            <td class="LeftClass">
                <asp:TextBox ID="uoTextBoxVesselCode" runat="server" Width="300px" MaxLength=10 onkeypress="return validateName(event);"></asp:TextBox>                
            </td>
        </tr>
        <tr>
            <td class="LeftClass" style="width:15%;">&nbsp Name:</td>
            <td class="LeftClass">
                <asp:TextBox ID="uoTextBoxVesselName" runat="server" Width="300px" MaxLength=50 onkeypress="return validateName(event);"></asp:TextBox>                
            </td>
        </tr>
        <tr><td colspan="2"></td></tr>
        <tr><td colspan="2"></td></tr>
        <tr>
            <td  class="LeftClass" style="width:15%;"></td>
            <td  class="LeftClass"> 
                <a id="uoHyperLinkManifestSearchView" runat="server">
                    <asp:Button ID="uoButtonSearch" runat="server" Text="Search" CssClass="SmallButton" Width="80px"
                    onclick="uoButtonSearch_Click"/>
                </a>
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td >
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <a class="HotelLink" href="#"></a>
            </td>
        </tr>
    </table>    
</asp:Content>


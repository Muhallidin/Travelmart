<%@ Page Title="" Language="C#" MasterPageFile="~/Popup.Master" AutoEventWireup="true" CodeBehind="CrewAssistConfirmHotel.aspx.cs" Inherits="TRAVELMART.CrewAssistConfirmHotel" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
 <style type="text/css">
        .hideHover
        {
            display:none;    
        }
        .showHover
        {
            display:inline;   
            position:absolute; z-index: 200; 
            background-color:White;
            left: 200px;
            top: 100px;
        } 
        .caption
        {
            text-align:left;
            padding-left: 5px;    
            font-size:12px;
        } 
        .value
        {
            text-align:left;
            padding-left: 5px;  
            font-size:12px;            
        }
    </style>
    <script type="text/javascript">
        function checkboxEnable(obj) {
            if (obj.checked == true) {
                    $("#<%=TextBoxConfirmrate.ClientID %>").disabled = false;
            }
            else {
                $("#<%=TextBoxConfirmrate.ClientID %>").disabled = true;
            }
        }
        
         window.onload = function() {
         var check = document.getElementById("<%=CheckBoxNo.ClientID %>");
            check.onchange = function() {
                if (this.checked == true)
                    document.getElementById("<%=TextBoxConfirmrate.ClientID %>").disabled = false;
                else
                    document.getElementById("<%=TextBoxConfirmrate.ClientID %>").disabled = true;
            };
        };
       
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


<div id="centerdiv" runat="server">
        <table style="width: 100%; text-align: left;" id="tbConfirm">
            <tr class="PageTitle">
                <td colspan="2">
                    Confirm
                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/FBox/fancybox/fancy_close.png"
                        ImageAlign="Right" CssClass="imagePos" />
                </td>
            </tr>
            <tr>
                <td style="width: 250px;">
                    <asp:Label ID="uoLabelContractRate" runat="server" Text="Was the contracted rate of $ 89 Confimed" />
                </td>
                <td>
                    <asp:CheckBox ID="CheckBoxYes" runat="server" Text="Yes" />&nbsp;
                    <asp:CheckBox ID="CheckBoxNo" runat="server" AutoPostBack="true" Text="No"/>&nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label2" runat="server" Text=" Name of the hotel agent who confimed	" />
                </td>
                <td>
                    <asp:TextBox ID="TextBoxWhoConfirm" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Confirmed rate
                </td>
                <td>
                    <asp:TextBox ID="TextBoxConfirmrate" runat="server" Enabled="false"></asp:TextBox>
                </td>
            </tr>
            <tr style="display: none; " >
                <td >
                    <asp:Label ID="Label1" runat="server" Text="Email Add: " />
                </td>
                <td>
                    <asp:TextBox ID="uoTextBoxEmailAdd" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right" colspan="2">
                    <asp:Button ID="ButtonSend" runat="server" Text="Send" OnClientClick="HiveDiv()"
                        />
                </td>
            </tr>
        </table>
    </div>


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>

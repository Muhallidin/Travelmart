<%@ Page Title="" Language="C#" MasterPageFile="~/Popup.Master" AutoEventWireup="true" CodeBehind="CrewAssistConfirmation.aspx.cs" Inherits="TRAVELMART.CrewAssistConfirmation" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>    
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
//
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
 <style type="text/css">
        .Background
        {
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.8;
        }
        .Popup
        {
            background-color: #FFFFFF;
            border-width: 3px;
            border-style: solid;
            border-color: black;
            padding-top: 10px;
            padding-left: 10px;
            width: 400px;
            height: 350px;
        }
        .lbl
        {
            font-size:16px;
            font-style:italic;
            font-weight:bold;
        }
    </style>

    <table style="width: 100%;">
        <tr class="PageTitle">
            <td colspan="2">Confirm</td>
        </tr>
        <tr>
            <td>
                <br />
            </td>
        </tr>
       <tr>
            <td style="width: 250px; white-space:nowrap;">
                <asp:label  ID="uoLabelContractRate"  runat="server" Text="Was the contracted rate of $0.00 Confimed"/>
            </td>
            <td >
               <asp:CheckBox ID="CheckBoxYes" runat="server" Text="Yes" />&nbsp;
               <asp:CheckBox ID="CheckBoxNo" runat="server" Text="No" 
                onlcick="checkboxEnable(this);" />&nbsp;
            </td>
        <%--   OnCheckedChanged="CheckBoxNo_OnCheckedChanged"--%>
        </tr>
        <tr>
            <td style="white-space:nowrap;">
                <asp:label ID="Label2" runat="server" Text=" Name of the hotel agent who confimed	"/>
            </td>
            <td style="width:98%;">
                <asp:TextBox ID="TextBoxWhoConfirm" runat="server" style="width:98%;"></asp:TextBox>
            </td>
        </tr>
        
        <tr>
            <td style="white-space:nowrap;">
               Confirmed rate  
            </td>
            <td >
                 <asp:TextBox ID="TextBoxConfirmrate" runat="server" Enabled="false" style="width:98%;"></asp:TextBox>
            </td>
        </tr>
                 
    <%--   <tr>
            <td>
               <asp:label ID="Label1" runat="server" Text="Email Add: "/>
            </td>
            <td >
                 <asp:TextBox ID="uoTextBoxEmailAdd" runat="server" style="width:98%;"></asp:TextBox>
            </td>
        </tr>  --%>
        <tr>
           <td  colspan="2"  align="right">
                <asp:Button ID="ButtonSend" runat="server" Text="Send"  OnClick="uoButtonSend_click" />
            </td>
        </tr>
    </table>
    
    
    <asp:HiddenField ID="uoHiddenFieldEmployeeID" runat="server" Value=""/>
    <asp:HiddenField ID="uoHiddenFieldCheckinDate" runat="server" Value=""/>
     <asp:HiddenField ID="uoHiddenFieldCheckOutDate" runat="server" Value=""/>
   
    <asp:HiddenField ID="uoHiddenFieldIDBigint" runat="server" Value=""/>
    <asp:HiddenField ID="uoHiddenFieldSeqNo" runat="server" Value=""/>
    <asp:HiddenField ID="uoHiddenFieldTravelRequestID" runat="server" Value=""/>
    <asp:HiddenField ID="uoHiddenFieldBranchID" runat="server" Value=""/>
    <asp:HiddenField ID="uoHiddenFieldHotelTransID" runat="server" Value=""/>
        
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/TravelMartMaster2.Master" AutoEventWireup="true" CodeBehind="Navigation.aspx.cs" Inherits="TRAVELMART.Navigation" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
    .Navigation
    {
        width: 300px;
        text-align: center;
        border-style: solid;
        border-width: thin;
        border-color: Black;
        padding-top:10px;
        vertical-align:middle;
         padding-bottom:10px;
    }
    .Button
    {
        text-align:center;
        font-size:large;    
        color: #0B55C4;           
        width:250px;
    }
    .ButtonOE
    {
        text-align:center;
        font-size:large;    
        color: Brown;            
        width:250px;
    }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NaviPlaceHolder" runat="server" >
    <script type="text/javascript">
        $(document).ready(function() {
            var wd = $(document).height() - 200;
            $("#div1").height(wd);
        });
        
    </script>
    <div id="div1">
        <table align="center" id="table1" >
            <tr >
                <td >
                    <asp:Button runat="server" ID="uoBtnResources" CssClass="Button" 
                        Text="Hotels" onclick="uoBtnResources_Click" BorderStyle="Solid" 
                        Font-Bold="True" Font-Overline="False" Font-Strikeout="False" 
                        Font-Underline="False"/>
                </td>
            </tr>
            <tr><td></td></tr>
            <tr>
                <td>
                    <asp:Button runat="server" ID="uoBtnOverflow" CssClass="ButtonOE"
                        Text="Overflow" onclick="uoBtnOverflow_Click" BorderStyle="Solid" 
                        Font-Bold="True" />
                </td>
            </tr>
            <tr><td></td></tr>
             <tr >
                <td >
                   <asp:Button runat="server" ID="uoBtnException" CssClass="ButtonOE" 
                        Text="Exceptions"
                        onclick="uoBtnException_Click" BorderStyle="Solid" Font-Bold="True" />
                </td>
            </tr>
            
        </table>
    </div>
    
</asp:Content>

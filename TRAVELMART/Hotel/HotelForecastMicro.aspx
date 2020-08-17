<%@ Page Title="" Language="C#" MasterPageFile="~/TravelMartMaintenanceMaster.Master" AutoEventWireup="true"
    CodeBehind="HotelForecastMicro.aspx.cs" Inherits="TRAVELMART.Hotel.HotelForecastMicro" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">     
</asp:Content>
<asp:Content runat="server" ID="Conetent3" ContentPlaceHolderID="HeaderContent">
    
     <table width="100%" cellpadding="0" cellspacing="0">
        <tr class="PageTitle">
            <td align="left">
                Hotel Forecast w/ Projection Intelligence
            </td>            
        </tr>
    </table>   
    <br />
    <div style="text-align:center">
        <iframe name="iframeName" style="text-align:center;text-align:center"
            id="iframeID" width="90%" height="600px;" 
            src="https://rclanalytics.com/Microstrategy/Asp/Main.aspx?Server=WIN-2DA0T8GGKKH&Project=RCCL&Port=0&evt=2048001&src=Main.aspx.2048001&visMode=0&currentViewMedia=8&documentID=4E32DA47466017B3D210B2AA40012E70&hiddenSections=header,path,dockTop&uid=Administrator&pwd=p^ssw*rd">&nbsp;
        </iframe>
    </div>
</asp:Content>

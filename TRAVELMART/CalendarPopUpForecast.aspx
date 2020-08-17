<%@ Page Title="" Language="C#" MasterPageFile="~/Popup.Master" AutoEventWireup="true" CodeBehind="CalendarPopUpForecast.aspx.cs" Inherits="TRAVELMART.CalendarPopUpForecast" %>

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
    </style>
    <script type="text/javascript">
        
        function pageLoad(sender, args) {
            var isAsyncPostback = Sys.WebForms.PageRequestManager.getInstance().get_isInAsyncPostBack();
            if (isAsyncPostback) {
                pageSettings();
            }
        }

        $(document).ready(function() {
            pageSettings();

        });

        function pageSettings() {
//            $(".hoverDate a").hover(function() {
//            var sParent = $(this).parent()[0].id;



//            $(this).hover(function(e) {
//                $("#divTestHover2").removeClass("hideHover");
//                $("#divTestHover2").addClass("showHover");
//         
//                },
//                function() {
//                    $("#divTestHover2").removeClass("showHover");
//                    $("#divTestHover2").addClass("hideHover");
//                });
//            });
        }
        function CloseModal(strURL) {
            window.location = strURL;
        }
       
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server" EnableViewState="true">
   <div align="center">
    
        <table  width="100%" cellpadding="0" cellspacing="0">
            <tr class="PageTitle">
                <td align="center">
                  Calendar Room Count Needed
                </td>
            </tr>
        </table>
        <br />
       <asp:Label ID="uoLabelCaption" runat="server" Text="Branch | Port | Region" Font-Bold="true"></asp:Label>
       <br /> 
        <asp:Calendar runat="server" ID="uoCalendarDashboard"
                BackColor="#ECDFC4" BorderColor="White" BorderWidth="1px" 
                Font-Names="Verdana" Font-Size="9pt" ForeColor="Black" 
                NextPrevFormat="FullMonth" ondayrender="uoCalendarDashboard_DayRender" 
                onselectionchanged="uoCalendarDashboard_SelectionChanged" 
                onvisiblemonthchanged="uoCalendarDashboard_VisibleMonthChanged"
               
                >
                <SelectedDayStyle BackColor="#FFFFCC" ForeColor="Black" Font-Bold="true"/>
                <TodayDayStyle BackColor="#CCCCCC" />
                <OtherMonthDayStyle ForeColor="#999999" />
                <DayStyle BorderColor="#666699" BorderWidth="1px" />
                <NextPrevStyle Font-Bold="True" Font-Size="8pt" ForeColor="#333333" 
                    VerticalAlign="Bottom" />
                <DayHeaderStyle Font-Bold="True" Font-Size="8pt" />
                <TitleStyle BackColor="White" BorderColor="Black" BorderWidth="4px" 
                    Font-Bold="True" Font-Size="12pt" ForeColor="#333399" />
        </asp:Calendar>
        <br />
       <asp:LinkButton ID="uoLinkButtonExport" runat="server" Text="Export" 
            onclick="uoLinkButtonExport_Click"></asp:LinkButton>
   </div>
   
    <asp:HiddenField ID="uoHiddenFieldBranchID" runat="server" />
   
</asp:Content>

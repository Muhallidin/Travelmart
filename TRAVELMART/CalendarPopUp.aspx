<%@ Page Title="" Language="C#" MasterPageFile="~/Popup.Master" AutoEventWireup="true" CodeBehind="CalendarPopUp.aspx.cs" Inherits="TRAVELMART.CalendarPopUp" %>

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
        <table width="100%" cellpadding="0" cellspacing="0" runat="server" id="uoTableFilters">
            <tr>
                <td class="caption">
                    <asp:Label ID="Label1" runat="server" Text="Type:"></asp:Label>
                </td>
                <td class="value">
                    <asp:DropDownList ID="uoDropDownListType" runat="server"  Width="200px" AutoPostBack="true"
                        onselectedindexchanged="uoDropDownListType_SelectedIndexChanged">
                        <asp:ListItem Value="1">Crew Admin</asp:ListItem>
                        <asp:ListItem Value="2">Hotel Vendor</asp:ListItem>
                    </asp:DropDownList>
                </td>
                
                 <td class="caption">
                    <asp:Label ID="Label2" runat="server" Text="Region:"></asp:Label>                   
                </td>
                <td class="value">
                    <asp:DropDownList ID="uoDropDownListRegion" runat="server"  Width="200px" 
                        AutoPostBack="true" 
                        onselectedindexchanged="uoDropDownListRegion_SelectedIndexChanged">
                        
                    </asp:DropDownList>
                </td>
            </tr>
             <tr>
                <td class="caption">
                    <asp:Label ID="Label3" runat="server" Text="Port:"></asp:Label>       
                </td>
                <td class="value">
                    <asp:DropDownList ID="uoDropDownListPort" runat="server"  Width="200px" 
                        AutoPostBack="true" 
                        onselectedindexchanged="uoDropDownListPort_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                
                 <td class="caption">
                   <asp:Label ID="ucLabelVesselOrHotel" runat="server" Text="VesselOrHotel:"></asp:Label>       
                </td>
                <td class="value">
                    <asp:DropDownList ID="uoDropDownListVesselOrHotel" runat="server"  Width="200px" AppendDataBoundItems="true">
                        
                    </asp:DropDownList>
                   
                </td>
            </tr>
            <tr>
                <td></td>
                <td class="value">
                 <asp:Button ID="uoButtonView" runat="server" Text="View" CssClass="caption" 
                        onclick="uoButtonView_Click"/>
                </td>
                <td></td>
                <td class="value">
                     <asp:Button ID="uoButtonExport" runat="server" Text="Export" CssClass="caption" 
                        onclick="uoButtonExport_Click"/>
                </td>                
            </tr>
        </table>
        
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
   </div>
   <asp:HiddenField ID="uoHiddenFieldRole" runat="server" />
   <asp:HiddenField ID="uoHiddenFieldUser" runat="server" />
   <asp:HiddenField ID="uoHiddenFielddateRange" runat="server" />
</asp:Content>

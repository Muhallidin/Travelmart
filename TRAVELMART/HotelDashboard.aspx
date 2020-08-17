<%@ Page Title="" Language="C#" MasterPageFile="~/TravelmartMaster.Master" AutoEventWireup="true" CodeBehind="HotelDashboard.aspx.cs" Inherits="TRAVELMART.HotelDashboard" %>

<script runat="server">
</script>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
//    function OpenDetails(Status, VendorId, BranchId, RoomType, date) {
//        var URLString = "HotelDashboardDetails.aspx?";
//        URLString += "Status=" + Status + "&date=" + date + "&vId=" + VendorId + "&bId=" + BranchId + "&rType=" +RoomType;

//        var screenWidth = screen.availwidth;
//        var screenHeight = screen.availheight;

//        screenWidth = 800;
//        screenHeight = 500;

//        window.open(URLString, 'Contract_Management', 'top=10, left=200, width=' + screenWidth + ', height=' + screenHeight + ', scrollbars=yes, menubar=no,toolbar=no,status=no,resizable=yes,addressbar=no');
//        return false;

//    }
    function filterSettings() {
        var role = $("#<%=uoHiddenFieldRole.ClientID %>").val();
        $("#ctl00_ContentPlaceHolder1_uoHotelList_uoCheckBoxCheckAll").click(function(ev) {
            var status = $(this).attr('checked');
            $('input:checkbox[name *= uoSelectCheckBox]:enabled').each(function() {                
                    $(this).attr('checked', status);
            });
          
        });

        if (role != 'Hotel Vendor') {
            $("#<%=uoButtonApproved.ClientID %>").click(function(ev) {
                var listOfCheckbox = $('input:checkbox[name*=uoSelectCheckBox]:checked');

                var i = 0;
                $(listOfCheckbox).each(function() {
                    i++;
                });


                if (i == 0) {
                    alert("No selected request!");
                    return false;
                }
                else {
                    return true;
                }
            });
        }
    }
    $(document).ready(function() {
        ShowPopup();
        HideSearch();
        filterSettings();
    });

    function pageLoad(sender, args) {
        var isAsyncPostback = Sys.WebForms.PageRequestManager.getInstance().get_isInAsyncPostBack();
        if (isAsyncPostback) {
            ShowPopup();
            HideSearch();

        }
        filterSettings();
    }

    function HideSearch() {

    }

    function ShowPopup() {
        $("a#<%=uoHyperLinkAdd.ClientID %>").fancybox(
        {
            'width': '50%',
            'height': '90%',
            'autoScale': false,
            'transitionIn': 'fadeIn',
            'transitionOut': 'fadeOut',
            'type': 'iframe',
            'onClosed': function() {
                var a = $("#<%=uoHiddenFieldPopupHotel.ClientID %>").val();
                if (a == '1')
                    $("#aspnetForm").submit();
            }
        });
        $(".HotelLink").fancybox(
        {
            'width': '50%',
            'height': '90%',
            'autoScale': false,
            'transitionIn': 'fadeIn',
            'transitionOut': 'fadeOut',
            'type': 'iframe',
            'onClosed': function() {
                var a = $("#<%=uoHiddenFieldPopupHotel.ClientID %>").val();
                if (a == '1')
                    $("#aspnetForm").submit();
            }
        });
    }
    function OpenEventsList(branchID, cityID, OnOffDate) {
        var URLString = "Maintenance/EventsList.aspx?bId=";
        URLString += branchID;
        URLString += "&cityId=" + cityID;
        URLString += "&Date=" + OnOffDate;

        var screenWidth = screen.availwidth;
        var screenHeight = screen.availheight;

        screenWidth = 600;
        screenHeight = 400;

        window.open(URLString, 'Events_List', 'top=10, left=200, width=' + screenWidth + ', height=' + screenHeight + ', scrollbars=yes, menubar=no,toolbar=no,status=no,resizable=yes,addressbar=no');
        return false;
    }               
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<table width="100%" class ="PageTitle">
    <tr>
        <td align="left">
             <asp:Label Font-Bold="true" Font-Size="11" ID="uoLabelBranchName" runat="server" Text=""></asp:Label>
             
             <asp:LinkButton ID="uoLinkButtonEvents" runat="server" Text="*" CssClass="EventNotification" ToolTip="With Event(s)" ></asp:LinkButton>
             &nbsp;<asp:Label Font-Bold="true" Font-Size="11" ID="Label1" runat="server" Text=" Hotel Dashboard "></asp:Label>                         
             
             <asp:Button runat="server" ID="btn" Text="new dashboard" onclick="btn_Click" />
        </td>
        <td align="right">
            <a runat="server" id="uoHyperLinkAdd">
                <asp:Button runat="server" ID="uoButtonAdd" Text="Add Hotel Room Blocks" CssClass ="SmallButton" Visible="false"/>
            </a>
        </td>
    </tr>
</table>

<br />

<div class="Module">
    <asp:UpdatePanel runat="server" ID="uoUpdatePanelDetails" UpdateMode="Conditional" >
        <ContentTemplate>
            <asp:ListView runat="server" ID="uoHotelDashboardDetails">
                <LayoutTemplate>
                    <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                        <tr>
                            <th>Date</th>                           
                            <th>Room Type</th>                           
                            <th>Expected</th>
                            <th>Arriving Pax</th>
                            <th>Checked In</th>
                            <th>Checked Out</th>
                            <th>Cancelled</th>
                            <th>No Show</th>
                             <th>Total Room Bookings</th>
                            <th>Total Room Blocks</th>                            
                        </tr>
                        <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                    </table>
                </LayoutTemplate>
                <ItemTemplate>
                    <%# addDashBoardGroup()%>
                    <tr>
                        <td class="leftAligned">
                            <%# setDate() %>
                        </td>                       
                        <td class="leftAligned">
                            <%# Eval("colRoomType") %>
                        </td>
                        
                         <td>
                             <%# Eval("ExpectedArriving")%>                            
                        </td>
                        <td>
                            <%# Eval("Unused")%>
                        </td>
                        <td>
                            <%# Eval("Checked In")%>
                        </td>
                        <td>
                          <%# Eval("Checked Out")%>
                        </td>
                        <td>
                            <%# Eval("Cancelled")%>
                        </td>
                        <td>
                            <%# Eval("No Show")%>
                        </td>
                        <td>
                            <%--<%# Eval("UsedRooms") %>--%>
                             <%# String.Format("{0:0.##}", Eval("UsedRooms"))%>
                        </td>                       
                        <td>
                            <%# Eval("TotalDayCount") %>
                        </td>                                  
                    </tr>
                </ItemTemplate>
                <EmptyDataTemplate>
                    <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                        <tr>
                           <th>Date</th>                           
                            <th>Room Type</th>
                            
                            <th>Expected</th>
                            <th>Arriving Pax</th>
                            <th>Checked In</th>
                            <th>Checked Out</th>
                            <th>Cancelled</th>
                            <th>No Show</th>
                            <th>Total Bookings</th>
                            <th>Total Room Blocks</th>
                        </tr>
                        <tr>
                            <td colspan="10" class="leftAligned" >No Record.</td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
            </asp:ListView>
        </ContentTemplate>
    </asp:UpdatePanel>
    <br />
    <div runat="server" class="PageSubTitle" style="text-decoration:underline;">Confirmed Bookings</div>
    <div class="Module">
         <asp:UpdatePanel runat="server" ID="upPanelDetails" UpdateMode="Conditional">
            <ContentTemplate>                
                <asp:ListView runat="server" ID="uoDashBoardListDetails">
                    <LayoutTemplate>                    
                        <table border="0" cellspacing="0" cellpadding="0" class="listViewTable">
                            <tr>
                                <th>Rec Loc</th>
                                <th>E1 ID</th>
                                <th>Room</th>
                                <th>Name</th>
                                <th>Date From</th>
                                <th>Date To</th>                                
                                <th>Rank</th>
                                <th>Gender</th>
                                <th>Nationality</th>
                                <th>Cost Center</th>
                                <th>Duration</th>
                                <th>Hotel City</th>
                                <th>Airline</th>
                                <th>From City</th>
                                <th>To City</th>
                            </tr>
                            <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                        </table>
                     </LayoutTemplate>
                     <ItemTemplate>
                        <tr>
                            <%# SetStatus()%>
                            <td class="leftAligned">
                                <%# Eval("recloc") %>
                            </td>
                            <td class="leftAligned">
                                <%# Eval("e1Id") %>
                            </td>
                           
                            <td class="leftAligned">
                                <%# Eval("colRoomNameVarchar") %>
                            </td>
                            <td class="leftAligned">
                               <asp:LinkButton runat="server" ID="uoLinkSeafarer" 
                                    PostBackUrl='<%# "~/SuperUserView.aspx?sfId=" + Eval("e1Id") +  "&trID=" + Eval("TravelRequestID") + "&st=" + Eval("sfStatus") + "&ufn=" + Request.QueryString["ufn"] + "&manualReqID=" + Eval("RequestID")%>' 
                                    Text='<%# Eval("Name") %>'></asp:LinkButton>
                            </td>
                            <td class="leftAligned">
                                <%# String.Format("{0:dd-MMM-yyyy}", Eval("cDate")) %>
                            </td>
                            <td class="leftAligned">
                                <%# String.Format("{0:dd-MMM-yyyy}", Eval("cEndDate")) %>
                            </td>
                            
                            <td class="leftAligned">
                                <%# Eval("colRankNameVarchar") %>
                            </td>
                            <td class="leftAligned">
                                <%# Eval("Gender") %>
                            </td>
                            <td class="leftAligned">
                                <%# Eval("Nationality") %>
                            </td>
                            <td class="leftAligned">
                                <%# Eval("colCostCenterNameVarchar") %>
                            </td>
                            <td class="leftAligned">
                                <%# Eval("colTimeSpanDurationInt") %>
                            </td>
                            <td class="leftAligned">
                                <%# Eval("colCityNameVarchar") %>
                            </td>
                            <td class="leftAligned">
                                <%# Eval("colMarketingAirlineCodeVarchar")%>
                            </td>
                            <td class="leftAligned">
                                <%# Eval("colDepartureAirportLocationCodeVarchar")%>
                            </td>
                            <td class="leftAligned">
                                <%# Eval("colArrivalAirportLocationCodeVarchar")%>
                            </td>
                        </tr>
                     </ItemTemplate>
                     <EmptyDataTemplate>
                        <table border="0" cellspacing="0" cellpadding="0" class="listViewTable">
                            <tr>
                                <th>Rec Loc</th>
                                <th>E1 ID</th>
                                <th>Room</th>
                                <th>Name</th>
                                <th>Date From</th>
                                <th>Date To</th>                                
                                <th>Rank</th>
                                <th>Gender</th>
                                <th>Nationality</th>
                                <th>Cost Center</th>
                                <th>Duration</th>
                                <th>Hotel City</th>
                                <th>Airline</th>
                                <th>From City</th>
                                <th>To City</th>
                            </tr>
                            <tr>
                                <td colspan ="15">No Record</td>
                            </tr>
                         </table>
                     </EmptyDataTemplate>
                </asp:ListView>
                <asp:DataPager runat="server" ID="uoDashBoardListDetailsPager"
                    PagedControlID="uoDashBoardListDetails"
                    PageSize="20">
                    <Fields>
                        <asp:NumericPagerField ButtonType ="Link" NumericButtonCssClass="PagerClass" />
                    </Fields>        
                </asp:DataPager>
                <asp:HiddenField runat="server" id="uoHiddenFieldStartDate" Value="0" />
                <asp:HiddenField runat="server" ID="uoHiddenFieldEndDate" Value="0" />
                <asp:HiddenField runat="server" ID="uoHiddenFieldUserBrand" Value="0" />
                <asp:HiddenField runat="server" ID="uoHiddenFieldUserBranch" Value="0" />
                <asp:HiddenField runat="server" ID ="uoHiddenFieldUserRegion" Value="0" />
                 <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" EnablePaging="True" 
                    OldValuesParameterFormatString="OldCount_{0}" 
                    SelectCountMethod="HotelDashboardDetailsbyStatusCount" 
                    SelectMethod="HotelDashboardDetailsbyStatus" TypeName="TRAVELMART.BLL.DashboardBLL">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="uoHiddenFieldStartDate" DbType="Date" 
                            Name="cDate" PropertyName="Value" />
                        <asp:ControlParameter ControlID="uoHiddenFieldUserBranch" DbType="Int32" 
                            Name="BranchId" PropertyName="Value" />
                        <asp:ControlParameter ControlID="uoHiddenFieldUserBrand" DbType="Int32" 
                            Name="BrandId" PropertyName="Value" />
                    </SelectParameters>
                </asp:ObjectDataSource>
                
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
   
   <br />
   <div runat="server" id="PendingDiv" class="PageSubTitle">Pending Bookings for Approval 
    &nbsp;&nbsp;
        <asp:HyperLink ID="uoHyperLinkNoBooking"  runat="server" Visible ="false">No Bookings</asp:HyperLink>        
   </div>
   <div class="RightClass">
        <asp:Button runat="server" ID="uoButtonApproved" Text="Approve"
            CssClass="SmallButton" onclick="uoButtonApproved_Click" />
   </div>
   
        <asp:UpdatePanel runat="server" ID="uoPanelPending" >
            <ContentTemplate>
                <asp:ListView runat="server" ID="uoHotelList">
                    <LayoutTemplate>
                         <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                            <tr>  
                            <th class="hideElement">Req ID</th> 
                            <th>Is Manual</th>       
                            <th>Couple</th>                 
                            <th>Gender</th>
                            <th>Nationality</th>                                                        
                            <th>Cost Center</th>
                            <th>Check-In</th>
                            <th>Check-Out</th>
                            <th>Name</th>
                            <th>Employee ID</th>
                            <th>Ship</th>                            
                            <th>Hotel Request</th>
                            <th>Single/Double</th>
                            <th>Title</th>
                            <th>Hotel City</th>
                            <th>Hotel Nites</th>
                            <th>From City</th>
                            <th>To City</th>
                            <th>Record Locator</th>
                            <th>Carrier</th>
                            <th>Dept Date</th>
                            <th>Arvl Date</th>
                            <th>Dept Time</th>
                            <th>Arvl Time</th>
                            <th>Flight No.</th>
                            <th>Sign ON/OFF</th>
                            <th>Voucher</th>
                            <%--<th>Travel Date</th>--%>
                            <th>Reason</th>
                            <th>Stripe</th>
                            <th>
                                <asp:CheckBox runat="server" ID="uoCheckBoxCheckAll" />
                            </th>
                            </tr>
                            <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                        </table>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <tr style='<%# ValidateData() %>'>
                            <td class="hideElement">
                                <asp:HiddenField runat="server" ID="uoHiddenFieldColIdBigInt"
                                    Value='<%# Eval("HotelPendingID") %>' />
                                <asp:HiddenField runat="server" ID="uoHiddenFieldIsManual" Value='<%# Eval("ISMANUAL") %>' />
                                <asp:HiddenField runat="server" ID="hfVendor" Value='<%# Eval("VendorId") %>' />
                                <asp:HiddenField runat="server" ID="hfBranch" Value='<%# Eval("BranchId") %>' />
                                <asp:HiddenField runat="server" ID="hfRoom" Value='<%# Eval("RoomTypeId") %>' />
                                <asp:HiddenField runat="server" ID="hfCountry" Value='<%# Eval("CountryID") %>' />
                                <asp:HiddenField runat="server" ID="hfCity" Value='<%# Eval("CityId") %>' />
                                <asp:HiddenField runat="server" ID="hfTRID" Value='<%# Eval("TravelRequestId") %>' />
                                <asp:HiddenField runat="server" ID="hfMRID" Value='<%# Eval("ManualRequestId") %>' />
                                <asp:HiddenField runat="server" ID="hfPort" Value='<%# Eval("PortId") %>' />
                                <asp:HiddenField runat="server" ID="hfVessel" Value='<%# Eval("VesselId") %>' />
                                <asp:HiddenField runat="server" ID="hfContractID" Value='<%# Eval("ContractId") %>' />
                               
                                <asp:HiddenField runat="server" ID="hfSFStatus" Value='<%# Eval("Status") %>' />
                                <asp:HiddenField runat="server" ID="hfHotelName" Value='<%# Eval("HotelName") %>' />
                                 <asp:HiddenField runat="server" ID="hfName" Value='<%# Eval("Name") %>' />
                                 <asp:HiddenField runat="server" ID="hfCheckIn" Value='<%# Eval("CheckInDate") %>' />
                                 <asp:HiddenField runat="server" ID="hfCheckOut" Value='<%# Eval("CheckOutDate") %>' />
                            </td>
                            <td class="leftAligned">
                                <%# Eval("ISMANUAL") %>
                            </td>
                            <td class="leftAligned">
                                <%# Eval("Couple") %>
                            </td>
                            <td class="leftAligned">
                                <%# Eval("Gender") %>
                            </td>
                            <td class="leftAligned">
                                <%# Eval("Nationality")%>
                            </td>
                            <td class="leftAligned">
                                <%# Eval("CostCenter")%>
                            </td>
                            <td class="leftAligned">
                                <asp:Label ID="uoLblCheckIn" runat="server"
                                    Text ='<%# String.Format("{0:dd-MMM-yyyy}", Eval("CheckInDate")) %>'></asp:Label>
                            </td>
                            <td class="leftAligned">
                                <asp:Label ID="uoLblCheckOut" runat="server"
                                    Text ='<%# String.Format("{0:dd-MMM-yyyy}", Eval("CheckOutDate")) %>'></asp:Label>
                            </td>
                             <td class="leftAligned">
                                <asp:LinkButton runat="server" ID="uoLinkSeafarer" 
                                    Visible='<%# Convert.ToBoolean(Eval("ISEDITABLE")) %>'
                                    PostBackUrl='<%# "~/SuperUserView.aspx?sfId=" + Eval("E1ID") +  "&trID=" + Eval("TravelRequestId") + "&st=" + Eval("Status") + "&ufn=" + Request.QueryString["ufn"] + "&manualReqID=" + Eval("ManualRequestID")%>' Text='<%# Eval("Name") %>'></asp:LinkButton>
                                <asp:Label runat="server" ID="uoLabelSeafarer" Visible='<%# !Convert.ToBoolean(Eval("ISEDITABLE")) %>'
                                    Text='<%# Eval("Name") %>'></asp:Label>
                            </td>
                            <td class="leftAligned">
                                <asp:Label ID="uoLblE1ID" runat="server" Text='<%# Eval("E1ID") %>'></asp:Label>
                            </td>
                            <td class="leftAligned">
                                <%# Eval("Vessel")%>
                            </td>
                            <td class="leftAligned">
                                <%# Eval("HotelRequest")%>
                            </td>
                            <td class="leftAligned">
                                <%# Eval("RoomName")%>
                            </td>
                            <td class="leftAligned">
                                <%# Eval("Rank")%>
                            </td>
                            <td class="leftAligned">
                                <%# Eval("HotelCity")%>
                            </td>
                            <td class="leftAligned">
                                <asp:Label ID="uoLblDuration" runat="server" Text='<%# Eval("HotelNites") %>'></asp:Label>
                            </td>
                            <td class="leftAligned">
                                <%# Eval("FromCity")%>
                            </td>
                            <td class="leftAligned">
                                <%# Eval("ToCity")%>
                            </td>
                             <td class="leftAligned">
                                <asp:Label ID="uoLblRecLoc" runat="server" Text='<%# Eval("RecordLocator") %>'></asp:Label>
                            </td>
                            <td class="leftAligned">
                                <%# Eval("Carrier")%>
                            </td>
                            <td class="leftAligned">
                                <%# String.Format("{0:dd-MMM-yyyy}", Eval("DeptDate"))%>
                            </td>
                            <td class="leftAligned">
                                <%# String.Format("{0:dd-MMM-yyyy}", Eval("ArvlDate"))%>
                            </td>
                            <td class="leftAligned">
                                <%# String.Format("{0:HH:MM:ss}", Eval("DeptDate"))%>
                            </td>
                            <td class="leftAligned">
                                <%# String.Format("{0:HH:MM:ss}", Eval("ArvlDate"))%>
                            </td>
                            <td class="leftAligned">
                                <%# Eval("FlightNo")%>
                            </td>
                            <td class="leftAligned">
                                <%# String.Format("{0:dd-MMM-yyyy}", Eval("OnOffDate"))%>
                            </td>
                            
                            <td class="leftAligned">
                                <asp:Label runat="server" ID="uoLblVoucher" Text='<%# String.Format("{0:0.00}",Eval("Voucher"))%>'></asp:Label>                                
                            </td>
                           <%-- <td class="leftAligned">
                                <%# Eval("TravelDate")%>
                            </td>--%>
                            <td class="leftAligned">
                                <%# Eval("ReasonCode")%>
                            </td>
                            <td class="leftAligned">
                                <%# Eval("Stripe") %>
                            </td>
                            <td>
                                <asp:CheckBox CssClass="Checkbox" ID="uoSelectCheckBox" runat="server" Enabled='<%# Convert.ToBoolean(Eval("ENABLEDBIT"))%>' />
                            </td>
                        </tr>
                    </ItemTemplate>
                    <EmptyDataTemplate>
                        <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                            <tr>  
                                <th class="hideElement">Req ID</th>      
                                <th>Is Manual</th>        
                                <th>Couple</th>                 
                                <th>Gender</th>
                                <th>Nationality</th>                                                        
                                <th>Cost Center</th>
                                <th>Check-In</th>
                                <th>Check-Out</th>
                                <th>Name</th>
                                <th>Employee ID</th>
                                <th>Ship</th>                            
                                <th>Hotel Request</th>
                                <th>Single/Double</th>
                                <th>Title</th>
                                <th>Hotel City</th>
                                <th>Hotel Nites</th>
                                <th>From City</th>
                                <th>To City</th>
                                <th>Record Locator</th>
                                <th>Carrier</th>
                                <th>Dept Date</th>
                                <th>Arvl Date</th>
                                <th>Dept Time</th>
                                <th>Arvl Time</th>
                                <th>Flight No.</th>
                                <th>Sign ON/OFF</th>
                                <th>Voucher</th>
                                <%--<th>Travel Date</th>--%>
                                <th>Reason</th>
                                <th>Stripe</th>
                            </tr>
                            <tr>
                                <td colspan="12" class="leftAligned">No Pending Hotel Bookings.</td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                </asp:ListView>           
                <asp:DataPager runat="server" ID="uoHotelListPager" PagedControlID="uoHotelList" 
                    PageSize="20" onprerender="uoHotelListPager_PreRender">
                    <Fields>
                        <asp:NumericPagerField ButtonType="Link" NumericButtonCssClass="PagerClass" />
                    </Fields>
                </asp:DataPager>     
                  
                <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" EnablePaging="True" 
                    MaximumRowsParameterName="maximumRows" OldValuesParameterFormatString="oldCount_{0}" 
                    onselecting="ObjectDataSource2_Selecting" 
                    SelectCountMethod="SelectAutomaticBookingCount" SelectMethod="SelectAutomaticBooking" 
                    StartRowIndexParameterName="startRowIndex" 
                    TypeName="TRAVELMART.BLL.AutomaticBookingBLL">
                    
                    <SelectParameters>
                        <asp:ControlParameter ControlID="uoHiddenFieldUser" 
                            ConvertEmptyStringToNull="False" DbType="String" Name="UserId" 
                            PropertyName="Value" />
                    </SelectParameters>
                    
                    
                </asp:ObjectDataSource>
            </ContentTemplate>
        </asp:UpdatePanel> 
   
</div>
<asp:HiddenField ID="uoHiddenFieldRole" Value="0" runat="server" />
<asp:HiddenField runat="server" ID="uoHiddenFieldPopupHotel" Value="0" />
<asp:HiddenField runat="server" ID="uoHiddenAvailableBlocks" Value="0"/>
<asp:HiddenField runat="server" ID="uoHiddenFieldUser" />
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/TravelmartMaster2.Master" AutoEventWireup="true"
    CodeBehind="HotelDashboard3.aspx.cs" Inherits="TRAVELMART.HotelDashboard3" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
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

        function RefreshPageFromPopup() {
            if ($("#<%=uoHiddenFieldPopEditor.ClientID %>").val() == 1) {
                $("#aspnetForm").submit();
            }
        }

        function OpenRequestEditor(sfID, sfName, Stripe, Status, RecLoc, TrId, BranchId, mReqId) {
            var screenWidth = screen.availwidth;
            var screenHeight = screen.availheight;
            //           var sfId2 = $(
            screenWidth = 1060;
            screenHeight = 600;

            window.open('Hotel/HotelEditor2.aspx?sfID=' + sfID + '&sfName=' + sfName + '&Stripe='
            + Stripe + '&Status=' + Status + '&RecLoc=' + RecLoc + '&trId=' + TrId + '&branchId='
            + BranchId + '&mReqId=' + mReqId, 'Hotel_Vehicle_Request', 'top=10, left=200, width=' + screenWidth + ', height=' + screenHeight + ', scrollbars=yes, menubar=no,toolbar=no,status=no,resizable=yes,addressbar=no');
            return false;
        }

        function confirmTag(e1ID, seafarerName) {
            var sMsg = "Tag " + e1ID + ": " + seafarerName + "? ";
            var x = confirm(sMsg);
            return x;
        }
    </script>

</asp:Content>
<asp:Content ContentPlaceHolderID="HeaderContent" ID="ContentHeader" runat="server">
    <div class="PageTitle">
        Hotel Dashboard &nbsp;
        <asp:Label runat="server" ID="uoLabelBranchName" CssClass="Title"></asp:Label>
        <asp:LinkButton runat="server" CssClass="EventNotification" ID="uoLinkButtonEvents">
            <img src="Images/calendar1.png" Width="20px" border="0" />
        </asp:LinkButton>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NaviPlaceHolder" runat="server">
    <%--ContentPlaceHolder1--%>

    <script language="javascript" type="text/javascript">
        $(document).ready(function() {
            SetRes();
            ShowPopup();
            ShowPopup2();
            filterSettings();
        });

        function pageLoad(sender, args) {
            var isAsyncPostback = Sys.WebForms.PageRequestManager.getInstance().get_isInAsyncPostBack();
            if (isAsyncPostback) {
                SetRes();
                ShowPopup();
                ShowPopup2();
            }
            filterSettings();
        }

        function filterSettings() {

            var role = $("#<%=uoHiddenFieldRole.ClientID %>").val();
            $("#ctl00_NaviPlaceHolder_uoHotelList_uoCheckBoxCheckAll").click(function(ev) {
                var status = $(this).attr('checked');

                $('input:checkbox[name *= uoSelectCheckBox]:enabled').each(function() {
                    $(this).attr('checked', status);
                });

            });

            if (role != 'Hotel Vendor') {
                $("#<%=uoButtonApprove.ClientID %>").click(function(ev) {
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
        function ShowPopup() {
            $(".Link").fancybox(
        {
            'centerOnScroll': false,
            'width': '35%',
            'height': '100%',
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
        function ShowPopup2() {
            $(".HotelLink").fancybox(
        {
            'centerOnScroll': false,
            'width': '40%',
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
        function SetRes() {
            var ht = $(window).height();
            var ht2 = $(window).height();

            var wd = $(window).width() * 0.90;
            var screenH = screen.height;
            var percent = 0.60;

            if (screen.height <= 600) {
                ht = ht * 0.44;
            }
            else if (screen.height <= 720) {
                ht = ht * 0.55;
            }
            else {
                if (screenH = 768) {
                    percent = (550 - (screenH - ht)) / ht;
                }
                ht = ht * percent;
            }

            $("#br").height(ht);
            $("#br").width(wd);
        }
    </script>

    <%--<div class="PageTitle" style="width:100%;">
        Hotel Dashboard &nbsp; 
        <asp:Label runat="server" ID="uoLabelBranchName" CssClass="Title"></asp:Label>
        <asp:LinkButton runat="server" CssClass="EventNotification" ID="uoLinkButtonEvents">
            <img src="Images/calendar1.png" Width="20px" border="0" />
        </asp:LinkButton>               
    </div>--%>
    <%--<asp:ImageButton runat="server" ID="uoImageButtonEvents" ImageUrl="~/Images/calendar1.png" Width="20px" ToolTip='<%# SetTooltip() %>' Enabled='<%# SetEventEnabled() %>'
        OnClientClick='<%# "return OpenEventsList(\"" + Request.QueryString["branchId"] + "\",\"" + 0 + "\",\"" + String.Format("{0:MM-dd-yyyy}", Request.QueryString["dt"].ToString()) + "\")" %>' />--%>
    <div id="br" style="overflow: auto; overflow-x: auto; overflow-y: auto;">
        <div runat="server" id="divNotVendor">
            <div class="Module">

                <script language="javascript" type="text/javascript">
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

                <%--get first list--%>
                <asp:UpdatePanel runat="server" ID="uoUpdatePanelDetails" UpdateMode="Conditional">
                    <ContentTemplate>
                        <%--<div style="overflow: auto; overflow-x: auto; overflow-y: auto;">--%>
                        <asp:ListView runat="server" ID="uoHotelDashboardDetails">
                            <LayoutTemplate>
                                <table class="listViewTable" border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <th style="width: 100px">
                                            <asp:LinkButton runat="server" ID="uoLinkButtonDashBoardDetailsDate" Text="Date"
                                                CommandArgument="Date" OnClick="uoLinkButtonConfirmedCheckin_Click"></asp:LinkButton>
                                        </th>
                                        <%--<th class="hideElement">RoomTypeID</th>--%>
                                        <th style="width: 90px">
                                            <asp:LinkButton runat="server" ID="uoLinkButtonDashBoardDetailsRoom" Text="Room"
                                                CommandArgument="Room" OnClick="uoLinkButtonConfirmedCheckin_Click"></asp:LinkButton>
                                        </th>
                                        <%--<th class="hideElement">ReservedRoom</th>--%>
                                        <th style="width: 90px">
                                            <asp:LinkButton runat="server" ID="uoLinkButtonDashBoardDetailsExpected" Text="Expected"
                                                CommandArgument="Expected" OnClick="uoLinkButtonConfirmedCheckin_Click"></asp:LinkButton>
                                        </th>
                                        <th style="width: 90px">
                                            <asp:LinkButton runat="server" ID="uoLinkButtonDashBoardDetailsArrivingPax" Text="Arriving Pax"
                                                CommandArgument="Arriving" OnClick="uoLinkButtonConfirmedCheckin_Click"></asp:LinkButton>
                                        </th>
                                        <th style="width: 90px">
                                            <asp:LinkButton runat="server" ID="uoLinkButtonDashBoardDetailsCheckIn" Text="Check-In"
                                                CommandArgument="CheckedIn" OnClick="uoLinkButtonConfirmedCheckin_Click"></asp:LinkButton>
                                        </th>
                                        <th style="width: 90px">
                                            <asp:LinkButton runat="server" ID="uoLinkButtonDashBoardDetailsCheckOut" Text="Check-Out"
                                                CommandArgument="CheckedOut" OnClick="uoLinkButtonConfirmedCheckin_Click"></asp:LinkButton>
                                        </th>
                                        <th style="width: 90px">
                                            <asp:LinkButton runat="server" ID="uoLinkButtonDashBoardDetailsCancelled" Text="Cancelled"
                                                CommandArgument="Cancelled" OnClick="uoLinkButtonConfirmedCheckin_Click"></asp:LinkButton>
                                        </th>
                                        <th style="width: 90px">
                                            <asp:LinkButton runat="server" ID="uoLinkButtonDashBoardDetailsNoShow" Text="No Show"
                                                CommandArgument="NoShow" OnClick="uoLinkButtonConfirmedCheckin_Click"></asp:LinkButton>
                                        </th>
                                        <th style="width: 90px">
                                            <asp:LinkButton runat="server" ID="uoLinkButtonDashBoardDetailsTRBooking" Text="Total Room Bookings"
                                                CommandArgument="TotalRoomBookings" OnClick="uoLinkButtonConfirmedCheckin_Click"></asp:LinkButton>
                                        </th>
                                        <th style="width: 90px">
                                            <asp:LinkButton runat="server" ID="uoLinkButtonDashBoardDetailsTRBlock" Text="Total Room Blocks"
                                                CommandArgument="TotalRoomBlocks" OnClick="uoLinkButtonConfirmedCheckin_Click"></asp:LinkButton>
                                        </th>
                                        <th style="width: 90px">
                                            <asp:LinkButton runat="server" ID="uoLinkButtonDashBoardDetailsTEBooking" Text="Total Emergency Bookings"
                                                CommandArgument="TotalEmergencyBookings" OnClick="uoLinkButtonConfirmedCheckin_Click"></asp:LinkButton>
                                        </th>
                                        <th style="width: 90px">
                                            <asp:LinkButton runat="server" ID="uoLinkButtonDashBoardDetailsTEBlock" Text="Total Emergency Room Blocks"
                                                CommandArgument="TotalEmergencyRoomBlocks" OnClick="uoLinkButtonConfirmedCheckin_Click"></asp:LinkButton>
                                        </th>
                                        <th style="width: 90px">
                                            Override
                                        </th>
                                        <th style="width: 90px">
                                            Emergency
                                        </th>
                                    </tr>
                                    <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                                </table>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td class="leftAligned">
                                        <%# setDate()%>
                                    </td>
                                    <%--<td class="hideElement">
                                <asp:HiddenField runat="server" id="uoHiddenFieldRoomTypeID" value='<%# Eval("RoomTypeID") %>'/>
                            </td> --%>
                                    <td class="leftAligned">
                                        <%# Eval("RoomName")%>
                                    </td>
                                    <%--<td class="hideElement">
                                <asp:HiddenField runat="server" id="uoHiddenFieldReservedRoom" value='<%# Eval("ReservedRoom") %>'/>
                            </td>--%>
                                    <td>
                                        <%# Eval("Expected")%>
                                    </td>
                                    <td>
                                        <%# Eval("Arriving")%>
                                    </td>
                                    <td>
                                        <%# Eval("CheckedIn")%>
                                    </td>
                                    <td>
                                        <%# Eval("CheckedOut")%>
                                    </td>
                                    <td>
                                        <%# Eval("Cancelled")%>
                                    </td>
                                    <td>
                                        <%# Eval("NoShow")%>
                                    </td>
                                    <td>
                                        <%# Eval("TotalRoomBookings").ToString().Replace(".0","") %>
                                    </td>
                                    <td>
                                        <%# Eval("TotalRoomBlocks")%>
                                    </td>
                                    <td>
                                        <%# Eval("TotalEmergencyBookings").ToString().Replace(".0", "")%>
                                    </td>
                                    <td>
                                        <%# Eval("TotalEmergencyRoomBlocks")%>
                                    </td>
                                    <td>
                                        <asp:HyperLink CssClass="Link" ID="uoHyperLinkEditOverride" runat="server" NavigateUrl='<%# "HotelRoomOverrideEdit2.aspx?bID=" + Request.QueryString["branchId"] + "&rID=" + Eval("RoomTypeID") + "&dt=" + String.Format("{0:MM-dd-yyyy}", Eval("Date")) + "&rc=" + String.Format("{0:#####}", Eval("ReservedRoom")) %>'>Edit</asp:HyperLink>
                                    </td>
                                    <td>
                                        <asp:HyperLink CssClass="Link" ID="uoHyperLinkEditEmergency" runat="server" NavigateUrl='<%# "HotelRoomEmergencyEdit2.aspx?bID=" + Request.QueryString["branchId"] + "&rID=" + Eval("RoomTypeID") + "&dt=" + String.Format("{0:MM-dd-yyyy}", Eval("Date")) %>'>Edit</asp:HyperLink>
                                    </td>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:ListView>
                        <%-- </div>--%>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <br />
            <div runat="server" id="PendingDiv" class="PageSubTitle" style="text-decoration: underline;">
                Overflow Bookings
            </div>
            <div class="LeftClass">
                <asp:Button runat="server" ID="uoButtonApprove" Text="Book" CssClass="SmallButton"
                    Visible="false" OnClick="uoButtonApprove_Click" />
                <asp:Button runat="server" ID="uoButtonExportOverflow" Text="Export Overflow List"
                    CssClass="SmallButton" OnClick="uoButtonExportOverflow_Click" />
            </div>
            <div style="overflow: auto;">
                <asp:UpdatePanel runat="server" ID="uoPanelPending">
                    <ContentTemplate>
                        <%--<div style="overflow:auto; overflow-x:auto; height:auto;">--%>
                        <asp:ListView runat="server" ID="uoHotelList">
                            <LayoutTemplate>
                                <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                                    <tr>
                                        <th class="hideElement">
                                            Req ID
                                        </th>
                                        <th style="display: none">
                                            <asp:CheckBox runat="server" ID="uoCheckBoxCheckAll" Visible="false" />
                                        </th>
                                        <th>
                                            <asp:LinkButton runat="server" ID="uoLinkOverBookingCouple" Text="Couple" CommandArgument="SortByCouple"
                                                OnClick="uoLinkOverBooking_Click"></asp:LinkButton>
                                        </th>
                                        <th>
                                            <asp:LinkButton runat="server" ID="uoLinkOverBookingGender" Text="Gender" CommandArgument="SortByGender"
                                                OnClick="uoLinkOverBooking_Click"></asp:LinkButton>
                                        </th>
                                        <th>
                                            <asp:LinkButton runat="server" ID="uoLinkOverBookingNationality" Text="Nationality"
                                                CommandArgument="SortByNationality" OnClick="uoLinkOverBooking_Click"></asp:LinkButton>
                                        </th>
                                        <th>
                                            <asp:LinkButton runat="server" ID="uoLinkOverBookingCostCenter" Text="Cost Center"
                                                CommandArgument="SortByCostCenter" OnClick="uoLinkOverBooking_Click"></asp:LinkButton>
                                        </th>
                                        <th>
                                            <asp:LinkButton runat="server" ID="uoLinkOverBookingCheckIn" Text="Check-In" CommandArgument="SortByCheckIn"
                                                OnClick="uoLinkOverBooking_Click"></asp:LinkButton>
                                        </th>
                                        <th>
                                            <asp:LinkButton runat="server" ID="uoLinkOverBookingCheckOut" Text="Check-Out" CommandArgument="SortByCheckOut"
                                                OnClick="uoLinkOverBooking_Click"></asp:LinkButton>
                                        </th>
                                        <th>
                                            <asp:LinkButton runat="server" ID="uoLinkOverBookingName" Text="Name" CommandArgument="SortByName"
                                                OnClick="uoLinkOverBooking_Click"></asp:LinkButton>
                                        </th>
                                        <th>
                                            <asp:LinkButton runat="server" ID="uoLinkOverBookingEmployeeId" Text="Employee ID"
                                                CommandArgument="SortByEmployeeId" OnClick="uoLinkOverBooking_Click"></asp:LinkButton>
                                        </th>
                                        <th>
                                            <asp:LinkButton runat="server" ID="uoLinkOverBookingShip" Text="Ship" CommandArgument="SortByShip"
                                                OnClick="uoLinkOverBooking_Click"></asp:LinkButton>
                                        </th>
                                        <th>
                                            <asp:LinkButton runat="server" ID="uoLinkOverBookingSingleDouble" Text="Single/Double"
                                                CommandArgument="SortBySingleDouble" OnClick="uoLinkOverBooking_Click"></asp:LinkButton>
                                        </th>
                                        <th>
                                            <asp:LinkButton runat="server" ID="uoLinkOverBookingTitle" Text="Title" CommandArgument="SortByTitle"
                                                OnClick="uoLinkOverBooking_Click"></asp:LinkButton>
                                        </th>
                                        <th>
                                            <asp:LinkButton runat="server" ID="uoLinkOverBookingHotelCity" Text="Hotel City"
                                                CommandArgument="SortByHotelCity" OnClick="uoLinkOverBooking_Click"></asp:LinkButton>
                                        </th>
                                        <th>
                                            <asp:LinkButton runat="server" ID="uoLinkOverBookingHotelNites" Text="Notel Nites"
                                                CommandArgument="SortByHotelNites" OnClick="uoLinkOverBooking_Click"></asp:LinkButton>
                                        </th>
                                        <th>
                                            <asp:LinkButton runat="server" ID="uoLinkOverBookingFromCity" Text="From City" CommandArgument="SortFromCity"
                                                OnClick="uoLinkOverBooking_Click"></asp:LinkButton>
                                        </th>
                                        <th>
                                            <asp:LinkButton runat="server" ID="uoLinkOverBookingToCity" Text="To City" CommandArgument="SortByToCity"
                                                OnClick="uoLinkOverBooking_Click"></asp:LinkButton>
                                        </th>
                                        <th>
                                            <asp:LinkButton runat="server" ID="uoLinkOverBookingRecLoc" Text="Record Loc" CommandArgument="SortByRecLoc"
                                                OnClick="uoLinkOverBooking_Click"></asp:LinkButton>
                                        </th>
                                        <th>
                                            <asp:LinkButton runat="server" ID="uoLinkOverBookingCarrier" Text="Carrier" CommandArgument="SortByCarrier"
                                                OnClick="uoLinkOverBooking_Click"></asp:LinkButton>
                                        </th>
                                        <th>
                                            <asp:LinkButton runat="server" ID="uoLinkOverBookingDeptDate" Text="Dept Date" CommandArgument="SortByDeptDate"
                                                OnClick="uoLinkOverBooking_Click"></asp:LinkButton>
                                        </th>
                                        <th>
                                            <asp:LinkButton runat="server" ID="uoLinkOverBookingArvlDate" Text="Arvl Date" CommandArgument="SortByArclDate"
                                                OnClick="uoLinkOverBooking_Click"></asp:LinkButton>
                                        </th>
                                        <th>
                                            <asp:LinkButton runat="server" ID="uoLinkOverBookingDeptTime" Text="Dept Time" CommandArgument="SortByDeptDate"
                                                OnClick="uoLinkOverBooking_Click"></asp:LinkButton>
                                        </th>
                                        <th>
                                            <asp:LinkButton runat="server" ID="uoLinkOverBookingArvlTime" Text="Arvl Time" CommandArgument="SortByArvlTime"
                                                OnClick="uoLinkOverBooking_Click"></asp:LinkButton>
                                        </th>
                                        <th>
                                            <asp:LinkButton runat="server" ID="uoLinkOverBookingStewards" Text="Flight No." CommandArgument="SortByFlightNo"
                                                OnClick="uoLinkOverBooking_Click"></asp:LinkButton>
                                        </th>
                                        <th>
                                            <asp:LinkButton runat="server" ID="uoLinkOverBookingSingOnOff" Text="Sign ON/OFF"
                                                CommandArgument="SortBySignOnOff" OnClick="uoLinkOverBooking_Click"></asp:LinkButton>
                                        </th>
                                        <th>
                                            <asp:LinkButton runat="server" ID="uoLinkOverBooking" Text="Voucher" CommandArgument="SortByVoucher"
                                                OnClick="uoLinkOverBooking_Click"></asp:LinkButton>
                                        </th>
                                        <th>
                                            <asp:LinkButton runat="server" ID="uoLinkOverReason" Text="Reason" CommandArgument="SortByReason"
                                                OnClick="uoLinkOverBooking_Click"></asp:LinkButton>
                                        </th>
                                        <th>
                                            <asp:LinkButton runat="server" ID="uoLinkOverStripeStripe" Text="Stripe" CommandArgument="SortByStripe"
                                                OnClick="uoLinkOverBooking_Click"></asp:LinkButton>
                                        </th>
                                    </tr>
                                    <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                                </table>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <tr style='<%# ValidateRequest()%>'>
                                    <td class="hideElement">
                                        <asp:HiddenField runat="server" ID="hfVendor" Value='<%# Eval("VendorId") %>' />
                                        <asp:HiddenField runat="server" ID="hfBranch" Value='<%# Eval("BranchId") %>' />
                                        <asp:HiddenField runat="server" ID="hfRoom" Value='<%# Eval("RoomTypeId") %>' />
                                        <asp:HiddenField runat="server" ID="hfCountry" Value='<%# Eval("CountryId") %>' />
                                        <asp:HiddenField runat="server" ID="hfCity" Value='<%# Eval("CityId") %>' />
                                        <asp:HiddenField runat="server" ID="hfTRID" Value='<%# Eval("TravelReqId") %>' />
                                        <asp:HiddenField runat="server" ID="hfPort" Value='<%# Eval("PortId") %>' />
                                        <asp:HiddenField runat="server" ID="hfVessel" Value='<%# Eval("VesselId") %>' />
                                        <asp:HiddenField runat="server" ID="hfSFStatus" Value='<%# Eval("SFStatus") %>' />
                                        <asp:HiddenField runat="server" ID="hfName" Value='<%# Eval("Name") %>' />
                                        <asp:HiddenField runat="server" ID="hfCheckIn" Value='<%# Eval("CheckInDate") %>' />
                                        <asp:HiddenField runat="server" ID="hfCheckOut" Value='<%# Eval("CheckOutDate") %>' />
                                        <asp:HiddenField runat="server" ID="uoHiddenFieldIDBigint" Value='<%# Eval("CheckOutDate") %>' />
                                        <asp:HiddenField runat="server" ID="uoHiddenFieldSeqNo" Value='<%# Eval("CheckOutDate") %>' />
                                    </td>
                                    <td style="display: none">
                                        <asp:CheckBox Visible="false" CssClass="Checkbox" ID="uoSelectCheckBox" runat="server"
                                            Enabled='<%# Convert.ToBoolean(Eval("EnabledBit"))%>' />
                                    </td>
                                    <td class="leftAligned">
                                        <%# Eval("CoupleId")%>
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
                                        <asp:Label ID="uoLblCheckIn" runat="server" Text='<%# String.Format("{0:dd-MMM-yyyy}", Eval("CheckInDate")) %>'></asp:Label>
                                    </td>
                                    <td class="leftAligned">
                                        <asp:Label ID="uoLblCheckOut" runat="server" Text='<%# String.Format("{0:dd-MMM-yyyy}", Eval("CheckOutDate")) %>'></asp:Label>
                                    </td>
                                    <td class="leftAligned">
                                        <%--<%# Eval("Name")%>--%>
                                        <asp:HyperLink ID="uoHyperLinkSeafarer" NavigateUrl='<%# "SuperUserView.aspx?sfId=" + Eval("SeafarerId") +  "&trID=" + Eval("TravelReqId") + "&st=" + Eval("SFStatus") + "&ufn=" + Request.QueryString["ufn"] + "&manualReqID=0&dt=" + Request.QueryString["dt"]%>'
                                            runat="server"><%# Eval("Name")%>
                                        </asp:HyperLink>
                                        <%--<asp:Image ID="imgHotelRequest" Width="15px" ImageUrl="~/Images/Hotel.png" runat="server"
                                        ToolTip="Hotel Request" Visible='<%# bool.Parse(Eval("HotelRequest").ToString()) %>' />--%>
                                    </td>
                                    <td class="leftAligned">
                                        <asp:Label ID="uoLblE1ID" runat="server" Text='<%# Eval("SeafarerId") %>'></asp:Label>
                                    </td>
                                    <td class="leftAligned">
                                        <%# Eval("VesselName")%>
                                    </td>
                                    <td class="leftAligned">
                                        <%# Eval("RoomName")%>
                                    </td>
                                    <td class="leftAligned">
                                        <%# Eval("RankName")%>
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
                                        <%# String.Format("{0:dd-MMM-yyyy}", Eval("DepartureDate"))%>
                                    </td>
                                    <td class="leftAligned">
                                        <%# String.Format("{0:dd-MMM-yyyy}", Eval("ArrivalDate"))%>
                                    </td>
                                    <td class="leftAligned">
                                        <%# String.Format("{0:HH:MM:ss}", Eval("DepartureDate"))%>
                                    </td>
                                    <td class="leftAligned">
                                        <%# String.Format("{0:HH:MM:ss}", Eval("ArrivalDate"))%>
                                    </td>
                                    <td class="leftAligned">
                                        <%# Eval("FlightNo")%>
                                    </td>
                                    <td class="leftAligned">
                                        <%# String.Format("{0:dd-MMM-yyyy}", Eval("OnOffDate"))%>
                                    </td>
                                    <td class="leftAligned">
                                        <asp:Label runat="server" ID="uoLblVoucher" Text='<%# String.Format("{0:00}" ,Eval("Voucher"))%>'></asp:Label>
                                    </td>
                                    <td class="leftAligned">
                                        <%# Eval("ReasonCode")%>
                                    </td>
                                    <td class="leftAligned">
                                        <asp:Label runat="server" ID="uoLblStripe" Text='<%# Eval("Stripe") %>'></asp:Label>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <EmptyDataTemplate>
                                <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                                    <tr>
                                        <th class="hideElement">
                                            Req ID
                                        </th>
                                        <th>
                                            Couple
                                        </th>
                                        <th>
                                            Gender
                                        </th>
                                        <th>
                                            Nationality
                                        </th>
                                        <th>
                                            Cost Center
                                        </th>
                                        <th>
                                            Check-In
                                        </th>
                                        <th>
                                            Check-Out
                                        </th>
                                        <th>
                                            Name
                                        </th>
                                        <th>
                                            Employee ID
                                        </th>
                                        <th>
                                            Ship
                                        </th>
                                        <th>
                                            Single/Double
                                        </th>
                                        <th>
                                            Title
                                        </th>
                                        <th>
                                            Hotel City
                                        </th>
                                        <th>
                                            Hotel Nites
                                        </th>
                                        <th>
                                            From City
                                        </th>
                                        <th>
                                            To City
                                        </th>
                                        <th>
                                            Record Locator
                                        </th>
                                        <th>
                                            Carrier
                                        </th>
                                        <th>
                                            Dept Date
                                        </th>
                                        <th>
                                            Arvl Date
                                        </th>
                                        <th>
                                            Dept Time
                                        </th>
                                        <th>
                                            Arvl Time
                                        </th>
                                        <th>
                                            Flight No.
                                        </th>
                                        <th>
                                            Sign ON/OFF
                                        </th>
                                        <th>
                                            Voucher
                                        </th>
                                        <th>
                                            Reason
                                        </th>
                                        <th>
                                            Stripe
                                        </th>
                                    </tr>
                                    <tr>
                                        <td colspan="26" class="leftAligned">
                                            No Record.
                                        </td>
                                    </tr>
                                </table>
                            </EmptyDataTemplate>
                        </asp:ListView>
                        <table id="uoTableHotelListPager" align="left">
                            <tr>
                                <td class="LeftClass">
                                    <asp:DataPager runat="server" ID="uoHotelListPager" PagedControlID="uoHotelList"
                                        PageSize="20">
                                        <Fields>
                                            <asp:NumericPagerField ButtonType="Link" NumericButtonCssClass="PagerClass" />
                                        </Fields>
                                    </asp:DataPager>
                                </td>
                            </tr>
                        </table>
                        <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" EnablePaging="True" SelectCountMethod="LoadHotelDashboardPendingTableCount"
                            SelectMethod="LoadHotelDashboardPendingTable" MaximumRowsParameterName="MaximumRows"
                            TypeName="TRAVELMART.BLL.DashboardBLL" OldValuesParameterFormatString="oldCount_{0}"
                            StartRowIndexParameterName="StartRowIndex">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="uoHiddenFieldStartDate" ConvertEmptyStringToNull="False"
                                    DbType="DateTime" Name="StartDate" PropertyName="Value" />
                                <asp:ControlParameter ControlID="uoHiddenFieldBranchId" ConvertEmptyStringToNull="False"
                                    DbType="Int32" Name="BranchId" PropertyName="Value" />
                                <asp:Parameter DefaultValue="2" Name="LoadType" />
                                <asp:ControlParameter ControlID="uoHiddenFieldUser" ConvertEmptyStringToNull="False"
                                    DbType="String" Name="UserId" PropertyName="Value" />
                                <asp:ControlParameter ControlID="uoHiddenFieldFromDefaultView" DbType="Int16" Name="FromDefaultView" />
                                <asp:ControlParameter ControlID="uoHiddenFieldSortBy" DbType="String" Name="SortBy" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                        <%--</div>--%>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <br />
        </div>
        <div class="PageSubTitle" style="text-decoration: underline;">
            Confirmed Bookings
            <asp:Image ID="uoImageConfirmedBookings" runat="server" ImageUrl="~/Images/box_up.png"
                Height="10px" />
            <cc1:CollapsiblePanelExtender ID="CollapsiblePanelExtender_ConfirmedBookings" runat="Server"
                TargetControlID="uoPanelConfirmedBookings" CollapsedSize="0" ExpandControlID="uoImageConfirmedBookings"
                CollapseControlID="uoImageConfirmedBookings" AutoCollapse="False" AutoExpand="False"
                ImageControlID="uoImageConfirmedBookings" ExpandedImage="~/images/box_up.png"
                CollapsedImage="~/images/box_down.png" ExpandDirection="Vertical" Collapsed="false" />
        </div>
        <%--<div class="Module">--%>
        <div class="LeftClass">
            <asp:Button runat="server" ID="uoButtonConfirmedBookings" Text="Export Confirmed Booking List"
                CssClass="SmallButton" OnClick="uoButtonConfirmedBookings_Click" />
        </div>
        <asp:Panel ID="uoPanelConfirmedBookings" runat="server" ScrollBars="Auto">
            <asp:UpdatePanel runat="server" ID="uoPanelDetails">
                <ContentTemplate>
                    <asp:ListView runat="server" ID="uoDashboardListDetails" OnItemCommand="uoListViewTR_ItemCommand">
                        <LayoutTemplate>
                            <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                                <tr>
                                    <th>
                                        Hotel City
                                    </th>
                                    <th>
                                        <asp:LinkButton runat="server" ID="uoLinkConfirmedCheckIn" Text="Check-In" CommandArgument="SortByCheckIn"
                                            OnClick="uoLinkConfirmed_Click"></asp:LinkButton>
                                    </th>
                                    <th>
                                        <asp:LinkButton runat="server" ID="uoLinkConfirmedCheckOut" Text="Check-Out" CommandArgument="SortByCheckOut"
                                            OnClick="uoLinkConfirmed_Click"></asp:LinkButton>
                                    </th>
                                    <th>
                                        <asp:LinkButton runat="server" ID="uoLinkConfirmedHotelNites" Text="Hotel Nite" CommandArgument="SortByHotelNites"
                                            OnClick="uoLinkConfirmed_Click"></asp:LinkButton>
                                    </th>
                                    <th>
                                        <asp:LinkButton runat="server" ID="uoLinkConfirmedResonCode" Text="Reason Code" CommandArgument="SortByReasonCode"
                                            OnClick="uoLinkConfirmed_Click"></asp:LinkButton>
                                    </th>
                                    <th>
                                        <asp:LinkButton runat="server" ID="uoLinkConfirmedLastName" Text="Last Name" CommandArgument="SortByLastName"
                                            OnClick="uoLinkConfirmed_Click"></asp:LinkButton>
                                    </th>
                                    <th>
                                        <asp:LinkButton runat="server" ID="uoLinkConfirmedFirstName" Text="First Name" CommandArgument="SortByFirstname"
                                            OnClick="uoLinkConfirmed_Click"></asp:LinkButton>
                                    </th>
                                    <th>
                                        <asp:LinkButton runat="server" ID="uoLinkConfirmedEmployeeId" Text="Employee ID"
                                            CommandArgument="SortByEmployeeId" OnClick="uoLinkConfirmed_Click"></asp:LinkButton>
                                    </th>
                                    <th>
                                        <asp:LinkButton runat="server" ID="uoLinkConfirmedGender" Text="Gender" CommandArgument="SortByGender"
                                            OnClick="uoLinkConfirmed_Click"></asp:LinkButton>
                                    </th>
                                    <%--<th>
                                        <asp:LinkButton runat="server" ID="uoLinkConfirmedSingleDouble" Text="Single/Double" CommandArgument="SortBySingleDouble"
                                            OnClick="uoLinkConfirmed_Click"></asp:LinkButton>
                                    </th>--%>
                                    <th>
                                        <asp:LinkButton runat="server" ID="uoLinkConfirmedRoomType" Text="Room Type" CommandArgument="SortByRoomType"
                                            OnClick="uoLinkConfirmed_Click"></asp:LinkButton>
                                    </th>
                                    <th>
                                        <asp:LinkButton runat="server" ID="uoLinkConfirmedCouple" Text="Couple" CommandArgument="SortByCouple"
                                            OnClick="uoLinkConfirmed_Click"></asp:LinkButton>
                                    </th>
                                    <th>
                                        <asp:LinkButton runat="server" ID="uoLinkConfirmedTitle" Text="Title" CommandArgument="SortByTitle"
                                            OnClick="uoLinkConfirmed_Click"></asp:LinkButton>
                                    </th>
                                    <th>
                                        <asp:LinkButton runat="server" ID="uoLinkConfirmedShip" Text="Ship" CommandArgument="SortByShip"
                                            OnClick="uoLinkConfirmed_Click"></asp:LinkButton>
                                    </th>
                                    <th>
                                        <asp:LinkButton runat="server" ID="uoLinkConfirmedCostCenter" Text="Cost Center"
                                            CommandArgument="SortByCostCenter" OnClick="uoLinkConfirmed_Click"></asp:LinkButton>
                                    </th>
                                    <th>
                                        <asp:LinkButton runat="server" ID="uoLinkConfirmedNationality" Text="Nationality"
                                            CommandArgument="SortByNationality" OnClick="uoLinkConfirmed_Click"></asp:LinkButton>
                                    </th>
                                    <th>
                                        <asp:LinkButton runat="server" ID="uoLinkConfirmedHotelRequest" Text="Hotel Request"
                                            CommandArgument="SortByHotelReq" OnClick="uoLinkConfirmed_Click"></asp:LinkButton>
                                    </th>
                                    <th>
                                        <asp:LinkButton runat="server" ID="uoLinkConfirmedRecLoc" Text="Rec Loc" CommandArgument="SortByRecLoc"
                                            OnClick="uoLinkConfirmed_Click"></asp:LinkButton>
                                    </th>
                                    <th>
                                        <asp:LinkButton runat="server" ID="uoLinkConfirmedDepCity" Text="Dep City" CommandArgument="SortByDepCity"
                                            OnClick="uoLinkConfirmed_Click"></asp:LinkButton>
                                    </th>
                                    <th>
                                        <asp:LinkButton runat="server" ID="uoLinkConfirmedArrCity" Text="Arr City" CommandArgument="SortByArrCity"
                                            OnClick="uoLinkConfirmed_Click"></asp:LinkButton>
                                    </th>
                                    <th>
                                        <asp:LinkButton runat="server" ID="uoLinkConfirmedDpDate" Text="Dep Date" CommandArgument="SortByDepDate"
                                            OnClick="uoLinkConfirmed_Click"></asp:LinkButton>
                                    </th>
                                    <th>
                                        <asp:LinkButton runat="server" ID="uoLinkConfirmedArrDate" Text="Arr Date" CommandArgument="SortByArrDate"
                                            OnClick="uoLinkConfirmed_Click"></asp:LinkButton>
                                    </th>
                                    <th>
                                        <asp:LinkButton runat="server" ID="uoLinkConfirmedDepTime" Text="Dep Time" CommandArgument="SortByDepTime"
                                            OnClick="uoLinkConfirmed_Click"></asp:LinkButton>
                                    </th>
                                    <th>
                                        <asp:LinkButton runat="server" ID="uoLinkConfirmedArrTime" Text="Arr Time" CommandArgument="SortByArrTime"
                                            OnClick="uoLinkConfirmed_Click"></asp:LinkButton>
                                    </th>
                                    <th>
                                        <asp:LinkButton runat="server" ID="uoLinkConfirmedCarrier" Text="Carrier" CommandArgument="SortByCarrier"
                                            OnClick="uoLinkConfirmed_Click"></asp:LinkButton>
                                    </th>
                                    <th>
                                        <asp:LinkButton runat="server" ID="uoLinkConfirmedFlightNo" Text="FlightNo" CommandArgument="SortByFlightNo"
                                            OnClick="uoLinkConfirmed_Click"></asp:LinkButton>
                                    </th>
                                    <th>
                                        <asp:LinkButton runat="server" ID="uoLinkConfirmedVoucher" Text="Voucher" CommandArgument="SortByVoucher"
                                            OnClick="uoLinkConfirmed_Click"></asp:LinkButton>
                                    </th>
                                    <th>
                                        <asp:LinkButton runat="server" ID="uoLinkConfirmedPassportNo" Text="Passport No."
                                            CommandArgument="SortByPassportNo" OnClick="uoLinkConfirmed_Click"></asp:LinkButton>
                                    </th>
                                    <th>
                                        <asp:LinkButton runat="server" ID="uoLinkConfirmedIssuedDate" Text="Issued Date"
                                            CommandArgument="SortByIssuedDate" OnClick="uoLinkConfirmed_Click"></asp:LinkButton>
                                    </th>
                                    <th>
                                        <asp:LinkButton runat="server" ID="uoLinkConfirmedPassportExp" Text="Passport Expiration"
                                            CommandArgument="SortByPassportExp" OnClick="uoLinkConfirmed_Click"></asp:LinkButton>
                                    </th>
                                    <th>
                                        <asp:LinkButton runat="server" ID="LinkButton4" Text="Birthdate"
                                            CommandArgument="SortByBirthdDay" OnClick="uoLinkConfirmed_Click"></asp:LinkButton>
                                    </th>
                                     <th>
                                        <asp:LinkButton runat="server" ID="LinkButton1" Text="Meet & Greet"
                                            CommandArgument="SortByMeetGreet" OnClick="uoLinkConfirmed_Click"></asp:LinkButton>
                                    </th>
                                     <th>
                                        <asp:LinkButton runat="server" ID="LinkButton2" Text="Service Provider"
                                            CommandArgument="SortByPortAgent" OnClick="uoLinkConfirmed_Click"></asp:LinkButton>
                                    </th>
                                     <th>
                                        <asp:LinkButton runat="server" ID="LinkButton3" Text="Hotel Vendor"
                                            CommandArgument="SortByHotelVendor" OnClick="uoLinkConfirmed_Click"></asp:LinkButton>
                                    </th>
                                </tr>
                                <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                            </table>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <tr>
                                <%--<%# SetStatus()%>
                                        <td>
                                            <asp:Label ID="Label26" runat="server" Text="Tagged" Visible='<%# Convert.ToBoolean(Eval("IsTaggedByUser")) %>'></asp:Label>
                                            <asp:LinkButton Width="50px" ID="uoLnkBtnTag" CommandName="Tag" Visible='<%# !Convert.ToBoolean(Eval("IsTaggedByUser")) %>'
                                            OnClientClick='<%# "javascript:return confirmTag("+ Eval("EmployeeID") + ", \"" + Eval("FirstName") + "\");" %>'
                                            CommandArgument='<%# Eval("colIdBigInt") + ":" + Eval("TravelReqId") + ":" + Session["UserBranchId"].ToString() %>'
                                            runat="server" Text="Tag"></asp:LinkButton>
                                        </td>--%>
                                <td class="leftAligned">
                                    <%# Eval("HotelCity")%>&nbsp;
                                </td>
                                <td class="leftAligned">
                                    <%# String.Format("{0:dd-MMM-yyyy}", Eval("CheckIn"))%>
                                </td>
                                <td class="leftAligned">
                                    <%# String.Format("{0:dd-MMM-yyyy}", Eval("CheckOut"))%>
                                </td>
                                <td class="leftAligned">
                                    <%# Eval("HotelNites")%>
                                </td>
                                <td class="leftAligned">
                                    <%# Eval("ReasonCode")%>
                                </td>
                                <td class="leftAligned">
                                    <%# Eval("LastName")%>
                                </td>
                                <td class="leftAligned">
                                    <asp:HyperLink ID="uoHyperLinkSeafarer" NavigateUrl='<%# "/SuperUserView.aspx?sfId=" + Eval("EmployeeID") +  "&trID=" + Eval("TravelReqId") + "&st=" + Eval("SFStatus") + "&ufn=" + Request.QueryString["ufn"] + "&manualReqID=0&dt=" + Request.QueryString["dt"]%>'
                                        runat="server"><%# Eval("FirstName")%>
                                    </asp:HyperLink>
                                </td>
                                <td class="leftAligned">
                                    <%# Eval("EmployeeId")%>
                                </td>
                                <td class="leftAligned">
                                    <%# Eval("Gender")%>
                                </td>
                               <%-- <td class="leftAligned">
                                    <%# Eval("SingleDouble")%>
                                </td>--%>
                                <td class="leftAligned">
                                    <%# Eval("RoomType")%>
                                </td>
                                <td class="leftAligned">
                                    <%# Eval("Couple") %>
                                </td>
                                <td class="leftAligned">
                                    <%# Eval("Title") %>
                                </td>
                                <td class="leftAligned">
                                    <%# Eval("Ship")%>
                                </td>
                                <td class="leftAligned">
                                    <%# Eval("CostCenter")%>
                                </td>
                                <td class="leftAligned">
                                    <%# Eval("Nationality")%>
                                </td>
                                <td class="leftAligned">
                                    <%# Eval("HotelRequest")%>
                                </td>
                                <td class="leftAligned">
                                    <%# Eval("RecordLocator")%>
                                </td>
                                <td class="leftAligned">
                                    <%# Eval("DeptCity")%>
                                </td>
                                <td class="leftAligned">
                                    <%# Eval("ArvlCity")%>
                                </td>
                                <td class="leftAligned">
                                    <%# String.Format("{0:dd-MMM-yyyy}", Eval("DeptDate"))%>
                                </td>
                                <td class="leftAligned">
                                    <%# String.Format("{0:dd-MMM-yyyy}", Eval("ArvlDate"))%>
                                </td>
                                <td class="leftAligned">
                                    <%# String.Format("{0:hh:mm:ss}", Eval("DeptTime"))%>
                                </td>
                                <td class="leftAligned">
                                    <%# String.Format("{0:hh:mm:ss}", Eval("ArvlTime"))%>
                                </td>
                                <td class="leftAligned">
                                    <%# Eval("Carrier")%>
                                </td>
                                <td class="leftAligned">
                                    <%# Eval("FlightNo")%>
                                </td>
                                <td class="leftAligned">
                                    <%# String.Format("{0:0.00 }", Eval("Voucher"))%>
                                </td>
                                <td class="leftAligned">
                                    <%# Eval("PassportNo")%>
                                </td>
                                <td class="leftAligned">
                                    <%# Eval("IssuedDate")%>
                                </td>
                                <td class="leftAligned">
                                    <%# Eval("PassportExpiration")%>
                                </td>
                                <td class="leftAligned">
                                    <%# Eval("Birthday")%>
                                </td>
                                <td class="leftAligned">
                                    <%# Eval("IsMeetGreet")%>
                                </td>
                                <td class="leftAligned">
                                    <%# Eval("IsPortAgent")%>
                                </td>
                                <td class="leftAligned">
                                    <%# Eval("IsHotelVendor")%>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <EmptyDataTemplate>
                            <table border="0" cellspacing="0" cellpadding="0" class="listViewTable">
                                <tr>
                                    <th>
                                        Hotel City
                                    </th>
                                    <th>
                                        Check-In
                                    </th>
                                    <th>
                                        Check-Out
                                    </th>
                                    <th>
                                        Hotel Nites
                                    </th>
                                    <th>
                                        Reason Code
                                    </th>
                                    <th>
                                        Last Name
                                    </th>
                                    <th>
                                        First Name
                                    </th>
                                    <th>
                                        Employee ID
                                    </th>
                                    <th>
                                        Gender
                                    </th>
                                   <%-- <th>
                                        Single/Double
                                    </th>--%>
                                    <th>
                                        Room Type
                                    </th>
                                    <th>
                                        Couple
                                    </th>
                                    <th>
                                        Title
                                    </th>
                                    <th>
                                        Ship
                                    </th>
                                    <th>
                                        Cost Center
                                    </th>
                                    <th>
                                        Nationality
                                    </th>
                                    <th>
                                        Hotel Request
                                    </th>
                                    <th>
                                        Rec Loc
                                    </th>
                                    <th>
                                        Dep City
                                    </th>
                                    <th>
                                        Arr City
                                    </th>
                                    <th>
                                        Dep Date
                                    </th>
                                    <th>
                                        Arr Date
                                    </th>
                                    <th>
                                        Dep Time
                                    </th>
                                    <th>
                                        Arr Time
                                    </th>
                                    <th>
                                        Carrier
                                    </th>
                                    <th>
                                        FlightNo
                                    </th>
                                    <th>
                                        Voucher
                                    </th>
                                    <th>
                                        Passport No.
                                    </th>
                                    <th>
                                        Issued Date
                                    </th>
                                    <th>
                                        Passport Expiration
                                    </th>
                                </tr>
                                <tr>
                                    <td colspan="30">
                                    </td>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
                    </asp:ListView>
                    <table id="uoTableDashboardListDetailsPager" width="100%">
                        <tr>
                            <td class="LeftClass">
                                <asp:DataPager runat="server" ID="uoDashBoardListDetailsPager" PagedControlID="uoDashboardListDetails"
                                    PageSize="20">
                                    <Fields>
                                        <asp:NumericPagerField ButtonType="Link" NumericButtonCssClass="PagerClass" />
                                    </Fields>
                                </asp:DataPager>
                            </td>
                        </tr>
                    </table>
                    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" EnablePaging="True" MaximumRowsParameterName="MaximumRows"
                        SelectCountMethod="LoadHotelDashboardconfirmedTableCount" SelectMethod="LoadHotelDashboardconfirmedTable"
                        TypeName="TRAVELMART.BLL.DashboardBLL" OldValuesParameterFormatString="oldCount_{0}"
                        StartRowIndexParameterName="StartRowIndex">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="uoHiddenFieldStartDate" ConvertEmptyStringToNull="False"
                                DbType="DateTime" Name="StartDate" PropertyName="Value" />
                            <asp:ControlParameter ControlID="uoHiddenFieldBranchId" ConvertEmptyStringToNull="False"
                                DbType="Int32" Name="BranchId" PropertyName="Value" />
                            <asp:Parameter DefaultValue="1" Name="LoadType" />
                            <asp:ControlParameter ControlID="uoHiddenFieldUser" ConvertEmptyStringToNull="False"
                                DbType="String" Name="UserId" PropertyName="Value" />
                            <asp:ControlParameter ControlID="uoHiddenFieldFromDefaultView" DbType="Int16" Name="FromDefaultView" />
                            <asp:ControlParameter ControlID="uoHiddenFieldSortBy" DbType="String" Name="SortBy" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:Panel>
        <%--</div>--%>
        <asp:HiddenField runat="server" ID="uoHiddenFieldStartDate" />
        <asp:HiddenField runat="server" ID="uoHiddenFieldBranchId" />
        <asp:HiddenField runat="server" ID="uoHiddenFieldUser" />
        <asp:HiddenField runat="server" ID="uoHiddenFieldRole" />
        <asp:HiddenField runat="server" ID="uoHiddenFieldPopEditor" Value="0" />
        <asp:HiddenField runat="server" ID="uoHiddenFieldPopupHotel" Value="0" />
        <asp:HiddenField runat="server" ID="uoHiddenFieldLoadType" Value="0" />
        <asp:HiddenField runat="server" ID="uoHiddenFieldFromDefaultView" Value="0" />
        <asp:HiddenField runat="server" ID="uoHiddenFieldSortBy" Value="" />
    </div>
    <%--<asp:HiddenField runat="server" ID="uoHiddenFieldRoomTypeId" />--%>
    <%--<asp:HiddenField runat="server" ID="uoHiddenFieldReservedRoom" />--%>
</asp:Content>

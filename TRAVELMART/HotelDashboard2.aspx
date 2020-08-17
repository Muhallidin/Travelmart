<%@ Page Title="" Language="C#" MasterPageFile="~/TravelmartMaster.Master" AutoEventWireup="true"
    CodeBehind="HotelDashboard2.aspx.cs" Inherits="TRAVELMART.HotelDashboard2" %>

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

        function filterSettings() {
            var role = $("#<%=uoHiddenFieldRole.ClientID %>").val();
            $("#ctl00_ContentPlaceHolder1_uoHotelList_uoCheckBoxCheckAll").click(function(ev) {
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
        $(document).ready(function() {
            filterSettings();
        });

        function pageLoad(sender, args) {
            var isAsyncPostback = Sys.WebForms.PageRequestManager.getInstance().get_isInAsyncPostBack();
            if (isAsyncPostback) {

            }
            filterSettings();
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
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="PageTitle" style="width: 100%;">
        Hotel Dashboard &nbsp;
        <asp:Label runat="server" ID="uoLabelBranchName" CssClass="Title"></asp:Label>
        <asp:LinkButton runat="server" Text="*" CssClass="EventNotification" ToolTip="With Event(s)"
            ID="uoLinkButtonEvents"></asp:LinkButton>
    </div>
    <div style="width: 100%;" class="Module">
        <%--get first list--%>
        <asp:UpdatePanel runat="server" ID="uoUpdatePanelDetails" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:ListView runat="server" ID="uoHotelDashboardDetails">
                    <LayoutTemplate>
                        <table class="listViewTable" border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <th style="width: 100px">
                                    Date
                                </th>
                                <th style="width: 90px">
                                    Room
                                </th>
                                <th style="width: 90px">
                                    Expected
                                </th>
                                <th style="width: 90px">
                                    Arriving Pax
                                </th>
                                <th style="width: 90px">
                                    Check-In
                                </th>
                                <th style="width: 90px">
                                    Check-Out
                                </th>
                                <th style="width: 90px">
                                    Cancelled
                                </th>
                                <th style="width: 90px">
                                    No Show
                                </th>
                                <th style="width: 90px">
                                    Total Room Bookings
                                </th>
                                <th style="width: 90px">
                                    Total Room Blocks
                                </th>
                                <th style="width: 90px">
                                    Total Emergency Bookings
                                </th>
                                <th style="width: 90px">
                                    Total Emergency Room Blocks
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
                            <td class="leftAligned">
                                <%# Eval("RoomName")%>
                            </td>
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
                        </tr>
                    </ItemTemplate>
                </asp:ListView>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <br />
    <div class="PageSubTitle" style="text-decoration: underline;">
        Confirmed Bookings</div>
    <div class="Module">
        <asp:UpdatePanel runat="server" ID="uoPanelDetails">
            <ContentTemplate>
                <asp:ListView runat="server" ID="uoDashboardListDetails">
                    <LayoutTemplate>
                        <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                            <tr>
                                <th>
                                    Booking Type
                                </th>
                                <th>
                                    Rec Loc
                                </th>
                                <th>
                                    E1 ID
                                </th>
                                <th>
                                    Room
                                </th>
                                <th>
                                    Name
                                </th>
                                <th>
                                    Date From
                                </th>
                                <th>
                                    Date To
                                </th>
                                <th>
                                    Rank
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
                                    Duration
                                </th>
                                <th>
                                    Hotel City
                                </th>
                                <th>
                                    Airline
                                </th>
                                <th>
                                    From City
                                </th>
                                <th>
                                    To City
                                </th>
                            </tr>
                            <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                        </table>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <tr>
                            <%# SetStatus()%>
                            <td class="leftAligned">
                                <%# Eval("BookingType")%>
                            </td>
                            <td class="leftAligned">
                                <%# Eval("RecordLocator")%>
                            </td>
                            <td class="leftAligned">
                                <%# Eval("E1ID") %>
                            </td>
                            <td class="leftAligned">
                                <%# Eval("RoomType")%>
                            </td>
                            <td class="leftAligned">
                                <asp:LinkButton runat="server" ID="uoLinkSeafarer" PostBackUrl='<%# "~/SuperUserView.aspx?sfId=" + Eval("E1ID") +  "&trID=" + Eval("TravelReqId") + "&st=" + Eval("SFStatus") + "&ufn=" + Request.QueryString["ufn"] + "&manualReqID=" + Eval("ReqId") + "&dt=" + Request.QueryString["dt"]%>'
                                    Text='<%# Eval("Name") %>'></asp:LinkButton>
                            </td>
                            <td class="leftAligned">
                                <%# String.Format("{0:dd-MMM-yyyy}", Eval("CheckInDate"))%>
                            </td>
                            <td class="leftAligned">
                                <%# String.Format("{0:dd-MMM-yyyy}", Eval("CheckOutDate"))%>
                            </td>
                            <td class="leftAligned">
                                <%# Eval("RankName")%>
                            </td>
                            <td class="leftAligned">
                                <%# Eval("Gender") %>
                            </td>
                            <td class="leftAligned">
                                <%# Eval("Nationality") %>
                            </td>
                            <td class="leftAligned">
                                <%# Eval("CostCenter") %>
                            </td>
                            <td class="leftAligned">
                                <%# Eval("HotelNites")%>
                            </td>
                            <td class="leftAligned">
                                <%# Eval("HotelCity")%>
                            </td>
                            <td class="leftAligned">
                                <%# Eval("Airline")%>
                            </td>
                            <td class="leftAligned">
                                <%# Eval("FromCity")%>
                            </td>
                            <td class="leftAligned">
                                <%# Eval("ToCity")%>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <EmptyDataTemplate>
                        <table border="0" cellspacing="0" cellpadding="0" class="listViewTable">
                            <tr>
                                <th>
                                    Rec Loc
                                </th>
                                <th>
                                    E1 ID
                                </th>
                                <th>
                                    Room
                                </th>
                                <th>
                                    Name
                                </th>
                                <th>
                                    Date From
                                </th>
                                <th>
                                    Date To
                                </th>
                                <th>
                                    Rank
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
                                    Duration
                                </th>
                                <th>
                                    Hotel City
                                </th>
                                <th>
                                    Airline
                                </th>
                                <th>
                                    From City
                                </th>
                                <th>
                                    To City
                                </th>
                            </tr>
                            <tr>
                                <td colspan="15">
                                    No Record
                                </td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                </asp:ListView>
                <asp:DataPager runat="server" ID="uoDashBoardListDetailsPager" PagedControlID="uoDashboardListDetails"
                    PageSize="20">
                    <Fields>
                        <asp:NumericPagerField ButtonType="Link" NumericButtonCssClass="PagerClass" />
                    </Fields>
                </asp:DataPager>
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
                    </SelectParameters>
                </asp:ObjectDataSource>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <br />
    <div runat="server" id="PendingDiv" class="PageSubTitle" style="text-decoration: underline;">
        Overflow Bookings
    </div>
    <div class="RightClass">
        <asp:Button runat="server" ID="uoButtonApprove" Text="Approve" CssClass="SmallButton"
            OnClick="uoButtonApprove_Click" />
    </div>
    <asp:UpdatePanel runat="server" ID="uoPanelPending">
        <ContentTemplate>
            <asp:ListView runat="server" ID="uoHotelList">
                <LayoutTemplate>
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
                            <th>
                                <asp:CheckBox runat="server" ID="uoCheckBoxCheckAll" />
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
                            <%# Eval("Name")%>
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
                            <asp:Label runat="server" ID="uoLblVoucher" Text='<%# String.Format("{0:0.00}",Eval("Voucher"))%>'></asp:Label>
                        </td>
                        <td class="leftAligned">
                            <%# Eval("ReasonCode")%>
                        </td>
                        <td class="leftAligned">
                            <asp:Label runat="server" ID="uoLblStripe" Text='<%# Eval("Stripe") %>'></asp:Label>
                        </td>
                        <td>
                            <asp:CheckBox CssClass="Checkbox" ID="uoSelectCheckBox" runat="server" Enabled='<%# Convert.ToBoolean(Eval("EnabledBit"))%>' />
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
                            <td colspan="12" class="leftAligned">
                                No Pending Hotel Bookings.
                            </td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
            </asp:ListView>
            <asp:DataPager runat="server" ID="uoHotelListPager" PagedControlID="uoHotelList"
                PageSize="20">
                <Fields>
                    <asp:NumericPagerField ButtonType="Link" NumericButtonCssClass="PagerClass" />
                </Fields>
            </asp:DataPager>
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
                    <asp:ControlParameter ControlID="uoHiddenFieldSortBy" ConvertEmptyStringToNull="False"
                        DbType="String" Name="SortBy" PropertyName="Value" />
                </SelectParameters>
            </asp:ObjectDataSource>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:HiddenField runat="server" ID="uoHiddenFieldStartDate" />
    <asp:HiddenField runat="server" ID="uoHiddenFieldBranchId" />
    <asp:HiddenField runat="server" ID="uoHiddenFieldUser" />
    <asp:HiddenField runat="server" ID="uoHiddenFieldRole" />
    <asp:HiddenField runat="server" ID="uoHiddenFieldPopEditor" Value="0" />
    <asp:HiddenField runat="server" ID="uoHiddenFieldSortBy" />
</asp:Content>

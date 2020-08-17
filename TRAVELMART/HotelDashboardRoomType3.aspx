<%@ Page Title="" Language="C#" MasterPageFile="~/TravelMartMaster2.Master" AutoEventWireup="true"
    CodeBehind="HotelDashboardRoomType3.aspx.cs" Inherits="TRAVELMART.HotelDashboardRoomType3" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%--<%@ OutputCache Duration="60" VaryByParam="none" VaryByControl="uoCalendarDashboard"%>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style1
        {
            text-align: left;
            vertical-align: middle;
            width: 43px;
        }
        .style2
        {
            text-align: left;
            vertical-align: middle;
            white-space: nowrap;
            width: 100px;
        }
    </style>
</asp:Content>
<asp:Content ContentPlaceHolderID="HeaderContent" ID="ContentHeader" runat="server">
    <div class="PageTitle">
        <asp:Panel ID="uopanelroomhead" runat="server">
            Hotel Dashboard &nbsp;</asp:Panel>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NaviPlaceHolder" runat="server">
    <%--ContentPlaceHolder1--%>

    <script language="javascript" type="text/javascript">


        function OpenContract(vendorID, ContractID) {
            var URLString = "../ContractManagement/HotelContractView.aspx?vId=" + vendorID + "&cId=" + ContractID;

            var screenWidth = screen.availwidth;
            var screenHeight = screen.availheight;

            screenWidth = 800;
            screenHeight = 650;

            window.open(URLString, 'Contract_Management', 'top=10, left=200, width=' + screenWidth + ', height=' + screenHeight + ', scrollbars=yes, menubar=no,toolbar=no,status=no,resizable=yes,addressbar=no');
            return false;
        }

        $(document).ready(function() {
            SetTRResolution();
            filterSettings();
            ShowPopup();

        });

        function pageLoad(sender, args) {
            var isAsyncPostback = Sys.WebForms.PageRequestManager.getInstance().get_isInAsyncPostBack();
            if (isAsyncPostback) {
                SetTRResolution();

                filterSettings();
                ShowPopup();

            }
        }

        function SetTRResolution() {
            
            var ht = $(window).height();

            var wd = $(window).width() * 0.90;

            if (screen.height <= 600) {
                ht = ht * 0.43;
            }
            else if (screen.height <= 720) {
                ht = ht * 0.56;
            }
            else {
                ht = ht * 0.60;
            }

            $("#Bv").height(ht);         
            
            $("#Bv").width(wd);
           
           
        }

        function filterSettings() {

            if ($("#<%=uoCheckBoxAdvanceSearch.ClientID %>").attr('checked')) {
                $("#<%=uoTableAdvanceSearch.ClientID %>").show();
            }
            else {
                $("#<%=uoTableAdvanceSearch.ClientID %>").hide();
            }

            $("#<%=uoCheckBoxAdvanceSearch.ClientID %>").click(function() {
                if ($(this).attr('checked')) {
                    $("#<%=uoTableAdvanceSearch.ClientID %>").fadeIn();
                }
                else {
                    $("#<%=uoTableAdvanceSearch.ClientID %>").fadeOut();
                    $("#<%=uoDropDownListRegion.ClientID %>").val('0');
                    $("#<%=uoDropDownListCountry.ClientID %>").val('0');
                    $("#<%=uoTextBoxFilterCity.ClientID %>").val('');
                    $("#<%=uoDropDownListCity.ClientID %>").val('0');
                    $("#<%=uoDropDownListPort.ClientID %>").val('0');
                    $("#<%=uoTextBoxSearch.ClientID %>").val('');
                }
            });
        }

        function ShowPopup() {
            var role = $("#<%=uoHiddenFieldRole.ClientID %>").val();
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

    <div id="Bv" style="overflow: auto; width: 100%; overflow-x: auto; overflow-y: auto;">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table width="100%" class="LeftClass" onload="opener.location.reload();">
                    <tr>
                        <td class="style1">
                            <asp:CheckBox ID="uoCheckBoxAdvanceSearch" Text="View Filter" runat="server" />
                        </td>
                    </tr>
                </table>
                <table width="100%" class="LeftClass" id="uoTableAdvanceSearch" runat="server">
                    <tr id="uoTRVessel" runat="server">
                        <td class="contentCaption">
                            Region:
                        </td>
                        <td class="contentValue">
                            <asp:DropDownList ID="uoDropDownListRegion" runat="server" Width="300px" AppendDataBoundItems="True"
                                AutoPostBack="true" OnSelectedIndexChanged="uoDropDownListRegion_SelectedIndexChanged">
                                <%--<asp:ListItem>--SELECT REGION--</asp:ListItem>--%>
                            </asp:DropDownList>
                            <cc1:ListSearchExtender ID="ListSearchExtender_uoDropDownListRegion" runat="server"
                                TargetControlID="uoDropDownListRegion" PromptText="Type to search" PromptPosition="Top"
                                IsSorted="true" PromptCssClass="dropdownSearch" />
                        </td>
                        <td class="contentCaption">
                            Port:
                        </td>
                        <td class="contentValue">
                            <asp:DropDownList ID="uoDropDownListPort" runat="server" Width="300px" AppendDataBoundItems="True">
                                <%--<asp:ListItem>--SELECT PORT--</asp:ListItem>--%>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="contentCaption">
                            Country:
                        </td>
                        <td class="contentValue">
                            <asp:DropDownList ID="uoDropDownListCountry" runat="server" Width="300px" AppendDataBoundItems="True"
                                AutoPostBack="true" OnSelectedIndexChanged="uoDropDownListCountry_SelectedIndexChanged">
                                <%--<asp:ListItem>--SELECT COUNTRY--</asp:ListItem>--%>
                            </asp:DropDownList>
                            <cc1:ListSearchExtender ID="ListSearchExtender_uoDropDownListCountry" runat="server"
                                TargetControlID="uoDropDownListCountry" PromptText="Type to search" PromptPosition="Top"
                                IsSorted="true" PromptCssClass="dropdownSearch" />
                        </td>
                        <td class="contentCaption">
                            Hotel:
                        </td>
                        <td class="contentValue">
                            <asp:TextBox ID="uoTextBoxSearch" runat="server" CssClass="TextBoxInput" Width="250px" />
                        </td>
                    </tr>
                    <tr>
                        <td class="contentCaption">
                            City Filter:
                        </td>
                        <td class="contentValue">
                            <asp:TextBox ID="uoTextBoxFilterCity" runat="server" Width="220px" CssClass="TextBoxInput"></asp:TextBox>
                            <asp:Button ID="uoButtonViewCity" runat="server" Text="Filter City" CssClass="SmallButton"
                                OnClick="uoButtonViewCity_Click" />
                        </td>
                        <td class="contentCaption">
                            &nbsp;
                        </td>
                        <td class="contentValue">
                            <asp:Button ID="uoButtonView" runat="server" CssClass="SmallButton" OnClick="uoButtonView_Click"
                                Text="View" />
                            <asp:Button ID="uoButtonClear" runat="server" CssClass="SmallButton" OnClick="uoButtonClear_Click"
                                Text="Clear" />
                        </td>
                    </tr>
                    <tr>
                        <td class="contentCaption">
                            City:
                        </td>
                        <td class="contentValue">
                            <asp:DropDownList ID="uoDropDownListCity" runat="server" Width="300px" AppendDataBoundItems="True">
                                <%--<asp:ListItem>--SELECT CITY--</asp:ListItem>--%>
                            </asp:DropDownList>
                            <cc1:ListSearchExtender ID="ListSearchExtender_uoDropDownListCity" runat="server"
                                TargetControlID="uoDropDownListCity" PromptText="Type to search" PromptPosition="Top"
                                IsSorted="true" PromptCssClass="dropdownSearch" />
                        </td>
                    </tr>
                </table>
                <%--<table width="100%" class="LeftClass">
                    <tr runat="server" id="TRSearch">
                        <td class="RightClass">
                            <asp:HyperLink ID="uoHyperLinkException" class="RedBgNotification" runat="server"
                                NavigateUrl="#">Exception Bookings</asp:HyperLink>
                            <asp:HyperLink ID="uoHyperLinkNoHotelRequest" class="RedBgNotification" runat="server"
                                NavigateUrl="#">No Travel Request</asp:HyperLink>
                        </td>
                    </tr>
                </table>--%>
                <asp:Panel ID="Panel1" runat="server" CssClass="PageSubTitle">
                    Exceptions / No Travel Request / Arrival Departure Same Date
                    <asp:Image ID="uoImageExceptionNoTravelRequest" runat="server" ImageUrl="~/Images/box_up.png"
                        Height="10px" />
                </asp:Panel>
                <asp:Panel ID="uoPanelExceptionNoTravelRequest" runat="server" ScrollBars="Auto">
                    <cc1:CollapsiblePanelExtender ID="CollapsiblePanelExtender_ExceptionNoTravelRequest"
                        runat="Server" TargetControlID="uoPanelExceptionNoTravelRequest" CollapsedSize="0"
                        ExpandControlID="uoImageExceptionNoTravelRequest" CollapseControlID="uoImageExceptionNoTravelRequest"
                        AutoCollapse="False" AutoExpand="False" ImageControlID="uoImageExceptionNoTravelRequest"
                        ExpandedImage="~/images/box_up.png" CollapsedImage="~/images/box_down.png" ExpandDirection="Vertical"
                        Collapsed="false" />
                    <asp:UpdatePanel runat="server" ID="uoUpdatePanelExceptionNoTravelRequest">
                        <ContentTemplate>
                            <asp:ListView runat="server" ID="uoListViewExceptionNoTravelRequest" OnItemCommand="uoListViewExceptionNoTravelRequest_ItemCommand"
                                OnItemDataBound="uoListViewExceptionNoTravelRequest_ItemDataBound">
                                <LayoutTemplate>
                                    <table border="0" cellpadding="0" cellspacing="0" class="listViewTable" width="100%">
                                        <tr>
                                            <th>
                                                Date
                                            </th>
                                            <th>
                                                Exception Count
                                            </th>
                                            <th>
                                                No Travel Request Count
                                            </th>
                                            <th>
                                                Arrival/Departure Same Date
                                            </th>
                                        </tr>
                                        <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                                    </table>
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <%-- <%# ExceptionNoTravelRequestAddGroup()%>--%>
                                    <%--<tr>--%>
                                    <%--<%# DashboardChangeRowColor()%>
                    <%# DashboardAddDateRow()%>--%>
                                    <td class="leftAligned">
                                        <%# String.Format("{0:dd-MMM-yyyy}", Eval("colDate"))%>
                                    </td>
                                    <td class="">
                                        <asp:LinkButton runat="server" ID="uoExceptionLink" Text='<%# Eval("ExceptionCount")%>'
                                            CommandArgument='<%# Eval("colDate")%>' CommandName="Exception"></asp:LinkButton>
                                    </td>
                                    <td class="">
                                        <asp:LinkButton runat="server" ID="LinkButton1" Text='<%# Eval("NoTravelCount")%>'
                                            CommandArgument='<%# Eval("colDate")%>' CommandName="NoTR"></asp:LinkButton>
                                    </td>
                                    <td class="">
                                        <asp:LinkButton runat="server" ID="LinkButton2" Text='<%# Eval("ArrDeptSameOnOffDateCount")%>'
                                            CommandArgument='<%# Eval("colDate")%>' CommandName="ArrDep"></asp:LinkButton>
                                    </td>
                                    </tr>
                                </ItemTemplate>
                                <EmptyDataTemplate>
                                    <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                                        <tr>
                                            <th>
                                                Date
                                            </th>
                                            <th>
                                                Exception Count
                                            </th>
                                            <th>
                                                No Travel Request Count
                                            </th>
                                            <th>
                                                Arrival/Departure Same Date
                                            </th>
                                        </tr>
                                        <tr>
                                            <td colspan="4" class="leftAligned">
                                                No Record
                                            </td>
                                        </tr>
                                    </table>
                                </EmptyDataTemplate>
                            </asp:ListView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:Panel>
                <br />
                <div class="PageSubTitle">
                    Room Types
                </div>
                <asp:ListView runat="server" ID="uoListViewDashboard" OnItemCommand="uoListViewDashboard_ItemCommand"
                    OnItemDataBound="uoListViewDashboard_ItemDataBound">
                    <LayoutTemplate>
                        <table border="0" cellpadding="0" cellspacing="0" class="listViewTable" width="100%">
                            <tr>
                                <th style="text-align: center;">
                                    <label style="width: 80px;">
                                        Date</label>
                                </th>
                                <th style="text-align: center;">
                                    <label style="width: 80px;">
                                        Room Type</label>
                                </th>
                                <th style="text-align: center;">
                                    <label style="width: 60px;">
                                        Total Pax Reserved</label>
                                </th>
                                <th style="text-align: center;">
                                    <label style="width: 60px;">
                                        Total Available Room(s)</label>
                                </th>
                                <th style="text-align: center;">
                                    <label style="width: 60px;">
                                        Overflow Crew</label>
                                </th>
                            </tr>
                            <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                        </table>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <%# DashboardAddGroup()%>
                        <%# DashboardChangeRowColor()%>
                        <td class="leftAligned">
                            <asp:LinkButton runat="server" ID="uoLinkBtnDate" CommandName="Select" Text='<%# String.Format("{0: dd-MMM-yyyy}, {1}", Eval("colDate"), Eval("colDateName")) %>'
                                Visible='<%# DashboardAddDateRow()%>' CommandArgument='<%# String.Format("{0}|{1}|{2}|{3}", Eval("colDate"), Eval("BranchID"), Eval("BrandId"), Eval("HotelBranchName")) %>'></asp:LinkButton>
                        </td>
                        <td class="leftAligned">
                            <%# Eval("RoomType")%>
                        </td>
                        <td class="rightAligned">
                            <%# Eval("ReservedCrew")%>
                        </td>
                        <td class="rightAligned">
                            <span style="font-weight: bold">
                                <%# String.Format("{0:0.#}", Eval("AvailableRoomBlocks"))%></span>
                        </td>
                        <td class="rightAligned">
                            <%# Eval("OverflowCrew")%>
                        </td>
                        </tr>
                    </ItemTemplate>
                    <EmptyDataTemplate>
                        <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                            <tr>
                                <th style="text-align: center;">
                                    <label style="width: 80px;">
                                        Date</label>
                                </th>
                                <th style="text-align: center;">
                                    <label style="width: 80px;">
                                        Room Type</label>
                                </th>
                                <th style="text-align: center;">
                                    <label style="width: 60px;">
                                        Total Pax Reserved</label>
                                </th>
                                <th style="text-align: center;">
                                    <label style="width: 60px;">
                                        Total Available Room(s)</label>
                                </th>
                                <th style="text-align: center;">
                                    <label style="width: 60px;">
                                        Overflow Crew</label>
                                </th>
                            </tr>
                            <tr>
                                <td colspan="6" class="leftAligned">
                                    No Record
                                </td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                </asp:ListView>
                <asp:ObjectDataSource ID="uoObjectDataSourceDashboard" runat="server" OnSelecting="uoObjectDataSourceDashboard_Selecting"
                    MaximumRowsParameterName="MaxRow" SelectCountMethod="LoadHotelDashboardListCount"
                    SelectMethod="LoadHotelDashboardList" StartRowIndexParameterName="StartRow" TypeName="TRAVELMART.BLL.HotelDashboardBLL"
                    OldValuesParameterFormatString="oldcount_{0}" EnablePaging="True">
                    <SelectParameters>
                        <asp:Parameter Name="iRegionID" Type="Int32" />
                        <asp:Parameter Name="iCountryID" Type="Int32" />
                        <asp:Parameter Name="iCityID" Type="Int64" />
                        <asp:Parameter Name="iPortID" Type="Int32" />
                        <asp:Parameter Name="sUserName" Type="String" />
                        <asp:Parameter Name="sRole" Type="String" />
                        <asp:Parameter Name="iBranchID" Type="Int64" />
                        <asp:Parameter Name="dFrom" Type="DateTime" />
                        <asp:Parameter Name="dTo" Type="DateTime" />
                        <asp:Parameter Name="sBranchName" Type="String" />
                        <asp:Parameter Name="iLoadType" Type="Int16" />
                        <asp:Parameter Name="FromDefaultView" Type="Int16" DefaultValue="1" />
                    </SelectParameters>
                </asp:ObjectDataSource>
                <asp:HiddenField ID="uoHiddenFieldPopupHotel" runat="server" Value="0" />
                <asp:HiddenField ID="uoHiddenFieldRole" runat="server" />
                <asp:HiddenField ID="uoHiddenFieldUser" runat="server" />
                <asp:HiddenField ID="uoHiddenFieldDateRange" runat="server" />
                <asp:HiddenField ID="uoHiddenFieldDate" runat="server" />
                <asp:HiddenField ID="uoHiddenFieldLoadType" runat="server" Value="0" />
                <asp:HiddenField ID="uoHiddenFieldBranchID" runat="server" Value="0" />
                <asp:HiddenField ID="uoHiddenFieldFromDefaultView" runat="server" Value="1" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>

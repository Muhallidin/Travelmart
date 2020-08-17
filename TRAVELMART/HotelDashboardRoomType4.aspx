<%@ Page Title="" Language="C#" MasterPageFile="~/TravelmartMaster2.Master" AutoEventWireup="true"
    CodeBehind="HotelDashboardRoomType4.aspx.cs" Inherits="TRAVELMART.HotelDashboardRoomType4" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%--<%@ OutputCache Duration="60" VaryByParam="none" VaryByControl="uoCalendarDashboard"%>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .styleA
        {
            text-align: left;
            vertical-align: middle;
            white-space: nowrap;
            width: 43px;
        }
        .styleB
        {
            text-align: left;
            vertical-align: middle;
            white-space: nowrap;
            width: 272px;
        }
        .style2
        {
            height: 17px;
        }
    </style>
</asp:Content>
<asp:Content ContentPlaceHolderID="HeaderContent" ID="ContentHeader" runat="server">
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr class="PageTitle">
            <td align="left">
                Hotel Dashboard
            </td>
            <td align="right">
                Region: &nbsp;&nbsp
                <asp:DropDownList ID="uoDropDownListRegion" runat="server" Width="200px" AutoPostBack="true"
                    OnSelectedIndexChanged="uoDropDownListRegion_SelectedIndexChanged">
                </asp:DropDownList>
                &nbsp;&nbsp;Port: &nbsp;&nbsp;
                <asp:DropDownList ID="uoDropDownListPortPerRegion" runat="server" Width="200px" AutoPostBack="true"
                    AppendDataBoundItems="true" OnSelectedIndexChanged ="uoDropDownListPortPerRegion_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NaviPlaceHolder" runat="server">
    <%--ContentPlaceHolder1--%>

    <script language="javascript" type="text/javascript">
        $(document).ready(function() {
            SetTRResolution();
            ShowPopup();
            SetOverflowVisibility();
        });

        function pageLoad(sender, args) {
            var isAsyncPostback = Sys.WebForms.PageRequestManager.getInstance().get_isInAsyncPostBack();
            if (isAsyncPostback) {
                SetTRResolution();
                ShowPopup();
                ShowSearchPopup();
                SetOverflowVisibility();
            }
        }
        function SetTRResolution() {
            var ht = $(window).height(); //550;
            var wd = $(window).width() * 0.90;
            var screenH = screen.height;
            var percent = 0.55;
            
            if (screenH <= 600) {
                //alert('less 600');
                ht = ht * 0.35;
            }
            else if (screenH <= 720) {
                //alert('less 720')
                ht = ht * 0.49;
            }
            else {
                if (screenH = 768) {
                    percent = (540 - (screenH - ht)) / ht;
                }
                ht = ht * percent;
            }
            $("#Bv").height(ht);
            $("#Bv").width(wd);
        }

        function ShowPopup() {
            $(".HotelLink").fancybox(
            {
                'centerOnScroll': false,
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

            $(".clsOverride").fancybox(
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
            $(".clsEmergency").fancybox(
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

        function OpenContract(vendorID, ContractID) {
            var URLString = "../ContractManagement/HotelContractView.aspx?vId=" + vendorID + "&cId=" + ContractID;

            var screenWidth = screen.availwidth;
            var screenHeight = screen.availheight;

            screenWidth = 800;
            screenHeight = 650;

            window.open(URLString, 'Contract_Management', 'top=10, left=200, width=' + screenWidth + ', height=' + screenHeight + ', scrollbars=yes, menubar=no,toolbar=no,status=no,resizable=yes,addressbar=no');
            return false;
        }


        function SetOverflowVisibility() {
            var t = $("#<%= uoHiddenFieldRole.ClientID %>").val();
            if (t == "Crew Assist") {
                $('#notCrewAssit').hide();
            }            
           
        }
    </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table align="center" width="90%">
                <tr>
                    <td style="width: auto;" align="center">
                    <span id="notCrewAssit">
                        <asp:HyperLink ID="uoHyperLinkOverflow" CssClass="RedBgNotification" runat="server"
                            NavigateUrl="#">
                        </asp:HyperLink>
                        |
                       
                        <asp:HyperLink ID="uoHyperLinkException" CssClass="RedBgNotification" runat="server"
                            NavigateUrl="#">
                        </asp:HyperLink>
                        |
                         </span>
                        <asp:HyperLink ID="uoHyperLinkNoHotelRequest" CssClass="RedBgNotification" runat="server"
                            NavigateUrl="#">
                        </asp:HyperLink>
                        |
                        <asp:HyperLink ID="uoHyperLinkArrDepSameDate" CssClass="RedBgNotification" runat="server"
                            NavigateUrl="#">
                        </asp:HyperLink>
                        |
                         <asp:HyperLink ID="uoHyperLinkNoHotelContract" CssClass="RedBgNotification" runat="server"
                            NavigateUrl="#">
                        </asp:HyperLink>
                        |
                         <asp:HyperLink ID="uoHyperLinkRestrictedNationality" CssClass="RedBgNotification" runat="server"
                            NavigateUrl="#">
                        </asp:HyperLink>
                    </td>
                </tr>
                <tr runat="server" id="TRSearch">
                    <td visible="false">
                        <label>
                            Hotel:</label>&nbsp;&nbsp;
                        <asp:TextBox ID="uoTextBoxSearch" runat="server" CssClass="TextBoxInput" Width="250px" />
                        &nbsp;&nbsp;
                        <asp:Button ID="uoButtonView" runat="server" CssClass="SmallButton" OnClick="uoButtonView_Click"
                            Text="View" />
                    </td>
                </tr>
            </table>
            <div id="Bv" style="overflow: auto; width: 100%; overflow-x: auto; overflow-y: auto;">
                <table cellspacing="0" border="0" align="center" style="vertical-align:top">
                    <tr align="center">
                        <td>
                            <asp:ListView runat="server" ID="uoListViewDashboard" OnItemCommand="uoListViewDashboard_ItemCommand"
                                OnItemDataBound="uoListViewDashboard_ItemDataBound" >
                                <LayoutTemplate>
                                    <table border="0" cellpadding="0" cellspacing="0" class="listViewTable3" width="100%">
                                        <tr>
                                        </tr>
                                        <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                                    </table>
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td colspan="5">
                                            <br />
                                        </td>
                                    </tr>
                                    <%# DashboardAddGroup()%>
                                    <%--<%# DashboardChangeRowColor()%>--%>
                                    <%--<%# DashboardAddDateRow()%>--%>
                                    <tr>
                                        <td class="tdLabelSingle" >
                                            <li class="bulleted">
                                                <asp:Label ID="uoLabelTotalSingleRoomBlock" runat="server" Text="Total Room Reserved (Single):"></asp:Label></li>
                                        </td>
                                        <td class="tdResultSingle">
                                            <%--tdAligned--%>
                                            <span style="font-weight: bold">
                                                <%# Eval("TotalSingleRoomBlock")%></span>
                                        </td>
                                        <%--leftAligned--%>
                                        <td class="tdLabelSingle2">
                                            <li class="bulleted">
                                                <asp:Label ID="uoLabelTotalSingleAvailableRoom" runat="server" Text="Total Available Room(s) (Single):"></asp:Label></li>
                                        </td>
                                        <td class="tdResultSingle2">
                                            <span style="font-weight: bold">
                                                <%# Eval("TotalSingleAvailableRoom")%></span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdLabelDouble">
                                            <li class="bulleted">
                                                <asp:Label ID="uoLabelTotalDoubleRoomBlock" runat="server" Text="Total Room Reserved (Double):"></asp:Label></li>
                                        </td>
                                        <td class="tdResultDouble">
                                            <%--tdAligned--%>
                                            <span style="font-weight: bold">
                                                <%# Eval("TotalDoubleRoomBlock")%></span>
                                        </td>
                                        <td class="tdLabelDouble2">
                                            <li class="bulleted">
                                                <asp:Label ID="uoLabelTotalDoubleAvailableRoom" runat="server" Text="Total Available Room(s) (Double):"></asp:Label></li>
                                        </td>
                                        <td class="tdResultDouble2">
                                            <span style="font-weight: bold">
                                                <%# String.Format("{0:0.#}", Eval("TotalDoubleAvailableRoom"))%></span>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:DataPager ID="uolistviewHotelInfoPager" runat="server" PagedControlID="uoListViewDashboard"
                                PageSize="50" Visible = "false">
                                <Fields>
                                    <asp:NumericPagerField ButtonType="link" NumericButtonCssClass="PagerClass" />
                                </Fields>
                            </asp:DataPager>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <asp:ObjectDataSource ID="uoObjectDataSourceDashboard" runat="server" OnSelecting="uoObjectDataSourceDashboard_Selecting"
                                MaximumRowsParameterName="MaxRow" SelectCountMethod="LoadHotelDashboardListCount3"
                                SelectMethod="LoadHotelDashboardList3" StartRowIndexParameterName="StartRow"
                                TypeName="TRAVELMART.BLL.HotelDashboardBLL" OldValuesParameterFormatString="oldcount_{0}"
                                EnablePaging="True">
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
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

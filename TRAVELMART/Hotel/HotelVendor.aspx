<%@ Page Language="C#" MasterPageFile="~/TravelMartMaster2.Master" AutoEventWireup="true"
    CodeBehind="HotelVendor.aspx.cs" Inherits="TRAVELMART.Hotel.HotelVendor" Title="Hotel Vendor" %>

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
    <div>
        <%-- <img  visible=false/>--%>
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr class="PageTitle">
                <td align="left">
                    Hotel Branch Dashboard
                </td>
                <%-- <td align="right">
                    Region: &nbsp;&nbsp
                    <asp:DropDownList ID="uoDropDownListRegion" runat="server" Width="200px" AutoPostBack="true"
                        OnSelectedIndexChanged="uoDropDownListRegion_SelectedIndexChanged">
                    </asp:DropDownList>
                    &nbsp;&nbsp;Port: &nbsp;&nbsp;
                    <asp:DropDownList ID="uoDropDownListPortPerRegion" runat="server" Width="200px" AutoPostBack="true"
                        AppendDataBoundItems="true" OnSelectedIndexChanged="uoDropDownListPortPerRegion_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>--%>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NaviPlaceHolder" runat="server">
    <%--ContentPlaceHolder1--%>

    <script type="text/javascript">
        $(document).ready(function() {
            SetTRResolution();
            ShowPopup();
            SetClientTime();
        });

        function pageLoad(sender, args) {
            var isAsyncPostback = Sys.WebForms.PageRequestManager.getInstance().get_isInAsyncPostBack();
            if (isAsyncPostback) {
                SetTRResolution();
                ShowPopup();
                SetClientTime();
            }
        }

        function SetTRResolution() {
            var ht = $(window).height(); //550;
            var wd = $(window).width() * 0.90;
            if (screen.height <= 600) {
                ht = ht * 0.35;
            }
            else if (screen.height <= 720) {
                ht = ht * 0.49;
            }
            else {
                ht = ht * 0.65;
            }
            $("#Bv").height(ht);
            $("#Bv").width(wd);
        }

        function SetClientTime() {
            $(".BtnTag").click(function() {
                var d = new Date();             
                //$("#<%=uoHiddenFieldTagTime.ClientID %>").val(d.format("yyyy-mm-dd hh:mm:ss"));
                $("#<%=uoHiddenFieldTagTime.ClientID %>").val(d.getMonth() + 1 + "/" + d.getDate() + "/" + d.getFullYear() + " " + d.getHours() + ":" + d.getMinutes() + ":" + d.getSeconds());
             });
        }


        function ShowPopup() {

            $(".Confirmation").fancybox(
                {
                    'width': '30%',
                    'height': '22%',
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

        function OpenContract(vendorID, ContractID) {
            var URLString = "../ContractManagement/HotelContractView.aspx?vId=" + vendorID + "&cId=" + ContractID;

            var screenWidth = screen.availwidth;
            var screenHeight = screen.availheight;

            screenWidth = 800;
            screenHeight = 650;

            window.open(URLString, 'Contract_Management', 'top=10, left=200, width=' + screenWidth + ', height=' + screenHeight + ', scrollbars=yes, menubar=no,toolbar=no,status=no,resizable=yes,addressbar=no');
            return false;
        }



        function confirmTag(e1ID, seafarerName) {
            var sMsg = "Tag " + e1ID + ": " + seafarerName + "? ";
            var x = confirm(sMsg);
            return x;
        }
           
    </script>

    <div id="br1" style="overflow: auto; overflow-x: auto; overflow-y: auto; display: none;">
        <div class="Module">
        </div>
    </div>
    <div id="br" style="overflow: auto; overflow-x: auto; overflow-y: auto;">
        <div class="Module">
            <asp:Panel runat="server" ID="uoPanel">
                <asp:UpdatePanel ID="uoUpdatePanelDetails" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div style="display: none;">
                            <%--<table align="center" width="90%">
                                <tr>
                                    <td style="width: auto;" align="center">
                                        <asp:HyperLink ID="uoHyperLinkOverflow" CssClass="RedBgNotification" runat="server"
                                            NavigateUrl="#">
                                        </asp:HyperLink>
                                        |
                                        <asp:HyperLink ID="uoHyperLinkException" CssClass="RedBgNotification" runat="server"
                                            NavigateUrl="#">
                                        </asp:HyperLink>
                                        |
                                        <asp:HyperLink ID="uoHyperLinkNoHotelRequest" CssClass="RedBgNotification" runat="server"
                                            NavigateUrl="#">
                                        </asp:HyperLink>
                                        |
                                        <asp:HyperLink ID="uoHyperLinkArrDepSameDate" CssClass="RedBgNotification" runat="server"
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
                            </table>--%>
                        </div>
                        <div id="Bv" style="overflow: auto; width: 100%; overflow-x: auto; overflow-y: auto;">
                            <table cellspacing="0" border="0" align="center" style="vertical-align: top">
                                <tr align="center">
                                    <td>
                                        <asp:ListView runat="server" ID="uoListViewDashboard" OnItemCommand="uoListViewDashboard_ItemCommand"
                                            OnItemDataBound="uoListViewDashboard_ItemDataBound">
                                            <LayoutTemplate>
                                                <table border="0" cellpadding="0" cellspacing="0" class="listViewTable3">
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
                                                    <td class="tdLabelSingle">
                                                        <li class="bulleted">
                                                            <asp:Label ID="uoLabelTotalSingleRoomBlock" runat="server" Text="Total Pax Reserved (Single):"></asp:Label>
                                                        </li>
                                                    </td>
                                                    <td class="tdResultSingle">
                                                        <%--tdAligned--%>
                                                        <span style="font-weight: bold">
                                                            <%# Eval("TotalSingleRoomBlock")%></span>
                                                    </td>
                                                    <%--leftAligned--%>
                                                    <td class="tdLabelSingle2">
                                                        <li class="bulleted">
                                                            <asp:Label ID="Label1" runat="server" Text="Total Pax Reserved (Double):"></asp:Label></li>
                                                    </td>
                                                    <td class="tdResultSingle2">
                                                        <%--tdAligned--%>
                                                        <span style="font-weight: bold">
                                                            <%# Eval("TotalDoubleRoomBlock")%></span>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="tdLabelDouble">
                                                    </td>
                                                    <td class="tdResultDouble">
                                                    </td>
                                                    <td class="tdLabelDouble2">
                                                    </td>
                                                    <td class="tdResultDouble2">
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:DataPager ID="uolistviewHotelInfoPager" runat="server" PagedControlID="uoListViewDashboard"
                                            PageSize="50" Visible="false">
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
                                                <asp:SessionParameter Name="iBranchID" SessionField="UserBranchId" Type="Int64" />
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
                                        <asp:HiddenField ID="uoHiddenFieldBranchId" runat="server" Value="0" />
                                        <asp:HiddenField ID="uoHiddenFieldFromDefaultView" runat="server" Value="1" />
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <br />
                            <br />
                            <%--<div style="position: fixed; left: 10px">
                            <asp:Button ID="btnExport" runat="server" Text="Export List" style="position: fixed; left: 10px" />
                            <div>--%>
                            <div style="right: auto; text-align: left;">
                                <asp:Button ID="uoBtnExportList" runat="server" Text="Export Hotel List" CssClass="SmallButton"
                                    OnClick="uoBtnExportList_Click" />
                                <asp:ListView runat="server" ID="uoDashboardListDetails" OnItemCommand="uoListViewTR_ItemCommand">
                                    <LayoutTemplate>
                                        <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                                            <tr>
                                                <th>
                                                </th>
                                                <th>
                                                    <asp:LinkButton Width="110px" ID="uoLinkButtonHotel" runat="server" CommandName="SortByHotelCity"
                                                        Text="Hotel City" />
                                                </th>
                                                <th>
                                                    <asp:LinkButton Width="100px" ID="uoLinkButtonCheckIn" runat="server" CommandName="SortByCheckIn"
                                                        Text="Check-In" />
                                                </th>
                                                <th>
                                                    <asp:LinkButton Width="100px" ID="uoLinkButtonCheckOut" runat="server" CommandName="SortByCheckOut"
                                                        Text="Check-Out" />
                                                </th>
                                                <th>
                                                    <asp:LinkButton Width="70px" ID="uoLinkButtonHotelNites" runat="server" CommandName="SortByHotelNites"
                                                        Text="Hotel Nites" />
                                                </th>
                                                <%-- <th>
                                                
                                                <asp:LinkButton Width="100px" ID="uoLinkButtonReason" runat="server" CommandName="SortByReasonCode" Text="Reason Code"/>
                                            </th>--%>
                                                <th>
                                                    <asp:LinkButton Width="200px" ID="uoLinkButtonLastname" runat="server" CommandName="SortByLastname"
                                                        Text="Last Name" />
                                                </th>
                                                <th>
                                                    <asp:LinkButton Width="200px" ID="uoLinkButtonFirstname" runat="server" CommandName="SortByFirstname"
                                                        Text="First Name" />
                                                </th>
                                                <th>
                                                    <asp:LinkButton Width="100px" ID="uoLinkButtonEmployeeID" runat="server" CommandName="SortByEmployeeID"
                                                        Text="Employee ID" />
                                                </th>
                                                <th>
                                                    <asp:LinkButton Width="50px" ID="uoLinkButtonGender" runat="server" CommandName="SortByGender"
                                                        Text="Gender" />
                                                </th>
                                                <th>
                                                    <asp:LinkButton Width="70px" ID="uoLinkButtonRoomType" runat="server" CommandName="SortByRoomType"
                                                        Text="Room Type" />
                                                </th>
                                                <th>
                                                    <asp:LinkButton Width="70px" ID="uoLinkButtonVoucher" runat="server" CommandName="SortByVoucher"
                                                        Text="Voucher" />
                                                </th>
                                                <th>
                                                    <asp:LinkButton Width="100px" ID="uoLinkButtonConfirmation" runat="server" CommandName="SortByConfirmation"
                                                        Text="Confirmation" />
                                                </th>
                                                <th>
                                                    <asp:LinkButton Width="50px" ID="uoLinkButtonCouple" runat="server" CommandName="SortByCouple"
                                                        Text="Couple" />
                                                </th>
                                                <th>
                                                    <asp:LinkButton Width="200px" ID="uoLinkButtonTitle" runat="server" CommandName="SortByTitle"
                                                        Text="Title" />
                                                </th>
                                                <th>
                                                    <asp:LinkButton Width="200px" ID="uoLinkButtonShip" runat="server" CommandName="SortByShip"
                                                        Text="Ship" />
                                                </th>
                                                <th>
                                                    <asp:LinkButton Width="200px" ID="uoLinkButtonCostCenter" runat="server" CommandName="SortByCostCenter"
                                                        Text="Cost Center" />
                                                </th>
                                                <th>
                                                    <asp:LinkButton Width="100px" ID="uoLinkButtonNationality" runat="server" CommandName="SortByNationality"
                                                        Text="Nationality" />
                                                </th>
                                                <%--<th>
                                                
                                                <asp:LinkButton Width="80px" ID="uoLinkButtonHotelRequest" runat="server" CommandName="SortByHotelReq" Text="Hotel Request"/>
                                            </th>--%>
                                                <th>
                                                    <asp:LinkButton Width="70px" ID="uoLinkButtonRecLoc" runat="server" CommandName="SortByRecLoc"
                                                        Text="Rec Loc" />
                                                </th>
                                                <th>
                                                    <asp:LinkButton Width="70px" ID="uoLinkButtonDepCity" runat="server" CommandName="SortByDepCity"
                                                        Text="Dept City" />
                                                </th>
                                                <th>
                                                    <asp:LinkButton Width="70px" ID="uoLinkButtonArrCity" runat="server" CommandName="SortByArrCity"
                                                        Text="Arvl City" />
                                                </th>
                                                <th>
                                                    <asp:LinkButton Width="100px" ID="uoLinkButtonDepDate" runat="server" CommandName="SortByDepDate"
                                                        Text="Dept Date" />
                                                </th>
                                                <th>
                                                    <asp:LinkButton Width="100px" ID="uoLinkButtonArrDate" runat="server" CommandName="SortByArrDate"
                                                        Text="Arvl Date" />
                                                </th>
                                                <th>
                                                    <asp:LinkButton Width="70px" ID="uoLinkButtonDepTime" runat="server" CommandName="SortByDepTime"
                                                        Text="Dept Time" />
                                                </th>
                                                <th>
                                                    <asp:LinkButton Width="70px" ID="uoLinkButtonArrTime" runat="server" CommandName="SortByArrTime"
                                                        Text="Arvl Time" />
                                                </th>
                                                <th>
                                                    <asp:LinkButton Width="70px" ID="uoLinkButtonCarrier" runat="server" CommandName="SortByCarrier"
                                                        Text="Carrier" />
                                                </th>
                                                <th>
                                                    <asp:LinkButton Width="70px" ID="uoLinkButtonFlightNo" runat="server" CommandName="SortByFlightNo"
                                                        Text="FlightNo" />
                                                </th>
                                                <th>
                                                    <asp:LinkButton Width="100px" ID="uoLinkButtonPassportNo" runat="server" CommandName="SortByPassportNo"
                                                        Text="Passport No." />
                                                </th>
                                                <th>
                                                    <asp:LinkButton Width="100px" ID="uoLinkButtonIssuedDate" runat="server" CommandName="SortByIssuedDate"
                                                        Text="Issued Date" />
                                                </th>
                                                <th>
                                                    <asp:LinkButton Width="150px" ID="uoLinkButtonPassportExp" runat="server" CommandName="SortByPassportExp"
                                                        Text="Passport Expiration" />
                                                </th>
                                            </tr>
                                            <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                                        </table>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <%--<%# SetStatus()%>--%>
                                            <td>
                                                <asp:Label ID="Label26" runat="server" Text="Tagged" Visible='<%# Convert.ToBoolean(Eval("IsTaggedByUser")) %>'
                                                    title='<%# "Tagged time is : " + Eval("TagDateTime") %>'></asp:Label>
                                                <asp:LinkButton Width="50px" CssClass="PropertyLinkButtons BtnTag" ID="uoLnkBtnTag"
                                                    CommandName="Tag" Visible='<%# !Convert.ToBoolean(Eval("IsTaggedByUser")) %>'
                                                    OnClientClick='<%# "javascript:return confirmTag("+ Eval("EmployeeID") + ", \"" + Eval("FirstName") + "\");" %>'
                                                    CommandArgument='<%# Eval("colIdBigInt") + ":" + Eval("TravelReqId") + ":" + uoHiddenFieldBranchId.Value + ":" + uoHiddenFieldTagTime.Value%>'
                                                    runat="server" Text="Tag">
                                            
                                                </asp:LinkButton>
                                            </td>
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
                                            <%--<td class="leftAligned">
                                            <%# Eval("ReasonCode")%>
                                        </td>--%>
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
                                            <td class="leftAligned">
                                                <%# Eval("RoomType")%>
                                            </td>
                                            <td class="leftAligned">
                                                <%# String.Format("{0:0.00 }", Eval("Voucher"))%>
                                            </td>
                                            <td class="centerAligned">
                                                <a runat="server" class="Confirmation" id="lbnConfirmation" forecolor='<%# System.Drawing.Color.Blue %> '
                                                    title='<%# Convert.ToString(Eval("colConfirmation")).Length > 0 ? "EDIT" : "ADD"%>'
                                                    href='<%# "~/Hotel/AddConfirmation.aspx?BigID=" + Eval("colIdBigInt") + " &TravelID=" + Eval("TravelReqId") + " &BranchID=" + uoHiddenFieldBranchId.Value + " &tType=" + Eval("colConfirmation") %>'>
                                                    <%# Convert.ToString(Eval("colConfirmation")).Length > 0 ? Eval("colConfirmation") : "ADD"%></a>
                                                <%-- <asp:LinkButton class="Confirmation" ID="uolbnConfirmation" CommandName="Confirmation" ForeColor='<%# System.Drawing.Color.Blue %>'
                                                    PostBackUrl='<%# "~/Hotel/AddConfirmation.aspx?BigID=" + Eval("colIdBigInt") + " &TravelID=" + Eval("TravelReqId") + " &BranchID=" + uoHiddenFieldBranchId.Value + " &tType=" + Eval("colConfirmation") %>'
                                                    runat="server" Text='<%# Convert.ToString(Eval("colConfirmation")).Length > 0 ? Eval("colConfirmation") : "ADD"%>'
                                                    ToolTip='<%# Convert.ToString(Eval("colConfirmation")).Length > 0 ? "EDIT" : "ADD"%>'>
                                                   
                                                </asp:LinkButton>--%>
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
                                            <%--  <td class="leftAligned">
                                            <%# Eval("HotelRequest")%>
                                        </td>--%>
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
                                                <%# Eval("PassportNo")%>
                                            </td>
                                            <td class="leftAligned">
                                                <%# Eval("IssuedDate")%>
                                            </td>
                                            <td class="leftAligned">
                                                <%# Eval("PassportExpiration")%>
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
                                                <%--<th>
                                                Reason Code
                                            </th>--%>
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
                                                <%--<th>
                                                Hotel Request
                                            </th>--%>
                                                <th>
                                                    Rec Loc
                                                </th>
                                                <th>
                                                    Dept City
                                                </th>
                                                <th>
                                                    Arvl City
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
                            </div>
                            <table id="uoTableDashboardListDetailsPager" width="100%">
                                <tr>
                                    <td class="LeftClass">
                                        <asp:DataPager runat="server" ID="uoDashBoardListDetailsPager" PagedControlID="uoDashboardListDetails"
                                            PageSize="20" OnPreRender="uoDashBoardListDetailsPager_PreRender">
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
                                    <asp:ControlParameter ControlID="uoHiddenFieldFromDefaultView" ConvertEmptyStringToNull="False"
                                        DbType="Int16" Name="FromDefaultView" PropertyName="Value" />
                                    <asp:ControlParameter ControlID="uoHiddenFieldSortBy" ConvertEmptyStringToNull="False"
                                        DbType="String" Name="SortBy" PropertyName="Value" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                            <%--</div>--%>
                            <asp:HiddenField runat="server" ID="uoHiddenFieldStartDate" />
                            <asp:HiddenField runat="server" ID="HiddenField1" />
                            <asp:HiddenField runat="server" ID="HiddenField2" />
                            <asp:HiddenField runat="server" ID="HiddenField3" />
                            <asp:HiddenField runat="server" ID="uoHiddenFieldPopEditor" Value="0" />
                            <asp:HiddenField runat="server" ID="HiddenField4" Value="0" />
                            <asp:HiddenField runat="server" ID="uoHiddenFieldHotelName" Value="" />
                            <asp:HiddenField runat="server" ID="uoHiddenFieldSortBy" Value="" />
                            <asp:HiddenField runat="server" ID="uoHiddenFieldConfirmation" Value="0" />
                            <asp:HiddenField runat="server" ID="uoHiddenFieldTagTime" Value="" />
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </asp:Panel>
        </div>
    </div>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/TravelMartMaster2.Master" AutoEventWireup="true"
    CodeBehind="Manifest.aspx.cs" Inherits="TRAVELMART.Manifest" %>

<%--<%@ OutputCache Duration="60" VaryByParam="mt" VaryByControl="uoCalendarDashboard"%>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style1
        {
            width: 143px;
        }
    </style>
</asp:Content>
<asp:Content runat="server" ID="Content3" ContentPlaceHolderID="HeaderContent">
    <div class="ViewTitlePadding" class="LeftClass">
        <table width="100%" cellpadding="0" cellspacing="0" class="PageTitle">
            <tr>
                <td>
                    Manifest
                </td>
                <td align="right">
                    <asp:Button ID="uoButtonRequest" runat="server" CssClass="SmallButton" OnClientClick="javascript: return OpenRequestEditor();"
                        Text="Add Hotel/Vehicle Request" Width="140px" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NaviPlaceHolder" runat="server">

    <script type="text/javascript" language="javascript">
        function SetValues(trId, mrId) {
            $("#<%= uoHiddenFieldtRid.ClientID %>").val(trId);
            $("#<%= uoHIddenFieldmRid.ClientID %>").val(mrId);
        }
        function Tag() {
            var e1Id = $("#<%=uoTextBoxBarcode.ClientID %>");
            var user = $("#<%= uoHiddenFieldUserId.ClientID %>");
            var role = $("#<%= uoHiddenFieldUserRole.ClientID %>");
            var tRID = $("#<%= uoHiddenFieldtRid.ClientID %>");
            var mRID = $("#<%= uoHIddenFieldmRid.ClientID %>");

            if (isNaN(e1Id.val()) == true) {
                alert("Invalid Barcode.");
            }
            else {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: "/PageMethods.aspx/TagAsScanned",
                    data: "{'e1Id': '" + e1Id.val() + "', 'tReqId': '" + tRID.val() + "', 'mReqId': '" + mRID.val() +
                    "', 'UserId': '" + user.val() + "', 'URole': '" + role.val() + "', 'retValue': '0'}",
                    dataType: "json",
                    success: function(data) {
                        if (data.d == 1) {
                            alert("Invalid Barcode.");
                        }
                        else {
                            alert("Seafarer Successfully Tagged.");
                            window.parent.$("#ctl00_ContentPlaceHolder1_uoHiddenFieldPopup").val("1");
                            window.parent.history.go(0);
                            parent.$.fancybox.close();
                        }
                    }
                        ,
                    error: function(objXMLHttpRequest, textStatus, errorThrown) {
                        alert(errorThrown);
                    }
                });
            }
        }

        function OpenRequestEditor() {
            var screenWidth = screen.availwidth;
            var screenHeight = screen.availheight;

            screenWidth = 1060;
            screenHeight = 600;

            window.open('RequestEditor.aspx?id=0', 'Hotel_Vehicle_Request', 'top=10, left=200, width=' + screenWidth + ', height=' + screenHeight + ', scrollbars=yes, menubar=no,toolbar=no,status=no,resizable=yes,addressbar=no');
            return false;
        }
        function confirmEmail() {
            if (confirm("Email record?") == true)
                return true;
            else
                return false;
        }
    </script>

    <script type="text/javascript" language="javascript">
        $(document).ready(function() {
            SetTRResolution();
            filterSettings();
        });

        function SetTRResolution() {
            var ht = $(window).height();
            var ht2 = $(window).height();
            var wd = $(window).width() * 0.90;

            if (screen.height <= 600) {
                ht = ht * 0.20;
                ht2 = ht2 * 0.20;
            }
            else if (screen.height <= 720) {
            ht = ht * 0.39;
            ht2 = ht2 * 0.39;
            }
            else {
                ht = ht * 0.47;
                ht2 = ht2 * 0.61;
            }
            $("#Av").width(wd);
            $("#Bv").height(ht);
            $("#Bv").width(wd);
            $("#PG").height(ht2);
            $("#PG").width(wd);

        }

        function pageLoad(sender, args) {
            var isAsyncPostback = Sys.WebForms.PageRequestManager.getInstance().get_isInAsyncPostBack();
            if (isAsyncPostback) {
                SetTRResolution();
                filterSettings();
            }
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
                }
            });

            $("#<%=uoDropDownListFilterBy.ClientID %>").change(function(ev) {
                if ($(this).val() != "1") {
                    $("#<%=uoTextBoxFilter.ClientID %>").val("");
                }
            });

            $(".EditLink").fancybox({
                'transitionIn': 'none',
                'transitionOut': 'none',
                'onClosed': function() {
                    var a = $("#<%=uoHiddenFieldPopUp.ClientID %>").val();
                    if (a == '1')
                        $("#aspnetForm").submit();
                }
            });
            //        $("#<%=uoListViewManifest.ClientID %>").fancybox({           
            //            'transitionIn': 'none',
            //            'transitionOut': 'none'
            //        });
            //        $("#<%=uoListViewManifest.ClientID %> tr a[id*='uoLinkButtonHeaderScanned']").fancybox({
            //            'transitionIn': 'none',
            //            'transitionOut': 'none'
            //        });
        }

        function validate(key) {
            if ($("#<%=uoDropDownListFilterBy.ClientID %>").val() != "1") {

                //getting key code of pressed key
                var keycode = (key.which) ? key.which : key.keyCode;
                if ((keycode < 48 || keycode > 57) && (keycode < 45 || keycode > 46)) {
                    return false;
                }
                else {
                    return true;
                }
            }
            return true;
        }
        function divScrollL() {
            var Right = document.getElementById('Av');
            var Left = document.getElementById('Bv');
            Right.scrollLeft = Left.scrollLeft;

        }    
    </script>

    <%--<asp:UpdatePanel ID="uoUpdatePanelManifest" runat="server">
    <ContentTemplate>
        --%>
    <div id="PG" style="width:auto; height:auto; overflow:auto;">   
    <div align="left" >
        <table width="90%" class="LeftClass" onload="opener.location.reload();" align="left"
            style="margin-left:10px;">
            <tr>
                <td class="style1">
                    <asp:CheckBox ID="uoCheckBoxAdvanceSearch" Text="View Advance Search" runat="server" />
                    &nbsp;
                    <asp:CheckBox ID="uoCheckBoxShowNoBooking" Text="View No Hotel Booking" runat="server"
                        AutoPostBack="True" OnCheckedChanged="uoCheckBoxShowNoBooking_CheckedChanged" />
                </td>
            </tr>
        </table>
        <table width="90%" class="LeftClass" id="uoTableAdvanceSearch" runat="server"
            style="margin-left:15px;">
            <tr id="uoTRVessel" runat="server">
                <td class="contentCaption">
                    Ship:
                </td>
                <td class="contentValue">
                    <asp:DropDownList ID="uoDropDownListVessel" runat="server" Width="300px" AppendDataBoundItems="True">
                        <asp:ListItem>--SELECT SHIP--</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td class="contentCaption">
                    Filter By:
                </td>
                <td class="contentValue">
                    <asp:DropDownList ID="uoDropDownListFilterBy" runat="server" Width="300px">
                        <asp:ListItem Value="1">SEAFARER NAME</asp:ListItem>
                        <asp:ListItem Value="2" Selected="True">EMPLOYEE ID</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:TextBox ID="uoTextBoxFilter" runat="server" CssClass="TextBoxInput" Width="300px"
                        onkeypress="return validate(event);"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="contentCaption">
                    Nationality:
                </td>
                <td class="contentValue">
                    <asp:DropDownList ID="uoDropDownListNationality" runat="server" Width="300px" AppendDataBoundItems="True">
                        <asp:ListItem>--SELECT NATIONALITY--</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td class="contentCaption">
                    Gender:
                </td>
                <td class="contentValue">
                    <asp:DropDownList ID="uoDropDownListGender" runat="server" Width="300px" AppendDataBoundItems="True">
                        <asp:ListItem>--SELECT GENDER--</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td class="contentCaption">
                    Rank:
                </td>
                <td class="contentValue">
                    <asp:DropDownList ID="uoDropDownListRank" runat="server" Width="300px" AppendDataBoundItems="True">
                        <asp:ListItem>--SELECT RANK--</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td class="contentCaption">
                    Status:
                </td>
                <td class="contentValue">
                    <asp:DropDownList ID="uoDropDownListStatus" runat="server" Width="300px">
                        <asp:ListItem Value="0">--SELECT STATUS--</asp:ListItem>
                        <asp:ListItem>ON</asp:ListItem>
                        <asp:ListItem>OFF</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td class="contentCaption">
                    &nbsp;
                </td>
                <td class="contentValue">
                    <asp:Button ID="uoButtonView" runat="server" CssClass="SmallButton" Height="21px"
                        OnClick="uoButtonView_Click" Text="View" />
                </td>
                <td>
                    <asp:LinkButton ID="uoLinkButtonReport" runat="server" Visible="false" OnClick="uoLinkButtonReport_Click">Report Manifest</asp:LinkButton>
                </td>
            </tr>
        </table>
    </div>
    <div id="Av" style="overflow: auto; overflow-x: hidden; width: 100%; overflow-y: hidden;">
        <asp:ListView runat="server" ID="ListView1" OnItemCommand="uoListViewManifest_ItemCommand">
            <LayoutTemplate>
                <table border="0" cellpadding="0" cellspacing="0" class="listViewTableClass">
                    <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                </table>
            </LayoutTemplate>
            <ItemTemplate>
            </ItemTemplate>
            <EmptyDataTemplate>
                <table class="listViewTableClass" style="margin-left: 3px;" align="left">
                    <tr>
                        <th class="hideElement">
                            IDBigInt
                        </th>
                        <th class="hideElement">
                            RequestID
                        </th>
                        <th>
                            <asp:Label runat="server" ID="uoLblManualHdr" Width="60px" Text="Is Manual"></asp:Label>
                        </th>
                        <th>
                            <asp:LinkButton ID="LinkButton1" runat="server" CommandName="SortByE1ID" Width="75px">Travel Req No</asp:LinkButton>
                        </th>
                        <th class="hideElement">
                            <asp:LinkButton ID="uoLinkButtonHeaderRecLoc" runat="server" CommandName="SortByRLoc">Rec Loc</asp:LinkButton>
                        </th>
                        <th>
                            <asp:LinkButton ID="uoLinkButtonHeaderE1ID" runat="server" CommandName="SortByE1"
                                Width="65px">E1 ID</asp:LinkButton>
                        </th>
                        <th>
                            <asp:LinkButton ID="uoLinkButtonHeaderName" runat="server" CommandName="SortByName"
                                Width="200px">Name</asp:LinkButton>
                        </th>
                        <th>
                            <asp:LinkButton ID="uoLinkButtonHeaderOnOff" runat="server" CommandName="SortByOnOff"
                                Width="100px">On/Off Date</asp:LinkButton>
                        </th>
                        <th>
                            <asp:LinkButton ID="uoLinkButtonHeaderCrew" runat="server" CommandName="SortByStatus"
                                Width="80px">Crew Status</asp:LinkButton>
                        </th>
                        <th>
                            <asp:LinkButton ID="uoLinkButtonHeaderReasonCode" runat="server" CommandName="SortByReason"
                                Width="80px">Reason Code</asp:LinkButton>
                        </th>
                        <th>
                            <asp:LinkButton ID="uoLinkButtonHeaderArrDep" runat="server" CommandName="SortByArrDep"
                                Width="100px">Arrival/Departure Date</asp:LinkButton>
                        </th>
                        <th>
                            <asp:LinkButton ID="uoLinkButtonHeaderPort" runat="server" CommandName="SortByPort"
                                Width="200px">Port</asp:LinkButton>
                        </th>
                        <th>
                            <asp:LinkButton ID="uoLinkButtonHeaderBrand" runat="server" CommandName="SortByBrand"
                                Width="60px">Brand</asp:LinkButton>
                        </th>
                        <th class="hideElement">
                            Ship
                        </th>
                        <th>
                            <asp:LinkButton ID="uoLinkButtonHeaderRank" runat="server" CommandName="SortByRank"
                                Width="200px">Rank</asp:LinkButton>
                        </th>
                        <th>
                            <asp:LinkButton ID="uoLinkButtonHeaderAir" runat="server" CommandName="SortByAirStat"
                                Width="50px">Air Status</asp:LinkButton>
                        </th>
                        <th>
                            <asp:LinkButton ID="uoLinkButtonHeaderVehicle" runat="server" CommandName="SortByVehicleStat"
                                Width="50px">Vehicle Status</asp:LinkButton>
                        </th>
                        <th>
                            <asp:LinkButton ID="uoLinkButtonHeaderHotel" runat="server" CommandName="SortByHotelStat"
                                Width="50px">Hotel Status</asp:LinkButton>
                        </th>
                        <th>
                            <asp:LinkButton ID="uoLinkButtonHeaderScanned" runat="server" CommandName="SortByScan"
                                Width="100px">Tag As Scanned</asp:LinkButton>
                        </th>
                        <td>
                            <asp:Label runat="server" ID="Label11" Text="" Width="20px">
                            </asp:Label>
                        </td>
                    </tr>
                </table>
            </EmptyDataTemplate>
        </asp:ListView>
    </div>
    <div id="Bv" style="overflow: auto; width: 100%; overflow-x: auto; overflow-y: auto;"
        onscroll="divScrollL();" class="Module">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table>
                    <tr>
                        <td>
                            <asp:ListView runat="server" ID="uoListViewManifest" DataSourceID="uoObjectDataSourceManifest">
                                <LayoutTemplate>
                                    <table border="0" cellpadding="0" cellspacing="0" class="listViewTableClass" align="left">
                                        <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                                    </table>
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <%# ManifestAddGroup() %>
                                    <tr>
                                        <td class="hideElement">
                                            <asp:HiddenField runat="server" ID="uoHiddenFieldIdBigInt" Value='<%# Eval("IDBigInt") %>' />
                                        </td>
                                        <td class="hideElement">
                                            <asp:HiddenField runat="server" ID="uoHiddenFieldIRequestID" Value='<%# Eval("RequestID") %>' />
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="uoLblManual" Width="62px" Text='<%# Eval("IsManual")%>'></asp:Label>
                                        </td>
                                        <td class="leftAligned">
                                            <asp:Label runat="server" ID="uoLblE1TravelRequest" Width="79px" Text='<%# Eval("E1TravelRequest")%>'></asp:Label>
                                        </td>
                                        <td class="hideElement">
                                            <%# Eval("RecLoc")%>
                                        </td>
                                        <td class="leftAligned">
                                            <asp:Label runat="server" ID="uoLblSfID" Width="69px" Text='<%# Eval("SfID")%>'></asp:Label>
                                        </td>
                                        <td class="leftAligned" style="white-space: normal;">
                                            <asp:HyperLink ID="uoHyperLinkName" NavigateUrl='<%# "SuperUserView.aspx?ufn=" + Request.QueryString["ufn"] + "&sfId=" + Eval("SfID") + "&recloc=" + Eval("RecLoc") + "&st=" + Eval("Status") + "&ID=" + Eval("IDBigInt") + "&trID=" + Eval("TravelRequestID") + "&manualReqID=" + Eval("RequestID") + "&dt=" + Request.QueryString["dt"]%>'
                                                runat="server" Width="204px"><%# Eval("Name")%></asp:HyperLink>
                                            <%--<asp:LinkButton ID="uoLinkButtonName" runat="server" Text='<%# Eval("Name")%>' OnClick="uoLinkButtonName_Click"></asp:LinkButton>--%>
                                        </td>
                                        <%-- <td class="leftAligned"><%# String.Format("{0:dd-MMM-yyyy HHmm}", Eval("DateOnOff"))%>  </td>
                            <td class="leftAligned"><%# String.Format("{0:dd-MMM-yyyy HHmm}", Eval("DateArrivalDeparture"))%>  </td>
--%>
                                        <td class="leftAligned">
                                            <asp:Label runat="server" ID="uoLblDateOnOff" Width="104px" Text='<%# String.Format("{0:dd-MMM-yyyy}", Eval("DateOnOff"))%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="uoLblStatus" Width="88px" Text='<%# Eval("Status")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="uoLblReasonCode" Width="88px" Text='<%# Eval("ReasonCode")%>'></asp:Label>
                                        </td>
                                        <td class="leftAligned">
                                            <asp:Label runat="server" ID="Label1" Width="104px" Text='<%# String.Format("{0:dd-MMM-yyyy}", Eval("DateArrivalDeparture"))%>'></asp:Label>
                                        </td>
                                        <td class="leftAligned" style="white-space: normal;">
                                            <asp:Label runat="server" ID="Label2" Width="204px" Text='<%# String.Format("{0}-{1}", Eval("PortCode") , Eval("Port"))%>'></asp:Label>
                                        </td>
                                        <td class="leftAligned">
                                            <asp:Label runat="server" ID="uoLblBrand" Width="64px" Text='<%# Eval("Brand")%>'></asp:Label>
                                        </td>
                                        <td class="hideElement">
                                            <%# Eval("Vessel")%>
                                        </td>
                                        <td class="leftAligned" style="white-space: normal;">
                                            <asp:Label runat="server" ID="Label3" Width="204px" Text='<%# String.Format("{0}-{1}", Eval("RankCode") , Eval("Rank"))%>'></asp:Label>
                                        </td>
                                        <%--<td><%# Eval("AirStatus")%></td>  
                            <td><%# Eval("VehicleStatus")%></td>  
                            <td><%# Eval("HotelStatus")%></td>  --%>
                                        <td>
                                            <asp:Panel runat="server" ID="uoPanelAir" Width="58px">
                                                <asp:Image ID="uoImageAir" runat="server" ImageUrl='<%#GetStatusImage(Eval("AirStatus")) %>'
                                                    ToolTip='<%# Eval("colAirStatusVarchar")%>' />
                                            </asp:Panel>
                                        </td>
                                        <td>
                                            <asp:Panel runat="server" ID="uoPanelCar" Width="58px">
                                                <asp:Image ID="uoImageCar" runat="server" ImageUrl='<%#GetStatusImage(Eval("VehicleStatus")) %>'
                                                    ToolTip='<%# Eval("colVehicleStatusVarchar")%>' />
                                            </asp:Panel>
                                        </td>
                                        <td>
                                            <asp:Panel runat="server" ID="uoPanelHotel" Width="58px">
                                                <asp:Image ID="uoImageHotel" runat="server" ImageUrl='<%#GetStatusImage(Eval("HotelStatus")) %>'
                                                    ToolTip='<%# Eval("colHotelStatusVarchar")%>' />
                                            </asp:Panel>
                                        </td>
                                        <td>
                                            <asp:Panel runat="server" ID="uoPanelTag" Width="108px">
                                                <asp:Label ID="uoLabelScanned" runat="server" Text="LOE Scanned" Visible='<%# ShowScanBtn(Eval("TravelRequestID"), Eval("RequestID")) %>'></asp:Label>
                                                <a class="EditLink" onclick='<%# "SetValues(" +  Eval("TravelRequestID") + "," + Eval("RequestID") + ");"  %>'
                                                    href="#uoDivScan" id="uoLinkButtonScan" runat="server" visible='<%# !ShowScanBtn(Eval("TravelRequestID"), Eval("RequestID")) %>'>
                                                    Tag</a></asp:Panel>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <EmptyDataTemplate>
                                    <table>
                                        <tr>
                                            <td colspan="15" class="leftAligned">
                                                No Record
                                            </td>
                                        </tr>
                                    </table>
                                </EmptyDataTemplate>
                            </asp:ListView>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div align="left">
                                <asp:DataPager ID="uoListViewManifestPager" runat="server" PagedControlID="uoListViewManifest"
                                    OnPreRender="uoListViewManifest_PreRender" PageSize="20">
                                    <Fields>
                                        <asp:NumericPagerField ButtonType="link" NumericButtonCssClass="PagerClass" />
                                    </Fields>
                                </asp:DataPager>
                                <asp:ObjectDataSource ID="uoObjectDataSourceManifest" runat="server" MaximumRowsParameterName="MaxRow"
                                    SelectCountMethod="LoadManifestListCount" SelectMethod="LoadManifestList" StartRowIndexParameterName="StartRow"
                                    TypeName="TRAVELMART.BLL.ManifestBLL" OldValuesParameterFormatString="oldcount_{0}"
                                    EnablePaging="True" OnSelecting="uoObjectDataSourceManifest_Selecting">
                                    <SelectParameters>
                                        <asp:Parameter Name="LoadType" Type="String" />
                                        <asp:Parameter Name="FromDate" Type="String" />
                                        <asp:Parameter Name="ToDate" Type="String" />
                                        <asp:Parameter Name="UserID" Type="String" />
                                        <asp:Parameter Name="Role" Type="String" />
                                        <asp:Parameter Name="OrderBy" Type="String" />
                                        <asp:Parameter Name="VesselID" Type="String" />
                                        <asp:Parameter Name="FilterByName" Type="String" />
                                        <asp:Parameter Name="SeafarerID" Type="String" />
                                        <asp:Parameter Name="NationalityID" Type="String" />
                                        <asp:Parameter Name="Gender" Type="String" />
                                        <asp:Parameter Name="RankID" Type="String" />
                                        <asp:Parameter Name="Status" Type="String" />
                                        <asp:Parameter Name="RegionID" Type="String" />
                                        <asp:Parameter Name="CountryID" Type="String" />
                                        <asp:Parameter Name="CityID" Type="String" />
                                        <asp:Parameter Name="PortID" Type="String" />
                                        <asp:Parameter Name="HotelID" Type="String" />
                                        <asp:Parameter Name="ViewNoHotelOnly" Type="String" />
                                    </SelectParameters>
                                </asp:ObjectDataSource>
                                <asp:HiddenField runat="server" ID="uoHiddenFieldOrderBy" Value="SortByStatus" />
                                <asp:HiddenField runat="server" ID="uoHiddenFieldLoadType" Value="0" />
                                <asp:HiddenField runat="server" ID="uoHiddenFieldNoBooking" Value="0" />
                            </div>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="uoButtonView" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="uoCheckBoxShowNoBooking" EventName="CheckedChanged" />
            </Triggers>
        </asp:UpdatePanel>
        <asp:HiddenField ID="uoHiddenFieldE1TravelRequest" runat="server" Value="0" />
        <asp:HiddenField ID="uoHiddenFieldManifest" runat="server" Value="0" />
        <asp:HiddenField ID="uoHiddenFieldByVessel" runat="server" Value="1" />
        <asp:HiddenField ID="uoHiddenFieldByName" runat="server" Value="2" />
        <asp:HiddenField ID="uoHiddenFieldByRecLoc" runat="server" Value="0" />
        <asp:HiddenField ID="uoHiddenFieldByE1ID" runat="server" Value="0" />
        <asp:HiddenField ID="uoHiddenFieldByDateOnOff" runat="server" Value="0" />
        <asp:HiddenField ID="uoHiddenFieldByDateArrDep" runat="server" Value="0" />
        <asp:HiddenField ID="uoHiddenFieldByStatus" runat="server" Value="0" />
        <asp:HiddenField ID="uoHiddenFieldByBrand" runat="server" Value="0" />
        <asp:HiddenField ID="uoHiddenFieldByPort" runat="server" Value="0" />
        <asp:HiddenField ID="uoHiddenFieldByRank" runat="server" Value="0" />
        <asp:HiddenField ID="uoHiddenFieldByAirStatus" runat="server" Value="0" />
        <asp:HiddenField ID="uoHiddenFieldByHotelStatus" runat="server" Value="0" />
        <asp:HiddenField ID="uoHiddenFieldByVehicleStatus" runat="server" Value="0" />
        <asp:HiddenField ID="uoHiddenFieldRole" runat="server" Value="" />
        <asp:HiddenField ID="uoHiddenFieldUser" runat="server" Value="" />
        <asp:HiddenField runat="server" ID="uoHiddenFieldPopUp" Value="0" />
        <asp:HiddenField runat="server" ID="uoHiddenFieldDateRange" Value="0" />
        <asp:HiddenField runat="server" ID="uoHiddenFieldDate" Value="0" />
        <div style="display: none">
            <%--Scan Barcode--%>
            <div id="uoDivScan" style="width: 300px">
                <div class="PageTitle">
                    Barcode Scan
                </div>
                <table>
                    <tr>
                        <td style="width: 100px">
                            Barcode:
                        </td>
                        <td>
                            <asp:TextBox ID="uoTextBoxBarcode" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            <asp:Button ID="uoButtonSaveBarcode" runat="server" OnClientClick="return Tag();"
                                Text="Scan" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:HiddenField runat="server" ID="uoHiddenFieldUserId" />
                            <asp:HiddenField runat="server" ID="uoHiddenFieldUserRole" />
                            <asp:HiddenField runat="server" ID="uoHiddenFieldtRid" />
                            <asp:HiddenField runat="server" ID="uoHIddenFieldmRid" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    </div>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/TravelMartMaintenanceMaster.Master"
    AutoEventWireup="true" CodeBehind="ManifestSearchView2.aspx.cs" Inherits="TRAVELMART.ManifestSearchView2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" runat="server">
    <div class="PageTitle">
        Manifest Search View</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="NaviPlaceHolder" runat="server">

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

//        function OpenRequestEditor() {
//            var screenWidth = screen.availwidth;
//            var screenHeight = screen.availheight;

//            screenWidth = 1060;
//            screenHeight = 600;

//            window.open('HotelRequest.aspx?id=0', 'Hotel_Vehicle_Request', 'top=10, left=200, width=' + screenWidth + ', height=' + screenHeight + ', scrollbars=yes, menubar=no,toolbar=no,status=no,resizable=yes,addressbar=no');
//            //window.open('RequestEditor.aspx?id=0', 'Hotel_Vehicle_Request', 'top=10, left=200, width=' + screenWidth + ', height=' + screenHeight + ', scrollbars=yes, menubar=no,toolbar=no,status=no,resizable=yes,addressbar=no');
//            return false;
//        }
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

        function filterSettings() {


            $(".EditLink").fancybox({
                'transitionIn': 'none',
                'transitionOut': 'none',
                'onClosed': function() {
                    var a = $("#<%=uoHiddenFieldPopUp.ClientID %>").val();
                    if (a == '1')
                        $("#aspnetForm").submit();
                }
            });

        }

        function ShowPopup() {
            $(".AddRequest").fancybox(
            {
                'centerOnScroll': false,
                'width': '100%',
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

        function SetTRResolution() {
            var ht = $(window).height(); //550;
            var wd = $(window).width() * 0.88;
            if (screen.height <= 600) {
                ht = ht * 0.35;
            }
            else if (screen.height <= 720) {
                ht = ht * 0.49;
            }
            else {
                ht = ht * 0.55;
            }

            $("#Bv").height(ht);
            $("#Bv").width(wd);
            $("#Av").width(wd);
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
    <table width="100%" class="LeftClass" onload="opener.location.reload();">
        <tr>
            <td class="contentValue">
                <asp:CheckBox ID="uoCheckBoxAdvanceSearch" Text="View Advance Search" runat="server"
                    Visible="false" 
                    oncheckedchanged="uoCheckBoxAdvanceSearch_CheckedChanged" />
            </td>
        </tr>
    </table>
    <%--<table width="100%" class="LeftClass" id="uoTableAdvanceSearch" runat="server">
        <tr id="uoTRVessel" runat="server">
            <td class="contentCaption">
                Vessel:
            </td>
            <td class="contentValue">
                <asp:DropDownList ID="uoDropDownListVessel" runat="server" Width="300px" AppendDataBoundItems="True">
                    <asp:ListItem>--SELECT VESSEL--</asp:ListItem>
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
    </table>--%>
    <div class="RightClass" style="padding-right: 10px">
        <%--<asp:Button ID="uoButtonRequest" runat="server" CssClass="SmallButton" OnClientClick="javascript: return OpenRequestEditor();"
            Text="Add Hotel Request" Width="140px" />--%>
            <table style="width:100%">
            <tr>
                <td style="text-align:left">
                    View All: <asp:CheckBox ID="uoCheckBoxViewALL" runat="server" Checked="false" AutoPostBack="true"/>
                </td>
                <td style="text-align:right">
                    <a id="uoHyperLinkHotelAdd" runat="server" class="AddRequest" style="text-decoration:none" >
                        <asp:Button ID="uoBtnHotelAdd" runat="server" Text="Add Hotel Request" Font-Size="X-Small" Visible="false"/>
                    </a>
                </td>
            </tr>
            </table>                        
    </div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div id="Av" style="overflow-x: hidden; width: 100%; overflow-y: hidden;">
                <asp:ListView runat="server" ID="ListView1" OnItemCommand="uoListViewManifest_ItemCommand">
                    <LayoutTemplate>
                        <table border="0" cellpadding="0" cellspacing="0" class="listViewTable" width="100%">
                            <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                        </table>
                    </LayoutTemplate>
                    <ItemTemplate>
                    </ItemTemplate>
                    <EmptyDataTemplate>
                        <table class="listViewTable" width="100%">
                            <tr>
                                <th class="hideElement">
                                    IDBigInt
                                </th>
                                <th class="hideElement">
                                    RequestID
                                </th>
                                <th style="text-align: center; white-space: normal;">
                                    <asp:Label runat="server" ID="uoLblIsManualHeader" Text="Is Manual" Width="46px"></asp:Label>
                                </th>
                                <th style="text-align: center; white-space: normal;">
                                    <asp:LinkButton ID="LinkButton1" runat="server" Width="55px" CommandName="SortByE1ID">E1 Travel Req No</asp:LinkButton>
                                </th>
                                <th class="hideElement">
                                    <asp:LinkButton ID="uoLinkButtonHeaderRecLoc" runat="server" CommandName="SortByRLoc">Rec Loc</asp:LinkButton>
                                </th>
                                <th style="text-align: center; white-space: normal;">
                                    <asp:LinkButton ID="uoLinkButtonHeaderE1ID" Width="74px" runat="server" CommandName="SortByE1">E1 ID</asp:LinkButton>
                                </th>
                                <th style="text-align: center; white-space: normal;">
                                    <asp:LinkButton ID="uoLinkButtonHeaderName" runat="server" Width="200px" CommandName="SortByName">Name</asp:LinkButton>
                                </th>
                                <th style="text-align: center; white-space: normal;">
                                    <asp:LinkButton ID="uoLinkButtonHeaderOnOff" runat="server" Width="74px" CommandName="SortByOnOff">On/Off Date</asp:LinkButton>
                                </th>
                                <th style="text-align: center; white-space: normal;">
                                    <asp:LinkButton ID="uoLinkButtonHeaderCrew" runat="server" Width="49px" CommandName="SortByStatus">Crew Status</asp:LinkButton>
                                </th>
                                <th style="text-align: center; white-space: normal;">
                                    <asp:LinkButton ID="uoLinkButtonHeaderReasonCode" Width="50px" runat="server" CommandName="SortByReason">Reason Code</asp:LinkButton>
                                </th>
                                <%--<th style="text-align: center; white-space: normal;">
                                    <asp:LinkButton ID="uoLinkButtonHeaderArrDep" runat="server" Width="100px" CommandName="SortByArrDep">Arrival/Departure Date</asp:LinkButton>
                                </th>--%>
                                <th style="text-align: center; white-space: normal;">
                                    <asp:LinkButton ID="uoLinkButtonHeaderPort" runat="server" Width="200px" CommandName="SortByPort">Port</asp:LinkButton>
                                </th>
                                <th style="text-align: center; white-space: normal;">
                                    <asp:LinkButton ID="uoLinkButtonHeaderBrand" runat="server" Width="44px" CommandName="SortByBrand">Brand</asp:LinkButton>
                                </th>
                                <th class="hideElement">
                                    Ship
                                </th>
                                <th style="text-align: center; white-space: normal;">
                                    <asp:LinkButton ID="uoLinkButtonHeaderRank" runat="server" Width="200px" CommandName="SortByRank">Rank</asp:LinkButton>
                                </th>
                                <th style="text-align: center; white-space: normal;">
                                    <asp:LinkButton ID="uoLinkButtonHeaderAir" runat="server" Width="40px" CommandName="SortByAirStat">Air Status</asp:LinkButton>
                                </th>
                                <th style="text-align: center; white-space: normal;">
                                    <asp:LinkButton ID="uoLinkButtonHeaderVehicle" runat="server" Width="40px" CommandName="SortByVehicleStat">Vehicle Status</asp:LinkButton>
                                </th>
                                <th style="text-align: center; white-space: normal;">
                                    <asp:LinkButton ID="uoLinkButtonHeaderHotel" runat="server" Width="40px" CommandName="SortByHotelStat">Hotel Status</asp:LinkButton>
                                </th>
                                <th style="text-align: center; white-space: normal;">
                                    <asp:Label runat="server" ID="uoLabelRecordLoc" Text="Record Locator" Width="50px"></asp:Label>
                                </th>
                                <th style="text-align: center; white-space: normal;">
                                    <asp:Label runat="server" ID="uoLabelOriginDestination" Text="Origin-Destination"
                                        Width="65px"></asp:Label>
                                </th>
                                <th   style="padding:-100px; margin:-3px; ">
                                    <table cellspacing="0" cellpadding="0" style="border-top-style:none; 
                                         border-collapse: collapse;
                                        border-bottom-style:none;padding:-3px; margin:-3px;
                                        border-width:0.1px; background-color: #fff;  Width:260px">
                                        <tr  style=" border:none; border-top-style:none; ">
                                            <th colspan="3"  >
                                                 <asp:Label runat="server"  ID="Label2" Text="Tag"
                                                    Width="240px"></asp:Label>
                                            </th>
                                        </tr>
                                        
                                        <tr   >
                                            <th  > 
                                                <asp:Label runat="server" ID="Label8" Text="Meet & Greet" Width="75px"></asp:Label>
                                            </th >
                                            <th >
                                                <asp:Label runat="server" ID="Label9" Text="Transport" Width="76px"></asp:Label>
                                            </th >
                                            <th >
                                                <asp:Label runat="server" ID="Label10" Text="Hotel" Width="72px"></asp:Label>
                                            </th>
                                        </tr>
                                    </table>
                                </th>
                                
                                 
                                
                               <%-- 
                                <th style="text-align: center; white-space: normal;">
                                    <asp:Label runat="server" ID="Label2" Text="Meet & Greet"
                                        Width="50px"></asp:Label>
                                </th>--%>
                               <%-- <th style="text-align: center; white-space: normal;">
                                    <asp:Label runat="server" ID="Label3" Text="Service Provider"
                                        Width="50px"></asp:Label>
                                </th>--%>
                                <%-- <th style="text-align: center; white-space: normal;">
                                    <asp:Label runat="server" ID="Label3" Text="Transport"
                                        Width="60px"></asp:Label>
                                </th>
                                <th style="text-align: center; white-space: normal;">
                                    <asp:Label runat="server" ID="Label4" Text="Hotel"
                                        Width="50px"></asp:Label>
                                </th>--%>
                                
                                
                                
                                <th style="text-align: center; white-space: normal;">
                                    <asp:Label runat="server" ID="uoLbLRemarks" Text="Remarks" Width="150px"></asp:Label>
                                </th>
                                <%--<th style="text-align: center; white-space: normal;">
                                    <asp:LinkButton ID="uoLinkButtonHeaderScanned" runat="server" Width="80px" CommandName="SortByScan">Tag As Scanned</asp:LinkButton>
                                </th>--%>
                                <th style="text-align: center; white-space: normal;">
                                    <asp:Label runat="server" ID="uoLblLast" Width="100px"></asp:Label>
                                </th>
                                <th style="text-align: center; white-space: normal;">
                                    <asp:Label runat="server" ID="Label1" Width="50px"></asp:Label>
                                </th>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                </asp:ListView>
            </div>
            <div id="Bv" style="overflow: auto; width: 100%; overflow-x: scroll; overflow-y: auto;"
                onscroll="divScrollL();">
                <asp:ListView runat="server" ID="uoListViewManifest" DataSourceID="uoObjectDataSourceManifestSearchView"
                    OnItemDataBound="uoListViewManifest_ItemDataBound" OnItemCommand="uoListViewManifest_ItemCommand">
                    <LayoutTemplate>
                        <table border="0" cellpadding="0" cellspacing="0" class="listViewTable" width="100%">
                            <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                        </table>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <%# ManifestSearchViewAddGroup() %>
                        <%--<tr class='<%# Eval("E1TravelRequest").ToString()=="0"?"notComplete":"" %>'>--%>
                        <tr>
                            <td class="hideElement">
                                <asp:HiddenField runat="server" ID="uoHiddenFieldIdBigInt" Value='<%# Eval("IDBigInt") %>' />
                                <asp:HiddenField runat="server" ID="uoHiddenFieldRemarksURL" Value='<%# Eval("RemarksURL") %>' />
                                <asp:HiddenField runat="server" ID="uoHiddenFieldRemarksParameter" Value='<%# Eval("RemarksParameter") %>' />
                            </td>
                            <td class="hideElement">
                                <asp:HiddenField runat="server" ID="uoHiddenFieldIRequestID" Value='<%# Eval("RequestID") %>' />
                            </td>
                            <td style="white-space: normal;">
                                <asp:Label runat="server" ID="uoLblIsManual" Text=' <%# Eval("IsManual")%>' 
                                Width="50px"  Visible='<%# Eval("IsVisible") %>'></asp:Label>
                            </td>
                            <td class="leftAligned" style="white-space: normal;">
                                <asp:Label runat="server" ID="uoLblE1TravelRequest" Text='<%# (Eval("E1TravelRequest").ToString() == "0" ? "" : Eval("E1TravelRequest"))%>'
                                    Width="60px" Visible='<%# Eval("IsVisible") %>'></asp:Label>
                            </td>
                            <td class="hideElement">
                                <%# Eval("RecLoc")%>
                            </td>
                            <td class="leftAligned" style="white-space: normal;">
                                <asp:Label runat="server" ID="uoLblSfID" Text='<%# Eval("SfID")%>' Width="80px" Visible='<%# Eval("IsVisible") %>'>
                                </asp:Label>
                            </td>
                            <td class="leftAligned" style="white-space: normal;">
                                <asp:HyperLink Width="206px" ID="uoHyperLinkName" NavigateUrl='<%# "SuperUserView.aspx?ufn=" + Request.QueryString["ufn"] + "&sfId=" + Eval("SfID") + "&recloc=" + Eval("RecLoc") + "&st=" + Eval("Status") + "&ID=" + Eval("IDBigInt") + "&trID=" + Eval("TravelRequestID") + "&manualReqID=" + Eval("RequestID") + "&dt=" + Request.QueryString["dt"] + "&e1TR=" + Eval("E1TravelRequest")%>'
                                    runat="server" Text='<%# Eval("Name")%>' Visible='<%# Eval("IsVisible") %>'></asp:HyperLink>
                            </td>
                            <td class="leftAligned" style="white-space: normal;">
                                <asp:Label runat="server" ID="uoLblDateOnOff" Text='<%# String.Format("{0:dd-MMM-yyyy}", Eval("DateOnOff"))%>'
                                    Width="80px"  Visible='<%# Eval("IsVisible") %>'></asp:Label>
                            </td>
                            <td style="white-space: normal;">
                                <asp:Label runat="server" ID="uoLblStatus" Text='<%# Eval("Status")%>' 
                                Width="60px"  Visible='<%# Eval("IsVisible") %>'></asp:Label>
                            </td>
                            <td style="white-space: normal;">
                                <asp:Label runat="server" ID="uoLblReasonCode" Text='<%# Eval("ReasonCode")%>' 
                                Width="60px"  Visible='<%# Eval("IsVisible") %>'></asp:Label>
                            </td>
                            <%--<td class="leftAligned" style="white-space: normal;">
                                <asp:Label runat="server" ID="uoLblDateArrivalDeparture" Text='<%# String.Format("{0:dd-MMM-yyyy}", Eval("DateArrivalDeparture"))%>'
                                    Width="106px"></asp:Label>
                            </td>--%>
                            <td class="leftAligned" style="white-space: normal;">
                                <asp:Label runat="server" ID="uoLblPort" Text='<%# String.Format("{0}-{1}", Eval("PortCode"),Eval("Port"))%>'
                                    Width="206px"  Visible='<%# Eval("IsVisible") %>'></asp:Label>
                            </td>
                            <td class="leftAligned" style="white-space: normal;">
                                <asp:Label runat="server" ID="uoLblBrand" Text='<%# Eval("Brand")%>' 
                                Width="50px"  Visible='<%# Eval("IsVisible") %>'></asp:Label>
                            </td>
                            <td class="hideElement">
                                <%# Eval("Vessel")%>
                            </td>
                            <td class="leftAligned" style="white-space: normal;">
                                <asp:Label runat="server" ID="uoLblRank" Text='<%# String.Format("{0}-{1}", Eval("RankCode"),Eval("Rank"))%>'
                                    Width="206px"  Visible='<%# Eval("IsVisible") %>'></asp:Label>
                            </td>
                            <%--<td><%# Eval("AirStatus")%></td>  
                            <td><%# Eval("VehicleStatus")%></td>  
                            <td><%# Eval("HotelStatus")%></td>  --%>
                            <td style="white-space: normal;">
                                <div style="width: 50px;">
                                    <asp:Image ID="uoImageAir" runat="server" ImageUrl='<%#GetStatusImage(Eval("AirStatus")) %>'
                                        ToolTip='<%# Eval("colAirStatusVarchar")%>'  Visible='<%# Eval("IsVisible") %>'/>
                                </div>
                            </td>
                            <td style="white-space: normal;">
                                <div style="width: 50px;">
                                    <asp:Image ID="uoImageCar" runat="server" ImageUrl='<%#GetStatusImage(Eval("VehicleStatus")) %>'
                                        ToolTip='<%# Eval("colVehicleStatusVarchar")%>'  Visible='<%# Eval("IsVisible") %>' />
                                </div>
                            </td>
                            <td style="white-space: normal;">
                                <div style="width: 50px;">
                                    <asp:Image ID="uoImageHotel" runat="server" ImageUrl='<%#GetStatusImage(Eval("HotelStatus")) %>'
                                        ToolTip='<%# Eval("colHotelStatusVarchar")%>'  Visible='<%# Eval("IsVisible") %>' />
                                </div>
                            </td>
                            <td class="leftAligned" style="white-space: normal;">
                                <asp:Label ID="uoLabelRecordLoc" runat="server" Width="50px" Text='<%# Eval("RecLoc")%>'  
                                Visible='<%# Eval("IsVisible") %>'></asp:Label>
                            </td>
                            <td class="leftAligned" style="white-space: normal;">
                                <asp:Label ID="uoLabelOriginDestination" runat="server" Width="65px" Text='<%# Eval("OriginDestination")%>'
                                 Visible='<%# Eval("IsVisible") %>'></asp:Label>
                            </td>
                            <td class="leftAligned" style="white-space: normal;">
                                <asp:Label ID="Label5" runat="server" Width="80px" Text='<%# string.Concat(Eval("IsMeetGreet"),  Eval("MGTagDate")) %>'  
                                Visible='<%# Eval("IsVisible") %>'></asp:Label>
                                 
                            </td>
                           <%-- <td class="leftAligned" style="white-space: normal;">
                                <asp:Label ID="Label6" runat="server" Width="50px" Text='<%# Eval("IsPortAgent")%>'  
                                Visible='<%# Eval("IsVisible") %>'></asp:Label>
                            </td>--%>
                             <td class="leftAligned" style="white-space: normal;">
                             
                              <asp:Label ID="Label6" runat="server"  Width="80px"  Text='<%# string.Concat(Eval("IsVehicleVendor"), Eval("PATagDate"))  %>'  
                                Visible='<%# Eval("IsVisible") %>'></asp:Label>
                             
                               <%-- <asp:Label ID="Label6" runat="server" Width="60px" Text='<%#  Eval("IsVehicleVendor") + Eval("PATagDate")%>'  
                                Visible='<%# Eval("IsVisible") %>'></asp:Label>--%>
                                
                                
                            </td>
                            <td class="leftAligned" style="white-space: normal;">
                                <asp:Label ID="Label7" runat="server"  Width="80px"  Text='<%# string.Concat( Eval("IsHotelVendor"),  Eval("HVTagDate"))%>'  
                                Visible='<%# Eval("IsVisible") %>'></asp:Label>
                                
                                 
                            </td>
                            <td class="leftAligned" style="white-space: normal;">
                                <asp:Label ID="uoLabelRemarks" runat="server" Width="150px" Text='<%# Eval("Remarks")%>'></asp:Label>
                                <asp:HyperLink ID="uoHyperLinkRemarks" Width="150px" runat="server"><%# Eval("Remarks")%></asp:HyperLink>
                            </td>
                            <%--<td style="white-space: normal;">
                                <asp:Label ID="uoLabelScanned" Width="90px" runat="server" Text="LOE Scanned" Visible='<%# ShowScanBtn(Eval("TravelRequestID"), Eval("RequestID")) %>'></asp:Label>
                                <div style="width: 90px;">
                                    <a class="EditLink" onclick='<%# "SetValues(" +  Eval("TravelRequestID") + "," + Eval("RequestID") + ");"  %>'
                                        href="#uoDivScan" id="uoLinkButtonScan" runat="server" visible='<%# !ShowScanBtn(Eval("TravelRequestID"), Eval("RequestID")) %>'>
                                        Tag</a>
                                </div>
                            </td>--%>
                            <td class="centerAligned" style="white-space: normal;">
                                <asp:HyperLink Width="100px" ID="uoHyperLinkAddRequest" CssClass="AddRequest" NavigateUrl='<%# "HotelRequest.aspx?sfId=" + Eval("SfID") + "&trID=" + Eval("TravelRequestID") + "&hrID=0&App=1" %>'
                                    runat="server" Visible='false' Text="Add Request"></asp:HyperLink>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <EmptyDataTemplate>
                        <table>
                            <tr>
                                <td colspan="16">
                                    <asp:Label runat="server" ID="uoLblScroll" Width="1630px"></asp:Label>
                                </td>
                            </tr>
                            <tr align="left">
                                <td colspan="16" class="leftAligned">
                                    No Record
                                </td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                </asp:ListView>
                <div align="left">
                    <asp:DataPager ID="uoListViewManifestPager" runat="server" PagedControlID="uoListViewManifest"
                        OnPreRender="uoListViewManifest_PreRender" PageSize="20">
                        <Fields>
                            <asp:NumericPagerField ButtonType="link" NumericButtonCssClass="PagerClass" />
                        </Fields>
                    </asp:DataPager>
                    <asp:ObjectDataSource ID="uoObjectDataSourceManifestSearchView" runat="server" MaximumRowsParameterName="MaxRow"
                        SelectCountMethod="LoadManifestSearchViewListCount" SelectMethod="LoadManifestSearchViewList"
                        StartRowIndexParameterName="StartRow" TypeName="TRAVELMART.BLL.ManifestBLL" OldValuesParameterFormatString="oldcount_{0}"
                        EnablePaging="True" OnSelecting="uoObjectDataSourceManifestSearchView_Selecting">
                        <SelectParameters>
                            <asp:Parameter Name="LoadType" Type="String" />
                            <asp:Parameter Name="CurrentDate" Type="String" />
                            <asp:Parameter Name="UserID" Type="String" />
                            <asp:Parameter Name="Role" Type="String" />
                            <asp:Parameter Name="OrderBy" Type="String" />
                            <asp:Parameter Name="SeafarerID" Type="String" />
                            <asp:Parameter Name="SeafarerLN" Type="String" />
                            <asp:Parameter Name="SeafarerFN" Type="String" />
                            <asp:Parameter Name="RecordLocator" Type="String" />
                            <asp:Parameter Name="VesselCode" Type="String" />
                            <asp:Parameter Name="VesselName" Type="String" />
                            <asp:Parameter Name="RegionID" Type="String" />
                            <asp:Parameter Name="CountryID" Type="String" />
                            <asp:Parameter Name="CityID" Type="String" />
                            <asp:Parameter Name="PortID" Type="String" />
                            <asp:Parameter Name="HotelID" Type="String" />
                            <asp:ControlParameter Name="IsShowAll" Type="Boolean" ControlID="uoCheckBoxViewALL"/>
                        </SelectParameters>
                    </asp:ObjectDataSource>
                    <asp:HiddenField runat="server" ID="uoHiddenFieldOrderBy" Value="SortByStatus" />
                    <asp:HiddenField runat="server" ID="uoHiddenFieldLoadType" Value="0" />
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <%--<asp:AsyncPostBackTrigger ControlID="uoButtonView" EventName="Click" />--%>
        </Triggers>
    </asp:UpdatePanel>
    <asp:HiddenField ID="uoHiddenFieldE1TravelRequest" runat="server" Value="0" />
    <asp:HiddenField ID="uoHiddenFieldManifest" runat="server" Value="0" EnableViewState="true" />
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
    <asp:HiddenField ID="uoHiddenFieldSeafarerID" runat="server" Value="" />
    <asp:HiddenField ID="uoHiddenFieldSeafarerLN" runat="server" Value="" />
    <asp:HiddenField ID="uoHiddenFieldSeafarerFN" runat="server" Value="" />
    <asp:HiddenField ID="uoHiddenFieldRecLoc" runat="server" Value="" />
    <asp:HiddenField ID="uoHiddenFieldVesselCode" runat="server" Value="" />
    <asp:HiddenField ID="uoHiddenFieldVesselName" runat="server" Value="" />
    <%--<asp:HiddenField runat="server" ID="uoHiddenFieldLoadType" Value = "<%# getDefualtLoadType() %>" />--%>
    <%--</ContentTemplate>
</asp:UpdatePanel>--%>
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
                        <asp:HiddenField runat="server" ID="uoHiddenFieldPopupHotel" />
                        <asp:HiddenField runat="server" ID="uoHiddenFieldSfID" />                      
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>

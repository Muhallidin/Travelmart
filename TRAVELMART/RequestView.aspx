<%@ Page Title="" Language="C#" MasterPageFile="~/TravelMartMaster2.Master" AutoEventWireup="true"
    CodeBehind="RequestView.aspx.cs" Inherits="TRAVELMART.RequestView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        HideHeader
        {
            display: none;
        }
        ShowHeader
        {
            display: block;
        }
    </style>
</asp:Content>
<asp:Content runat="server" ID="Content3" ContentPlaceHolderID="HeaderContent">
    <div class="ViewTitlePadding">
        <table width="100%" cellpadding="0" cellspacing="0" class="PageTitle">
            <tr>
                <td class="PageTitle">
                    Hotel/Vehicle Manual Request
                </td>
                <td class="RightClass">
                    <asp:Button ID="uoButtonRequest" runat="server" CssClass="SmallButton" OnClientClick="javascript: return OpenRequestEditorNew();"
                        Text="Add Hotel/Vehicle Request" Width="140px" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NaviPlaceHolder" runat="server">

    <script type="text/javascript" language="javascript">
        $(document).ready(function() {
            SetResolution();
            filterSettings();

        });

        function pageLoad(sender, args) {
            var isAsyncPostback = Sys.WebForms.PageRequestManager.getInstance().get_isInAsyncPostBack();
            if (isAsyncPostback) {
                SetResolution();
                filterSettings();
            }
        }

        function SetResolution() {
            var ht = $(window).height();
            var ht2 = $(window).height();
            var wd = $(window).width() * 0.90;
            if (screen.height <= 600) {
                ht = ht * 0.20;
                ht2 = ht2 * 0.43;
            }
            else if (screen.height <= 720) {
            ht = ht * 0.40;
            ht2 = ht2 * 0.56;
            }
            else {
                ht = ht * 0.45;
                ht2 = ht2 * 0.62;
            }

            $("#Bv").height(ht);
            $("#AllDiv").height(ht2);
            $("#AllDiv").width(wd);
            $("#Bv").width(wd);
            $("#Av").width(wd);
            
        }

        function divScrollL() {
            var Right = document.getElementById('Av');
            var Left = document.getElementById('Bv');
            Right.scrollLeft = Left.scrollLeft;

        }

        function filterSettings() {

            if ($("#<%=uoCheckBoxAdvanceSearch.ClientID %>").attr('checked')) {
                $("#<%=uoTableAdvanceSearch.ClientID %>").show();
            }
            else {
                $("#<%=uoTableAdvanceSearch.ClientID %>").hide();
            }

            //Events
            $("#<%=uoCheckBoxAdvanceSearch.ClientID %>").click(function() {
                if ($(this).attr('checked')) {
                    $("#<%=uoTableAdvanceSearch.ClientID %>").fadeIn();
                }
                else {
                    $("#<%=uoTableAdvanceSearch.ClientID %>").fadeOut();
                }
            });

            $("#ctl00_ContentPlaceHolder1_uoListViewRequest_uoCheckBoxAll").click(function(ev) {
                var status = $(this).attr('checked');
                $('input:checkbox[name *= uoCheckBoxSelect]').attr('checked', status);
            });

            $("#<%=uoButtonApproved.ClientID %>").click(function(ev) {
                var listOfCheckbox = $('input:checkbox[name*=uoCheckBoxSelect]:checked');

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

            $("#<%=uoDropDownListFilterBy.ClientID %>").change(function(ev) {
                if ($(this).val() != "1") {
                    $("#<%=uoTextBoxFilter.ClientID %>").val("");
                }
            });
        }

      
        
        function OpenRequestEditor(id) {
            var screenWidth = screen.availwidth;
            var screenHeight = screen.availheight;

            screenWidth = 1060;
            screenHeight = 600;

            window.open('RequestEditor.aspx?id=' + id, 'Hotel_Vehicle_Request', 'top=10, left=200, width=' + screenWidth + ', height=' + screenHeight + ', scrollbars=yes, menubar=no,toolbar=no,status=no,resizable=yes,addressbar=no');
            return false;
        }

        function RefreshPageFromPopup() {
            $("#<%=uoHiddenFieldPopEditor.ClientID %>").val(1);
            $("#aspnetForm").submit();
        }

        function OpenRequestEditorNew() {
            var screenWidth = screen.availwidth;
            var screenHeight = screen.availheight;

            screenWidth = 1060;
            screenHeight = 600;

            window.open('RequestEditor.aspx?id=0&r=1', 'Hotel_Vehicle_Request', 'top=10, left=200, width=' + screenWidth + ', height=' + screenHeight + ', scrollbars=yes, menubar=no,toolbar=no,status=no,resizable=yes,addressbar=no');
            return false;
        }
    </script>

    <div id="AllDiv"  style="overflow: auto; overflow-x: auto; overflow-y: auto;">
        <%--<div class="LeftClass" id="Hdr">--%>
            <table class="LeftClass" align="left">
                <tr>
                    <td colspan="2">
                        <asp:CheckBox ID="uoCheckBoxAdvanceSearch" Text="View Advance Search" runat="server" />
                    </td>
                    <td class="contentCaption">
                    </td>
                    <td class="contentValue">
                    </td>
                </tr>
            </table>
            <table class="LeftClass" id="uoTableAdvanceSearch" runat="server" align="left">
                <tr>
                    <td class="contentCaption">
                        Ship:
                    </td>
                    <td class="contentValue">
                        <asp:DropDownList ID="uoDropDownListVessel" runat="server" Width="300px" AppendDataBoundItems="True">
                            <asp:ListItem>--SELECT SHIP--</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="contentCaption">
                        Group By:
                    </td>
                    <td class="contentValue">
                        <asp:DropDownList ID="uoDropDownListGroupBy" runat="server" Width="305px" AppendDataBoundItems="True">
                            <asp:ListItem Value="VesselName">SHIP</asp:ListItem>
                            <asp:ListItem Value="HotelName">HOTEL</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="contentCaption">
                        Filter By:
                    </td>
                    <td class="contentValue">
                        <asp:DropDownList ID="uoDropDownListFilterBy" runat="server" Width="300px">
                            <asp:ListItem Value="1">SEAFARER NAME</asp:ListItem>
                            <asp:ListItem Value="2">EMPLOYEE ID</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="contentCaption">
                    </td>
                    <td class="contentValue">
                        <asp:TextBox ID="uoTextBoxFilter" runat="server" CssClass="TextBoxInput" Width="300px"></asp:TextBox>
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
                    <td class="contentCaption">
                    </td>
                    <td class="contentValue">
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
                    <td class="contentCaption">
                    </td>
                    <td class="contentValue">
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
                    <td class="contentCaption">
                    </td>
                    <td class="contentValue">
                    </td>
                </tr>
                <tr>
                    <td class="contentCaption">
                        Status:
                    </td>
                    <td class="contentValue">
                        <asp:DropDownList ID="uoDropDownListStatus" runat="server" Width="300px">
                            <asp:ListItem Value="">--SELECT STATUS--</asp:ListItem>
                            <asp:ListItem>ON</asp:ListItem>
                            <asp:ListItem>OFF</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="contentCaption">
                    </td>
                    <td class="contentCaption">
                    </td>
                </tr>
                <tr>
                    <td class="contentCaption">
                    </td>
                    <td class="contentValue">
                        <asp:Button ID="uoButtonView" runat="server" Text="View" CssClass="SmallButton" OnClick="uoButtonView_Click" />
                        <asp:Button ID="uoClearFilter" runat="server" Text="Clear Filter" CssClass="SmallButton"
                            OnClick="uoClearFilter_Click"/>
                    </td>
                </tr>
            </table>
        <%--</div>--%>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table class="LeftClass" width="100%">
                    <tr>
                        <td>
                            <asp:Button ID="uoButtonApproved" runat="server" Text="Approve" CssClass="SmallButton"
                                Width="70px" OnClick="uoButtonApproved_Click" />
                            <asp:Button ID="uoButtonViewAll" runat="server" Text="View All" CssClass="SmallButton"
                                OnClick="uoButtonViewAll_Click" Visible="false" />
                        </td>
                    </tr>
                </table>
                <div id="Av" style="overflow: auto; overflow-x: hidden; overflow-y: hidden; text-align: left;">
                    <asp:ListView runat="server" ID="ListView1">
                        <LayoutTemplate>
                        </LayoutTemplate>
                        <ItemTemplate>
                        </ItemTemplate>
                        <EmptyDataTemplate>
                            <table border="0" cellpadding="0" cellspacing="0" class="listViewTableClass" width="100%">
                                <tr>
                                    <th class="hideElement">
                                        Req ID
                                    </th>
                                    <th>
                                        <asp:CheckBox ID="uoCheckBoxAll" runat="server" Width="30px" />
                                    </th>
                                    <th>
                                        <asp:Label runat="server" ID="uoLblViewHdr" Text="" Width="30px"></asp:Label>
                                    </th>
                                    <th>
                                        <asp:Label runat="server" ID="uoLblE1Hdr" Text="E1 ID" Width="60px"></asp:Label>
                                    </th>
                                    <th>
                                        <asp:Label runat="server" ID="uoLblNameHdr" Text="Name" Width="250px"></asp:Label>
                                    </th>
                                    <th>
                                        <asp:Label runat="server" ID="uoLblOnOffHdr" Text="On/Off Date" Width="80px"></asp:Label>
                                    </th>
                                    <th>
                                        <asp:Label runat="server" ID="uoLblPortHdr" Text="Port" Width="200px"></asp:Label>
                                    </th>
                                    <th>
                                        <asp:Label runat="server" ID="uoLblBrandHdr" Text="Brand" Width="200px"></asp:Label>
                                    </th>
                                    <th>
                                        <asp:Label runat="server" ID="uoLblShipHdr" Text="Ship" Width="200px"></asp:Label>
                                    </th>
                                    <th>
                                        <asp:Label runat="server" ID="uoLblRankHdr" Text="Rank" Width="200px"></asp:Label>
                                    </th>
                                    <th>
                                        <asp:Label runat="server" ID="uoLblStatusHdr" Text="Crew Status" Width="60px"></asp:Label>
                                    </th>
                                    <th>
                                        <asp:Label runat="server" ID="uoLblScroll" Text="" Width="30px"></asp:Label>
                                    </th>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
                    </asp:ListView>
                    <asp:ListView runat="server" ID="ListView2">
                        <LayoutTemplate>
                        </LayoutTemplate>
                        <ItemTemplate>
                        </ItemTemplate>
                        <EmptyDataTemplate>
                            <table border="0" cellpadding="0" cellspacing="0" class="listViewTableClass" width="100%">
                                <tr>
                                    <th class="hideElement">
                                        Req ID
                                    </th>
                                    <th>
                                        <asp:CheckBox ID="uoCheckBoxAll" runat="server" Width="30px" />
                                    </th>
                                    <th>
                                        <asp:Label runat="server" ID="uoLblViewHdr" Text="" Width="30px"></asp:Label>
                                    </th>
                                    <th>
                                        <asp:Label runat="server" ID="uoLblE1Hdr" Text="E1 ID" Width="60px"></asp:Label>
                                    </th>
                                    <th>
                                        <asp:Label runat="server" ID="uoLblNameHdr" Text="Name" Width="250px"></asp:Label>
                                    </th>
                                    <th>
                                        <asp:Label runat="server" ID="uoLblOnOffHdr" Text="On/Off Date" Width="80px"></asp:Label>
                                    </th>
                                    <th>
                                        <asp:Label runat="server" ID="uoLblPortHdr" Text="Port" Width="200px"></asp:Label>
                                    </th>
                                    <th>
                                        <asp:Label runat="server" ID="uoLblBrandHdr" Text="Brand" Width="200px"></asp:Label>
                                    </th>
                                    <th>
                                        <asp:Label runat="server" ID="uoLblRankHdr" Text="Rank" Width="200px"></asp:Label>
                                    </th>
                                    <th>
                                        <asp:Label runat="server" ID="uoLblHotelHdr" Text="Hotel" Width="200px"></asp:Label>
                                    </th>
                                    <th>
                                        <asp:Label runat="server" ID="uoLblStatusHdr" Text="Crew Status" Width="60px"></asp:Label>
                                    </th>
                                    <th>
                                        <asp:Label runat="server" ID="uoLblScroll" Text="" Width="30px"></asp:Label>
                                    </th>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
                    </asp:ListView>
                </div>
                <div id="Bv" style="overflow: auto; overflow-x: auto; overflow-y: auto;" onscroll="divScrollL();"
                    align="left">
                    <asp:ListView runat="server" ID="uoListViewRequest">
                        <LayoutTemplate>
                            <table border="0" cellpadding="0" cellspacing="0" class="listViewTable" width="100%">
                                <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                            </table>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <%# RequestViewAddGroup() %>
                            <tr>
                                <td class="hideElement">
                                    <asp:HiddenField runat="server" ID="uoHiddenFieldIdBigInt" Value='<%# Eval("RequestID") %>' />
                                    <asp:HiddenField runat="server" ID="uoHiddenFieldCountry" Value='<%# Eval("colCountryIDInt") %>' />
                                    <asp:HiddenField runat="server" ID="uoHiddenFieldBranch" Value='<%# Eval("colBranchIDInt") %>' />
                                </td>
                                <td style="white-space: normal;">
                                    <asp:CheckBox CssClass="Checkbox" ID="uoCheckBoxSelect" runat="server" Width="31px" />
                                </td>
                                <td style="white-space: normal;">
                                    <asp:LinkButton ID="uoLinkButtonView" runat="server" Text="View" Width="31px" OnClientClick='<%# "javascript: return OpenRequestEditor(" + Eval("RequestID")  + ");" %>'></asp:LinkButton>
                                </td>
                                <td class="leftAligned" style="white-space: normal;">
                                    <asp:Label ID="uoLabelE1ID" runat="server" Text='<%# Eval("SfID")%>' Width="57px"></asp:Label>
                                </td>
                                <td class="leftAligned" style="white-space: normal;">
                                    <asp:Label ID="uoLabelName" runat="server" Text='<%# Eval("Name")%>' Width="247px"></asp:Label>
                                </td>
                                <td class="leftAligned" style="white-space: normal;">
                                    <asp:Label ID="uoLabelOnOffDate" runat="server" Width="77px" Text='<%# String.Format("{0:dd-MMM-yyyy}", Eval("OnOffDate"))%>'></asp:Label>
                                </td>
                                <td class="leftAligned" style="white-space: normal;">
                                    <asp:Label ID="uoLabelPortName" runat="server" Text='<%# Eval("PortName")%>' Width="197px"></asp:Label>
                                </td>
                                <td class="leftAligned" style="white-space: normal;">
                                    <asp:Label ID="uoLabelBrandName" runat="server" Text='<%# Eval("BrandName")%>' Width="197px"></asp:Label>
                                </td>
                                <td class='<%# (HideVessel()==""?"leftAligned":HideVessel()) %>' style="white-space: normal;">
                                    <asp:Label ID="uoLabelVesselName" runat="server" Text='<%# Eval("VesselName")%>'
                                        Width="197px"></asp:Label>
                                </td>
                                <td class="leftAligned" style="white-space: normal;">
                                    <asp:Label ID="uoLabelRankName" runat="server" Text='<%# Eval("RankName")%>' Width="197px"></asp:Label>
                                </td>
                                <td class='<%# (HideHotel()==""?"leftAligned":HideHotel()) %>' style="white-space: normal;">
                                    <asp:Label ID="uoLabelHotelName" runat="server" Text='<%# Eval("HotelName")%>' Width="197px"></asp:Label>
                                </td>
                                <td style="white-space: normal;">
                                    <asp:Label ID="uoLabelStatus" runat="server" Text='<%# Eval("Status")%>' Width="60px"></asp:Label>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <EmptyDataTemplate>
                            <table class="listViewTable">
                                <tr>
                                    <td colspan="10" class="leftAligned">
                                        <asp:Label runat="server" ID="uoLblScroll" Width="1342px" Text="No Record"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
                    </asp:ListView>
                    <asp:DataPager ID="uoListViewRequestPager" runat="server" PagedControlID="uoListViewRequest"
                        PageSize="20" OnPreRender="uoListViewRequest_PreRender">
                        <Fields>
                            <asp:NumericPagerField ButtonType="link" NumericButtonCssClass="PagerClass" />
                        </Fields>
                    </asp:DataPager>
                    <asp:HiddenField ID="uoHiddenFieldPopEditor" runat="server" Value="0" />
                    <asp:HiddenField ID="uoHiddenFieldRole" runat="server" Value="" />
                    <asp:HiddenField ID="uoHiddenFieldDate" runat="server" Value="" />
                    <asp:HiddenField ID="uoHiddenFieldDateRange" runat="server" Value="" />
                    <asp:HiddenField ID="uoHiddenFieldUser" runat="server" Value="" />
                </div>
            </ContentTemplate>
            <%-- <Triggers>
        <asp:AsyncPostBackTrigger ControlID="uoButtonView" EventName="Click" />
        <asp:AsyncPostBackTrigger ControlID="uoButtonViewAll" EventName="Click" />
        <asp:AsyncPostBackTrigger ControlID="uoButtonApproved" EventName="Click" />
    </Triggers>--%>
        </asp:UpdatePanel>
    </div>
</asp:Content>

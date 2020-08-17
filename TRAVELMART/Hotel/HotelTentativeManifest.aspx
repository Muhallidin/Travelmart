<%@ Page Title="" Language="C#" MasterPageFile="~/TravelMartMaster2.Master" AutoEventWireup="true"
    CodeBehind="HotelTentativeManifest.aspx.cs" Inherits="TRAVELMART.Hotel.HotelTentativeManifest" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" runat="server">
    <div class="PageTitle">
        Hotel Tentative Manifest</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="NaviPlaceHolder" runat="server">

    <script type="text/javascript" language="javascript">
        function CloseModal(strURL) {
            window.location = strURL;
        }

        function confirmEmail() {
            if (confirm("Email record?") == true)
                return true;
            else
                return false;
        }
        function confirmLock() {
            if ($("#<%=uoDropDownListHotel.ClientID %>").val() == "0") {
                alert("Select Hotel Branch");
                return false;
            }
            if (confirm("Lock record?") == true)
                return true;
            else
                return false;
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
    </script>

    <script type="text/javascript" language="javascript">
        $(document).ready(function() {
            SetTMResolution();
            filterSettings();
            ShowPopup();
            ShowSearchPopup();
        });

        function pageLoad(sender, args) {
            var isAsyncPostback = Sys.WebForms.PageRequestManager.getInstance().get_isInAsyncPostBack();
            if (isAsyncPostback) {
                SetTMResolution();
                filterSettings();
                ShowPopup();
                ShowSearchPopup();
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
        }

        function SetTMResolution() {
            var ht = $(window).height();
            var ht2 = $(window).height();

            var wd = $(window).width() * 0.90;

            if (screen.height <= 600) {
                ht = ht * 0.22;
                ht2 = ht2 * 0.46;
                wd = $(window).width();
            }
            else if (screen.height <= 720) {
                ht = ht * 0.28;
                ht2 = ht2 * 0.59;
            }
            else {
                ht = ht * 0.335;
                ht2 = ht2 * 0.62;
            }

            $("#Bv").height(ht);
            $("#PG").height(ht2);
            $("#Av").width(wd);
            $("#Bv").width(wd);
            $("#PG").width(wd);

        }

        function divScrollL() {
            var Right = document.getElementById('Av');
            var Left = document.getElementById('Bv');
            Right.scrollLeft = Left.scrollLeft;
        }
    </script>

    <div id="PG" style="width: auto; height: auto; overflow: auto;">
        <%--<asp:UpdatePanel runat="server" ID="uoUpdatePanel">
            <ContentTemplate>--%>
                <table width="100%" class="LeftClass">
                    <tr>
                        <td class="contentCaption">
                            Region:
                        </td>
                        <td class="contentValue">
                            <asp:DropDownList ID="uoDropDownListRegion" runat="server" Width="300px" AppendDataBoundItems="true"
                                AutoPostBack="true" OnSelectedIndexChanged="uoDropDownListRegion_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="contentCaption">
                            Port:
                        </td>
                        <td class="contentValue">
                            <asp:DropDownList ID="uoDropDownListPortPerRegion" runat="server" Width="300px" AppendDataBoundItems="true"
                                AutoPostBack="true" OnSelectedIndexChanged="uoDropDownListPortPerRegion_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="contentCaption">
                            Hotel Branch:
                        </td>
                        <td class="contentValue">
                            <asp:DropDownList ID="uoDropDownListHotel" runat="server" Width="300px" AppendDataBoundItems="True"
                                AutoPostBack="True" OnSelectedIndexChanged="uoDropDownListHotel_SelectedIndexChanged">
                                <asp:ListItem Value="0">--SELECT HOTEL--</asp:ListItem>
                            </asp:DropDownList>
                            <cc1:ListSearchExtender ID="ListSearchExtender_uoDropDownListBranch" runat="server"
                                TargetControlID="uoDropDownListHotel" PromptText="Type to search" PromptPosition="Top"
                                IsSorted="true" PromptCssClass="dropdownSearch" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="uoDropDownListHotel"
                                Enabled="False" ErrorMessage="Required Hotel" InitialValue="0">*</asp:RequiredFieldValidator>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="contentCaption">
                            Manifest Type:
                        </td>
                        <td class="contentValue">
                            <asp:DropDownList ID="uoDropDownListHours" runat="server" Width="300px">
                                <asp:ListItem Value="24">24 Hrs</asp:ListItem>
                                <asp:ListItem Value="48">48 Hrs</asp:ListItem>
                                <asp:ListItem Value="72">72 Hrs</asp:ListItem>
                                <%--<asp:ListItem Value="192">8 Days</asp:ListItem>
                                <asp:ListItem Value="384">16 Days</asp:ListItem>--%>
                            </asp:DropDownList>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                </table>
                <table width="100%" class="LeftClass">
                    <tr>
                        <td class="contentValue">
                            <asp:CheckBox ID="uoCheckBoxAdvanceSearch" Text="View Advance Search" runat="server" />
                        </td>
                    </tr>
                </table>
                <table width="100%" class="LeftClass" id="uoTableAdvanceSearch" runat="server">
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
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="contentCaption">
                            Filter By:
                        </td>
                        <td class="contentValue">
                            <asp:DropDownList ID="uoDropDownListFilterBy" runat="server" Width="300px">
                                <asp:ListItem Value="2">EMPLOYEE ID</asp:ListItem>
                                <asp:ListItem Value="1">SEAFARER NAME</asp:ListItem>
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
                            <asp:Button ID="uoButtonView" runat="server" Text="View" CssClass="SmallButton" OnClick="uoButtonView_Click" />
                            &nbsp;
                            <asp:Button ID="uoButtonClear" runat="server" Text="Clear Filter" CssClass="SmallButton"
                                OnClick="uoButtonClear_Click" />
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
                <table width="100%" class="LeftClass">
                    <tr>
                        <td class="contentCaption">
                            <asp:Button ID="uoButtonView2" runat="server" CssClass="SmallButton" Visible="false"
                                Text="View" />
                            <asp:Button ID="uoBtnExportList" runat="server" CssClass="SmallButton" Text="Export Tentative Manifest"
                                OnClick="uoBtnExportList_Click" />
                        </td>
                        <td class="LeftClass">
                            <%--<asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>--%>
                                    <asp:Button ID="uoButtonLock" runat="server" Text="Lock" CssClass="SmallButton" OnClientClick="javascript: return confirmLock(); "
                                        Width="100px" OnClick="uoButtonLock_Click" />
                                <%--</ContentTemplate>
                            </asp:UpdatePanel>--%>
                        </td>
                    </tr>
                </table>
                <span style="font-weight:bold; font-size:larger;"><asp:Label runat="server" id="uoLabelHeader"></asp:Label></span>
                <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>--%>
                        <div id="Av" style="overflow: auto; overflow-x: hidden; width: 100%; overflow-y: hidden;">
                            <asp:ListView runat="server" ID="ListView1" onitemcommand="ListView1_ItemCommand">
                                <LayoutTemplate>
                                </LayoutTemplate>
                                <ItemTemplate>
                                </ItemTemplate>
                                <EmptyDataTemplate>
                                    <table class="listViewTable">
                                        <tr>
                                            <th style="text-align: center; white-space: normal;">
                                                <%--<asp:Label runat="server" ID="uoLblTRHeader" Text="Travel Request No." Width="60px"></asp:Label>--%>
                                                <asp:LinkButton ID="uoLblTRHeader" runat="server" CommandName="SortByTR" Text="Travel Request No." Width="53px"/>
                                            </th>
                                            <th style="text-align: center; white-space: normal;">
                                                <%--<asp:Label runat="server" ID="uoLblCoupleHeader" Text="Couple" Width="45px"></asp:Label>--%>
                                                <asp:LinkButton ID="uoLblCoupleHeader" runat="server" CommandName="SortByCouple" Text="Couple" Width="39px"/>
                                            </th>
                                            <th style="text-align: center; white-space: normal;">
                                                <%--<asp:Label runat="server" ID="uoLblGenderHeader" Text="Gender" Width="50px"></asp:Label>--%>
                                                <asp:LinkButton ID="uoLblGenderHeader" runat="server" CommandName="SortByGender" Text="Gender" Width="45px"/>
                                            </th>
                                            <th style="text-align: center; white-space: normal;">
                                                <%--<asp:Label runat="server" ID="uoLblNationalityHeader" Text="Nationality" Width="200px"></asp:Label>--%>
                                                <asp:LinkButton ID="uoLblNationalityHeader" runat="server" CommandName="SortByNationality" Text="Nationality" Width="195px"/>
                                            </th>
                                            <th style="text-align: center; white-space: normal;">
                                                <%--<asp:Label runat="server" ID="uoLblCCHeader" Text="Cost Center" Width="50px"></asp:Label>--%>
                                                <asp:LinkButton ID="uoLblCCHeader" runat="server" CommandName="SortByCost" Text="Cost Center" Width="43px"/>
                                            </th>
                                            <th style="text-align: center; white-space: normal;">
                                                <%--<asp:Label runat="server" ID="uoLblCheckInHeader" Text="Check-In" Width="70px"></asp:Label>--%>
                                                <asp:LinkButton ID="uoLblCheckInHeader" runat="server" CommandName="SortByCheckIn" Text="Check-In" Width="64px"/>
                                            </th>
                                            <th style="text-align: center; white-space: normal;">
                                                <%--<asp:Label runat="server" ID="uoLblCheckOutHeader" Text="Check-Out" Width="70px"></asp:Label>--%>
                                                <asp:LinkButton ID="uoLblCheckOutHeader" runat="server" CommandName="SortByCheckOut" Text="Check-Out" Width="64px"/>
                                            </th>
                                            <th style="text-align: center; white-space: normal;">
                                                <%--<asp:Label runat="server" ID="uoLblLastNameHeader" Text="Last Name" Width="100px"></asp:Label>--%>
                                                <asp:LinkButton ID="uoLblLastNameHeader" runat="server" CommandName="SortByLastName" Text="Last Name" Width="93px"/>
                                            </th>
                                            <th style="text-align: center; white-space: normal;">
                                                <%--<asp:Label runat="server" ID="uoLblFirstNameHeader" Text="First Name" Width="120px"></asp:Label>--%>
                                                <asp:LinkButton ID="uoLblFirstNameHeader" runat="server" CommandName="SortByFirstName" Text="First Name" Width="114px"/>
                                            </th>
                                            <th style="text-align: center; white-space: normal;">
                                                <%--<asp:Label runat="server" ID="uoLblEmpIDHeader" Text="Employee ID" Width="60px"></asp:Label>--%>
                                                <asp:LinkButton ID="uoLblEmpIDHeader" runat="server" CommandName="SortByEmployeeID" Text="Employee ID" Width="54px"/>
                                            </th>
                                            <th style="text-align: center; white-space: normal;">
                                                <%--<asp:Label runat="server" ID="uoLblShipHeader" Text="Ship" Width="170px"></asp:Label>--%>
                                                <asp:LinkButton ID="uoLblShipHeader" runat="server" CommandName="SortByShip" Text="Ship" Width="164px"/>
                                            </th>
                                            <th style="text-align: center; white-space: normal;">
                                                <%--<asp:Label runat="server" ID="uoLblHRHeader" Text="Hotel Request" Width="60px"></asp:Label>--%>
                                                <asp:LinkButton ID="uoLblHRHeader" runat="server" CommandName="SortByHotelRequest" Text="Hotel Request" Width="55px"/>
                                            </th>
                                            <th style="text-align: center; white-space: normal;">
                                                <%--<asp:Label runat="server" ID="" Text="Single/Double" Width="80px"></asp:Label>--%>
                                                <asp:LinkButton ID="uoLblRoomHeader" runat="server" CommandName="SortBySingleDouble" Text="Single/Double" Width="74px"/>
                                            </th>
                                            <th style="text-align: center; white-space: normal;">
                                                <%--<asp:Label runat="server" ID="uoLblTitleHeader" Text="Title" Width="220px"></asp:Label>--%>
                                                <asp:LinkButton ID="uoLblTitleHeader" runat="server" CommandName="SortByTitle" Text="Title" Width="215px"/>
                                            </th>
                                            <th style="text-align: center; white-space: normal;">
                                                <%--<asp:Label runat="server" ID="uoLblCityHeader" Text="Hotel City" Width="50px"></asp:Label>--%>
                                                <asp:LinkButton ID="uoLblCityHeader" runat="server" CommandName="SortByHotelCity" Text="Hotel City" Width="43px"/>
                                            </th>
                                            <th style="text-align: center; white-space: normal;">
                                                <%--<asp:Label runat="server" ID="uoLblNitesHeader" Text="Hotel Nites" Width="40px"></asp:Label>--%>
                                                <asp:LinkButton ID="uoLblNitesHeader" runat="server" CommandName="SortByHotelNites" Text="Hotel Nites" Width="34px"/>
                                            </th>
                                            <th style="text-align: center; white-space: normal;">
                                                <%--<asp:Label runat="server" ID="uoLblFCityHeader" Text="From City" Width="50px"></asp:Label>--%>
                                                <asp:LinkButton ID="uoLblFCityHeader" runat="server" CommandName="SortByFromCity" Text="From City" Width="44px"/>
                                            </th>
                                            <th style="text-align: center; white-space: normal;">
                                                <%--<asp:Label runat="server" ID="uoLblTCityHeader" Text="To City" Width="50px"></asp:Label>--%>
                                                <asp:LinkButton ID="uoLblTCityHeader" runat="server" CommandName="SortByToCity" Text="To City" Width="44px"/>
                                            </th>
                                            <th style="text-align: center; white-space: normal;">
                                                <%--<asp:Label runat="server" ID="uoLblALHeader" Text="A/L" Width="35px"></asp:Label>--%>
                                                <asp:LinkButton ID="uoLblALHeader" runat="server" CommandName="SortByAL" Text="A/L" Width="28px"/>
                                            </th>
                                            <th style="text-align: center; white-space: normal;">
                                                <%--<asp:Label runat="server" ID="uoLblRLHeader" Text="Record Locator" Width="60px"></asp:Label>--%>
                                                <asp:LinkButton ID="uoLblRLHeader" runat="server" CommandName="SortByRecordLocator" Text="Record Locator" Width="55px"/>
                                            </th>
                                            <th style="text-align: center; white-space: normal;">
                                                <%--<asp:Label runat="server" ID="uoLblPNumHeader" Text="Passport No." Width="60px"></asp:Label>--%>
                                                <asp:LinkButton ID="uoLblPNumHeader" runat="server" CommandName="SortByPassportNo" Text="Passport No." Width="57px"/>
                                            </th>
                                            <th style="text-align: center; white-space: normal;">
                                                <%--<asp:Label runat="server" ID="uoLblIssuedHeader" Text="Date Issued" Width="65px"></asp:Label>--%>
                                                <asp:LinkButton ID="uoLblIssuedHeader" runat="server" CommandName="SortByDateIssued" Text="Date Issued" Width="60px"/>
                                            </th>
                                            <th style="text-align: center; white-space: normal;">
                                                <%--<asp:Label runat="server" ID="uoLblExpHeader" Text="Passport Expiration" Width="65px"></asp:Label>--%>
                                                <asp:LinkButton ID="uoLblExpHeader" runat="server" CommandName="SortByPassportExpiration" Text="Passport Expiration" Width="59px"/>
                                            </th>
                                            <th style="text-align: center; white-space: normal;">
                                                <%--<asp:Label runat="server" ID="uoLblDDateHeader" Text="Dept Date" Width="65px"></asp:Label>--%>
                                                <asp:LinkButton ID="uoLblDDateHeader" runat="server" CommandName="SortByDeptDate" Text="Dept Date" Width="59px"/>
                                            </th>
                                            <th style="text-align: center; white-space: normal;">
                                                <%--<asp:Label runat="server" ID="uoLblADateHeader" Text="Arvl Date" Width="65px"></asp:Label>--%>
                                                <asp:LinkButton ID="uoLblADateHeader" runat="server" CommandName="SortByArvlDate" Text="Arvl Date" Width="59px"/>
                                            </th>
                                            <th style="text-align: center; white-space: normal;">
                                                <%--<asp:Label runat="server" ID="uoLblDCityHeader" Text="Dept City" Width="50px"></asp:Label>--%>
                                                <asp:LinkButton ID="uoLblDCityHeader" runat="server" CommandName="SortByDeptCity" Text="Dept City" Width="45px"/>
                                            </th>
                                            <th style="text-align: center; white-space: normal;">
                                                <%--<asp:Label runat="server" ID="uoLblACityHeader" Text="Arvl City" Width="50px"></asp:Label>--%>
                                                <asp:LinkButton ID="uoLblACityHeader" runat="server" CommandName="SortByArvlCity" Text="Arvl City" Width="45px"/>
                                            </th>
                                            <th style="text-align: center; white-space: normal;">
                                                <%--<asp:Label runat="server" ID="uoLblDTimeHeader" Text="Dept Time" Width="60px"></asp:Label>--%>
                                                <asp:LinkButton ID="uoLblDTimeHeader" runat="server" CommandName="SortByDeptTime" Text="Dept Time" Width="53px"/>
                                            </th>
                                            <th style="text-align: center; white-space: normal;">
                                                <%--<asp:Label runat="server" ID="uoLblATimeHeader" Text="Arvl Time" Width="60px"></asp:Label>--%>
                                                <asp:LinkButton ID="uoLblATimeHeader" runat="server" CommandName="SortByArvlTime" Text="Arvl Time" Width="53px"/>
                                            </th>
                                            <th style="text-align: center; white-space: normal;">
                                                <%--<asp:Label runat="server" ID="uoLblCarrierHeader" Text="Carrier" Width="40px"></asp:Label>--%>
                                                <asp:LinkButton ID="uoLblCarrierHeader" runat="server" CommandName="SortByCarrier" Text="Carrier" Width="35px"/>
                                            </th>
                                            <th style="text-align: center; white-space: normal;">
                                                <%--<asp:Label runat="server" ID="uoLblFlightNumHeader" Text="Flight No." Width="50px"></asp:Label>--%>
                                                <asp:LinkButton ID="uoLblFlightNumHeader" runat="server" CommandName="SortByFlightNo" Text="Flight No." Width="44px"/>                                            
                                            </th>
                                            <th style="text-align: center; white-space: normal;">
                                                <%--<asp:Label runat="server" ID="uoLblOnOffHeader" Text="On/Off" Width="80px"></asp:Label>--%>
                                                <asp:LinkButton ID="uoLblOnOffHeader" runat="server" CommandName="SortByOnOff" Text="On/Off" Width="74px"/>
                                            </th>
                                            <th style="text-align: center; white-space: normal;">
                                                <%--<asp:Label runat="server" ID="uoLblVoucherHeader" Text="Voucher" Width="50px"></asp:Label>--%>
                                                <asp:LinkButton ID="uoLblVoucherHeader" runat="server" CommandName="SortByVoucher" Text="Voucher" Width="44px"/>
                                            </th>
                                            <th style="text-align: center; white-space: normal;">
                                                <%--<asp:Label runat="server" ID="uoLblTDateHeader" Text="Travel Date" Width="70px"></asp:Label>--%>
                                                <asp:LinkButton ID="uoLblTDateHeader" runat="server" CommandName="SortByTravelDate" Text="Travel Date" Width="64px"/>
                                            </th>
                                            <th style="text-align: center; white-space: normal;">
                                                <%--<asp:Label runat="server" ID="uoLblReasonHeader" Text="Reason" Width="50px"></asp:Label>--%>
                                                <asp:LinkButton ID="uoLblReasonHeader" runat="server" CommandName="SortByReason" Text="Reason" Width="44px"/>
                                            </th>                                           
                                             <th style="text-align: center; white-space: normal;">
                                                <%--<asp:Label runat="server" ID="Label4" Text="Booking Remarks" Width="70px"></asp:Label>--%>
                                                <asp:LinkButton ID="uoLblBookingRemarksHeader" runat="server" CommandName="SortByBookingRemarks" Text="Booking Remarks" Width="65px"/>
                                            </th>
                                             <th style="text-align: center; white-space: normal;">
                                                <asp:Label runat="server" ID="Label2" Text="" Width="25px"></asp:Label>
                                            </th>
                                        </tr>
                                    </table>
                                </EmptyDataTemplate>
                            </asp:ListView>
                        </div>
                        <div id="Bv" style="overflow: auto; width: 100%; overflow-x: scroll; overflow-y: auto;"
                            onscroll="divScrollL();">
                            <asp:ListView runat="server" ID="uolistviewHotelInfo" 
                                DataSourceID="uoObjectDataSourceManifest" 
                                ondatabound="uolistviewHotelInfo_DataBound" >
                                <LayoutTemplate>
                                    <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                                        <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                                    </table>
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <%# HotelAddGroup()%>
                                    <tr>
                                        <td class="leftAligned" style="white-space: normal;">
                                            <asp:Label runat="server" ID="uoLblTR" Text='<%# Eval("E1TravelReqId")%>' Width="60px"></asp:Label>
                                        </td>
                                        <td class="leftAligned" style="white-space: normal;">
                                            <asp:Label runat="server" ID="uoLblCouple" Text='<%# Eval("Couple")%>' Width="45px"></asp:Label>
                                        </td>
                                        <td class="leftAligned" style="white-space: normal;">
                                            <asp:Label runat="server" ID="uoLblGender" Text='<%# Eval("Gender")%>' Width="50px"></asp:Label>
                                        </td>
                                        <td class="leftAligned" style="white-space: normal;">
                                            <asp:Label runat="server" ID="uoLblNationality" Text='<%# Eval("Nationality")%>'
                                                Width="200px"></asp:Label>
                                        </td>
                                        <td class="leftAligned" style="white-space: normal;">
                                            <asp:Label runat="server" ID="uoLblCostCenter" Text='<%# Eval("CostCenter")%>' Width="50px"></asp:Label>
                                        </td>
                                        <td class="leftAligned" style="white-space: normal;">
                                            <asp:Label runat="server" ID="uoLblCheckIn" Text='<%# String.Format("{0:dd-MMM-yyyy}", Eval("colTimeSpanStartDate"))%>'
                                                Width="70px"></asp:Label>
                                        </td>
                                        <td class="leftAligned" style="white-space: normal;">
                                            <asp:Label runat="server" ID="uoLblCheckOut" Text='<%# String.Format("{0:dd-MMM-yyyy}", Eval("colTimeSpanEndDate"))%>'
                                                Width="70px"></asp:Label>
                                        </td>
                                         <td class="leftAligned" style="white-space: normal;">
                                            <asp:Label runat="server" ID="uoLabelLast" Text='<%# Eval("LastName")%>' Width="100px"></asp:Label>
                                        </td>
                                        <td class="leftAligned" style="white-space: normal;">
                                            <asp:LinkButton Width="120px" runat="server" ID="SeafarerLinkButton" PostBackUrl='<%# "../SuperUserView.aspx?sfId=" + Eval("sfID") + "&trID=" + Eval("colTravelReqIdInt") + "&st=" + Eval("Status") + "&recloc=" + Eval("RecLoc") + "&manualReqID=" + Eval("RequestID") + "&p=" + Request.QueryString["p"]+ "&ufn=" + Request.QueryString["ufn"] + "&ID=" + Eval("IDBigInt") + "&from=" + Request.QueryString["from"] + "&to=" + Request.QueryString["to"] + "&dt=" + Request.QueryString["dt"]%>'><%# Eval("FirstName") %></asp:LinkButton>
                                            <%--<asp:LinkButton runat="server" ID="SeafarerLinkButton" Text='<%# Eval("Name") %>' OnClick="SeafarerLinkButton_Click"></asp:LinkButton>--%>
                                        </td>
                                        <td class="leftAligned" style="white-space: normal;">
                                            <asp:Label runat="server" ID="uoLblSfID" Text='<%# Eval("SfID")%>' Width="60px"></asp:Label>
                                        </td>
                                        <td class="leftAligned" style="white-space: normal;">
                                            <asp:Label runat="server" ID="uoLblVessel" Text='<%# Eval("colVesselCodeVarchar") + " - " + Eval("Vessel")%>'
                                                Width="170px"></asp:Label>
                                        </td>
                                        <td class="leftAligned" style="white-space: normal;">
                                            <asp:Label runat="server" ID="uoLblHRequest" Text='<%# Eval("HotelRequest")%>' Width="60px"></asp:Label>
                                        </td>
                                        <td class="leftAligned" style="white-space: normal;">
                                            <asp:Label runat="server" ID="uoLblRoom" Text='<%# Eval("colRoomNameVarchar")%>'
                                                Width="80px"></asp:Label>
                                        </td>
                                        <td class="leftAligned" style="white-space: normal;">
                                            <asp:Label runat="server" ID="uoLblRank" Text='<%# Eval("Rank")%>' Width="220px"></asp:Label>
                                        </td>
                                        <td class="leftAligned" style="white-space: normal;">
                                            <%--<%# Eval("HotelCity")%>--%>
                                            <asp:Label ID="Label1" runat="server" Width="50px" Text='<%# Eval("HotelCity") %>'></asp:Label>
                                            <asp:Label ID="uoLabelWithEvent" runat="server" Text="*" Visible='<%# Eval("WithEvent") %>'
                                                ForeColor="red" Font-Bold="true" Font-Size="Large"></asp:Label>
                                        </td>
                                        <td class="leftAligned" style="white-space: normal;">
                                            <asp:Label runat="server" ID="uoLblNites" Text='<%# Eval("colTimeSpanDurationInt")%>'
                                                Width="40px"></asp:Label>
                                        </td>
                                        <td class="leftAligned" style="white-space: normal;">
                                            <asp:Label runat="server" ID="uoLblFromCity" Text='<%# Eval("FromCity")%>' Width="50px"></asp:Label>
                                        </td>
                                        <td class="leftAligned" style="white-space: normal;">
                                            <asp:Label runat="server" ID="uoLblToCity" Text='<%# Eval("ToCity")%>' Width="50px"></asp:Label>
                                        </td>
                                        <td class="leftAligned" style="white-space: normal;">
                                            <asp:Label runat="server" ID="uoLblAL" Text='<%# Eval("AL")%>' Width="35px"></asp:Label>
                                        </td>
                                        <td class="leftAligned" style="white-space: normal;">
                                            <asp:Label runat="server" ID="uoLblRecLoc" Text='<%# Eval("RecLoc")%>' Width="60px"></asp:Label>
                                        </td>
                                        <td class="leftAligned" style="white-space: normal;">
                                            <asp:Label runat="server" ID="uoLblPNum" Text='<%# Eval("colPassportNoVarchar")%>'
                                                Width="65px"></asp:Label>
                                        </td>
                                        <td class="leftAligned" style="white-space: normal;">
                                            <asp:Label runat="server" ID="uoLblIssuedBy" Text='<%# Eval("colPassportIssuedDate")%>'
                                                Width="65px"></asp:Label>
                                        </td>
                                        <td class="leftAligned" style="white-space: normal;">
                                            <asp:Label runat="server" ID="uoLblExp" Text='<%# Eval("colPassportExpirationDate")%>'
                                                Width="65px"></asp:Label>
                                            <%--<asp:Label runat="server" ID="uoLblExp" Text='<%# String.Format("{0:dd-MMM-yyyy}", Eval("colPassportExpirationDate"))%>'
                                                Width="65px"></asp:Label>--%>
                                        </td>
                                        <td class="leftAligned" style="white-space: normal;">
                                            <asp:Label runat="server" ID="uoLblDepDate" Text='<%# String.Format("{0:dd-MMM-yyyy}", Eval("colDepartureDateTime"))%>'
                                                Width="65px"></asp:Label>
                                        </td>
                                        <td class="leftAligned" style="white-space: normal;">
                                            <asp:Label runat="server" ID="uoLblArDate" Text='<%# String.Format("{0:dd-MMM-yyyy}", Eval("colArrivalDateTime"))%>'
                                                Width="65px"></asp:Label>
                                        </td>
                                        <td class="leftAligned" style="white-space: normal;">
                                            <asp:Label runat="server" ID="uoLblDeptCity" Text='<%# Eval("DeptCity")%>' Width="50px"></asp:Label>
                                        </td>
                                        <td class="leftAligned" style="white-space: normal;">
                                            <asp:Label runat="server" ID="uoLblArvlCity" Text='<%# Eval("ArvlCity")%>' Width="50px"></asp:Label>
                                        </td>
                                        <td class="leftAligned" style="white-space: normal;">
                                            <asp:Label runat="server" ID="uoLblDepartureTime" Text='<%# Eval("DepartureTime")%>'
                                                Width="60px"></asp:Label>
                                        </td>
                                        <td class="leftAligned" style="white-space: normal;">
                                            <asp:Label runat="server" ID="uoLblArrivalTime" Text='<%# Eval("ArrivalTime")%>'
                                                Width="60px"></asp:Label>
                                        </td>
                                        <td class="leftAligned" style="white-space: normal;">
                                            <asp:Label runat="server" ID="uoLblCarrier" Text='<%# Eval("Carrier")%>' Width="40px"></asp:Label>
                                        </td>
                                        <td class="leftAligned" style="white-space: normal;">
                                            <asp:Label runat="server" ID="uoLblFN" Text='<%# Eval("colFlightNoVarchar")%>' Width="50px"></asp:Label>
                                        </td>
                                        <td class="leftAligned" style="white-space: normal;">
                                            <asp:Label runat="server" ID="uoLblSignOn" Text='<%# String.Format("{0:dd-MMM-yyyy}", Eval("SignOn"))%>'
                                                Width="80px"></asp:Label>
                                        </td>
                                        <td class="leftAligned" style="white-space: normal;">
                                            <asp:Label runat="server" ID="uoLblVoucher" Text='<%# String.Format("{0:0.00}", Eval("Voucher")) %>'
                                                Width="50px"></asp:Label>
                                        </td>
                                        <td class="leftAligned" style="white-space: normal;">
                                            <asp:Label runat="server" ID="uoLblTravelDate" Text='<%# String.Format("{0:dd-MMM-yyyy}", Eval("TravelDate"))%>'
                                                Width="70px"></asp:Label>
                                        </td>
                                        <td class="leftAligned" style="white-space: normal;">
                                            <asp:Label runat="server" ID="uoLblReason" Text='<%# Eval("Reason")%>' Width="50px"></asp:Label>
                                            <asp:HiddenField ID="uoHiddenFieldsfID" runat="server" Value="" />
                                            <asp:HiddenField ID="uoHiddenFieldcolTravelReqIdInt" runat="server" Value="" />
                                            <asp:HiddenField ID="uoHiddenFieldStatus" runat="server" Value="" />
                                            <asp:HiddenField ID="uoHiddenFieldRecLoc" runat="server" Value="" />
                                            <asp:HiddenField ID="uoHiddenFieldRequestID" runat="server" Value="" />
                                            <asp:HiddenField ID="uoHiddenFieldIDBigInt" runat="server" Value="" />
                                        </td>
                                        <td class="leftAligned" style="white-space: normal;">
                                            <asp:Label runat="server" ID="Label3" Text='<%# Eval("colRemarksVarchar") %>'
                                                Width="70px"></asp:Label>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <EmptyDataTemplate>
                                    <table class="listViewTable">
                                        <%--<tr>
                                        <td colspan="33">
                                            <asp:Label runat="server" ID="uoLblScroll" Width="2640px"></asp:Label>
                                        </td>
                                    </tr>--%>
                                        <tr>
                                            <td colspan="33" class="leftAligned">
                                                No Record
                                            </td>
                                        </tr>
                                    </table>
                                </EmptyDataTemplate>
                            </asp:ListView>
                            <div align="left">
                                <asp:DataPager ID="uolistviewHotelInfoPager" runat="server" PagedControlID="uolistviewHotelInfo"
                                    PageSize="50">
                                    <Fields>
                                        <asp:NumericPagerField ButtonType="link" NumericButtonCssClass="PagerClass" />
                                    </Fields>
                                </asp:DataPager>
                                <asp:HiddenField ID="uoHiddenFieldDate" runat="server" />
                                <asp:HiddenField ID="uoHiddenFieldRole" runat="server" />
                                <asp:HiddenField ID="uoHiddenFieldUser" runat="server" />
                                <asp:ObjectDataSource ID="uoObjectDataSourceManifest" runat="server" MaximumRowsParameterName="MaxRow"
                                    SelectCountMethod="GetTentativeManifestCount" SelectMethod="GetTentativeManifestList2"
                                    StartRowIndexParameterName="StartRow" TypeName="TRAVELMART.BLL.ManifestBLL" OldValuesParameterFormatString="oldcount_{0}"
                                    EnablePaging="True" OnSelecting="uoObjectDataSourceManifest_Selecting">
                                    <SelectParameters>
                                        <asp:Parameter Name="DateFromString" Type="String" />
                                        <asp:Parameter Name="strUser" Type="String" />
                                        <asp:Parameter Name="DateFilter" Type="String" />
                                        <asp:Parameter Name="ByNameOrID" Type="String" />
                                        <asp:Parameter Name="filterNameOrID" Type="String" />
                                        <asp:Parameter Name="Nationality" Type="String" />
                                        <asp:Parameter Name="Gender" Type="String" />
                                        <asp:Parameter Name="Rank" Type="String" />
                                        <asp:Parameter Name="Status" Type="String" />
                                        <asp:Parameter Name="Region" Type="String" />
                                        <asp:Parameter Name="Country" Type="String" />
                                        <asp:Parameter Name="City" Type="String" />
                                        <asp:Parameter Name="Port" Type="String" />
                                        <asp:Parameter Name="Hotel" Type="String" />
                                        <asp:Parameter Name="Vessel" Type="String" />
                                        <asp:Parameter Name="UserRole" Type="String" />
                                        <asp:Parameter Name="LoadType" Type="String" />
                                        <asp:Parameter Name="SortBy" Type="String" />
                                    </SelectParameters>
                                </asp:ObjectDataSource>
                            </div>
                        </div>
                    <%--</ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="uoButtonView" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>--%>
            <%--</ContentTemplate>
        </asp:UpdatePanel>--%>
    </div>
    <asp:HiddenField runat="server" ID="uoHiddenFieldSortBy" EnableViewState="true" Value="" />
</asp:Content>

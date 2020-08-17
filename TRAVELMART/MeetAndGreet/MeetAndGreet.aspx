<%@ Page Title="" Language="C#" MasterPageFile="~/TravelMartMaster2.Master" AutoEventWireup="true" 
    CodeBehind="MeetAndGreet.aspx.cs" Inherits="TRAVELMART.MeetAndGreet" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" runat="server">
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr class="PageTitle">
            <td align="left" style="height: 25px">
                <asp:Label ID="uoLabelTitle" runat="server" class="Title" Text=""></asp:Label>
            </td>
            <td align="right" style="white-space: nowrap">
                <asp:RadioButtonList ID="uoRadioButtonListPort" runat="server" RepeatDirection="Horizontal"
                    AutoPostBack="true" OnSelectedIndexChanged="uoRadioButtonListPort_SelectedIndexChanged">
                    <asp:ListItem Value="1">Airport</asp:ListItem>
                    <asp:ListItem Value="2">Seaport</asp:ListItem>
                </asp:RadioButtonList>
                <asp:Label ID="ucLabelPort" runat="server" class="Title" Text="AirportSeaport"></asp:Label>
            </td>
            <td align="right" style="width: 100px">
                <asp:DropDownList ID="uoDropDownListPort" runat="server" Width="300px" AutoPostBack="true"
                    OnSelectedIndexChanged="uoDropDownListPort_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="NaviPlaceHolder" runat="server">

    <script type="text/javascript" language="javascript">
        $(document).ready(function() {
            SetTRResolution();
            filterSettings();
            ShowPopup();
            SetClientTime();
        });

        function pageLoad(sender, args) {
            var isAsyncPostback = Sys.WebForms.PageRequestManager.getInstance().get_isInAsyncPostBack();
            if (isAsyncPostback) {
                SetTRResolution();
                filterSettings();
                ShowPopup();
                SetClientTime();
            }
        }
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
                ht = ht * 0.52;
                ht2 = ht2 * 0.62;
            }
            $("#Av").width(wd);
            $("#Bv").height(ht);
            $("#Bv").width(wd);

            $("#AvSea").width(wd);
            $("#BvSea").height(ht);
            $("#BvSea").width(wd);

            $("#PG").height(ht2);
            $("#PG").width(wd);
        }

        function SetClientTime() {
            $(".BtnTag").click(function() {
                var d = new Date();
                $("#<%=uoHiddenFieldTagTime.ClientID %>").val(d.getMonth() + 1 + "/" + d.getDate() + "/" + d.getFullYear() + " " + d.getHours() + ":" + d.getMinutes() + ":" + d.getSeconds());
            });
        }

        function divScrollL(sSource) {
            if (sSource == 'Air') {
                var Right = document.getElementById('Av');
                var Left = document.getElementById('Bv');
            }
            else {
                var Right = document.getElementById('AvSea');
                var Left = document.getElementById('BvSea');
            }
            Right.scrollLeft = Left.scrollLeft;
        }

        function confirmTag(e1ID, seafarerName) {
            var sMsg = "Tag " + e1ID + ": " + seafarerName + "? ";
            var x = confirm(sMsg);
            return x;
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

    <div id="PG" style="width: auto; height: auto; overflow: auto;">
        <table class="LeftClass" align="left" width="100%" style="margin-left: 0px;">
            <tr>
                <td style="width: 30%; text-align: left; white-space: nowrap">
                    <asp:CheckBox ID="uoCheckBoxAdvanceSearch" Text="View Advance Search" runat="server" />
                    <asp:Button ID="uoButtonExport" runat="server" Text="Export Manifest" CssClass="SmallButton"
                        OnClick="uoButtonExport_Click" />
                </td>
                <td style="width: 40%; text-align: center; vertical-align: middle; white-space: nowrap">
                    <table style="text-align: center">
                        <tr>
                            <td>
                                View:
                            </td>
                            <td>
                                <asp:RadioButtonList ID="uoRadioButtonListView" runat="server" RepeatDirection="Horizontal"
                                    AutoPostBack="true" OnSelectedIndexChanged="uoRadioButtonListView_SelectedIndexChanged">
                                    <asp:ListItem Value="1">All</asp:ListItem>
                                    <asp:ListItem Value="2">Tagged</asp:ListItem>
                                    <asp:ListItem Value="3">Untagged</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                    </table>
                </td>
                <td style="width: 30%; text-align: left; white-space: nowrap">
                    <table width="100%">
                        <tr>
                            <td>
                                Total Count:&nbsp;<asp:Label Font-Bold="true" ID="uoLabelTotalCount" runat="server"
                                    Text="0"></asp:Label>
                            </td>
                            <td>
                                Tagged Count:&nbsp;<asp:Label Font-Bold="true" ID="uoLabelTaggedCount" runat="server"
                                    Text="0"></asp:Label>
                            </td>
                            <td>
                                Untagged Count:&nbsp;<asp:Label Font-Bold="true" ID="uoLabelUntaggedCount" runat="server"
                                    Text="0"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <table width="90%" class="LeftClass" id="uoTableAdvanceSearch" runat="server" style="margin-left: 15px;">
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
            <tr runat="server" id="uoTRStatus">
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
                </td>
            </tr>
        </table>
        <div>
            &nbsp</div>
        <div runat="server" id="uoDivAirport">
            <div id="Av" style="overflow: auto; overflow-x: hidden; width: 100%; overflow-y: hidden;">
                <asp:ListView runat="server" ID="ListViewHeaderAir" OnItemCommand="ListViewHeaderAir_ItemCommand">
                    <LayoutTemplate>
                    </LayoutTemplate>
                    <ItemTemplate>
                    </ItemTemplate>
                    <EmptyDataTemplate>
                        <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                            <tr>
                                <th style="text-align: center; white-space: normal;">
                                    <asp:LinkButton ID="LinkButton1" Text="Tagged" Width="39px" runat="server" CommandName="IsTaggedByUser" />
                                </th>
                                <th style="text-align: center; white-space: normal;">
                                    <asp:LinkButton ID="LinkButton2" Text="Hotel" runat="server" Width="210px" CommandName="OrderByHotel" />
                                </th>
                                <th style="text-align: center; white-space: normal;">
                                    <asp:LinkButton ID="LinkButton3" Text="Check In" runat="server" Width="90px" CommandName="OrderByCheckIn" />
                                </th>
                                <th style="text-align: center; white-space: normal;">
                                    <asp:LinkButton ID="LinkButton4" Text="Check Out" runat="server" Width="90px" CommandName="Checkout" />
                                </th>
                                <th style="text-align: center; white-space: normal;">
                                    <asp:LinkButton ID="LinkButton5" Text="Hotel Nites" runat="server" Width="65px" CommandName="Duration" />
                                </th>
                                <th style="text-align: center; white-space: normal;">
                                    <asp:LinkButton ID="LinkButton6" Text="Last Name" runat="server" Width="300px" CommandName="OrderByLastname" />
                                </th>
                                <th style="text-align: center; white-space: normal;">
                                    <asp:LinkButton ID="LinkButton7" Text="First Name" runat="server" Width="130px" CommandName="OrderByFirstname" />
                                </th>
                                <th style="text-align: center; white-space: normal;">
                                    <asp:LinkButton ID="LinkButton8" Text="Employee ID" runat="server" Width="60px" CommandName="colSeafarerIdInt" />
                                </th>
                                <th style="white-space: normal;">
                                    <asp:LinkButton ID="LinkButton9" Text="Ship" runat="server" Width="182px" CommandName="OrderByVessel" />
                                </th>
                                <th style="text-align: center; white-space: normal;">
                                    <asp:LinkButton ID="LinkButton10" Text="Single / Double" runat="server" Width="60px"
                                        CommandName="colSingleDouble" />
                                </th>
                                <th style="text-align: center; white-space: normal;">
                                    <asp:LinkButton ID="LinkButton11" Text="Title" runat="server" Width="220px" CommandName="Rank" />
                                </th>
                                <th style="text-align: center; white-space: normal;">
                                    <asp:LinkButton ID="LinkButton12" Text="Gender" runat="server" Width="50px" CommandName="colGender" />
                                </th>
                                <th style="text-align: center; white-space: normal;">
                                    <asp:LinkButton ID="LinkButton13" Text="Cost Center" runat="server" Width="130px"
                                        CommandName="OrderByCostCenter" />
                                </th>
                                <th style="text-align: center; white-space: normal;">
                                    <asp:LinkButton ID="LinkButton14" Text="Nationality" runat="server" Width="70px"
                                        CommandName="OrderByNationality" />
                                </th>
                                <th style="text-align: center; white-space: normal;">
                                    <asp:LinkButton ID="LinkButton33" Text="Birthday" Width="70px" runat="server" CommandName="colBirthday" />
                                </th>
                                <th style="text-align: center; white-space: normal;">
                                    <asp:LinkButton ID="LinkButton15" Text="Meal Allowance" runat="server" Width="60px"
                                        CommandName="colMealAllowance" />
                                </th>
                                <th style="text-align: center; white-space: normal;">
                                    <asp:LinkButton ID="LinkButton16" Text="Rec Loc" runat="server" Width="50px" CommandName="OrderByRecLoc" />
                                </th>
                                <th style="text-align: center; white-space: normal;">
                                    <asp:LinkButton ID="LinkButton17" Text="Dept Date" runat="server" Width="80px" CommandName="colDepartureDateTime" />
                                </th>
                                <th style="text-align: center; white-space: normal;">
                                    <asp:LinkButton ID="LinkButton18" Text="Arvl Date" runat="server" Width="80px" CommandName="colArrivalDateTime" />
                                </th>
                                <th style="text-align: center; white-space: normal;">
                                    <asp:LinkButton ID="LinkButton19" Text="From City" runat="server" Width="60px" CommandName="AirportDeparture" />
                                </th>
                                <th style="text-align: center; white-space: normal;">
                                    <asp:LinkButton ID="LinkButton20" Text="To   City" runat="server" Width="40px" CommandName="AirportArrival" />
                                </th>
                                <th style="text-align: center; white-space: normal;">
                                    <asp:LinkButton ID="LinkButton21" Text="Dept Time" runat="server" Width="60px" CommandName="colDepartureDateTime" />
                                </th>
                                <th style="text-align: center; white-space: normal;">
                                    <asp:LinkButton ID="LinkButton22" Text="Arvl Time" runat="server" Width="50px" CommandName="colArrivalDateTime" />
                                </th>
                                <th style="text-align: center; white-space: normal;">
                                    <asp:LinkButton ID="LinkButton23" Text="Airline" runat="server" Width="50px" CommandName="Airline" />
                                </th>
                                <th style="text-align: center; white-space: normal;">
                                    <asp:LinkButton ID="LinkButton24" Text="Flight No" runat="server" Width="60px" CommandName="colFlightNoVarchar" />
                                </th>
                                <%--<th style="white-space: normal;">
                                <asp:Label runat="server" ID="Label8" Text="Port" Width="150px"></asp:Label>
                            </th>
                            <th style="text-align: center; white-space: normal;">
                                <asp:Label runat="server" ID="Label9" Text="Signon Date" Width="80px"></asp:Label>
                            </th>                           
                            <th style="text-align: center; white-space: normal;">
                                <asp:Label runat="server" ID="Label12" Text="Passport No" Width="100px"></asp:Label>
                            </th>
                            <th style="text-align: center; white-space: normal;">
                                <asp:Label runat="server" ID="Label23" Text="Expiration Date" Width="100px"></asp:Label>
                            </th>
                            <th style="text-align: center; white-space: normal;">
                                <asp:Label runat="server" ID="Label25" Text="Issued Date" Width="100px"></asp:Label>
                            </th>--%>
                                <th>
                                    <asp:Label runat="server" ID="Label11" Text="" Width="10px">
                                    </asp:Label>
                                </th>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                </asp:ListView>
            </div>
            <div id="Bv" style="overflow: auto; width: 100%; overflow-x: scroll; overflow-y: auto;"
                onscroll="divScrollL('Air');">
                <asp:ListView runat="server" ID="uoListViewTRAir" OnItemCommand="uoListViewTR_ItemCommand">
                    <LayoutTemplate>
                        <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                            <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                        </table>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <tr>
                            <td class="leftAligned" style="white-space: normal;">
                                <asp:HiddenField ID="uoHiddenFieldStatus" runat="server" Value='<%# Eval("Status") %>'/>
                                <asp:Label ID="Label26" Width="45px" runat="server" Text="Tagged" Visible='<%# Convert.ToBoolean(Eval("IsTaggedByUser")) %>'
                                    title='<%# "Tagged time is : " + Eval("TagDateTime") %>'></asp:Label>
                                <asp:LinkButton ID="uoLinkButtonTag" Width="45px" CssClass="PropertyLinkButtons BtnTag"
                                    CommandName="Tag" Visible='<%# !Convert.ToBoolean(Eval("IsTaggedByUser")) %>'
                                    OnClientClick='<%# "javascript:return confirmTag("+ Eval("SfID") + ", \"" + Eval("Name") + "\");" %>'
                                    CommandArgument='<%# Eval("IDBigInt") + ":" + Eval("TravelRequestID") + ":" + Eval("AirportID") + ":" + Eval("SeaportID") %>'
                                    runat="server" Text="Tag"></asp:LinkButton>
                            </td>
                            <td class="leftAligned" style="white-space: normal;">
                                <asp:Label runat="server" ID="uoLblReason" Width="216px" Text='<%# Eval("Hotel")%>'></asp:Label>
                            </td>
                            <td class="leftAligned" style="white-space: normal;">
                                <asp:Label runat="server" ID="Label25" Width="96px" Text='<%# String.Format("{0:dd-MMM-yyyy}", Eval("Checkin"))%>'></asp:Label>
                            </td>
                            <td class="leftAligned" style="white-space: normal;">
                                <asp:Label runat="server" ID="Label32" Width="96px" Text='<%# String.Format("{0:dd-MMM-yyyy}", Eval("Checkout"))%>'></asp:Label>
                            </td>
                            <td class="centerAligned" style="white-space: normal;">
                                <asp:Label runat="server" ID="Label33" Width="71px" Text='<%# Eval("Duration")%>'></asp:Label>
                            </td>
                            <td class="leftAligned" style="white-space: normal;">
                                <asp:Label runat="server" ID="Label54" Width="300px" Text='<%# Eval("Lastname")%>'></asp:Label>
                            </td>
                            <td class="leftAligned">
                                <asp:HyperLink ID="uoHyperLinkName" NavigateUrl='<%# "../SuperUserView.aspx?ufn=" + Request.QueryString["ufn"] + "&sfId=" + Eval("SfID") + "&recloc=&st=" + Eval("Status") + "&ID=0&trID="+ Eval("TravelRequestID") +"&manualReqID=0" + "&dt=" + Request.QueryString["dt"]%>'
                                    runat="server" Width="136px" Text='<%# Eval("Firstname")%>'></asp:HyperLink>
                            </td>
                            <td class="leftAligned" style="white-space: normal;">
                                <asp:Label runat="server" Width="66px" ID="ucLabelE1ID" Text='<%# Eval("SfID")%>'></asp:Label>
                            </td>
                            <td class="leftAligned">
                                <asp:Label runat="server" ID="uoLblVessel" Width="188px" Text='<%# Eval("Vessel")%>'></asp:Label>
                                <%--<asp:Image ID="uoImageSailMaster" ImageUrl="~/Images/Vessel.png" runat="server" ToolTip="With Sail Master"
                            Visible='<%# bool.Parse(Eval("IsWithSail").ToString()) %>' />
                        <asp:Label runat="server" ID="uoSpace" Width="18px" Text='' Visible='<%# !bool.Parse(Eval("IsWithSail").ToString()) %>'></asp:Label>--%>
                            </td>
                            <td class="centerAligned" style="white-space: normal;">
                                <asp:Label runat="server" ID="Label34" Width="66px" Text='<%# Eval("SingleDouble")%>'></asp:Label>
                            </td>
                            <td class="leftAligned" style="white-space: normal;">
                                <asp:Label runat="server" Width="226px" ID="Label20" Text='<%# Eval("Rank")%>'></asp:Label>
                            </td>
                            <td class="leftAligned" style="white-space: normal;">
                                <asp:Label runat="server" ID="Label35" Width="56px" Text='<%# Eval("Gender")%>'></asp:Label>
                            </td>
                            <td class="leftAligned" style="white-space: normal;">
                                <asp:Label runat="server" ID="Label36" Width="136px" Text='<%# Eval("CostCenter")%>'></asp:Label>
                            </td>
                            <td class="leftAligned" style="white-space: normal;">
                                <asp:Label runat="server" ID="Label37" Width="76px" Text='<%# Eval("Nationality")%>'></asp:Label>
                            </td>
                            <td class="leftAligned" style="white-space: normal;">
                                <asp:Label runat="server" ID="Label7" Width="76px" Text='<%#String.Format("{0:dd-MMM-yyyy}", Eval("Birthday"))%>'></asp:Label>
                            </td>
                            <td class="centerAligned" style="white-space: normal;">
                                <asp:Label runat="server" ID="Label38" Width="66px" Text='<%# Eval("MealAllowance")%>'></asp:Label>
                            </td>
                            <td class="leftAligned" style="white-space: normal;">
                                <asp:Label runat="server" ID="ucLabelRecloc" Text='<%# Eval("RecordLocator")%>' Width="56px"></asp:Label>
                            </td>
                            <td class="leftAligned" style="white-space: normal;">
                                <%--<asp:Label runat="server" ID="Label24" Width="86px" Text='<%# String.Format("{0:dd-MMM-yyyy}", Eval("DepartureDateTime"))%>'></asp:Label>--%>
                                <asp:Label runat="server" ID="Label24" Width="86px" Text='<%# Eval("DepartureDate")%>'></asp:Label>
                            </td>
                            <td class="leftAligned" style="white-space: normal;">
                                <asp:Label runat="server" ID="Label39" Width="86px" Text='<%# Eval("ArrivalDate")%>'></asp:Label>
                            </td>
                            <td class="centerAligned" style="white-space: normal;">
                                <asp:Label runat="server" ID="Label40" Width="66px" Text='<%# Eval("Departure")%>'></asp:Label>
                            </td>
                            <td class="centerAligned" style="white-space: normal;">
                                <asp:Label runat="server" Width="46px" ID="Label41" Text='<%# Eval("Arrival")%>'></asp:Label>
                            </td>
                            <td class="centerAligned" style="white-space: normal;">
                                <asp:Label runat="server" ID="Label42" Width="66px" Text='<%# Eval("DepartureTime")%>'></asp:Label>
                            </td>
                            <td class="centerAligned" style="white-space: normal;">
                                <asp:Label runat="server" ID="Label22" Width="56px" Text='<%# Eval("ArrivalTime")%>'></asp:Label>
                            </td>
                            <td class="centerAligned" style="white-space: normal;">
                                <asp:Label runat="server" Width="56px" ID="ucLabelFlightNo" Text='<%# Eval("Airline")%>'></asp:Label>
                            </td>
                            <td class="centerAligned" style="white-space: normal;">
                                <asp:Label runat="server" ID="Label43" Width="66px" Text='<%# Eval("FlightNo")%>'></asp:Label>
                            </td>
                            <%--<td class="leftAligned" style="white-space: normal;">
                        <asp:Label runat="server" Width="150px" ID="Label19" Text='<%# Eval("Port")%>'></asp:Label>
                    </td>
                    <td class="leftAligned" style="white-space: normal;">
                        <asp:Label runat="server" ID="uoLblOnOff" Width="80px" Text='<%# String.Format("{0:dd-MMM-yyyy}", Eval("DateOnOff"))%>'></asp:Label>
                    </td>                 
                    <td class="leftAligned" style="white-space: normal;">
                        <asp:Label runat="server" ID="Label18" Width="100px" Text='<%# Eval("PassportNo")%>'></asp:Label>
                    </td>
                    <td class="leftAligned" style="white-space: normal;">
                        <asp:Label runat="server" ID="Label27" Width="100px" Text='<%# Eval("PassportExpiryDate")%>'></asp:Label>
                    </td>
                    <td class="leftAligned" style="white-space: normal;">
                        <asp:Label runat="server" ID="Label28" Width="100px" Text='<%# Eval("PassportIssueDate")%>'></asp:Label>
                    </td>--%>
                        </tr>
                    </ItemTemplate>
                    <EmptyDataTemplate>
                        <table class="" width="100%">
                            <tr>
                                <th colspan="24">
                                    <asp:Label runat="server" ID="uoScroll" Width="2000px"></asp:Label>
                                </th>
                            </tr>
                            <tr>
                                <td colspan="23" class="LeftClass">
                                    No Record
                                </td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                </asp:ListView>
                <div style="text-align: left">
                    <asp:DataPager ID="uoListViewPagerAir" runat="server" PagedControlID="uoListViewTRAir"
                        OnPreRender="uoListViewPagerAir_PreRender" PageSize="20">
                        <Fields>
                            <asp:NumericPagerField ButtonType="link" NumericButtonCssClass="PagerClass" />
                        </Fields>
                    </asp:DataPager>
                </div>
            </div>
        </div>
        <div runat="server" id="uoDivSeaport">
            <div id="AvSea" style="overflow: auto; overflow-x: hidden; width: 100%; overflow-y: hidden;">
                <asp:ListView runat="server" ID="ListViewHeaderSea" OnItemCommand="ListViewHeaderSea_ItemCommand">
                    <LayoutTemplate>
                    </LayoutTemplate>
                    <ItemTemplate>
                    </ItemTemplate>
                    <EmptyDataTemplate>
                        <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                            <tr>
                                <%--<th style="text-align: center; white-space: normal;">
                                    <asp:Label runat="server" ID="Label1" Text="Tagged" Width="50px"></asp:Label>
                                </th>
                                <th style="text-align: center; white-space: normal;">
                                    <asp:Label runat="server" ID="Label4" Text="E1 ID" Width="60px"></asp:Label>
                                </th>
                                <th style="text-align: center; white-space: normal;">
                                    <asp:Label runat="server" ID="Label5" Text="Seafarer" Width="200px"></asp:Label>
                                </th>
                                <th style="white-space: normal;">
                                    <asp:Label runat="server" ID="Label2" Text="Ship" Width="182px"></asp:Label>
                                </th>
                                <th style="text-align: center; white-space: normal;">
                                    <asp:Label runat="server" ID="Label7" Text="Rec Loc" Width="50px"></asp:Label>
                                </th>
                                <th style="text-align: center; white-space: normal;">
                                    <asp:Label runat="server" ID="Label3" Text="Rank" Width="150px"></asp:Label>
                                </th>
                                <th style="white-space: normal;">
                                    <asp:Label runat="server" ID="Label8" Text="Status" Width="40px"></asp:Label>
                                </th>
                                <th style="text-align: center; white-space: normal;">
                                    <asp:Label runat="server" ID="Label14" Text="Dep - Arr" Width="60px"></asp:Label>
                                </th>
                                <th style="text-align: center; white-space: normal;">
                                    <asp:Label runat="server" ID="Label16" Text="Departure Date" Width="100px"></asp:Label>
                                </th>
                                <th style="text-align: center; white-space: normal;">
                                    <asp:Label runat="server" ID="Label17" Text="Arrival Date" Width="100px"></asp:Label>
                                </th>
                                <th style="text-align: center; white-space: normal;">
                                    <asp:Label runat="server" ID="Label13" Text="Flight No - Airline" Width="150px"></asp:Label>
                                </th>
                                <th style="text-align: center; white-space: normal;">
                                    <asp:Label runat="server" ID="Label29" Text="Passport No" Width="100px"></asp:Label>
                                </th>
                                <th style="text-align: center; white-space: normal;">
                                    <asp:Label runat="server" ID="Label30" Text="Expiration Date" Width="100px"></asp:Label>
                                </th>
                                <th style="text-align: center; white-space: normal;">
                                    <asp:Label runat="server" ID="Label31" Text="Issued Date" Width="100px"></asp:Label>
                                    <th style="text-align: center; white-space: normal;">
                                        <asp:Label runat="server" ID="Label10" Text="Hotel" Width="200px"></asp:Label>
                                    </th>--%>
                                <th style="text-align: center; white-space: normal;">
                                    <asp:LinkButton ID="LinkButton1" Text="Tagged" Width="39px" runat="server" CommandName="OrderByTagByUser" />
                                </th>
                                <th style="text-align: center; white-space: normal;">
                                    <asp:LinkButton ID="LinkButton2" Text="Hotel" runat="server" Width="210px" CommandName="OrderByHotel" />
                                </th>
                                <th style="text-align: center; white-space: normal;">
                                    <asp:LinkButton ID="LinkButton3" Text="Check In" runat="server" Width="90px" CommandName="OrderByCheckIn" />
                                </th>
                                <th style="text-align: center; white-space: normal;">
                                    <asp:LinkButton ID="LinkButton4" Text="Check Out" runat="server" Width="90px" CommandName="Checkout" />
                                </th>
                                <th style="text-align: center; white-space: normal;">
                                    <asp:LinkButton ID="LinkButton5" Text="Hotel Nites" runat="server" Width="65px" CommandName="Duration" />
                                </th>
                                <th style="text-align: center; white-space: normal;">
                                    <asp:LinkButton ID="LinkButton6" Text="Last Name" runat="server" Width="110px" CommandName="OrderByLastname" />
                                </th>
                                <th style="text-align: center; white-space: normal;">
                                    <asp:LinkButton ID="LinkButton7" Text="First Name" runat="server" Width="130px" CommandName="OrderByName" />
                                </th>
                                <th style="text-align: center; white-space: normal;">
                                    <asp:LinkButton ID="LinkButton8" Text="Employee ID" runat="server" Width="60px" CommandName="colSeafarerIdInt" />
                                </th>
                                <th style="white-space: normal;">
                                    <asp:LinkButton ID="LinkButton9" Text="Ship" runat="server" Width="182px" CommandName="OrderByVessel" />
                                </th>
                                 <th style="white-space: normal;">
                                    <asp:LinkButton ID="LinkButton26" Text="Status" runat="server" Width="58px" CommandName="OrderByStatus" />
                                </th>
                                <th style="text-align: center; white-space: normal;">
                                    <asp:LinkButton ID="LinkButton10" Text="Single / Double" runat="server" Width="60px"
                                        CommandName="colSingleDouble" />
                                </th>
                                <th style="text-align: center; white-space: normal;">
                                    <asp:LinkButton ID="LinkButton11" Text="Title" runat="server" Width="220px" CommandName="Rank" />
                                </th>
                                <th style="text-align: center; white-space: normal;">
                                    <asp:LinkButton ID="LinkButton12" Text="Gender" runat="server" Width="50px" CommandName="colGender" />
                                </th>
                                <th style="text-align: center; white-space: normal;">
                                    <asp:LinkButton ID="LinkButton13" Text="Cost Center" runat="server" Width="130px"
                                        CommandName="OrderByCostCenter" />
                                </th>
                                <th style="text-align: center; white-space: normal;">
                                    <asp:LinkButton ID="LinkButton14" Text="Nationality" runat="server" Width="70px"
                                        CommandName="OrderByNationality" />
                                </th>
                                <th style="text-align: center; white-space: normal;">
                                    <asp:LinkButton ID="LinkButton33" Text="Birthday" Width="70px" runat="server" CommandName="colBirthday" />
                                </th>
                                <th style="text-align: center; white-space: normal;">
                                    <asp:LinkButton ID="LinkButton15" Text="Meal Allowance" runat="server" Width="60px"
                                        CommandName="colMealAllowance" />
                                </th>
                                <th style="text-align: center; white-space: normal;">
                                    <asp:LinkButton ID="LinkButton16" Text="Rec Loc" runat="server" Width="50px" CommandName="OrderByRecLoc" />
                                </th>
                                <th style="text-align: center; white-space: normal;">
                                    <asp:LinkButton ID="LinkButton25" Text="Seq.No" runat="server" Width="50px" CommandName="colSeqNoInt" />
                                </th>
                                <th style="text-align: center; white-space: normal;">
                                    <asp:LinkButton ID="LinkButton17" Text="Dept Date" runat="server" Width="80px" CommandName="colDepartureDateTime" />
                                </th>
                                <th style="text-align: center; white-space: normal;">
                                    <asp:LinkButton ID="LinkButton18" Text="Arvl Date" runat="server" Width="80px" CommandName="colArrivalDateTime" />
                                </th>
                                <th style="text-align: center; white-space: normal;">
                                    <asp:LinkButton ID="LinkButton19" Text="From City" runat="server" Width="60px" CommandName="AirportDeparture" />
                                </th>
                                <th style="text-align: center; white-space: normal;">
                                    <asp:LinkButton ID="LinkButton20" Text="To   City" runat="server" Width="40px" CommandName="AirportArrival" />
                                </th>
                                <th style="text-align: center; white-space: normal;">
                                    <asp:LinkButton ID="LinkButton21" Text="Dept Time" runat="server" Width="60px" CommandName="colDepartureDateTime" />
                                </th>
                                <th style="text-align: center; white-space: normal;">
                                    <asp:LinkButton ID="LinkButton22" Text="Arvl Time" runat="server" Width="50px" CommandName="colArrivalDateTime" />
                                </th>
                                <th style="text-align: center; white-space: normal;">
                                    <asp:LinkButton ID="LinkButton23" Text="Airline" runat="server" Width="50px" CommandName="Airline" />
                                </th>
                                <th style="text-align: center; white-space: normal;">
                                    <asp:LinkButton ID="LinkButton24" Text="Flight No" runat="server" Width="60px" CommandName="colFlightNoVarchar" />
                                </th>
                                <th>
                                    <asp:Label runat="server" ID="Label11" Text="" Width="10px">
                                    </asp:Label>
                                </th>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                </asp:ListView>
            </div>
            <div id="BvSea" style="overflow: auto; width: 100%; overflow-x: scroll; overflow-y: auto;"
                onscroll="divScrollL('Sea');">
                <asp:ListView runat="server" ID="uoListViewTRSea" OnItemCommand="uoListViewTR_ItemCommand">
                    <LayoutTemplate>
                        <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                            <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                        </table>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <tr>
                            <td class="leftAligned" style="white-space: normal;">
                                <asp:HiddenField ID="uoHiddenFieldStatus" runat="server" Value='<%# Eval("Status") %>'/>
                                <asp:Label ID="Label26" runat="server" Width="46px" Text="Tagged" Visible='<%# IsTaggedLabelVisible(Eval("IsTaggedByUser"), Eval("IsFirstPartition"))%>'
                                    title='<%# "Tagged time is : " + Eval("TagDateTime") %>'></asp:Label>
                                <asp:LinkButton Width="46px" ID="uoLinkButtonTag" CssClass="PropertyLinkButtons BtnTag"
                                    CommandName="Tag" Visible='<%# IsTaggedLinkVisible(Eval("IsTaggedByUser"), Eval("IsFirstPartition"))%>'
                                    OnClientClick='<%# "javascript:return confirmTag("+ Eval("SfID") + ", \"" + Eval("Name") + "\");" %>'
                                    CommandArgument='<%# Eval("IDBigInt") + ":" + Eval("TravelRequestID") + ":" + Eval("AirportID") + ":" + Eval("SeaportID") %>'
                                    runat="server" Text="Tag"></asp:LinkButton>
                            </td>
                            <td class="leftAligned" style="white-space: normal;">
                                <asp:Label runat="server" ID="uoLblReason" Width="216px" Text='<%# Eval("Hotel")%>'></asp:Label>
                            </td>
                            <td class="leftAligned" style="white-space: normal;">
                                <asp:Label runat="server" ID="Label25" Width="95px" Text='<%# String.Format("{0:dd-MMM-yyyy}", Eval("Checkin"))%>'></asp:Label>
                            </td>
                            <td class="leftAligned" style="white-space: normal;">
                                <asp:Label runat="server" ID="Label32" Width="96px" Text='<%# String.Format("{0:dd-MMM-yyyy}", Eval("Checkout"))%>'></asp:Label>
                            </td>
                            <td class="centerAligned" style="white-space: normal;">
                                <asp:Label runat="server" ID="Label33" Width="71px" Text='<%# Eval("Duration")%>'></asp:Label>
                            </td>
                            <td class="leftAligned" style="white-space: normal;">
                                <asp:Label runat="server" ID="Label54" Width="116px" Text='<%# Eval("Lastname")%>'></asp:Label>
                            </td>
                            <td class="leftAligned">
                                <asp:HyperLink ID="uoHyperLinkName" NavigateUrl='<%# "../SuperUserView.aspx?ufn=" + Request.QueryString["ufn"] + "&sfId=" + Eval("SfID") + "&recloc=&st=" + Eval("Status") + "&ID=0&trID="+ Eval("TravelRequestID") +"&manualReqID=0" + "&dt=" + Request.QueryString["dt"]%>'
                                    runat="server" Width="136px" Text='<%# Eval("Firstname")%>'></asp:HyperLink>
                            </td>
                            <td class="leftAligned" style="white-space: normal;">
                                <asp:Label runat="server" Width="66px" ID="ucLabelE1ID" Text='<%# Eval("SfID")%>' Visible='<%# Eval("IsFirstPartition") %>'></asp:Label>
                            </td>
                            <td class="leftAligned">
                                <asp:Label runat="server" ID="uoLblVessel" Width="188px" Text='<%# Eval("Vessel")%>'></asp:Label>
                            </td>
                            <td class="leftAligned">
                                <asp:Label runat="server" ID="Label2" Width="60px" Text='<%# Eval("Status")%>'></asp:Label>
                            </td>
                            <td class="centerAligned" style="white-space: normal;">
                                <asp:Label runat="server" ID="Label34" Width="66px" Text='<%# Eval("SingleDouble")%>'></asp:Label>
                            </td>
                            <td class="leftAligned" style="white-space: normal;">
                                <asp:Label runat="server" Width="226px" ID="Label20" Text='<%# Eval("Rank")%>'></asp:Label>
                            </td>
                            <td class="leftAligned" style="white-space: normal;">
                                <asp:Label runat="server" ID="Label35" Width="56px" Text='<%# Eval("Gender")%>'></asp:Label>
                            </td>
                            <td class="leftAligned" style="white-space: normal;">
                                <asp:Label runat="server" ID="Label36" Width="136px" Text='<%# Eval("CostCenter")%>'></asp:Label>
                            </td>
                            <td class="leftAligned" style="white-space: normal;">
                                <asp:Label runat="server" ID="Label37" Width="76px" Text='<%# Eval("Nationality")%>'></asp:Label>
                            </td>
                            <td class="leftAligned" style="white-space: normal;">                             
                                <asp:Label runat="server" ID="Label7" Width="76px" Text='<%# Eval("Birthday")%>'></asp:Label>
                            </td>
                            <td class="centerAligned" style="white-space: normal;">
                                <asp:Label runat="server" ID="Label38" Width="66px" Text='<%# Eval("MealAllowance")%>'></asp:Label>
                            </td>
                            <td class="leftAligned" style="white-space: normal;">
                                <asp:Label runat="server" ID="ucLabelRecloc" Text='<%# Eval("RecordLocator")%>' Width="56px"></asp:Label>
                            </td>
                            <td class="leftAligned" style="white-space: normal;">
                                <asp:Label runat="server" ID="Label1" Text='<%# Eval("SequenceNo")%>' Width="56px"></asp:Label>
                            </td>
                            <td class="leftAligned" style="white-space: normal;">
                                <asp:Label runat="server" ID="Label24" Width="86px" Text='<%# Eval("DepartureDate")%>'></asp:Label>
                            </td>
                            <td class="leftAligned" style="white-space: normal;">
                                <asp:Label runat="server" ID="Label39" Width="86px" Text='<%# Eval("ArrivalDate")%>'></asp:Label>
                            </td>
                            <td class="centerAligned" style="white-space: normal;">
                                <asp:Label runat="server" ID="Label40" Width="66px" Text='<%# Eval("Departure")%>'></asp:Label>
                            </td>
                            <td class="centerAligned" style="white-space: normal;">
                                <asp:Label runat="server" Width="46px" ID="Label41" Text='<%# Eval("Arrival")%>'></asp:Label>
                            </td>
                            <td class="centerAligned" style="white-space: normal;">
                                <asp:Label runat="server" ID="Label42" Width="66px" Text='<%# Eval("DepartureTime")%>'></asp:Label>
                            </td>
                            <td class="centerAligned" style="white-space: normal;">
                                <asp:Label runat="server" ID="Label22" Width="56px" Text='<%# Eval("ArrivalTime")%>'></asp:Label>
                            </td>
                            <td class="centerAligned" style="white-space: normal;">
                                <asp:Label runat="server" Width="56px" ID="ucLabelFlightNo" Text='<%# Eval("Airline")%>'></asp:Label>
                            </td>
                            <td class="centerAligned" style="white-space: normal;">
                                <asp:Label runat="server" ID="Label43" Width="66px" Text='<%# Eval("FlightNo")%>'></asp:Label>
                            </td>
                            <%-- <td class="leftAligned" style="white-space: normal;">
                                <asp:Label runat="server" Width="60px" ID="Label21" Text='<%# Eval("SfID")%>'></asp:Label>
                            </td>
                            <td class="leftAligned">
                                <asp:HyperLink ID="uoHyperLinkName" NavigateUrl='<%# "../SuperUserView.aspx?ufn=" + Request.QueryString["ufn"] + "&sfId=" + Eval("SfID") + "&recloc=&st=" + Eval("Status") + "&ID=0&trID="+ Eval("TravelRequestID") +"&manualReqID=0" + "&dt=" + Request.QueryString["dt"]%>'
                                    runat="server" Width="200px"><%# Eval("Name")%></asp:HyperLink>
                            </td>
                            <td class="leftAligned">
                                <asp:Label runat="server" ID="uoLblVessel" Width="182px" Text='<%# Eval("Vessel")%>'></asp:Label>&nbsp;--%>
                            <%--<asp:Image ID="uoImageSailMaster" ImageUrl="~/Images/Vessel.png" runat="server" ToolTip="With Sail Master"
                            Visible='<%# bool.Parse(Eval("IsWithSail").ToString()) %>' />
                        <asp:Label runat="server" ID="uoSpace" Width="18px" Text='' Visible='<%# !bool.Parse(Eval("IsWithSail").ToString()) %>'></asp:Label>--%>
                            <%-- </td>
                            <td class="leftAligned" style="white-space: normal;">
                                <asp:Label runat="server" ID="ucLabelRecloc" Text='<%# Eval("RecordLocator")%>' Width="50px"></asp:Label>
                            </td>
                            <td class="leftAligned" style="white-space: normal;">
                                <asp:Label runat="server" Width="150px" ID="Label20" Text='<%# Eval("Rank")%>'></asp:Label>
                            </td>
                            <td class="leftAligned" style="white-space: normal;">
                                <asp:Label runat="server" Width="40px" ID="Label6" Text='<%# Eval("Status")%>'></asp:Label>
                            </td>
                            <td class="leftAligned" style="white-space: normal;">
                                <asp:Label runat="server" ID="Label24" Width="60px" Text='<%# Eval("Departure") + " - " + Eval("Arrival") %>'></asp:Label>
                            </td>
                            <td class="leftAligned" style="white-space: normal;">
                                <asp:Label runat="server" ID="uoLabelDepartureDate" Width="100px" Text='<%# String.Format("{0:dd-MMM-yyyy HHmm}", Eval("DepartureDateTime"))%>'></asp:Label>
                            </td>
                            <td class="leftAligned" style="white-space: normal;">
                                <asp:Label runat="server" ID="uoLabelArrivalDate" Width="100px" Text='<%# String.Format("{0:dd-MMM-yyyy HHmm}", Eval("ArrivalDateTime"))%>'></asp:Label>
                            </td>--%>
                            <%-- <td class="leftAligned" style="white-space: normal;">
                        <asp:Label runat="server" ID="ucLabelFlightNo" Width="40px" Text='<%# Eval("FlightNo")%>'></asp:Label>
                    </td>
                     <td class="leftAligned" style="white-space: normal;">
                        <asp:Label runat="server" ID="Label12" Width="150px" Text='<%# Eval("Airline")%>'></asp:Label>
                    </td>--%>
                            <%-- <td class="leftAligned" style="white-space: normal;">
                                <asp:Label runat="server" ID="ucLabelFlightNo" Width="150px" Text='<%# Eval("FlightNo") + "-" + Eval("Airline")%>'></asp:Label>
                            </td>
                            <td class="leftAligned" style="white-space: normal;">
                                <asp:Label runat="server" ID="Label18" Width="100px" Text='<%# Eval("PassportNo")%>'></asp:Label>
                            </td>
                            <td class="leftAligned" style="white-space: normal;">
                                <asp:Label runat="server" ID="Label27" Width="100px" Text='<%# Eval("PassportExpiryDate")%>'></asp:Label>
                            </td>
                            <td class="leftAligned" style="white-space: normal;">
                                <asp:Label runat="server" ID="Label28" Width="100px" Text='<%# Eval("PassportIssueDate")%>'></asp:Label>
                            </td>
                            <td class="leftAligned" style="white-space: normal;">
                                <asp:Label runat="server" ID="uoLblReason" Width="200px" Text='<%# Eval("Hotel")%>'></asp:Label>
                            </td>--%>
                        </tr>
                    </ItemTemplate>
                    <EmptyDataTemplate>
                        <table class="" width="100%">
                            <tr>
                                <th colspan="17">
                                    <asp:Label runat="server" ID="uoScroll" Width="1820px"></asp:Label>
                                </th>
                            </tr>
                            <tr>
                                <td colspan="17" class="LeftClass">
                                    No Record
                                </td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                </asp:ListView>
                <div style="text-align: left">
                    <asp:DataPager ID="DataPager1" runat="server" PagedControlID="uoListViewTRSea" OnPreRender="uoListViewPagerAir_PreRender"
                        PageSize="20">
                        <Fields>
                            <asp:NumericPagerField ButtonType="link" NumericButtonCssClass="PagerClass" />
                        </Fields>
                    </asp:DataPager>
                </div>
            </div>
        </div>
        <asp:ObjectDataSource ID="uoObjectDataSourceTR" runat="server" MaximumRowsParameterName="iMaxRow"
            SelectCountMethod="GetMeetGreetTravelRequestCount" SelectMethod="GetMeetGreetTravelRequest"
            StartRowIndexParameterName="iStartRow" TypeName="TRAVELMART.BLL.TravelRequestBLL"
            OldValuesParameterFormatString="oldcount_{0}" EnablePaging="True" OnSelecting="uoObjectDataSourceTR_Selecting">
            <SelectParameters>
                <asp:Parameter Name="LoadType" Type="Int16" />
                <asp:Parameter Name="sUser" Type="String" />
                <asp:Parameter Name="sRole" Type="String" />
                <asp:Parameter Name="sPortID" Type="String" />
                <asp:Parameter Name="sAirportID" Type="String" />
                <asp:Parameter Name="dDate" Type="DateTime" />
                <asp:Parameter Name="VesselID" Type="String" />
                <asp:Parameter Name="FilterByName" Type="String" />
                <asp:Parameter Name="SeafarerID" Type="String" />
                <asp:Parameter Name="NationalityID" Type="String" />
                <asp:Parameter Name="Gender" Type="String" />
                <asp:Parameter Name="RankID" Type="String" />
                <asp:Parameter Name="Status" Type="String" />
                <asp:Parameter Name="iViewType" Type="Int16" />
                <asp:Parameter Name="SortBy" Type="String" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:HiddenField runat="server" ID="uoHiddenFieldDate" EnableViewState="true" />
        <asp:HiddenField runat="server" ID="uoHiddenFieldRole" EnableViewState="true" />
        <asp:HiddenField runat="server" ID="uoHiddenFieldIsAirport" EnableViewState="true" />
        <asp:HiddenField runat="server" ID="uoHiddenFieldIsSeaport" EnableViewState="true" />
        <asp:HiddenField runat="server" ID="uoHiddenFieldUser" EnableViewState="true" />
        <asp:HiddenField runat="server" ID="uoHiddenFieldSeaportID" EnableViewState="true" />
        <asp:HiddenField runat="server" ID="uoHiddenFieldAirpordID" EnableViewState="true" />
        <asp:HiddenField runat="server" ID="uoHiddenFieldLoadType" Value="0" />
        <asp:HiddenField runat="server" ID="uoHiddenFieldSortBy" Value="colRecordLocatorVarchar" />
        <asp:HiddenField runat="server" ID="uoHiddenFieldTagTime" Value="" />
    </div>
</asp:Content>
<%--<asp:Content ID="Content4" ContentPlaceHolderID="SidePlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
</asp:Content>
--%>
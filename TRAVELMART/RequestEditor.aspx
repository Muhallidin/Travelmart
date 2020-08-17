<%@ Page Title="" Language="C#" MasterPageFile="~/Popup.Master" AutoEventWireup="true"
    CodeBehind="RequestEditor.aspx.cs" Inherits="TRAVELMART.RequestEditor" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script language="javascript" type="text/javascript">

        function GetVessel(pDate) {
            if (ValidateDate(pDate)) {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: "PageMethods.aspx/ManualRequestGetVessel",
                    data: "{'DateFrom': '" + pDate + "', 'DateTo': '" + pDate + "'}",
                    dataType: "json",
                    success: function(data) {

                        var ddlVessel = $("#<%=uoDropDownListVessel.ClientID %>");
                        //remove all the options in dropdown
                        $("#<%=uoDropDownListVessel.ClientID %>> option").remove();

                        //add option in dropdown
                        $("<option value='PROCESSING'>PROCESSING...</option>").appendTo(ddlVessel);
                        $("<option value='0'>--SELECT SHIP--</option>").appendTo(ddlVessel);

                        for (var i = 0; i < data.d.length; i++) {
                            //add the data coming from the result
                            $("<option value='" + data.d[i].VesselIDString + "'>" + data.d[i].VesselNameString + "</option>").appendTo(ddlVessel);
                        }
                        $("#<%=uoDropDownListVessel.ClientID %>> option[value='PROCESSING']").remove();
                    },
                    error: function(objXMLHttpRequest, textStatus, errorThrown) {
                        alert(errorThrown);
                    }
                });
            }
        }

        function GetSeafarer(E1ID) {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "PageMethods.aspx/GetSeafarer",
                data: "{'E1ID': '" + E1ID + "'}",
                dataType: "json",
                success: function(data) {

                    $("#<%=uoTextBoxNationality.ClientID %>").val(data.d[0].Nationality);
                    $("#<%=uoTextBoxGender.ClientID %>").val(data.d[0].Gender);
                    $("#<%=uoTextBoxRank.ClientID %>").val(data.d[0].Rank);
                    $("#<%=uoTextBoxCostCenter.ClientID %>").val(data.d[0].CostCenter);

                    $("#<%=uoHiddenFieldNationalityID.ClientID %>").val(data.d[0].NationalityID);
                    $("#<%=uoHiddenFieldRankID.ClientID %>").val(data.d[0].RankID);
                    $("#<%=uoHiddenFieldCostCenterID.ClientID %>").val(data.d[0].CostCenterID);

                },
                error: function(objXMLHttpRequest, textStatus, errorThrown) {
                    alert('GetSeafarer' + errorThrown);
                }
            });
        }

        function GetVesselDetails(VesselID, DateFrom) {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "PageMethods.aspx/ManualRequestGetVesselDetails",
                data: "{'VesselID': '" + VesselID + "','DateFrom': '" + DateFrom + "' }",
                dataType: "json",
                success: function(data) {

                    $("#<%=uoTextBoxPort.ClientID %>").val(data.d[0].PortName);
                    $("#<%=uoTextBoxCountry.ClientID %>").val(data.d[0].CountryName);
                    $("#<%=uoHiddenFieldPortID.ClientID %>").val(data.d[0].PortID);
                    $("#<%=uoHiddenFieldCountryID.ClientID %>").val(data.d[0].CountryID);

                },
                error: function(objXMLHttpRequest, textStatus, errorThrown) {
                    alert('GetVesselDetails: ' + errorThrown);
                }
            });
        }

        function GetPortAgent(PortID) {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "PageMethods.aspx/ManualRequestGetPortAgentList",
                data: "{'PortID': '" + PortID + "', 'BranchID': '" + 0 + "'}",
                dataType: "json",
                success: function(data) {

                    var ddlPortAgent = $("#<%=uoDropDownListPortAgent.ClientID %>");
                    //remove all the options in dropdown
                    $("#<%=uoDropDownListPortAgent.ClientID %>> option").remove();

                    //add option in dropdown
                    $("<option value='PROCESSING'>PROCESSING...</option>").appendTo(ddlPortAgent);
                    $("<option value='0'>--SELECT Service Provider--</option>").appendTo(ddlPortAgent);

                    for (var i = 0; i < data.d.length; i++) {
                        //add the data coming from the result
                        $("<option value='" + data.d[i].PortAgentID + "'>" + data.d[i].PortAgentName + "</option>").appendTo(ddlPortAgent);
                    }
                    $("#<%=uoDropDownListPortAgent.ClientID %>> option[value='PROCESSING']").remove();
                    $("<option value='1'>Other</option>").appendTo(ddlPortAgent);
                },
                error: function(objXMLHttpRequest, textStatus, errorThrown) {
                    alert('GetPortAgent: ' + errorThrown);
                }
            });
        }

        function GetHotel(RegionID, PortID, CountryID) {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "PageMethods.aspx/GetHotel",
                data: "{'RegionID': '" + RegionID + "', 'PortID': '" + PortID + "', 'CountryID': '" + CountryID + "'}",
                dataType: "json",
                success: function(data) {

                    var ddlHotel = $("#<%=uoDropDownListHotel.ClientID %>");
                    //remove all the options in dropdown
                    $("#<%=uoDropDownListHotel.ClientID %>> option").remove();

                    //add option in dropdown
                    $("<option value='PROCESSING'>PROCESSING...</option>").appendTo(ddlHotel);
                    $("<option value='0'>--SELECT HOTEL--</option>").appendTo(ddlHotel);

                    for (var i = 0; i < data.d.length; i++) {
                        //add the data coming from the result
                        $("<option value='" + data.d[i].HotelIDString + "'>" + data.d[i].HotelNameString + "</option>").appendTo(ddlHotel);
                    }
                    $("#<%=uoDropDownListHotel.ClientID %>> option[value='PROCESSING']").remove();
                },
                error: function(objXMLHttpRequest, textStatus, errorThrown) {
                    alert('GetHotel:' + errorThrown);
                }
            });
        }

        function ValidateDate(pDate) {
            try {
                var dt = Date.parse(pDate);
                return true;
            }
            catch (err) {
                return false;
            }
        }     
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript" src="../FBox/jquery-1.6.1.min.js"></script>

    <script type="text/javascript" language="javascript">
        $(document).ready(function() {
            Settings();
        });

        function pageLoad(sender, args) {
            var isAsyncPostback = Sys.WebForms.PageRequestManager.getInstance().get_isInAsyncPostBack();
            if (isAsyncPostback) {
                Settings();
            }
        }

        function ValidateIfPastDate(pDate, fromControl) {
            if (ValidateDate(pDate)) {

                var currentDate = new Date();
                var currentDate2 = new Date(currentDate.getFullYear(), currentDate.getMonth(), currentDate.getDate());

                var selectedDate = Date.parse(pDate);

                if (selectedDate < currentDate2) {
                    alert("Past date is invalid!");
                    if (fromControl == 'CheckIn') {
                        $("#<%=uoTextBoxCheckinDate.ClientID %>").val(currentDate2.format('MM/dd/yyyy'));
                    }
                    else if (fromControl == 'CheckOut') {
                        $("#<%=uoTextBoxCheckoutDate.ClientID %>").val(currentDate2.format('MM/dd/yyyy'));
                    }
                    else if (fromControl == 'OnOffDate') {
                        $("#<%=uoTextBoxDate.ClientID %>").val(currentDate2.format('MM/dd/yyyy'));
                        GetVessel($("#<%=uoTextBoxDate.ClientID %>").val());
                    }
                    else {
                        $("#<%=uoTextBoxPickupdate.ClientID %>").val(currentDate2.format('MM/dd/yyyy'));
                    }
                    return false;
                }
                else {
                    if (fromControl == 'CheckOut' && $("#<%=uoTextBoxCheckinDate.ClientID %>").val() != 'MM/dd/yyyy') {
                        if ($("#<%=uoTextBoxCheckoutDate.ClientID %>").val() < $("#<%=uoTextBoxCheckinDate.ClientID %>").val()) {
                            alert("Invalid Date range!");
                            $("#<%=uoTextBoxCheckoutDate.ClientID %>").val($("#<%=uoTextBoxCheckinDate.ClientID %>").val());
                            return false;
                        }
                    }
                    if (fromControl == 'CheckIn' && $("#<%=uoTextBoxCheckoutDate.ClientID %>").val() != 'MM/dd/yyyy') {
                        if ($("#<%=uoTextBoxCheckoutDate.ClientID %>").val() < $("#<%=uoTextBoxCheckinDate.ClientID %>").val()) {
                            alert("Invalid Date range!");
                            $("#<%=uoTextBoxCheckinDate.ClientID %>").val($("#<%=uoTextBoxCheckoutDate.ClientID %>").val());
                            return false;
                        }
                    }
                }
            }
            else {
                alert("Invalid date!");
                if (fromControl == 'CheckIn') {
                    $("#<%=uoTextBoxCheckinDate.ClientID %>").val(currentDate2.format('MM/dd/yyyy'));
                }
                else if (fromControl == 'CheckOut') {
                    $("#<%=uoTextBoxCheckoutDate.ClientID %>").val(currentDate2.format('MM/dd/yyyy'));
                }
                else if (fromControl == 'OnOffDate') {
                    $("#<%=uoTextBoxDate.ClientID %>").val(currentDate2.format('MM/dd/yyyy'));
                    GetVessel($("#<%=uoTextBoxDate.ClientID %>").val());
                }
                else {
                    $("#<%=uoTextBoxPickupdate.ClientID %>").val(currentDate2.format('MM/dd/yyyy'));
                }
                return false;
            }

            if (fromControl == 'OnOffDate') {
                GetVessel($("#<%=uoTextBoxDate.ClientID %>").val());
            }
            return true;
        }

        function OnDateChange(fromControl) {
            if (fromControl == 'CheckIn') {
                ValidateIfPastDate($("#<%=uoTextBoxCheckinDate.ClientID %>").val(), fromControl);
            }

            else if (fromControl == 'CheckOut') {
                ValidateIfPastDate($("#<%=uoTextBoxCheckoutDate.ClientID %>").val(), fromControl);
            }
            else if (fromControl == 'OnOffDate') {
                ValidateIfPastDate($("#<%=uoTextBoxDate.ClientID %>").val(), fromControl);
            }
            else {
                ValidateIfPastDate($("#<%=uoTextBoxPickupdate.ClientID %>").val(), fromControl);
                
            }
        }

        function Settings() {

            if ($("#<%=uoCheckBoxHotel.ClientID %>").attr('checked')) {
                $("#ocRowHotelCity").show();
                $("#ocRowHotel").show();
                $("#ocRowCheckin").show();

                ValidatorEnable(document.getElementById('<%= RequiredFieldValidator_uoDropDownListHotel.ClientID %>'), true);
                ValidatorEnable(document.getElementById('<%= RequiredFieldValidator_uoTextBoxCheckinDate.ClientID %>'), true);
                ValidatorEnable(document.getElementById('<%= RequiredFieldValidator_uoTextBoxCheckoutDate.ClientID %>'), true);
            }
            else {
                $("#ocRowHotelCity").hide();
                $("#ocRowHotel").hide();
                $("#ocRowCheckin").hide();

                ValidatorEnable(document.getElementById('<%= RequiredFieldValidator_uoDropDownListHotel.ClientID %>'), false);
                ValidatorEnable(document.getElementById('<%= RequiredFieldValidator_uoTextBoxCheckinDate.ClientID %>'), false);
                ValidatorEnable(document.getElementById('<%= RequiredFieldValidator_uoTextBoxCheckoutDate.ClientID %>'), false);
            }

            if ($("#<%= uoCheckBoxTransportation.ClientID %>").attr('checked')) {
                $("#ucRowOrigin").show();
                $("#ucRowDestination").show();
                $("#ucRowPickupDate").show();

                ValidatorEnable(document.getElementById('<%= RequiredFieldValidator_uoDropDownListOrigin.ClientID %>'), true);
                ValidatorEnable(document.getElementById('<%= RequiredFieldValidator_uoDropDownListDestination.ClientID %>'), true);
                ValidatorEnable(document.getElementById('<%= RequiredFieldValidator_uoTextBoxPickupdate.ClientID %>'), true);

                if ($("#<%=uoDropDownListOrigin.ClientID %>").val() == "Other") {
                    $("#<%=uoTextBoxOrigin.ClientID %>").show();
                    ValidatorEnable(document.getElementById('<%=RequiredFieldValidator_uoTextBoxOrigin.ClientID %>'), true);
                }
                else {
                    $("#<%=uoTextBoxOrigin.ClientID %>").hide();
                    ValidatorEnable(document.getElementById('<%=RequiredFieldValidator_uoTextBoxOrigin.ClientID %>'), false);
                }
                if ($("#<%=uoDropDownListDestination.ClientID %>").val() == "Other") {
                    $("#<%=uoTextBoxDestination.ClientID %>").show();
                    ValidatorEnable(document.getElementById('<%=RequiredFieldValidator_uoTextBoxDestination.ClientID %>'), true);
                }
                else {
                    $("#<%=uoTextBoxDestination.ClientID %>").hide();
                    ValidatorEnable(document.getElementById('<%=RequiredFieldValidator_uoTextBoxDestination.ClientID %>'), false);
                }
            }
            else {
                $("#ucRowOrigin").hide();
                $("#ucRowDestination").hide();
                $("#ucRowPickupDate").hide();

                ValidatorEnable(document.getElementById('<%= RequiredFieldValidator_uoDropDownListOrigin.ClientID %>'), false);
                ValidatorEnable(document.getElementById('<%= RequiredFieldValidator_uoDropDownListDestination.ClientID %>'), false);
                ValidatorEnable(document.getElementById('<%=RequiredFieldValidator_uoTextBoxOrigin.ClientID %>'), false);
                ValidatorEnable(document.getElementById('<%=RequiredFieldValidator_uoTextBoxDestination.ClientID %>'), false);
                ValidatorEnable(document.getElementById('<%= RequiredFieldValidator_uoTextBoxPickupdate.ClientID %>'), false);
            }

            if ($("#<%=uoDropDownListOrigin.ClientID %>").val() == "Other") {
                $("#<%=uoTextBoxOrigin.ClientID %>").show();
                ValidatorEnable(document.getElementById('<%=RequiredFieldValidator_uoTextBoxOrigin.ClientID %>'), true);
            }
            else {
                $("#<%=uoTextBoxOrigin.ClientID %>").hide();
                ValidatorEnable(document.getElementById('<%=RequiredFieldValidator_uoTextBoxOrigin.ClientID %>'), false);
            }
            if ($("#<%=uoDropDownListDestination.ClientID %>").val() == "Other") {
                $("#<%=uoTextBoxDestination.ClientID %>").show();
                ValidatorEnable(document.getElementById('<%=RequiredFieldValidator_uoTextBoxDestination.ClientID %>'), true);
            }
            else {
                $("#<%=uoTextBoxDestination.ClientID %>").hide();
                ValidatorEnable(document.getElementById('<%=RequiredFieldValidator_uoTextBoxDestination.ClientID %>'), false);
            }

            //--Events
            $("#<%=uoCheckBoxHotel.ClientID %>").click(function() {
                if ($(this).attr('checked')) {
                    $("#ocRowHotelCity").fadeIn();
                    $("#ocRowHotel").fadeIn();
                    $("#ocRowCheckin").fadeIn();

                    ValidatorEnable(document.getElementById('<%= RequiredFieldValidator_uoDropDownListHotel.ClientID %>'), true);
                    ValidatorEnable(document.getElementById('<%= RequiredFieldValidator_uoTextBoxCheckinDate.ClientID %>'), true);
                    ValidatorEnable(document.getElementById('<%= RequiredFieldValidator_uoTextBoxCheckoutDate.ClientID %>'), true);

                    GetHotel(0, $("#<%=uoHiddenFieldPortID.ClientID %>").val(), 0);
                }
                else {
                    $("#ocRowHotelCity").fadeOut();
                    $("#ocRowHotel").fadeOut();
                    $("#ocRowCheckin").fadeOut();

                    ValidatorEnable(document.getElementById('<%= RequiredFieldValidator_uoDropDownListHotel.ClientID %>'), false);
                    ValidatorEnable(document.getElementById('<%= RequiredFieldValidator_uoTextBoxCheckinDate.ClientID %>'), false);
                    ValidatorEnable(document.getElementById('<%= RequiredFieldValidator_uoTextBoxCheckoutDate.ClientID %>'), false);
                }
            });
            $("#<%=uoCheckBoxTransportation.ClientID %>").click(function() {
                if ($(this).attr('checked')) {
                    $("#ucRowOrigin").fadeIn();
                    $("#ucRowDestination").fadeIn();
                    $("#ucRowPickupDate").fadeIn();

                    ValidatorEnable(document.getElementById('<%= RequiredFieldValidator_uoDropDownListOrigin.ClientID %>'), true);
                    ValidatorEnable(document.getElementById('<%= RequiredFieldValidator_uoDropDownListDestination.ClientID %>'), true);
                    ValidatorEnable(document.getElementById('<%= RequiredFieldValidator_uoTextBoxPickupdate.ClientID %>'), true);
                }
                else {
                    $("#ucRowOrigin").fadeOut();
                    $("#ucRowDestination").fadeOut();
                    $("#ucRowPickupDate").fadeOut();

                    ValidatorEnable(document.getElementById('<%= RequiredFieldValidator_uoDropDownListOrigin.ClientID %>'), false);
                    ValidatorEnable(document.getElementById('<%= RequiredFieldValidator_uoDropDownListDestination.ClientID %>'), false);
                    ValidatorEnable(document.getElementById('<%= RequiredFieldValidator_uoTextBoxPickupdate.ClientID %>'), false);
                }
            });

            $("#<%=uoDropDownListOrigin.ClientID %>").click(function() {
                if ($(this).val() == "Other") {
                    $("#<%=uoTextBoxOrigin.ClientID %>").fadeIn();
                    ValidatorEnable(document.getElementById('<%=RequiredFieldValidator_uoTextBoxOrigin.ClientID %>'), true);
                }
                else {
                    $("#<%=uoTextBoxOrigin.ClientID %>").fadeOut();
                    ValidatorEnable(document.getElementById('<%=RequiredFieldValidator_uoTextBoxOrigin.ClientID %>'), false);
                }
            });
            $("#<%=uoDropDownListDestination.ClientID %>").click(function() {
                if ($(this).val() == "Other") {
                    $("#<%=uoTextBoxDestination.ClientID %>").fadeIn();
                    ValidatorEnable(document.getElementById('<%=RequiredFieldValidator_uoTextBoxDestination.ClientID %>'), true);
                }
                else {
                    $("#<%=uoTextBoxDestination.ClientID %>").fadeOut();
                    ValidatorEnable(document.getElementById('<%=RequiredFieldValidator_uoTextBoxDestination.ClientID %>'), false);
                }
            });

            $("#<%=uoDropDownListVessel.ClientID %>").change(function(ev) {
                $("#<%=uoHiddenFieldVesselID.ClientID %>").val($(this).val());
            });

            $("#<%=uoDropDownListSeafarer.ClientID %>").change(function(event) {
                GetSeafarer($(this).val());
            });

            $("#<%=uoDropDownListVessel.ClientID %>").change(function(ev) {
                var VesselID = $("#<%=uoDropDownListVessel.ClientID %>").val();
                var DateFrom = $("#<%=uoTextBoxDate.ClientID %>").val();
                var PortID = $("#<%=uoHiddenFieldPortID.ClientID %>").val();

                GetVesselDetails(VesselID, DateFrom);
                GetPortAgent(PortID);

                if ($("#<%=uoCheckBoxHotel.ClientID %>").attr('checked')) {
                    GetHotel(0, PortID, 0);
                }
            });

            $("#<%=uoDropDownListHotel.ClientID %>").change(function(ev) {
                $("#<%=uoHiddenFieldHotelID.ClientID %>").val($(this).val());
            });
        }


    
    </script>

    <div class="PageTitle">
        Hotel/Vehicle Manual Request
    </div>
    <hr />
    <div>
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" BackColor="Beige" ValidationGroup="Validate" />
    </div>
    <div>
        <table class="TableView">
            <tr>
                <td class="caption">
                    Filter By:
                </td>
                <td class="value">
                    <asp:DropDownList ID="uoDropDownListFilterBy" runat="server" Width="300px">
                        <asp:ListItem Value="1">SEAFARER NAME</asp:ListItem>
                        <asp:ListItem Value="2" Selected="True">EMPLOYEE ID</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td colspan="2">
                    <asp:TextBox ID="uoTextBoxFilter" runat="server" CssClass="TextBoxInput" Width="294px"></asp:TextBox>
                    <asp:Button ID="uoButtonView" runat="server" Text="Filter Seafarer" CssClass="SmallButton"
                        OnClick="uoButtonView_Click" />
                </td>
            </tr>
            <tr>
                <td class="caption">
                    Seafarer
                </td>
                <td class="value">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="uoDropDownListSeafarer" runat="server" Width="300px" AppendDataBoundItems="True">
                                <asp:ListItem Value="0">--SELECT SEAFARER--</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="uoDropDownListSeafarer"
                                ErrorMessage="Seafarer Required." InitialValue="0" ValidationGroup="Validate">*</asp:RequiredFieldValidator>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="uoButtonView" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
                <td class="caption">
                </td>
                <td class="value">
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <hr />
                </td>
            </tr>
        </table>
    </div>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <table class="TableView">
                <tr>
                    <td class="caption">
                        Nationality:
                    </td>
                    <td class="value">
                        <asp:TextBox ID="uoTextBoxNationality" runat="server" Width="295px" ReadOnly="true"
                            CssClass="ReadOnly"></asp:TextBox>
                    </td>
                    <td class="caption">
                        Gender:
                    </td>
                    <td class="value">
                        <asp:TextBox ID="uoTextBoxGender" runat="server" Width="295px" ReadOnly="true" CssClass="ReadOnly"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="caption">
                        Rank:
                    </td>
                    <td class="value">
                        <asp:TextBox ID="uoTextBoxRank" runat="server" Width="295px" ReadOnly="true" CssClass="ReadOnly"></asp:TextBox>
                    </td>
                    <td class="caption">
                        Cost Center:
                    </td>
                    <td class="value">
                        <asp:TextBox ID="uoTextBoxCostCenter" runat="server" Width="295px" ReadOnly="true"
                            CssClass="ReadOnly"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <hr />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="uoButtonView" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="uoDropDownListSeafarer" EventName="SelectedIndexChanged" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
            <table class="TableView">
                <tr>
                    <td class="caption">
                        Date:
                    </td>
                    <td class="value">
                        <asp:TextBox ID="uoTextBoxDate" runat="server" Text="" Width="295px" onchange="return OnDateChange('OnOffDate');"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="uoTextBoxDate"
                            ErrorMessage="Signingon/Signingoff Date Required." ValidationGroup="Validate">*</asp:RequiredFieldValidator>
                        <cc1:TextBoxWatermarkExtender ID="uoTextBoxDate_TextBoxWatermarkExtender" runat="server"
                            Enabled="True" TargetControlID="uoTextBoxDate" WatermarkCssClass="fieldWatermark"
                            WatermarkText="MM/dd/yyyy">
                        </cc1:TextBoxWatermarkExtender>
                        <%--<asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/Calendar_scheduleHS.png" />--%>
                        <cc1:CalendarExtender ID="uoTextBoxDate_CalendarExtender" runat="server" Enabled="True"
                            TargetControlID="uoTextBoxDate" Format="MM/dd/yyyy">
                        </cc1:CalendarExtender>
                        <cc1:MaskedEditExtender ID="uoTextBoxDate_MaskedEditExtender" runat="server" CultureAMPMPlaceholder=""
                            CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                            CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                            Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="uoTextBoxDate"
                            UserDateFormat="MonthDayYear">
                        </cc1:MaskedEditExtender>
                    </td>
                    <td class="caption">
                        Status:
                    </td>
                    <td class="value">
                        <asp:RadioButtonList ID="uoRadioButtonListOnOff" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Selected="True">ON</asp:ListItem>
                            <asp:ListItem>OFF</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td class="caption">
                        Ship:
                    </td>
                    <td class="value">
                        <asp:DropDownList ID="uoDropDownListVessel" runat="server" Width="300px" AppendDataBoundItems="True">
                            <asp:ListItem Value="0">--SELECT SHIP--</asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="uoDropDownListVessel"
                            ErrorMessage="Ship is Required." ValidationGroup="Validate" InitialValue="0">*</asp:RequiredFieldValidator>
                    </td>
                    <td class="caption">
                    </td>
                    <td class="value">
                    </td>
                </tr>
                <tr>
                    <td class="caption">
                        Port:
                    </td>
                    <td class="value">
                        <asp:TextBox ID="uoTextBoxPort" runat="server" Width="295px" ReadOnly="true" CssClass="ReadOnly"></asp:TextBox>
                    </td>
                    <td class="caption">
                        Country:
                    </td>
                    <td class="value">
                        <asp:TextBox ID="uoTextBoxCountry" runat="server" CssClass="ReadOnly" ReadOnly="true"
                            Width="295px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="caption">
                        Service Provider:
                    </td>
                    <td class="value">
                        <asp:DropDownList ID="uoDropDownListPortAgent" runat="server" Width="300px" AppendDataBoundItems="True">
                            <asp:ListItem Value="0">--SELECT Service Provider--</asp:ListItem>
                            <asp:ListItem Value="1" Selected="True">OTHER</asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="uoDropDownListPortAgent"
                            ErrorMessage="Service Provider is Required." ValidationGroup="Validate" InitialValue="0">*</asp:RequiredFieldValidator>
                    </td>
                    <td class="caption">
                        &nbsp;
                    </td>
                    <td class="value">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <hr />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
        <ContentTemplate>
            <table class="TableView">
                <tr>
                    <td class="caption">
                        &nbsp;
                    </td>
                    <td class="value">
                        <asp:CheckBox ID="uoCheckBoxHotel" runat="server" Text="Need Hotel?" />
                    </td>
                    <td class="caption">
                        &nbsp;
                    </td>
                    <td class="value">
                        &nbsp;
                    </td>
                </tr>
                <tr id="ocRowHotel">
                    <td class="caption">
                        Hotel:
                    </td>
                    <td class="value">
                        <asp:DropDownList ID="uoDropDownListHotel" runat="server" Width="300px" AppendDataBoundItems="True">
                            <asp:ListItem Value="0">--SELECT HOTEL--</asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_uoDropDownListHotel" runat="server"
                            ControlToValidate="uoDropDownListHotel" ErrorMessage="Hotel is Required." ValidationGroup="Validate"
                            InitialValue="0">*</asp:RequiredFieldValidator>
                    </td>
                    <td class="caption">
                        &nbsp;
                    </td>
                    <td class="value">
                        &nbsp;
                    </td>
                </tr>
                <tr id="ocRowCheckin">
                    <td class="caption">
                        Checkin Date:
                    </td>
                    <td class="value">
                        <asp:TextBox ID="uoTextBoxCheckinDate" runat="server" Text="" Width="295px" onchange="return OnDateChange('CheckIn');"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_uoTextBoxCheckinDate" runat="server"
                            ControlToValidate="uoTextBoxCheckinDate" ErrorMessage="Check In Date Required."
                            ValidationGroup="Validate">*</asp:RequiredFieldValidator>
                        <cc1:TextBoxWatermarkExtender ID="uoTextBoxCheckinDate_Textboxwatermarkextender"
                            runat="server" Enabled="True" TargetControlID="uoTextBoxCheckinDate" WatermarkCssClass="fieldWatermark"
                            WatermarkText="MM/dd/yyyy">
                        </cc1:TextBoxWatermarkExtender>
                        <%--<asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Images/Calendar_scheduleHS.png" />--%>
                        <cc1:CalendarExtender ID="uoTextBoxCheckinDate_Calendarextender" runat="server" Enabled="True"
                            TargetControlID="uoTextBoxCheckinDate" Format="MM/dd/yyyy">
                        </cc1:CalendarExtender>
                        <cc1:MaskedEditExtender ID="uoTextBoxCheckinDate_Maskededitextender" runat="server"
                            CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                            CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                            CultureTimePlaceholder="" Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="uoTextBoxCheckinDate"
                            UserDateFormat="MonthDayYear">
                        </cc1:MaskedEditExtender>
                    </td>
                    <td class="caption">
                        Checkout Date:
                    </td>
                    <td class="value">
                        <asp:TextBox ID="uoTextBoxCheckoutDate" runat="server" Text="" Width="295px" onchange="return OnDateChange('CheckOut');"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_uoTextBoxCheckoutDate" runat="server"
                            ControlToValidate="uoTextBoxCheckoutDate" ErrorMessage="Check Out Date Required."
                            ValidationGroup="Validate">*</asp:RequiredFieldValidator>
                        <cc1:TextBoxWatermarkExtender ID="uoTextBoxCheckoutDate_Textboxwatermarkextender"
                            runat="server" Enabled="True" TargetControlID="uoTextBoxCheckoutDate" WatermarkCssClass="fieldWatermark"
                            WatermarkText="MM/dd/yyyy">
                        </cc1:TextBoxWatermarkExtender>
                        <%--<asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/Images/Calendar_scheduleHS.png" />--%>
                        <cc1:CalendarExtender ID="uoTextBoxCheckoutDate_Calendarextender" runat="server"
                            Enabled="True" TargetControlID="uoTextBoxCheckoutDate" Format="MM/dd/yyyy">
                        </cc1:CalendarExtender>
                        <cc1:MaskedEditExtender ID="uoTextBoxCheckoutDate_Maskededitextender" runat="server"
                            CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                            CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                            CultureTimePlaceholder="" Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="uoTextBoxCheckoutDate"
                            UserDateFormat="MonthDayYear">
                        </cc1:MaskedEditExtender>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <hr />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="uoDropDownListVessel" EventName="SelectedIndexChanged" />
        </Triggers>
    </asp:UpdatePanel>
    <div>
        <table class="TableView">
            <tr>
                <td class="caption">
                    &nbsp;
                </td>
                <td class="value">
                    <asp:CheckBox ID="uoCheckBoxTransportation" runat="server" Text="Transportation" />
                </td>
                <td class="caption">
                    &nbsp;
                </td>
                <td class="value">
                    &nbsp;
                </td>
            </tr>
            <tr id="ucRowOrigin">
                <td class="caption">
                    Origin:
                </td>
                <td class="value">
                    <asp:DropDownList ID="uoDropDownListOrigin" runat="server" Width="300px" AppendDataBoundItems="True">
                        <asp:ListItem Value="0" Selected="True">--SELECT ORIGIN--</asp:ListItem>
                        <asp:ListItem>Hotel</asp:ListItem>
                        <asp:ListItem>Airport</asp:ListItem>
                        <asp:ListItem>Port</asp:ListItem>
                        <asp:ListItem>Other</asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_uoDropDownListOrigin" runat="server"
                        ControlToValidate="uoDropDownListOrigin" ErrorMessage="Origin is Required." ValidationGroup="Validate"
                        InitialValue="0">*</asp:RequiredFieldValidator>
                </td>
                <td class="caption">
                    &nbsp;
                </td>
                <td class="value">
                    <asp:TextBox ID="uoTextBoxOrigin" runat="server" CssClass="TextBoxInput" Width="295px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_uoTextBoxOrigin" runat="server"
                        ControlToValidate="uoTextBoxOrigin" ErrorMessage="Other Origin is Required."
                        ValidationGroup="Validate">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr id="ucRowDestination">
                <td class="caption">
                    Destination:
                </td>
                <td class="value">
                    <asp:DropDownList ID="uoDropDownListDestination" runat="server" Width="300px" AppendDataBoundItems="True">
                        <asp:ListItem Value="0" Selected="True">--SELECT DESTINATION--</asp:ListItem>
                        <asp:ListItem>Hotel</asp:ListItem>
                        <asp:ListItem>Airport</asp:ListItem>
                        <asp:ListItem>Port</asp:ListItem>
                        <asp:ListItem>Other</asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_uoDropDownListDestination"
                        runat="server" ControlToValidate="uoDropDownListDestination" ErrorMessage="Destination is Required."
                        ValidationGroup="Validate" InitialValue="0">*</asp:RequiredFieldValidator>
                </td>
                <td class="caption">
                    &nbsp;
                </td>
                <td class="value">
                    <asp:TextBox ID="uoTextBoxDestination" runat="server" CssClass="TextBoxInput" Width="295px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_uoTextBoxDestination" runat="server"
                        ControlToValidate="uoTextBoxDestination" ErrorMessage="Other Destination is Required."
                        ValidationGroup="Validate">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr id="ucRowPickupDate">
                <td class="caption">
                    Pickup Date:
                </td>
                <td class="value">
                    <asp:TextBox ID="uoTextBoxPickupdate" runat="server" Text="" Width="295px" onchange="return OnDateChange('PickUp');"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_uoTextBoxPickupdate" runat="server"
                        ControlToValidate="uoTextBoxPickupdate" ErrorMessage="Pickup Date is Required."
                        ValidationGroup="Validate">*</asp:RequiredFieldValidator>
                    <cc1:TextBoxWatermarkExtender ID="Textboxwatermarkextender1" runat="server" Enabled="True"
                        TargetControlID="uoTextBoxPickupdate" WatermarkCssClass="fieldWatermark" WatermarkText="MM/dd/yyyy">
                    </cc1:TextBoxWatermarkExtender>
                    <%--<asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Images/Calendar_scheduleHS.png" />--%>
                    <cc1:CalendarExtender ID="Calendarextender_uoTextBoxPickupdate" runat="server" Enabled="True"
                        TargetControlID="uoTextBoxPickupdate" Format="MM/dd/yyyy">
                    </cc1:CalendarExtender>
                    <cc1:MaskedEditExtender ID="Maskededitextender1" runat="server" CultureAMPMPlaceholder=""
                        CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                        CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                        Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="uoTextBoxPickupdate"
                        UserDateFormat="MonthDayYear">
                    </cc1:MaskedEditExtender>
                </td>
                <td class="caption">
                    &nbsp;
                </td>
                <td class="value">
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <hr />
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    <asp:Button ID="uoButtonSave" runat="server" Text="Save" ValidationGroup="Validate"
                        OnClick="uoButtonSave_Click" />
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
        </table>
    </div>
    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="uoHiddenFieldNationalityID" runat="server" Value="0" />
            <asp:HiddenField ID="uoHiddenFieldCostCenterID" runat="server" Value="0" />
            <asp:HiddenField ID="uoHiddenFieldRankID" runat="server" Value="0" />
            <asp:HiddenField ID="uoHiddenFieldCountryID" runat="server" Value="0" />
            <%--<asp:HiddenField ID="uoHiddenFieldCityID" runat="server" Value="0"/>   --%>
            <asp:HiddenField ID="uoHiddenFieldPortID" runat="server" Value="0" />
            <asp:HiddenField ID="uoHiddenFieldVesselID" runat="server" Value="0" />
            <asp:HiddenField ID="uoHiddenFieldRequestID" runat="server" Value="0" />
            <asp:HiddenField ID="uoHiddenFieldHotelID" runat="server" Value="0" />
            <asp:HiddenField ID="uoHiddenFieldRole" runat="server" Value="0" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

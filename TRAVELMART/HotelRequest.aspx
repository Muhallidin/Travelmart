<%@ Page Title="" Language="C#" MasterPageFile="~/Popup.Master" AutoEventWireup="true"
    CodeBehind="HotelRequest.aspx.cs" Inherits="TRAVELMART.HotelRequest" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript" language="javascript">

        $(document).ready(function() {
            filterSettings();
            setRoomCount();
        });

        function pageLoad(sender, args) {
            var isAsyncPostback = Sys.WebForms.PageRequestManager.getInstance().get_isInAsyncPostBack();
            if (isAsyncPostback) {
                filterSettings();
                setRoomCount();
            }
        }

        function setRoomCount() {
            var date1 = $("#<%=uoTextBoxCheckinDate.ClientID %>").val();
            var date2 = $("#<%=uoTextBoxCheckoutDate.ClientID %>").val();
            var diff = (Math.floor((Date.parse(date2) - Date.parse(date1)) / 86400000));
            $("#<%=uoTextNites.ClientID %>").val(diff);
            $("#<%=uoHiddenFieldNoOfNites.ClientID %>").val(diff);
        }

        function filterSettings() {

            if ($("#<%=uoCheckBoxAddCompanion.ClientID %>").attr('checked')) {
                $("#<%=uoTableCompanion.ClientID %>").show();
            }
            else {
                $("#<%=uoTableCompanion.ClientID %>").hide();
            }

            $("#<%=uoCheckBoxAddCompanion.ClientID %>").click(function() {
                if ($(this).attr('checked')) {
                    $("#<%=uoTableCompanion.ClientID %>").fadeIn();
                }
                else {
                    $("#<%=uoTableCompanion.ClientID %>").fadeOut();
                }
            });
        }





        function OnDateChange(fromControl) {
            if (fromControl == 'CheckIn') {
                ValidateIfPastDate($("#<%=uoTextBoxCheckinDate.ClientID %>").val(), fromControl);

            }

            else if (fromControl == 'CheckOut') {
                ValidateIfPastDate($("#<%=uoTextBoxCheckoutDate.ClientID %>").val(), fromControl);
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
                    return false;
                }
                else {
                    if (fromControl == 'CheckOut' && $("#<%=uoTextBoxCheckinDate.ClientID %>").val() != 'MM/dd/yyyy') {
                        if ($("#<%=uoTextBoxCheckoutDate.ClientID %>").val() < $("#<%=uoTextBoxCheckinDate.ClientID %>").val()) {
                            alert("Invalid Date range!");
                            $("#<%=uoTextBoxCheckinDate.ClientID %>").val("");
                            return false;
                        }
                    }
                    if (fromControl == 'CheckIn' && $("#<%=uoTextBoxCheckoutDate.ClientID %>").val() != 'MM/dd/yyyy') {
                        if ($("#<%=uoTextBoxCheckoutDate.ClientID %>").val() < $("#<%=uoTextBoxCheckinDate.ClientID %>").val()) {
                            alert("Invalid Date range!");
                            $("#<%=uoTextBoxCheckoutDate.ClientID %>").val("");
                            return false;
                        }
                    }
                    var date1 = $("#<%=uoTextBoxCheckinDate.ClientID %>").val();
                    var date2 = $("#<%=uoTextBoxCheckoutDate.ClientID %>").val();
                    var diff = (Math.floor((Date.parse(date2) - Date.parse(date1)) / 86400000));
                    $("#<%=uoTextNites.ClientID %>").val(diff);
                    $("#<%=uoHiddenFieldNoOfNites.ClientID %>").val(diff);
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
                return false;
            }
            GetHotelRate();
            return true;
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

        function ConfirmSave() {
            if (confirm("Do you want to save this request?") == true)
                return true;
            else
                return false;
        }

        function ConfirmSubmit() {
            if (confirm("Do you want to submit this request?") == true)
                return true;
            else
                return false;
        }

        function ConfirmEdit() {
            if (confirm("Do you want to continue updating?") == true)
                return true;
            else
                return false;
        }

        function ConfirmDelete() {
            if (confirm("Do you want to continue removing?") == true)
                return true;
            else
                return false;
        }


        function divScrollL() {
            var Right = document.getElementById('Av');
            var Left = document.getElementById('Bv');
            Right.scrollLeft = Left.scrollLeft;

        }

        function OpenContract() {
            var vendorID = $("#<%=uoDropDownListHotel.ClientID %>").val();
            var ContractID = $("#<%=uoHiddenFieldContractID.ClientID %>").val();
            //alert(vendorID + "::" + ContractID)

            if (vendorID == "0" || ContractID == "0") {
                alert("No contract for this date.");
            }
            else {
                var URLString = "../ContractManagement/HotelContractView.aspx?vId=" + vendorID + "&cId=" + ContractID;
                var screenWidth = screen.availwidth;
                var screenHeight = screen.availheight;
                screenWidth = 800;
                screenHeight = 650;

                window.open(URLString, 'Contract_Management', 'top=10, left=200, width=' + screenWidth + ', height=' + screenHeight + ', scrollbars=yes, menubar=no,toolbar=no,status=no,resizable=yes,addressbar=no');
                return false;
            }
        }
        function GetHotelRate() {            
            var BranchID = $("#<%=uoDropDownListHotel.ClientID %>").val();
            $("#<%=uoHiddenFieldBranchID.ClientID %>").val(BranchID); 
            
            var RoomTypeID = $("#<%=uoDropdownRoomOccupancy.ClientID %>").val();
            var EffectiveDate = $("#<%=uoTextBoxCheckinDate.ClientID %>").val();            

            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "/PageMethods.aspx/GetHotelRoomBlocks",
                data: "{'BranchID': '" + BranchID + "', 'RoomTypeID': '" + RoomTypeID + "','EffectiveDate': '" + EffectiveDate + "'}",
                dataType: "json",
                success: function(data) {
                    //if there is data

                    if (data.d.length > 0) {

                        if (data.d[0].ContractRate > 0) {
                            $("#<%=uoTextBoxAmount.ClientID %>").val(data.d[0].ContractRate);
                        }
                        else {
                            $("#<%=uoTextBoxAmount.ClientID %>").val(data.d[0].OverrideRate);
                        }

                        if (data.d[0].ContractCurrency > 0) {
                            $("#<%=uoDropDownListCurrency.ClientID %>").val(data.d[0].ContractCurrency);
                        }
                        else {
                            $("#<%=uoDropDownListCurrency.ClientID %>").val(data.d[0].OverrideCurrentcyID);
                        }

                        if (data.d[0].ContractTaxPercent > 0) {
                            $("#<%=uoTextBoxTaxPercent.ClientID %>").val(data.d[0].ContractTaxPercent);
                        }
                        else {
                            $("#<%=uoTextBoxTaxPercent.ClientID %>").val(data.d[0].OverrideTaxPercent);
                        }
                        
                        if (data.d[0].ContractIsTaxInclusive) {
                            $("#<%= uoTextBoxTaxPercent.ClientID %>").removeAttr("disabled");
                        }
                        else {
                            $("#<%= uoTextBoxTaxPercent.ClientID %>").attr("disabled", true);
                        }

                        $("#<%=uoCheckContractBoxTaxInclusive.ClientID %>").attr('checked', data.d[0].ContractIsTaxInclusive);
                        $("#<%= uoHiddenFieldContractID.ClientID %>").val(data.d[0].ContractID);

                        $("#<%=uoCheckBoxIsWithShuttle.ClientID %>").attr('checked', data.d[0].IsWithSuttle);

                        $("#<%=uoCheckboxBreakfast.ClientID %>").attr('checked', data.d[0].IsMealBreakfast);
                        $("#<%=uoCheckboxLunch.ClientID %>").attr('checked', data.d[0].IsMealLunch);
                        $("#<%=uoCheckboxDinner.ClientID %>").attr('checked', data.d[0].IsMealDinner);
                        $("#<%=uoCheckBoxLunchDinner.ClientID %>").attr('checked', data.d[0].IsMealLunchDinner);
                    }
                    // no data found
                    else {
                        $("#<%=uoTextBoxAmount.ClientID %>").text(0);
                        $("#<%=uoDropDownListCurrency.ClientID %>").val(0);
                        $("#<%=uoCheckContractBoxTaxInclusive.ClientID %>").attr('checked', false);
                        $("#<%= uoTextBoxTaxPercent.ClientID %>").val(0);
                        $("#<%= uoHiddenFieldContractID.ClientID %>").val(0);

                        $("#<%=uoCheckBoxIsWithShuttle.ClientID %>").attr('checked', false);

                        $("#<%=uoCheckboxBreakfast.ClientID %>").attr('checked', false);
                        $("#<%=uoCheckboxLunch.ClientID %>").attr('checked', false);
                        $("#<%=uoCheckboxDinner.ClientID %>").attr('checked', false);
                        $("#<%=uoCheckBoxLunchDinner.ClientID %>").attr('checked', false);                        
                    }
                },
                error: function(objXMLHttpRequest, textStatus, errorThrown) {
                    alert(errorThrown);
                }
            });

        }

        function undermaintenance() {
            alert("Under Maintenance!");
            return false;
        }
    </script>

    
    <div id="divSFExist" runat="server">
        
        <div>
            <table class="TableView">
                <tr>
                    <td colspan="6">
                        <div class="PageTitle">
                            Hotel Request
                        </div>
                    </td>
                </tr>
                <tr>
                    <td class="caption" >
                        Request No:
                    </td>
                    <td  class="value">
                        <asp:TextBox ID="uoTextBoxRequestNo" Width="150px" CssClass="ReadOnly" runat="server" ReadOnly="true" />
                    </td>          
                    <td class="caption">
                        Employee ID:
                    </td>
                    <td class="value">
                        <asp:TextBox ID="uoTextBoxE1ID" Width="150px" runat="server" ReadOnly="true" CssClass="ReadOnly" />
                        &nbsp;
                        <asp:RequiredFieldValidator ID="uoRFVID" ControlToValidate="uoTextBoxE1ID" ValidationGroup="Request"
                        runat="server" ErrorMessage="required" Font-Bold="true" />
                    </td>
                    <td></td>
                    <td></td>
                </tr>                
                <tr>
                    <td class="caption">
                        Last Name:
                    </td>
                    <td class="value">
                        <asp:TextBox ID="uoTextBoxLastname" MaxLength="200" Width="300px" runat="server" />
                        <asp:RequiredFieldValidator ID="uoRFVLastname" ControlToValidate="uoTextBoxLastname"
                            ValidationGroup="Request" runat="server" ErrorMessage="required" Font-Bold="true" />
                    </td>
                    <td  class="caption">
                        Ship:
                    </td>
                    <td class="value">
                        <asp:DropDownList ID="uoDropDownListVessel" runat="server" Width="300px" AppendDataBoundItems="True">
                            <asp:ListItem>--SELECT SHIP--</asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="uoRFVShip" ControlToValidate="uoDropDownListVessel"
                            InitialValue="0" ValidationGroup="Request" runat="server" ErrorMessage="required"
                            Font-Bold="true" />
                    </td>
                   <td></td>
                   <td></td>
                </tr>
                <tr>
                    <td class="caption">
                        Firstname:
                    </td>
                    <td class="value">
                        <asp:TextBox ID="uoTextBoxFirstname" MaxLength="200" Width="300px" runat="server" />
                        <asp:RequiredFieldValidator ID="uoRFVFirstname" ControlToValidate="uoTextBoxFirstname"
                            ValidationGroup="Request" runat="server" ErrorMessage="required" Font-Bold="true" />
                    </td>
                    <td class="caption">
                        Position:
                    </td>
                    <td class="value">
                        <asp:DropDownList ID="uoDropDownListRank" runat="server" Width="300px" AutoPostBack="true"
                            AppendDataBoundItems="True" OnSelectedIndexChanged="uoDropDownListRank_SelectedIndexChanged">
                            <asp:ListItem>--SELECT RANK--</asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="uoRFVRank" ControlToValidate="uoDropDownListRank"
                            InitialValue="0" ValidationGroup="Request" runat="server" ErrorMessage="required"
                            Font-Bold="true" />
                    </td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td class="caption">
                        Gender:
                    </td>
                    <td class="value">
                        <asp:DropDownList ID="uoDropDownListGender" runat="server" Width="305px" AppendDataBoundItems="True">
                            <asp:ListItem>--SELECT GENDER--</asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="uoRFVGender" ControlToValidate="uoDropDownListGender"
                            InitialValue="0" ValidationGroup="Request" runat="server" ErrorMessage="required"
                            Font-Bold="true" />
                    </td>
                    <td class="caption">
                        Cost Center:
                    </td>
                    <td class="value">
                        <asp:TextBox ID="uoTextBoxCostCenter" Width="295px" runat="server" ReadOnly="true" CssClass="ReadOnly" />
                    </td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td colspan="6">
                        <hr />
                    </td>
                </tr>
                
                <tr>
                    <td class="caption">
                        Region:
                    </td>
                    <td class="value">
                        <asp:DropDownList ID="uoDropDownListRegion" runat="server" Width="305px" AppendDataBoundItems="true"
                            AutoPostBack="true" OnSelectedIndexChanged="uoDropDownListRegion_SelectedIndexChanged">
                            <asp:ListItem>--SELECT REGION--</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="caption">
                        Check In:
                    </td>
                    <td class="value">
                          <asp:TextBox ID="uoTextBoxCheckinDate" runat="server" Text="" Width="80px" onchange="return OnDateChange('CheckIn');"></asp:TextBox>
                      
                        <cc1:TextBoxWatermarkExtender ID="uoTextBoxCheckinDate_Textboxwatermarkextender"
                            runat="server" Enabled="True" TargetControlID="uoTextBoxCheckinDate" WatermarkCssClass="fieldWatermark"
                            WatermarkText="MM/dd/yyyy">
                        </cc1:TextBoxWatermarkExtender>
                        <cc1:CalendarExtender ID="uoTextBoxCheckinDate_Calendarextender" runat="server" Enabled="True"
                            TargetControlID="uoTextBoxCheckinDate" Format="MM/dd/yyyy">
                        </cc1:CalendarExtender>
                        <cc1:MaskedEditExtender ID="uoTextBoxCheckinDate_Maskededitextender" runat="server"
                            CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                            CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                            CultureTimePlaceholder="" Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="uoTextBoxCheckinDate"
                            UserDateFormat="MonthDayYear">
                        </cc1:MaskedEditExtender>
                        <asp:RequiredFieldValidator ID="uoRFVCheckIn" ControlToValidate="uoTextBoxCheckinDate"
                            ValidationGroup="Request" runat="server" ErrorMessage="required" Font-Bold="true" />
                    </td>
                    <td class="caption">
                        Time:
                    </td>
                    <td class="value">
                        <asp:TextBox runat="server" ID="uoTxtBoxTimeIn" Width="45px" Text="00:00"></asp:TextBox>
                        <cc1:TextBoxWatermarkExtender ID="uoTxtBoxTime_TextBoxWatermarkExtender" runat="server"
                            Enabled="True" TargetControlID="uoTxtBoxTimeIn" WatermarkCssClass="fieldWatermark"
                            WatermarkText="HH:mm">
                        </cc1:TextBoxWatermarkExtender>
                        <cc1:MaskedEditExtender ID="uoMaskedEditExtenderCheckInTime" runat="server" CultureAMPMPlaceholder=""
                            CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                            CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                            Enabled="True" Mask="99:99" MaskType="Time" TargetControlID="uoTxtBoxTimeIn"
                            UserDateFormat="None" UserTimeFormat="TwentyFourHour">
                        </cc1:MaskedEditExtender>
                        <%--<asp:RequiredFieldValidator ID="uoRFVTimeIn" ControlToValidate="uoTxtBoxTimeIn" InitialValue="00:00"
                            ValidationGroup="Request" runat="server" ErrorMessage="required" Font-Bold="true" />--%>
                    </td>
                </tr>
                <tr>
                    <td class="caption">Port:</td>
                    <td class="value">
                        <asp:DropDownList ID="uoDropDownListPort" runat="server" Width="305px" AppendDataBoundItems="true"
                            AutoPostBack="true" OnSelectedIndexChanged="uoDropDownListPort_SelectedIndexChanged" >
                            <asp:ListItem>--SELECT PORT--</asp:ListItem>
                        </asp:DropDownList>
                        
                    </td>
                    <td class="caption">Check Out:</td>
                    <td class="value">
                         <asp:TextBox ID="uoTextBoxCheckoutDate" runat="server" Text="" Width="80px" onchange="return OnDateChange('CheckOut');"></asp:TextBox>
                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator_uoTextBoxCheckoutDate" runat="server"
                            ControlToValidate="uoTextBoxCheckoutDate" ErrorMessage="Check Out Date Required."
                            ValidationGroup="Validate">*</asp:RequiredFieldValidator>--%>
                        <cc1:TextBoxWatermarkExtender ID="uoTextBoxCheckoutDate_Textboxwatermarkextender"
                            runat="server" Enabled="True" TargetControlID="uoTextBoxCheckoutDate" WatermarkCssClass="fieldWatermark"
                            WatermarkText="MM/dd/yyyy">
                        </cc1:TextBoxWatermarkExtender>
                        <cc1:CalendarExtender ID="uoTextBoxCheckoutDate_Calendarextender" runat="server"
                            Enabled="True" TargetControlID="uoTextBoxCheckoutDate" Format="MM/dd/yyyy">
                        </cc1:CalendarExtender>
                        <cc1:MaskedEditExtender ID="uoTextBoxCheckoutDate_Maskededitextender" runat="server"
                            CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                            CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                            CultureTimePlaceholder="" Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="uoTextBoxCheckoutDate"
                            UserDateFormat="MonthDayYear">
                        </cc1:MaskedEditExtender>
                        <asp:RequiredFieldValidator ID="uoRFVCheckOut" ControlToValidate="uoTextBoxCheckoutDate"
                            ValidationGroup="Request" runat="server" ErrorMessage="required" Font-Bold="true" />
                    </td>  
                    <td class="caption">
                        Time:
                    </td>    
                    <td class="value">
                        <asp:TextBox runat="server" ID="uoTxtBoxTimeOut" Width="45px" Text="00:00"></asp:TextBox>
                        <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" Enabled="True"
                            TargetControlID="uoTxtBoxTimeOut" WatermarkCssClass="fieldWatermark" WatermarkText="HH:mm">
                        </cc1:TextBoxWatermarkExtender>
                        <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" CultureAMPMPlaceholder=""
                            CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                            CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                            Enabled="True" Mask="99:99" MaskType="Time" TargetControlID="uoTxtBoxTimeOut"
                            UserDateFormat="None" UserTimeFormat="TwentyFourHour">
                        </cc1:MaskedEditExtender>
                        <%--<asp:RequiredFieldValidator ID="uoRFVTimeOut" InitialValue="00:00" ControlToValidate="uoTxtBoxTimeOut"
                            ValidationGroup="Request" runat="server" ErrorMessage="required" Font-Bold="true" />--%>
                    </td>                                  
                </tr>
                <tr>
                    <td class="caption" >Airport</td>
                    <td class="value">
                        <asp:DropDownList ID="uoDropDownListAirport" runat="server" Width="305px" AppendDataBoundItems="true" 
                            AutoPostBack="true" OnSelectedIndexChanged="uoDropDownListAirport_SelectedIndexChanged" >
                            <asp:ListItem>--SELECT AIRPORT--</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="caption">
                         Room Type:
                    </td>
                    <td class="value">
                         <asp:DropDownList ID="uoDropdownRoomOccupancy" Width="85px" runat="server" onchange="GetHotelRate()">
                            <asp:ListItem Text="Single" Value="1" />
                            <asp:ListItem Text="Double" Value="2" />
                        </asp:DropDownList> &nbsp;
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="uoDropdownRoomOccupancy"
                            InitialValue="0" ValidationGroup="Request" runat="server" ErrorMessage="required"
                            Font-Bold="true" />
                    </td>
                     <td class="caption">
                         No. of Nights:
                    </td>
                     <td class="value">
                        <asp:TextBox runat="server" ID="uoTextNites" Text="1" Width="45px" MaxLength="3" CssClass="ReadOnly"
                            ReadOnly="true" />
                    </td>
                </tr>
                <tr>
                    <td class="caption">
                         Hotel:
                    </td>
                    <td class="value">
                        <asp:DropDownList ID="uoDropDownListHotel" runat="server" Width="305px" AppendDataBoundItems="True" onchange="GetHotelRate()">
                            <asp:ListItem Value="0">--SELECT HOTEL--</asp:ListItem>
                        </asp:DropDownList> &nbsp
                        <a id="uoContract" class="rightAligned" title="View Contract" href="#" onclick='return OpenContract()'>View Contract
                            <%--<img id="uoImageContract" src="Images/contract.jpg" width="20px" alt="View Contract" border="0"/>--%></a>
                        &nbsp
                        <asp:RequiredFieldValidator ID="uoRFVHotel" ControlToValidate="uoDropDownListHotel"
                            InitialValue="0" ValidationGroup="Request" runat="server" ErrorMessage="required"
                            Font-Bold="true" />
                    </td>
                    <td class="caption">
                        Currency:
                    </td>
                    <td class="value">
                        <asp:DropDownList ID="uoDropDownListCurrency" runat="server" CssClass="TextBoxInput"
                            Width="195px">
                        </asp:DropDownList>
                          &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="uoDropDownListCurrency"
                            ErrorMessage="required" Font-Bold="true" ValidationGroup="Request" InitialValue="0" />
                    </td>                      
                     <td class="caption" >
                         Tax %:
                    </td>                    
                    <td class="value">
                        <asp:TextBox ID="uoTextBoxTaxPercent" runat="server" onkeypress="return validate(event);" Width="45px"></asp:TextBox>
                    </td>                  
                </tr>
                <tr>
                    <td class="caption" rowspan="6" style="vertical-align:top">
                        Remarks:
                    </td>
                    <td class="value" rowspan="6">
                         <asp:TextBox class="TextBoxInputRemarks" ID="uoTextBoxRemarks" runat="server" Text=""
                            Width="298px" MaxLength="800" TextMode="MultiLine" Height="100px" />  &nbsp;
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="uoTextBoxRemarks"
                            ValidationGroup="Submit" runat="server" ErrorMessage="Remark is required" Font-Bold="true" ></asp:RequiredFieldValidator>
                    </td>
                    <td class="caption" >
                        Room Rate/Day:
                    </td>
                    <td class="value">
                        <asp:TextBox ID="uoTextBoxAmount" runat="server" Width="50px" Text="" onkeypress="return validate(event);"></asp:TextBox>
                        &nbsp;
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="uoTextBoxAmount"
                            ErrorMessage="Amount is required." ValidationGroup="Validate">*</asp:RequiredFieldValidator>
                        <cc1:MaskedEditExtender ID="uoTextBoxEmergencyAmount_MaskedEditExtender" runat="server"
                            CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                            CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                            CultureTimePlaceholder="" Enabled="True" Mask="99,999.99" MaskType="Number" InputDirection="RightToLeft"
                            TargetControlID="uoTextBoxAmount">
                        </cc1:MaskedEditExtender>
                    </td>
                    <td class="caption">
                        Tax Inclusive:
                    </td>
                    <td class="value">
                        <asp:CheckBox ID="uoCheckContractBoxTaxInclusive"   runat="server"/>
                    </td>                                       
                </tr>
                <tr>
                    <td class="caption" style="vertical-align:top">
                        Meal Amt/Day:
                    </td> 
                    <td class="value">
                        <asp:TextBox ID="uoTextBoxMealVoucher" runat="server" Width="50px" Text="" onkeypress="return validate(event);"></asp:TextBox>
                         <cc1:MaskedEditExtender ID="MaskedEditExtender2" runat="server"
                            CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                            CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                            CultureTimePlaceholder="" Enabled="True" Mask="99,999.99" MaskType="Number" InputDirection="RightToLeft"
                            TargetControlID="uoTextBoxMealVoucher">
                        </cc1:MaskedEditExtender>
                    </td>
                    <td class="caption" >
                        With Shuttle:
                    </td>                    
                    <td class="value" >
                        <asp:CheckBox ID="uoCheckBoxIsWithShuttle" runat="server"/>                       
                    </td> 
                </tr>
                <tr>
                    <td class="caption" rowspan="3" style="vertical-align:top">
                        Meals:
                    </td>                    
                    <td class="value" >
                        <asp:CheckBox ID="uoCheckboxBreakfast" runat="server" Text="Breakfast" />                       
                    </td>                    
                                                           
                </tr>
                <tr>
                    <td class="value" colspan="3">
                         <asp:CheckBox ID="uoCheckboxLunch" runat="server" Text="Lunch" />
                    </td>
                </tr>
                <tr>
                    <td class="value" colspan="3">
                         <asp:CheckBox ID="uoCheckboxDinner" runat="server" Text="Dinner" />
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td class="value" colspan="3">
                         <asp:CheckBox ID="uoCheckBoxLunchDinner" runat="server" Text="Lunch or Dinner" />
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td></td>
                    <td>
                        <asp:Button ID="uoButtonSave" runat="server" Width="100px" CssClass="SmallButton"
                            Text="Save Request" ValidationGroup="Request"
                            OnClick="uoButtonSave_Click" />
                    </td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td colspan="6">
                        <hr />
                    </td>
                </tr>
                <tr runat="server" id="tblConfirm">
                    <td class="caption">
                        Contact Name:
                    </td>
                    <td class="value" >
                        <asp:TextBox ID="txtContactName" MaxLength="200" Width="300px" runat="server" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtContactName"
                            ValidationGroup="Submit" runat="server" ErrorMessage="required" Font-Bold="true" />
                    </td>                    
                    <td class="caption">
                         Contact Number:
                    </td>
                    <td class="value" >
                         <asp:TextBox ID="txtContactNo" MaxLength="100" Width="180px" runat="server" />
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="txtContactNo"
                            ValidationGroup="Submit" runat="server" ErrorMessage="required" Font-Bold="true" />&nbsp;&nbsp;
                         
                    </td> 
                    <td></td>                   
                    <td></td>                                       
                </tr>    
                <tr>
                     <td></td>
                    <td></td>
                    <td>
                        <asp:Button ID="uoButtonSubmit" runat="server" Width="100px" CssClass="SmallButton"   OnClientClick="javascript: return undermaintenance();"                        
                            Text="Submit Request" ValidationGroup="Submit" OnClick="uoButtonSubmit_Click" />
                    </td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
            </table>
            
                                   
            <hr />
            <table class="TableView">
                <tr>
                    <td style="width: 10%">
                    </td>
                    <td valign="top">
                        <asp:CheckBox ID="uoCheckBoxAddCompanion" Text="ADD COMPANION/S" runat="server" Font-Bold="true"
                            ForeColor="Gray" />
                        <asp:Label runat="server" ID="uoLabelMessage" ForeColor="GrayText" Text="(The above information must be save first before you can add companion)"></asp:Label>
                    </td>
                </tr>
            </table>
            <table runat="server" id="uoTableCompanion" class="TableView">
                <tr>
                    <td width="10%">
                        Lastname:
                    </td>
                    <td>
                        <asp:TextBox ID="uoTextBoxCompLastname" Width="300px" MaxLength="200" runat="server" />
                        <asp:RequiredFieldValidator ID="uoRFVCompLastname" ControlToValidate="uoTextBoxCompLastname"
                            ValidationGroup="Companion" runat="server" ErrorMessage="required" Font-Bold="true" />
                    </td>
                </tr>
                <tr>
                    <td>
                        Firstname:
                    </td>
                    <td>
                        <asp:TextBox ID="uoTextBoxCompFirstname" Width="300px" MaxLength="200" runat="server" />
                        <asp:RequiredFieldValidator ID="uoRFVCompFirstname" ControlToValidate="uoTextBoxCompFirstname"
                            ValidationGroup="Companion" runat="server" ErrorMessage="required" Font-Bold="true" />
                    </td>
                </tr>
                <tr>
                    <td>
                        Relationship:
                    </td>
                    <td>
                        <asp:TextBox ID="uoTextBoxCompRelationship" Width="300px" MaxLength="200" runat="server" />
                        <asp:RequiredFieldValidator ID="uoRFVCompRelationship" ControlToValidate="uoTextBoxCompRelationship"
                            ValidationGroup="Companion" runat="server" ErrorMessage="required" Font-Bold="true" />
                    </td>
                </tr>
                <tr>
                    <td>
                        Gender:
                    </td>
                    <td class="value">
                        <asp:DropDownList ID="uoDropDownListCompGender" runat="server" Width="150px" AppendDataBoundItems="True">
                            <asp:ListItem>--SELECT GENDER--</asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="uoRFVCompGender" ControlToValidate="uoDropDownListCompGender"
                            InitialValue="0" ValidationGroup="Companion" runat="server" ErrorMessage="required"
                            Font-Bold="true" />
                        &nbsp;&nbsp;
                        <asp:Button ID="uoButtonAddComp" runat="server" Text="Save Companion" CssClass="SmallButton"
                            OnClick="uoButtonAddComp_Click" ValidationGroup="Companion" />
                    </td>
                </tr>
            </table>
            <table id="Table1" runat="server" class="TableView">
                <tr>
                    <td width="10%">
                    </td>
                    <td>
                        <div id="Av" style="overflow-x: hidden; width: 100%; overflow-y: hidden;">
                            <asp:ListView runat="server" ID="uoListViewCompanion">
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
                                            <th>
                                                <asp:Label runat="server" ID="uoLabelLastname" Width="148px" Text="LASTNAME"></asp:Label>
                                            </th>
                                            <th>
                                                <asp:Label runat="server" ID="uoLabelFirstname" Width="150px" Text="FIRSTNAME"></asp:Label>
                                            </th>
                                            <th>
                                                <asp:Label runat="server" ID="uoLabelRelationShip" Width="150px" Text="RELATIONSHIP"></asp:Label>
                                            </th>
                                            <th>
                                                <asp:Label runat="server" ID="uoLabelGender" Width="148px" Text="GENDER"></asp:Label>
                                            </th>
                                            <th>
                                                <asp:Label runat="server" ID="Label1" Width="67px" Text="EDIT" />
                                            </th>
                                            <th>
                                                <asp:Label runat="server" ID="Label2" Width="67px" Text="REMOVE" />
                                            </th>
                                        </tr>
                                    </table>
                                </EmptyDataTemplate>
                            </asp:ListView>
                        </div>
                        <div id="Bv" style="overflow: auto; width: 100%; overflow-x: auto; overflow-y: auto;"
                            onscroll="divScrollL();">
                            <table style="padding-left: 1px">
                                <tr>
                                    <td>
                                        <asp:ListView runat="server" ID="uoListViewCompanionList" OnItemCommand="uoListViewCompanionList_ItemCommand"
                                            OnItemDeleting="uoListViewCompanionList_ItemDeleting" OnItemEditing="uoListViewCompanionList_ItemEditing">
                                            <LayoutTemplate>
                                                <table border="0" cellpadding="0" cellspacing="0" class="listViewTableClass" align="left">
                                                    <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                                                </table>
                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td>
                                                        <asp:Label runat="server" ID="uoLabelLastname" Width="150px" Text='<%# Eval("LASTNAME") %>' />
                                                    </td>
                                                    <td>
                                                        <asp:Label runat="server" ID="uoLabelFirstname" Width="150px" Text='<%# Eval("FIRSTNAME") %>' />
                                                    </td>
                                                    <td>
                                                        <asp:Label runat="server" ID="uoLabelRelationship" Width="150px" Text='<%# Eval("RELATIONSHIP") %>' />
                                                    </td>
                                                    <td>
                                                        <asp:Label runat="server" ID="uoLabelGender" Width="150px" Text='<%# Eval("GENDER") %>' />
                                                    </td>
                                                    <td>
                                                        <asp:LinkButton ID="uoLinkButtonEdit" Width="70px" runat="server" CommandArgument='<%# Eval("DETAILID") + ":" + Eval("REQUESTID") + ":" + Eval("LASTNAME") + ":" + Eval("FIRSTNAME") + ":" + Eval("RELATIONSHIP") + ":" + Eval("GENDERID")%>'
                                                            CommandName="edit" Text="Edit" />
                                                    </td>
                                                    <td>
                                                        <asp:LinkButton ID="uoLinkButtonRemove" Width="68px" runat="server" OnClientClick="return ConfirmDelete()"
                                                            CommandArgument='<%# Eval("DETAILID") %>' CommandName="remove" Text="Remove" />
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                            <EmptyDataTemplate>
                                                <table>
                                                    <tr>
                                                        <td colspan="4" class="leftAligned">
                                                            No Record
                                                        </td>
                                                    </tr>
                                                </table>
                                            </EmptyDataTemplate>
                                        </asp:ListView>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
            </table>
            <asp:HiddenField ID="uoHiddenFieldDate" runat="server" Value="" />
            <asp:HiddenField ID="uoHiddenFieldRegion" runat="server" Value="" />
            <asp:HiddenField ID="uoHiddenFieldUser" runat="server" Value="" />
            <asp:HiddenField ID="uoHiddenFieldSFStatus" runat="server" Value="" />
            <asp:HiddenField ID="uoHiddenFieldPort" runat="server" Value="" />
            <asp:HiddenField ID="uoHiddenFieldAirPort" runat="server" Value="" />
            <asp:HiddenField ID="uoHiddenFieldCostCenterID" runat="server" Value="0" />
            <asp:HiddenField ID="uoHiddenFieldTravelRequestID" runat="server" Value="0" />
            <asp:HiddenField ID="uoHiddenFieldHotelRequestID" runat="server" Value="0" />
            <asp:HiddenField ID="uoHiddenFieldHotelRequestNo" runat="server" Value="0" />
            <asp:HiddenField ID="uoHiddenFieldHotelRequestApp" runat="server" Value="0" />
            <asp:HiddenField ID="uoHiddenFieldHotelRequestDetailID" runat="server" Value="0" />
            <asp:HiddenField ID="uoHiddenFieldRoomAmount" runat="server" Value="0" />
            <asp:HiddenField ID="uoHiddenFieldContractID" runat="server" Value="0" />
            <asp:HiddenField ID="uoHiddenFieldBranchID" runat="server" Value="0" />
            
            <asp:HiddenField runat="server" ID="uoHiddenFieldNoOfNites" EnableViewState="true"
                Value="1" />
        </div>
    </div>
    <div id="divSFNotExist" runat="server">
        <h2>
            Sorry, crew does not exist.</h2>
    </div>
</asp:Content>

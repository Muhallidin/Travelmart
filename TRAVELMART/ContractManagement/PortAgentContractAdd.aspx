<%@ Page Title="" Language="C#" MasterPageFile="~/TravelMartMaintenanceMaster.Master" AutoEventWireup="True" CodeBehind="PortAgentContractAdd.aspx.cs" Inherits="TRAVELMART.ContractManagement.PortAgentContractAdd" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style24
        {
            width: 212px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="NaviPlaceHolder" runat="server">

    <script type="text/javascript" language="javascript">
        function validate(key) {
            
            //getting key code of pressed key
            var keycode = (key.which) ? key.which : key.keyCode;
            if ((keycode < 48 || keycode > 57) && (keycode < 46 || keycode > 46)) {
                return false;
            }
            else {
                return true;
            }
          
            return true;
        }
        function confirmDelete() {
            if (confirm("Remove record?") == true)
                return true;
            else
                return false;
        }
    </script>   
   <div class="leftAligned">
        <div class="PageTitle">
            Service Provider Contract
        </div>
        <hr />
        <table width="100%" style="text-align: left">
            <tr>
                <td colspan="4">
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Header" />
                </td>
            </tr>           
            <tr>
                <td class="contentCaptionOrig">
                    Contract Title :
                </td>
                <td colspan="3">
                    <asp:TextBox ID="uoTextBoxContractTitle" runat="server" Width="447px" />
                    &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="uoTextBoxContractTitle"
                        ErrorMessage="Contract title required." ValidationGroup="Header">*</asp:RequiredFieldValidator></td>
            </tr>
            <tr>
                <td class="contentCaptionOrig">
                    Remarks :
                </td>
                <td colspan="3">
                    <asp:TextBox ID="uoTextBoxRemarks" runat="server" Width="447px" TextMode="MultiLine"
                        CssClass="TextBoxInput" />
                </td>
            </tr>
            <tr>
                <td class="contentCaptionOrig">
                    Effective Date :
                </td>
                <td class="contentValueOrig">
                    <asp:TextBox ID="uoTextBoxContractStartDate" runat="server"></asp:TextBox>
                    <cc1:TextBoxWatermarkExtender ID="uoTextBoxContractStartDate_TextBoxWatermarkExtender"
                        runat="server" Enabled="True" TargetControlID="uoTextBoxContractStartDate" WatermarkCssClass="fieldWatermark"
                        WatermarkText="MM/dd/yyyy">
                    </cc1:TextBoxWatermarkExtender>
                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/Calendar_scheduleHS.png" />
                    <cc1:CalendarExtender ID="uoTextBoxContractStartDate_CalendarExtender" runat="server"
                        Enabled="True" TargetControlID="uoTextBoxContractStartDate" PopupButtonID="ImageButton1"
                        Format="MM/dd/yyyy">
                    </cc1:CalendarExtender>
                    <cc1:MaskedEditExtender ID="uoTextBoxContractStartDate_MaskedEditExtender" runat="server"
                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                        CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                        CultureTimePlaceholder="" Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="uoTextBoxContractStartDate"
                        UserDateFormat="MonthDayYear">
                    </cc1:MaskedEditExtender>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ValidationGroup="Header"
                        ControlToValidate="uoTextBoxContractStartDate" ErrorMessage="Contract start date required.">*</asp:RequiredFieldValidator>
                </td>
                <td class="contentCaptionOrig">
                    Expiration Date :
                </td>
                <td class="contentValueOrig">
                    <asp:TextBox ID="uoTextBoxContractEndDate" runat="server"></asp:TextBox>
                    <cc1:TextBoxWatermarkExtender ID="uoTextBoxContractEndDate_TextBoxWatermarkExtender"
                        runat="server" Enabled="True" TargetControlID="uoTextBoxContractEndDate" WatermarkCssClass="fieldWatermark"
                        WatermarkText="MM/dd/yyyy">
                    </cc1:TextBoxWatermarkExtender>
                    <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Images/Calendar_scheduleHS.png" />
                    <cc1:CalendarExtender ID="uoTextBoxContractEndDate_CalendarExtender" runat="server"
                        Enabled="True" TargetControlID="uoTextBoxContractEndDate" PopupButtonID="ImageButton2"
                        Format="MM/dd/yyyy">
                    </cc1:CalendarExtender>
                    <cc1:MaskedEditExtender ID="uoTextBoxContractEndDate_MaskedEditExtender" runat="server"
                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                        CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                        CultureTimePlaceholder="" Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="uoTextBoxContractEndDate"
                        UserDateFormat="MonthDayYear">
                    </cc1:MaskedEditExtender>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="uoTextBoxContractEndDate"
                        ValidationGroup="Header" ErrorMessage="Contract end date required.">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="contentCaptionOrig">
                    Vendor Contract Date Accepted :
                </td>
                <td class="contentValueOrig">
                    <asp:TextBox ID="uoTextBoxVendorDateAccepted" runat="server" Width="125px"></asp:TextBox>
                    <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" Enabled="True"
                        TargetControlID="uoTextBoxVendorDateAccepted" WatermarkCssClass="fieldWatermark"
                        WatermarkText="MM/dd/yyyy">
                    </cc1:TextBoxWatermarkExtender>
                    <cc1:MaskedEditExtender ID="uoTextBoxvendorDateAccepted0_MaskedEditExtender" runat="server"
                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                        CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                        CultureTimePlaceholder="" Enabled="True" MaskType="Date" UserDateFormat="MonthDayYear"
                        Mask="99/99/9999" TargetControlID="uoTextBoxVendorDateAccepted">
                    </cc1:MaskedEditExtender>
                    <cc1:CalendarExtender ID="uoTextBoxvendorDateAccepted0_CalendarExtender" runat="server"
                        Enabled="True" TargetControlID="uoTextBoxVendorDateAccepted" PopupButtonID="Image6"
                        Format="MM/dd/yyyy">
                    </cc1:CalendarExtender>
                    <asp:Image ID="Image6" runat="server" ImageUrl="~/Images/Calendar_scheduleHS.png"
                        Height="16px" />
                </td>
                <td class="contentCaptionOrig">
                    RCCL Contract Date Accepted :
                </td>
                <td class="contentValueOrig">
                    <asp:TextBox ID="uoTextBoxRCCLDateAccepted" runat="server" Width="125px"></asp:TextBox>
                    <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" Enabled="True"
                        TargetControlID="uoTextBoxRCCLDateAccepted" WatermarkCssClass="fieldWatermark"
                        WatermarkText="MM/dd/yyyy">
                    </cc1:TextBoxWatermarkExtender>
                    <cc1:MaskedEditExtender ID="uoTextBoxRCCLDateAccepted_MaskedEditExtender" runat="server"
                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                        CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                        CultureTimePlaceholder="" Enabled="True" MaskType="Date" UserDateFormat="MonthDayYear"
                        Mask="99/99/9999" TargetControlID="uoTextBoxRCCLDateAccepted">
                    </cc1:MaskedEditExtender>
                    <cc1:CalendarExtender ID="uoTextBoxRCCLDateAccepted_CalendarExtender" runat="server"
                        Enabled="True" TargetControlID="uoTextBoxRCCLDateAccepted" PopupButtonID="Image5"
                        Format="MM/dd/yyyy">
                    </cc1:CalendarExtender>
                    <asp:Image ID="Image5" runat="server" ImageUrl="~/Images/Calendar_scheduleHS.png"
                        Height="16px" />
                </td>
            </tr>
            <tr>
                <td class="contentCaptionOrig">
                    Vendor Personnel :
                </td>
                <td class="contentValueOrig">
                    <asp:TextBox ID="uotextboxVendorRep" runat="server" Width="125px" />
                </td>
                <td class="contentCaptionOrig">
                    RCCL Personnel :
                </td>
                <td class="contentValueOrig">
                    <asp:TextBox ID="uotextboxRCCLRep" runat="server" Width="125px" />
                </td>
            </tr>
            <tr>
                <td class="contentCaptionOrig">
                    Currency:
                </td>
                <td class="contentValueOrig">
                    <asp:DropDownList ID="uoDropDownListCurrency" runat="server" Width="300px" AppendDataBoundItems="true">
                    </asp:DropDownList>
                </td>
                <td class="contentCaptionOrig">
                    <asp:CheckBox runat="server" ID="uoCheckBoxAirportToHotel" Text="Airport To Hotel"/>
                </td>
                <td class="contentValueOrig">
                    <asp:CheckBox runat="server" ID="uoCheckBoxHotelToShip" Text="Hotel To Ship"/>
                </td>
            </tr>
            <tr>
                <td class="contentCaptionOrig">                    
                    Brand:
                </td>
                <td class="contentValueOrig">
                    <asp:CheckBoxList ID="CheckBoxList1" runat="server" RepeatDirection="Horizontal">
                         <asp:ListItem Value="1">RCI</asp:ListItem>
                        <asp:ListItem Value="2">AZA</asp:ListItem>
                        <asp:ListItem Value="3">CEL</asp:ListItem>
                        <asp:ListItem Value="4">PUL</asp:ListItem>   
                        <asp:ListItem Value="4">SKS</asp:ListItem>                   
                    </asp:CheckBoxList>
                
                </td>
                <td class="contentCaptionOrig">                    
                </td>
                <td class="contentValueOrig">
                </td>
            </tr>            
            <tr>
                <td colspan="4">
                </td>
            </tr>
            <tr>
                <td colspan="4">
                   <%-- <asp:HiddenField ID="uoHiddenFieldPortAgentIdInt" runat="server" />--%>
                </td>
            </tr>
        </table>
        <div class="PageTitle">
            <asp:Panel ID="Panel2" runat="server">
                Service Provider Company</asp:Panel>
        </div>
        <table width="100%" style="text-align: left">           
            <tr>
                <td class="contentCaptionOrig">
                    Service Provider Name :
                </td>
                <td class="contentValueOrig" colspan="2">
                    <asp:TextBox ID="uoTextBoxVendorBranch" runat="server" CssClass="ReadOnly" ReadOnly="true"
                        Width="447px"></asp:TextBox>                    
                </td>
                <td></td>
            </tr>
            <tr>
                <td class="contentCaptionOrig">
                    Country :
                </td>
                <td class="contentValueOrig" colspan="2">
                    <asp:TextBox ID="uoTextBoxCountry" runat="server" CssClass="ReadOnly" ReadOnly="true"
                        Width="300px"></asp:TextBox>                   
                </td>
                <td></td>
            </tr>
            <tr>
                <td class="contentCaptionOrig">
                    City :
                </td>
                <td class="contentValueOrig" colspan="2">
                    <asp:TextBox ID="uoTextBoxCity" runat="server" Width="300px" ReadOnly="true" CssClass="ReadOnly"></asp:TextBox>
                </td>
                <td></td>
            </tr>
        </table>
        <br />
        <%--</div>--%>
        <div class="PageTitle">
            Assigned Airport & Seaport
        </div>
        <table width="100%" style="text-align: left">
            <tr>
                <td class="contentCaptionOrig">
                    Airport:
                </td>
                <td class="contentValueOrig">
                    <asp:DropDownList ID="uoDropDownListAirport" runat="server" Width="300px" AppendDataBoundItems="true">
                    </asp:DropDownList>
                    &nbsp;
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="Airport"
                        ControlToValidate="uoDropDownListAirport" InitialValue="0" ErrorMessage="Aiport Required">*</asp:RequiredFieldValidator>
                    &nbsp;
                    <asp:Button ID="uoButtonAirportAdd" runat="server" Text="Add" ValidationGroup="Airport"
                        CssClass="SmallButton" Width="50px" OnClick="uoButtonAirportAdd_Click" />
                </td>
                <td class="contentCaptionOrig">
                    Seaport:
                </td>
                <td class="contentValueOrig">
                    <asp:DropDownList ID="uoDropDownListSeaport" runat="server" Width="300px" AppendDataBoundItems="true">
                    </asp:DropDownList>
                    &nbsp;
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="Seaport"
                        ControlToValidate="uoDropDownListSeaport" InitialValue="0" ErrorMessage="Seaport Required">*</asp:RequiredFieldValidator>
                    &nbsp;
                    <asp:Button ID="uoButtonSeaportAdd" runat="server" Text="Add" ValidationGroup="Seaport"
                        CssClass="SmallButton" Width="50px" OnClick="uoButtonSeaportAdd_Click" />
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <asp:ListView runat="server" ID="uoListViewAirport" OnItemDeleting="uoListViewAirport_ItemDeleting">
                        <LayoutTemplate>
                            <table border="0" cellpadding="0" cellspacing="0" class="listViewTable" width="50%">
                                <tr>
                                    <th style="text-align: center; white-space: normal;">
                                        Airport
                                    </th>
                                    <th runat="server" style="width: 5%" id="DeleteTH" />
                                </tr>
                                <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                            </table>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <tr>
                                <td class="leftAligned">
                                    <asp:HiddenField ID="uoHiddenFieldContractAirportID" runat="server" Value='<%# Eval("AirportSeaportID") %>' />
                                    <asp:HiddenField ID="uoHiddenFieldAirportID" runat="server" Value='<%# Eval("AirportID") %>' />
                                    <asp:Label ID="uoLabelAirport" runat="server" Text='<%# Eval("AirportName")%>'></asp:Label>
                                </td>
                                <td style="width: 10%">
                                    <asp:LinkButton OnClientClick="return confirmDelete();" ID="uoLinkButtonDelete" runat="server"
                                        CommandName="Delete">Delete</asp:LinkButton>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <EmptyDataTemplate>
                            <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                                <tr>
                                    <th>
                                        Airport
                                    </th>
                                </tr>
                                <tr>
                                    <td class="leftAligned">
                                        No Record
                                    </td>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
                    </asp:ListView>
                </td>
                <td>
                </td>
                <td>
                    <asp:ListView runat="server" ID="uoListViewSeaport" OnItemDeleting="uoListViewSeaport_ItemDeleting">
                        <LayoutTemplate>
                            <table border="0" cellpadding="0" cellspacing="0" class="listViewTable" width="50%">
                                <tr>
                                    <th style="text-align: center; white-space: normal;">
                                        Seaport
                                    </th>
                                    <th runat="server" style="width: 5%" id="DeleteTH" />
                                </tr>
                                <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                            </table>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <tr>
                                <td class="leftAligned">
                                    <asp:HiddenField ID="uoHiddenFieldContractSeaportID" runat="server" Value='<%# Eval("ID") %>' />
                                    <asp:HiddenField ID="uoHiddenFieldSeaportID" runat="server" Value='<%# Eval("SeaportID") %>' />
                                    <asp:Label ID="uoLabelSeaport" runat="server" Text='<%# Eval("SeaportName")%>'></asp:Label>
                                </td>
                                <td style="width: 10%">
                                    <asp:LinkButton OnClientClick="return confirmDelete();" ID="uoLinkButtonDelete" runat="server"
                                        CommandName="Delete">Delete</asp:LinkButton>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <EmptyDataTemplate>
                            <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                                <tr>
                                    <th>
                                        Seaport
                                    </th>
                                </tr>
                                <tr>
                                    <td class="leftAligned">
                                        No Record
                                    </td>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
                    </asp:ListView>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <br />
                </td>
            </tr>
        </table>       
        
        <%--Vehicle Rates--%>
        <div class="PageTitle">
            <asp:Panel ID="Panel1" runat="server">
                Vehicle Type and Capacity</asp:Panel>
        </div>
        <table width="100%">
            <tr>
                <td class="contentCaptionOrig">
                    Vehicle Type
                </td>
                <td class="contentValueOrig" colspan="3">
                    <asp:DropDownList ID="uoDropDownListVehicleType" runat="server" Width="300px" AppendDataBoundItems="true">
                    </asp:DropDownList>
                    &nbsp;
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ValidationGroup="VehicleCapacity"
                        ControlToValidate="uoDropDownListVehicleType" InitialValue="0" ErrorMessage="Max Count Required">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="contentCaptionOrig">
                    Min/Max Capacity:
                </td>
                <td class="contentValueOrig">
                    Min:&nbsp;
                    <asp:TextBox ID="uoTextBoxMin" runat="server" Width="100px" CssClass="SmallText"
                        onkeypress="return validate(event);"></asp:TextBox>
                    &nbsp;
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ValidationGroup="VehicleCapacity"
                        ControlToValidate="uoTextBoxMin" ErrorMessage="Min Count Required">*</asp:RequiredFieldValidator>
                    &nbsp;&nbsp;Max:&nbsp;
                    <asp:TextBox ID="uoTextBoxMax" runat="server" Width="100px" CssClass="SmallText"
                        onkeypress="return validate(event);"></asp:TextBox>
                    &nbsp;
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ValidationGroup="VehicleCapacity"
                        ControlToValidate="uoTextBoxMax" ErrorMessage="Max Count Required">*</asp:RequiredFieldValidator>
                    &nbsp;
                    <asp:Button ID="uoButtonVehicleTypeCapacityAdd" runat="server" Text="Add" CssClass="SmallButton"
                        Width="50px" ValidationGroup="VehicleCapacity" OnClick="uoButtonVehicleTypeCapacityAdd_Click" />
                </td>
                <td class="contentCaptionOrig">
                </td>
                <td class="contentValueOrig">
                </td>
            </tr>
            <tr>
                <td class="contentCaptionOrig">
                </td>
                <td class="contentValueOrig" valign="top" style="vertical-align: top">
                    <asp:ListView runat="server" ID="uoListViewVehicleTypeCapacity" OnItemDeleting="uoListViewVehicleTypeCapacity_ItemDeleting">
                        <LayoutTemplate>
                            <table border="0" cellpadding="0" cellspacing="0" class="listViewTable" width="50%">
                                <tr>
                                    <th style="text-align: center; white-space: normal;">
                                        Vehicle Type
                                    </th>
                                    <th style="text-align: center; white-space: normal;">
                                        Min Capacity
                                    </th>
                                    <th style="text-align: center; white-space: normal;">
                                        Max Capacity
                                    </th>
                                    <th runat="server" style="width: 5%" id="DeleteTH" />
                                </tr>
                                <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                            </table>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <tr>
                                <td class="leftAligned">
                                    <asp:HiddenField ID="uoHiddenFieldContractVehicleCapacityID" runat="server" Value='<%# Eval("ContractVehicleCapacityIDInt") %>' />
                                    <asp:HiddenField ID="uoHiddenFieldVehicleTypeID" runat="server" Value='<%# Eval("VehicleTypeID") %>' />
                                    <asp:Label ID="uoLabelVehicleTypeName" runat="server" Text='<%# Eval("VehicleType")%>'></asp:Label>
                                </td>
                                <td class="leftAligned">
                                    <asp:Label ID="uoLabelMinCapacity" runat="server" Text='<%# Eval("MinCapacity")%>'></asp:Label>
                                </td>
                                <td class="leftAligned">
                                    <asp:Label ID="uoLabelMaxCapacity" runat="server" Text='<%# Eval("MaxCapacity")%>'></asp:Label>
                                </td>
                                <td style="width: 10%">
                                    <asp:LinkButton OnClientClick="return confirmDelete();" ID="uoLinkButtonDelete" runat="server"
                                        CommandName="Delete" CommandArgument='<%# Eval("VehicleTypeID") %>'>Delete</asp:LinkButton>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <EmptyDataTemplate>
                            <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                                <tr>
                                    <th style="text-align: center; white-space: normal;">
                                        Vehicle Type
                                    </th>
                                    <th style="text-align: center; white-space: normal;">
                                        Min Capacity
                                    </th>
                                    <th style="text-align: center; white-space: normal;">
                                        Max Capacity
                                    </th>
                                </tr>
                                <tr>
                                    <td class="leftAligned" colspan="3">
                                        No Record
                                    </td>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
                    </asp:ListView>
                </td>
                <td class="contentCaptionOrig">
                </td>
                <td class="contentValueOrig" valign="top" style="vertical-align: top">
                </td>
            </tr>
        </table>
        <div class="PageTitle">
            Transportation Service        
        </div>
        <table width="100%">
            <tr>
                <td align="left" colspan="4">
                    <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="Vehicle" />
                </td>
            </tr>
            <tr>
                <td class="contentCaptionOrig">
                    Vehicle :
                </td>
                <td align="left" class="contentValueOrig">
                    <asp:DropDownList ID="uoDropDownListVehicleTypeCapacity" runat="server" Width="250px"
                        AppendDataBoundItems="true">
                    </asp:DropDownList>
                </td>
                <td colspan="2" style="text-align: left">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="uoDropDownListVehicleTypeCapacity"
                        ErrorMessage="Vehicle type required." InitialValue="0" ValidationGroup="Vehicle">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="contentCaptionOrig">
                    Route From:
                </td>
                <td class="contentValueOrig">
                    <asp:DropDownList ID="uoDropDownListRouteFrom" runat="server" Width="250px" AutoPostBack="true" AppendDataBoundItems="true"
                        OnSelectedIndexChanged="uoDropDownListRouteFrom_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td colspan="2" style="text-align: left">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="uoDropDownListRouteFrom" 
                        ErrorMessage="Route From required." ValidationGroup="Vehicle" InitialValue="0">*</asp:RequiredFieldValidator>
                </td>               
            </tr>
            <tr>
                <td class="contentCaptionOrig">
                    Route To:
                </td>
                <td class="contentValueOrig">
                    <asp:DropDownList ID="uoDropDownListRouteTo" runat="server" Width="250px" AutoPostBack="true" AppendDataBoundItems="true"
                        OnSelectedIndexChanged="uoDropDownListRouteTo_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td colspan="2" style="text-align: left">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="uoDropDownListRouteTo"
                        ErrorMessage="Route To required." ValidationGroup="Vehicle" InitialValue="0">*</asp:RequiredFieldValidator>                       
                </td>               
            </tr>
            
            <tr>
                <td class="contentCaptionOrig">
                    Origin :
                </td>
                <td class="contentValueOrig">
                    <asp:DropDownList ID="uoDropDownListOrigin" runat="server" Width="250px" AppendDataBoundItems="true">
                    </asp:DropDownList>
                </td>
                <td colspan="2" style="text-align: left">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorOrigin" runat="server" ControlToValidate="uoDropDownListOrigin"
                        ErrorMessage="Origin required." ValidationGroup="Vehicle" InitialValue="0">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="contentCaptionOrig">
                    Destination :
                </td>
                <td class="contentValueOrig">
                    <asp:DropDownList ID="uoDropDownListDestination" runat="server" Width="250px" AppendDataBoundItems="true">
                    </asp:DropDownList>
                </td>
                <td colspan="2" style="text-align: left">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorDestination" runat="server" ControlToValidate="uoDropDownListDestination"
                        ErrorMessage="Destination required." ValidationGroup="Vehicle" InitialValue="0">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="contentCaptionOrig">
                    Rate:
                </td>
                <td class="contentValueOrig">
                    <asp:TextBox ID="uoTextBoxVehicleRate" runat="server" Width="180px" onkeypress="return validate(event);"/>                  
                </td>
                <td colspan="2" style="text-align: left">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="uoTextBoxVehicleRate"
                        ErrorMessage="Rate required." ValidationGroup="Vehicle">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="contentCaptionOrig">
                    Tax:
                </td>
                <td class="contentValueOrig">
                    <asp:TextBox ID="uoTextBoxTax" runat="server" Width="180px" onkeypress="return validate(event);"></asp:TextBox> &nbsp;%                    
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td align="left">
                    <asp:Button ID="uoButtonAddVehicle" runat="server" Text="Add" ValidationGroup="Vehicle"
                        OnClick="uoButtonAddVehicle_Click" CssClass="SmallButton"/>
                </td>
                <td>
                    <asp:HiddenField ID="uoHiddenFieldDetail" runat="server" Value="0" />
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td valign="top" align="left" colspan="2">
                    <asp:GridView ID="uoGridViewVehicle" runat="server" Width="100%" AutoGenerateColumns="False"
                        CssClass="listViewTable" OnRowDeleting="uoGridViewVehicle_RowDeleting" OnSelectedIndexChanging="uoGridViewVehicle_SelectedIndexChanging">
                        <Columns>
                            <asp:BoundField DataField="ContractDetailID" HeaderText="Contract Detail ID" Visible="false" />
                            <asp:BoundField DataField="PortAgentID" HeaderText="Vehicle ID" Visible="false" />
                            <asp:BoundField DataField="RouteFrom" HeaderText="Route ID From" Visible="false" />
                            <asp:BoundField DataField="RouteTo" HeaderText="Route ID To" Visible="false" />
                            <asp:BoundField DataField="VehicleType" HeaderText="Type" />
                            <asp:BoundField DataField="RouteFrom" HeaderText="Route From" />
                            <asp:BoundField DataField="RouteTo" HeaderText="Route To" />
                            <asp:BoundField DataField="Origin" HeaderText="Origin" />
                            <asp:BoundField DataField="Destination" HeaderText="Destination" />
                            <%--<asp:BoundField DataField="StartDate" HeaderText="Start Date"/>--%>
                            <%--<asp:BoundField DataField="EndDate" HeaderText="End Date"/>--%>
                            <%--<asp:BoundField DataField="CurrencyID" HeaderText="Currency ID" Visible="false"/>--%>
                            <%--<asp:BoundField DataField="Currency" HeaderText="Currency"/>--%>
                            <asp:BoundField DataField="RateAmount" HeaderText="Rate" DataFormatString="{0:#,##0.00}" />
                            <asp:BoundField DataField="Tax" HeaderText="Tax" DataFormatString="{0:#,##0.00}" />
                            <asp:ButtonField CommandName="Delete" Text="Delete" />
                            <asp:ButtonField CommandName="Select" Text="Edit" />
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>
    
    <br/>
    <div class="PageTitle">
        Hotel Service          
    </div>
    <table width="100%">
        <tr>
            <td class="contentCaptionOrig">
                Single Room Rate:
            </td>
            <td class="contentValueOrig">
                <asp:TextBox ID="uoTextBoxSingleRoomRate" runat="server" Width="180px" onkeypress="return validate(event);" ></asp:TextBox>                 
                           </td>
             <td class="contentCaptionOrig">
                Double Room Rate:
            </td>
            <td class="contentValueOrig">
                <asp:TextBox ID="uoTextBoxDoubleRoomRate" runat="server" Width="180px" onkeypress="return validate(event);" ></asp:TextBox>                
            </td>
        </tr>
        <tr>
            <td class="contentCaptionOrig">
                Is Tax Inclusive:
            </td>
            <td class="contentValueOrig">
                <asp:CheckBox runat="server" ID="uoCheckBoxIsTaxInclusive" />
            </td>
             <td class="contentCaptionOrig">
                Is Rate by Percent:
            </td>
            <td class="contentValueOrig">
                <asp:CheckBox runat="server" ID="uoCheckBoxIsRateByPercent" />
            </td>
        </tr>
         <tr>
            <td class="contentCaptionOrig">
                Room Tax:
            </td>
            <td class="contentValueOrig">
                <asp:TextBox ID="uoTextBoxTaxHotel" runat="server" Width="180px" onkeypress="return validate(event);" ></asp:TextBox>&nbsp;%
            </td>
             <td class="contentCaptionOrig">
                Cost Plus:
            </td>
            <td class="contentValueOrig">
                <asp:TextBox ID="uoTextBoxCostPlus" runat="server" Width="180px" onkeypress="return validate(event);" ></asp:TextBox>&nbsp;%
            </td>
        </tr>
        <tr>
            <td class="contentCaptionOrig">
                Standard Meal:
            </td>
            <td class="contentValueOrig">
                <asp:TextBox ID="uoTextBoxMealStandard" runat="server" Width="180px" onkeypress="return validate(event);" ></asp:TextBox>
            </td>
             <td class="contentCaptionOrig">
                Increased Meal:
            </td>
            <td class="contentValueOrig">
                <asp:TextBox ID="uoTextBoxMealIncreased" runat="server" Width="180px" onkeypress="return validate(event);" ></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="contentCaptionOrig">
                Breakfast Rate:
            </td>
            <td class="contentValueOrig">
                <asp:TextBox ID="uoTextBoxBreakfastRate" runat="server" Width="180px" onkeypress="return validate(event);" ></asp:TextBox>
            </td>
             <td class="contentCaptionOrig">
                Lunch Rate:
            </td>
            <td class="contentValueOrig">
                <asp:TextBox ID="uoTextBoxLunchRate" runat="server" Width="180px" onkeypress="return validate(event);" ></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="contentCaptionOrig">
                Dinner Rate:
            </td>
            <td class="contentValueOrig">
                <asp:TextBox ID="uoTextBoxDinnerRate" runat="server" Width="180px" onkeypress="return validate(event);" ></asp:TextBox>
            </td>
             <td class="contentCaptionOrig">
                Miscellanea:
            </td>
            <td class="contentValueOrig">
                <asp:TextBox ID="uoTextBoxMiscellaneaRate" runat="server" Width="180px" onkeypress="return validate(event);" ></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="contentCaptionOrig">
                Meal Tax:
            </td>
            <td class="contentValueOrig">
                <asp:TextBox ID="uoTextBoxMealTax" runat="server" Width="180px" onkeypress="return validate(event);" ></asp:TextBox>&nbsp;%
            </td>
             <td class="contentCaptionOrig">
                
            </td>
            <td class="contentValueOrig">
            </td>
        </tr>
         <tr>
            <td class="contentCaptionOrig">
               Surcharge Hotel Single:
            </td>
            <td class="contentValueOrig">
                <asp:TextBox ID="uoTextBoxSurchargeSingle" runat="server" Width="180px" onkeypress="return validate(event);" ></asp:TextBox>
            </td>
             <td class="contentCaptionOrig">
                Surcharge Hotel Double:
            </td>
            <td class="contentValueOrig">
                <asp:TextBox ID="uoTextBoxSurchargeDouble" runat="server" Width="180px" onkeypress="return validate(event);" ></asp:TextBox>
            </td>
        </tr>
    </table>
    <br />
    
    <div class="PageTitle">
        Luggage Service          
    </div>
    <table width="100%">
        <tr>
            <td class="contentCaptionOrig">
                Luggage Individual:
            </td>
            <td class="contentValueOrig">
                <asp:TextBox ID="uoTextBoxLuggageRate" runat="server" Width="180px" onkeypress="return validate(event);" ></asp:TextBox>                 
            </td>
             <td class="contentCaptionOrig">
                Unit of Measurement:
            </td>
            <td class="contentValueOrig">
                <asp:DropDownList runat="server" ID="uoDropDownListLuggaeUOM" Width="230px" AppendDataBoundItems="true"></asp:DropDownList>
            </td>
        </tr>
    </table>
    <br />
    
    <div class="PageTitle">
        Safeguard Service          
    </div>
    <table width="100%">
        <tr>
            <td class="contentCaptionOrig">
                Rate Amount:
            </td>
            <td class="contentValueOrig">
                <asp:TextBox ID="uoTextBoxSafeguardRate" runat="server" Width="180px" onkeypress="return validate(event);" ></asp:TextBox>
                &nbsp;&nbsp;
                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ValidationGroup="Safeguard"
                        ControlToValidate="uoTextBoxSafeguardRate" ErrorMessage="Safeguard Rate Required">*</asp:RequiredFieldValidator>
                    
            </td>
             <td class="contentCaptionOrig">
                Unit of Measurement:
            </td>
            <td class="contentValueOrig">
                <asp:DropDownList runat="server" ID="uoDropDownListSafeguardUOM" Width="230px" AppendDataBoundItems="true"></asp:DropDownList> 
                &nbsp;&nbsp;
                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ValidationGroup="Safeguard"
                        ControlToValidate="uoDropDownListSafeguardUOM" 
                        ErrorMessage="Unit of Measurement Required" InitialValue="0">*</asp:RequiredFieldValidator>
                &nbsp;&nbsp;
                <asp:Button ID="uoButtonSafeguardAdd" runat="server" Text="Add" CssClass="SmallButton"
                        Width="50px" ValidationGroup="Safeguard" OnClick="uoButtonSafeguardAdd_Click" />
            </td>
        </tr>
        <tr>
            <td></td>
            <td colspan="2">
                 <asp:GridView ID="uoGridViewSafeguard" runat="server" Width="100%" AutoGenerateColumns="False"
                        CssClass="listViewTable" OnRowDeleting="uoGridViewSafeguard_RowDeleting" OnSelectedIndexChanging="uoGridViewSafeguard_SelectedIndexChanging">
                        <Columns>
                            <asp:BoundField DataField="ContractDetailID" HeaderText="Contract Detail ID" Visible="false" />
                            <asp:BoundField DataField="PortAgentID" HeaderText="Vehicle ID" Visible="false" />
                            <asp:BoundField DataField="SafeguardUOMId" HeaderText="UOM ID" Visible ="false" />
                            <asp:BoundField DataField="SafeguardUOMName" HeaderText="UOM"  />
                            <asp:BoundField DataField="SafeguardRate" HeaderText="Rate" DataFormatString="{0:#,##0.00}" />                            
                            <asp:ButtonField CommandName="Delete" Text="Delete" />
                            <asp:ButtonField CommandName="Select" Text="Edit" />
                        </Columns>
                    </asp:GridView>
            </td>
        </tr>
    </table>
    
    <br />
    <div class="PageTitle">
        Meet and Greet Service       
    </div>
    <table width="100%">
        <tr>
            <td class="contentCaptionOrig">
                Rate Amount Per Person:
            </td>
            <td class="contentValueOrig">
                <asp:TextBox ID="uoTextBoxMeetGreetRate" runat="server" Width="180px" onkeypress="return validate(event);" ></asp:TextBox>
                &nbsp;&nbsp;</td>
             <td class="contentCaptionOrig">
                 
            </td>
            <td class="contentValueOrig">
                  <asp:Label ID="Label4"  CssClass="RedNotification" runat="server" Text="From & To: 24 hr format (e.g. 22:30)"> </asp:Label> 
            </td>
        </tr>
        <tr>
            <td class="contentCaptionOrig">
                Surcharge Fee:
            </td>
            <td class="contentValueOrig">
                <asp:TextBox ID="uoTextBoxMeetGreetSurchargeFee" runat="server" Width="180px" onkeypress="return validate(event);" ></asp:TextBox>
                &nbsp;&nbsp;                
                    
                <asp:CheckBox runat="server" ID="uoCheckBoxMeetGreetSurchargeIsPercent" Text="Is by Percent"/>
                    
            </td>
             <td class="contentCaptionOrig">
                 Over normal fee (Assistance between)</td>
            <td class="contentValueOrig">
                <table>
                    <tr>
                        <td>From:</td>
                        <td> <asp:TextBox ID="uoTextBoxFromMeetGreet" runat="server" Width="80px" onkeypress="return validate(event);" ></asp:TextBox>
                            <cc1:TextBoxWatermarkExtender ID="uoTxtBoxTime_TextBoxWatermarkExtender"
                                runat="server" Enabled="True" TargetControlID="uoTextBoxFromMeetGreet" WatermarkCssClass="fieldWatermark" WatermarkText="HH:mm">
                            </cc1:TextBoxWatermarkExtender>
                            <cc1:MaskedEditExtender ID="uoMaskedEditExtenderCheckInTime" runat="server" CultureAMPMPlaceholder=""
                                CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                Enabled="True" Mask="99:99" MaskType="Time" TargetControlID="uoTextBoxFromMeetGreet"
                                UserDateFormat="None" UserTimeFormat="TwentyFourHour">
                            </cc1:MaskedEditExtender>
                            
                        </td>
                        <td>To:</td>
                        <td> <asp:TextBox ID="uoTextBoxToMeetGreet" runat="server" Width="80px" onkeypress="return validate(event);" ></asp:TextBox>
                             <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3"
                                runat="server" Enabled="True" TargetControlID="uoTextBoxToMeetGreet" WatermarkCssClass="fieldWatermark" WatermarkText="HH:mm">
                            </cc1:TextBoxWatermarkExtender>
                            <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" CultureAMPMPlaceholder=""
                                CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                Enabled="True" Mask="99:99" MaskType="Time" TargetControlID="uoTextBoxToMeetGreet"
                                UserDateFormat="None" UserTimeFormat="TwentyFourHour">
                            </cc1:MaskedEditExtender>
                        </td>                        
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="contentCaptionOrig">
                Meet & Greet Remarks:
            </td>
            <td class="contentValueOrig" colspan="3">
                <asp:TextBox ID="uoTextBoxRemarksMeetGreet" runat="server" Width="800px" Rows="5" TextMode="MultiLine"></asp:TextBox>
                &nbsp;&nbsp;
            </td>             
        </tr>
    </table>
    
    <br />
    <div class="PageTitle">
        Visa Service      
    </div>
    <table width="100%">
        <tr>
            <td class="contentCaptionOrig">
                Visa:
            </td>
            <td class="contentValueOrig">
                <asp:TextBox ID="uoTextBoxVisaAmt" runat="server" Width="180px" onkeypress="return validate(event);" ></asp:TextBox>
             </td>
             <td class="contentCaptionOrig">
                 Visa-Accompany:
            </td>
            <td class="contentValueOrig">
                <asp:TextBox ID="uoTextBoxVisaAccompany" runat="server" Width="180px" onkeypress="return validate(event);" ></asp:TextBox>
            </td>
        </tr>        
         <tr>
            <td class="contentCaptionOrig">
                Immigration Fees:
            </td>
            <td class="contentValueOrig">
                <asp:TextBox ID="uoTextBoxImmigrationFees" runat="server" Width="180px" onkeypress="return validate(event);" ></asp:TextBox>
             </td>
             <td class="contentCaptionOrig">
                 Immigration Fees 2:
            </td>
            <td class="contentValueOrig">
                <asp:TextBox ID="uoTextBoxImmigrationFees2" runat="server" Width="180px" onkeypress="return validate(event);" ></asp:TextBox>
            </td>
        </tr> 
        <tr>
            <td class="contentCaptionOrig">
                Letter of Invitation:
            </td>
            <td class="contentValueOrig">
                <asp:TextBox ID="uoTextBoxLetterOfInvitation" runat="server" Width="180px" onkeypress="return validate(event);" ></asp:TextBox>
             </td>
             <td class="contentCaptionOrig">
                 Immigration/Port Captaincy Letter:
            </td>
            <td class="contentValueOrig">
                <asp:TextBox ID="uoTextBoxImmigrationPortCaptaincyLetter" runat="server" Width="180px" onkeypress="return validate(event);" ></asp:TextBox>
            </td>
        </tr> 
        <tr>
            <td class="contentCaptionOrig">
                Business Parole Request:
            </td>
            <td class="contentValueOrig">
                <asp:TextBox ID="uoTextBoxBusinessParoleRequest" runat="server" Width="180px" onkeypress="return validate(event);" ></asp:TextBox>
             </td>
             <td class="contentCaptionOrig">
                 Business Parole Processing Fee:
            </td>
            <td class="contentValueOrig">
                <asp:TextBox ID="uoTextBoxBusinessParoleProcessFee" runat="server" Width="180px" onkeypress="return validate(event);" ></asp:TextBox>
            </td>
        </tr> 
        <tr>
            <td class="contentCaptionOrig">
                Visa Remarks:
            </td>
            <td class="contentValueOrig" colspan="3">
                <asp:TextBox ID="uoTextBoxRemarksVisas" runat="server" Width="800px" Rows="5" TextMode="MultiLine"></asp:TextBox>
                &nbsp;&nbsp;
            </td>             
        </tr>
    </table>
    
     <br />
    <div class="PageTitle">
        Other Service      
    </div>
    <table width="100%">
        <tr>
            <td class="contentCaptionOrig">
                Shore Passes:
            </td>
            <td class="contentValueOrig">
                <asp:TextBox ID="uoTextBoxShorePassesRate" runat="server" Width="180px" onkeypress="return validate(event);" ></asp:TextBox>
             </td>
             <td class="contentCaptionOrig">
                 Answering Telephone Calls/Emails Rate:
            </td>
            <td class="contentValueOrig">
                <asp:TextBox ID="uoTextBoxAnsweringTelephoneCallsEmailRate" runat="server" Width="180px" onkeypress="return validate(event);" ></asp:TextBox>
            </td>
        </tr>  
        <tr>
            <td class="contentCaptionOrig">
                Lost Luggage Rate:
            </td>
            <td class="contentValueOrig">
                <asp:TextBox ID="uoTextBoxLostLuggageRate" runat="server" Width="180px" onkeypress="return validate(event);" ></asp:TextBox>
             </td>
             <td class="contentCaptionOrig">
                 Car Rate:
            </td>
            <td class="contentValueOrig">
                <asp:TextBox ID="uoTextBoxCarRate" runat="server" Width="180px" onkeypress="return validate(event);" ></asp:TextBox>
            </td>
        </tr>       
         <tr>
            <td class="contentCaptionOrig">
                Immigration Custody: Airport to Hotel
            </td>
            <td class="contentValueOrig">
                <asp:TextBox ID="uoTextBoxImmigrationCustodyServiceAirportHotelRate" runat="server" Width="180px" onkeypress="return validate(event);" ></asp:TextBox>
             </td>
             <td class="contentCaptionOrig">
                 Immigration Custody: at Hotel:
            </td>
            <td class="contentValueOrig">
                <asp:TextBox ID="uoTextBoxImmigrationCustodyServiceHotelRate" runat="server" Width="180px" onkeypress="return validate(event);" ></asp:TextBox>
            </td>
        </tr> 
        <tr>
            <td class="contentCaptionOrig">
               Immigration Custody: Hotel to Ship
            </td>
            <td class="contentValueOrig">
                <asp:TextBox ID="uoTextBoxImmigrationCustodyServiceHotelShipRate" runat="server" Width="180px" onkeypress="return validate(event);" ></asp:TextBox>
             </td>
             <td class="contentCaptionOrig">
                 Waiting Time Rate:
            </td>
            <td class="contentValueOrig">
                <asp:TextBox ID="uoTextBoxWaitingTimeRate" runat="server" Width="180px" onkeypress="return validate(event);" ></asp:TextBox>
            </td>
        </tr> 
        <tr>
            <td class="contentCaptionOrig">
                Transportation To Pharmacy:
            </td>
            <td class="contentValueOrig">
                <asp:TextBox ID="uoTextBoxTransportationToPharmacyRate" runat="server" Width="180px" onkeypress="return validate(event);" ></asp:TextBox>
             </td>
             <td class="contentCaptionOrig">
                 Transportation To Medical Facility:
            </td>
            <td class="contentValueOrig">
                <asp:TextBox ID="uoTextBoxTransportationToMedicalFacilityRate" runat="server" Width="180px" onkeypress="return validate(event);" ></asp:TextBox>
            </td>
        </tr> 
        <tr>
            <td class="contentCaptionOrig">
                Other Remarks:
            </td>
            <td class="contentValueOrig" colspan="3">
                <asp:TextBox ID="uoTextBoxRemarksOther" runat="server" Width="800px" Rows="5" TextMode="MultiLine"></asp:TextBox>
                &nbsp;&nbsp;
            </td>             
        </tr>
    </table>
    
    <br />
    <asp:UpdatePanel ID="UpdatePanel11" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <div class="PageTitle">
                <asp:Panel ID="Panel3" runat="server">
                    Upload Contract</asp:Panel>
            </div>
            <table width="100%">
                <tr>
                    <td align="left">
                        <asp:ValidationSummary ID="ValidationSummaryUpload" runat="server" ValidationGroup="Upload" />
                    </td>
                </tr>
                <tr>
                    <td class="style24" align="left">
                        Contract Attachment (pdf/doc/docx) :
                    </td>
                    <td align="left">
                        <asp:FileUpload ID="uoFileUploadContract" runat="server" Width="500px" />
                        <asp:Label ID="Label1" runat="server" ForeColor="Red"></asp:Label><br />
                        <asp:RegularExpressionValidator ID="uoRegularExpressionValidatorUpload" runat="server"
                            ControlToValidate="uoFileUploadContract" ErrorMessage="Please select a .pdf or .doc or .docx file"
                            ValidationExpression="^([a-zA-Z].*|[1-9].*)\.(((p|P)(d|D)(f|F))|((d|D)(o|O)(c|C))|((d|D)(o|O)(c|C)(x|X)))$"
                            ValidationGroup="Upload">*</asp:RegularExpressionValidator>
                        <asp:LinkButton runat="server" ID="uoLinkAttach" Text="Attach" OnClick="uoButtonUpload_Click">
                        </asp:LinkButton>
                        <%--<asp:FileUpload ID="uoFileUploadContract" runat="server" Width="70%" />
                    <asp:Button ID="uoButtonUpload" runat="server" onclick="uoButtonUpload_Click" 
                        Text="Upload"  />
                    <asp:Label ID="Label1" runat="server" ForeColor="Red"></asp:Label>
                    <asp:RegularExpressionValidator ID="uoRegularExpressionValidatorUpload" 
                                    runat="server" ControlToValidate="uoFileUploadContract" 
                                    ErrorMessage="Please select a .pdf or .doc or .docx file" 
                                    ValidationExpression="^([a-zA-Z].*|[1-9].*)\.(((p|P)(d|D)(f|F))|((d|D)(o|O)(c|C))|((d|D)(o|O)(c|C)(x|X)))$" 
                                    ValidationGroup="Upload">*</asp:RegularExpressionValidator>--%>
                    </td>
                </tr>
                <tr>
                    <td class="style24" align="left">
                    </td>
                    <td align="left">
                        <asp:ListView runat="server" ID="uoListViewAttachment" OnItemDeleting="uoListViewAttachment_ItemDeleting">
                            <LayoutTemplate>
                                <table border="0" cellpadding="0" cellspacing="0" class="listViewTable" width="200px">
                                    <tr>
                                        <th style="text-align: center; white-space: normal;">
                                            Filename
                                        </th>
                                        <th runat="server" style="width: 5%" id="DeleteTH" />
                                    </tr>
                                    <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                                </table>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td class="leftAligned">
                                        <%--<asp:HiddenField ID="uoHiddenFieldID" runat="server" Value='<%# Eval("AttachmentId") %>' />                                    
                                    <asp:Label ID="uoLabelVehicleTypeName" runat="server" Text='<%# Eval("FileName")%>'></asp:Label>--%>
                                        <asp:LinkButton runat="server" ID="uoLinkButtonFileName" Text='<%# Eval("FileName") %>'
                                            OnClick="ucLabelAttached_Click" CommandArgument='<%# Eval("FileName") %>'></asp:LinkButton>
                                    </td>
                                    <td style="width: 10%">
                                        <asp:LinkButton ID="uoLinkButtonDelete" runat="server" CommandName="Delete">Delete</asp:LinkButton>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <EmptyDataTemplate>
                                <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                                    <tr>
                                        <th style="text-align: center; white-space: normal;">
                                            Filename
                                        </th>
                                    </tr>
                                    <tr>
                                        <td class="leftAligned">
                                            No Record
                                        </td>
                                    </tr>
                                </table>
                            </EmptyDataTemplate>
                        </asp:ListView>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="style24" align="left">
                        <asp:HiddenField ID="uoHiddenFieldFileName" runat="server" />
                    </td>
                    <td align="left">
                        <asp:Label ID="ucLabelAttached" runat="server" ForeColor="Red" Visible="False"></asp:Label>
                        &nbsp;<asp:LinkButton ID="uoLinkButtonRemove" runat="server" Visible="False">Remove</asp:LinkButton></td>
                </tr>
                <tr>
                    <td class="style24" align="left">
                        <asp:HiddenField ID="uoHiddenFieldFileType" runat="server" />
                        <asp:HiddenField ID="uoHiddenFieldContractID" runat="server" Value="0" />
                        <asp:HiddenField ID="uoHiddenFieldUserName" runat="server" Value="" />
                        <asp:HiddenField ID="uoHiddenFieldVendorPortAgentIdInt" runat="server" Value="0" />
                        <asp:HiddenField ID="uoHiddenFieldURLFrom" runat="server" />
                    </td>
                    <td align="left">
                        <%--<asp:HiddenField ID="uoHiddenFieldFileData" runat="server" />--%>
                        &nbsp;
                    </td>
                </tr>
            </table>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="uoLinkAttach" />
            <asp:PostBackTrigger ControlID="uoListViewAttachment" />
        </Triggers>
    </asp:UpdatePanel>
    <table width="100%">
        <tr>
            <td align="center">
                <hr />
                <asp:Button ID="uoButtonSave" runat="server" Text="Save" Width="80" OnClick="uoButtonSave_Click"
                    ValidationGroup="Header" />
            &nbsp;
                <asp:Button ID="uoButtonBack" runat="server" Text="Back" Width="80" OnClick="uoButtonBack_Click"/>
               
            </td>
        </tr>
    </table>
    </div>
   </asp:Content>
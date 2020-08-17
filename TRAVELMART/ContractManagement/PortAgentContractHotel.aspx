<%@ Page Title="" Language="C#" MasterPageFile="~/Popup.Master" AutoEventWireup="true" CodeBehind="PortAgentContractHotel.aspx.cs" Inherits="TRAVELMART.ContractManagement.PortAgentContractHotel" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script language="javascript" type="text/javascript">
    function confirmDelete() {
        if (confirm("Delete Room?") == true)
            return true;
        else
            return false;
    }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<table width="100%">
    <tr>
        <td class="PageTitle" colspan="2"><%= AddEditLabel %></td>
    </tr>
    <tr><td colspan="2"><hr /></td></tr>
    <tr>
        <td class="LeftClass" style="width:150px;">&nbsp;Hotel Brand Name :</td>
        <td class="LeftClass">
            <asp:DropDownList runat="server" ID="uoDropDownListHotelBrandName" Width="95%"
                AutoPostBack="true" AppendDataBoundItems="true" 
                onselectedindexchanged="uoDropDownListHotelBrandName_SelectedIndexChanged"></asp:DropDownList>
           <asp:RequiredFieldValidator runat="server" ID="rfvBrand"
                ControlToValidate="uoDropDownListHotelBrandName" InitialValue="0"
                ErrorMessage="Hotel Brand required." ValidationGroup="Save">*</asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td class="LeftClass" style="width:150px;">&nbsp;Hotel Branch Name :</td>
        <td class="LeftClass">
            <asp:DropDownList runat="server" ID="uoDropDownListHotelBranch" Width="95%"
                AppendDataBoundItems="true" AutoPostBack="true" 
                onselectedindexchanged="uoDropDownListHotelBranch_SelectedIndexChanged">
                <asp:ListItem Text="--Select Vendor Branch--" Value="0"></asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator runat="server" ID="rfvBranch"
                ControlToValidate="uoDropDownListHotelBranch" InitialValue="0"
                ErrorMessage="Hotel Branch required." ValidationGroup="Save">*</asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr><td colspan="2"></td></tr>
    <tr>
        <td colspan="2" class="PageTitle">Shuttle</td>
    </tr>
    <tr><td colspan="2"><hr /></td></tr>
    <tr>
        <td class="leftAligned" style="width:150px;">&nbsp;With Shuttle :</td>
        <td class="leftAligned">
            <asp:CheckBox runat="server" ID="uoCheckBoxWithShuttle" />
        </td>
    </tr>
    <tr><td colspan="2"></td></tr>
    <tr>
        <td class="PageTitle" colspan="2">&nbsp;Meals</td>
    </tr>
    <tr><td colspan="2"><hr /></td></tr>
    <tr>
        <td class="leftAligned" style="width:150px;">&nbsp;Meal Rate :</td>
        <td class="leftAligned">
            <asp:TextBox runat="server" ID="uoTextBoxMealRate" Width="200px"></asp:TextBox>
            <cc1:MaskedEditExtender runat="server" ID="uoTextBoxMealRate_MaskedEditExtender"
                InputDirection="RightToLeft" Mask="9,999.99" TargetControlID="uoTextBoxMealRate"
                MaskType="Number" Enabled="true"></cc1:MaskedEditExtender>
            <asp:RequiredFieldValidator runat="server" ID="rfvMealRate"
                ControlToValidate="uoTextBoxMealRate" ValidationGroup="Save"
                ErrorMessage="Meal Rate is required.">*</asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td class="leftAligned" style="width:150px;">&nbsp;Meal Rate Tax (%) :</td>
        <td class="leftAligned">
            <asp:TextBox runat="server" ID="uoTextBoxMealTax" Width="200px"></asp:TextBox>
            <cc1:MaskedEditExtender runat="server" ID="uoTextBoxMealTax_MaskedEditExtender"
                InputDirection="RightToLeft" Mask="999.99" TargetControlID="uoTextBoxMealTax"
                MaskType="Number" Enabled="true"></cc1:MaskedEditExtender>
            <asp:RequiredFieldValidator runat="server" ID="rfvMealTax"
                ControlToValidate="uoTextBoxMealTax" ValidationGroup="Save"
                ErrorMessage="Meal Rate Tax is required.">*</asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td class="leftAligned">&nbsp;Meal Rate Tax inclusive :</td>
        <td class="leftAligned">
            <asp:CheckBox runat="server" ID="uoCheckBoxTaxInclusive" />
        </td>
    </tr>
    <tr>
        <td class="leftAligned">&nbsp;Meals included:</td>
        <td class="leftAligned">
            <asp:CheckBox runat="server" ID="uoCheckBoxBreakFast" Text="Breakfast" />
        </td>
    </tr>
    <tr>
        <td></td>
        <td class="leftAligned">
            <asp:CheckBox runat="server" ID="uoCheckBoxLunch" Text="Lunch" />
        </td>
    </tr>
     <tr>
        <td></td>
        <td class="leftAligned">
            <asp:CheckBox runat="server" ID="uoCHeckBoxDinner" Text="Dinner" />
        </td>
    </tr>
     <tr>
        <td></td>
        <td class="leftAligned">
            <asp:CheckBox runat="server" ID="uoCheckBoxLunchOrDinner" Text="Lunch or Dinner" />
        </td>
    </tr>
    <tr><td colspan="2"></td></tr>
    <tr runat="server" id="uoTRRooms" visible="false">
        <td colspan="2">
            <asp:Panel runat="server" ID="panelRooms">
                <div  class="PageTitle">
                    Rooms &nbsp;
                    <asp:Label runat="server" ID="uoLabelRoomHeader" Text="" Font-Size="Smaller" Font-Italic="true"></asp:Label>
                </div>                
            </asp:Panel>
            <asp:Panel runat="server" ID="panelRoomDetails">
                <cc1:CollapsiblePanelExtender runat="server" ID="CollapsiblePanelExtenderRooms"
                    CollapseControlID="panelRooms" Collapsed="true"
                    ExpandControlID="panelRooms"
                    TargetControlID="panelRoomDetails"
                    CollapsedText="(Show Details)"
                    ExpandedText="(Hide Details)"
                    TextLabelID="uoLabelRoomHeader"
                    ExpandDirection="Vertical">
                </cc1:CollapsiblePanelExtender>
                <table width="100%">
                    <tr>
                        <td style="width:50%">
                            <table width="100%">
                                <tr>
                                    <td class="LeftClass">&nbsp; Room Type :</td>
                                    <td class="LeftClass">
                                        <asp:DropDownList runat="server" ID="uoDropDownListRoomType"
                                            AppendDataBoundItems="true" Width="200px"></asp:DropDownList>
                                        <asp:RequiredFieldValidator runat="server" ID="rfvRoomType"
                                            ValidationGroup="SaveRoom" ControlToValidate="uoDropDownListRoomType"
                                            ErrorMessage="Room Type required." InitialValue="0">*</asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="LeftClass">&nbsp; Date From :</td>
                                    <td class="LeftClass">
                                        <asp:TextBox runat="server" ID="uoTextBoxDateFrom" Width="200px"></asp:TextBox>
                                        <cc1:TextBoxWatermarkExtender ID="uoTextBoxDateFrom_TextBoxWatermarkExtender" runat="server"
                                            Enabled="True" TargetControlID="uoTextBoxDateFrom" WatermarkCssClass="fieldWatermark"
                                            WatermarkText="MM/dd/yyyy">
                                        </cc1:TextBoxWatermarkExtender>
                                        <cc1:MaskedEditExtender ID="uoTextBoxDateFrom_MaskedEditExtender" 
                                            runat="server" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                                            CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                            CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                                            TargetControlID="uoTextBoxDateFrom" Mask="99/99/9999" UserDateFormat="MonthDayYear" MaskType="Date">
                                        </cc1:MaskedEditExtender>
                                        <cc1:CalendarExtender ID="uoTextBoxDateFrom_CalendarExtender" runat="server" 
                                            Enabled="True" PopupButtonID="Image1" TargetControlID="uoTextBoxDateFrom"  Format="MM/dd/yyyy">
                                        </cc1:CalendarExtender>
                                        <asp:Image ID="Image1" runat="server" 
                                            ImageUrl="~/Images/Calendar_scheduleHS.png" Height="16px" />
                                        <asp:RequiredFieldValidator ID="rfvStartDate" ControlToValidate="uoTextBoxDateFrom"
                                            runat="server" ErrorMessage="Date From required."
                                            ValidationGroup="SaveRoom">*</asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="LeftClass">&nbsp; Date To :</td>
                                    <td class="LeftClass">
                                        <asp:TextBox runat="server" ID="uoTextBoxDateTo" Width="200px"></asp:TextBox>
                                        <cc1:TextBoxWatermarkExtender ID="uoTextBoxDateTo_TextBoxWatermarkExtender" runat="server"
                                            Enabled="True" TargetControlID="uoTextBoxDateTo" WatermarkCssClass="fieldWatermark"
                                            WatermarkText="MM/dd/yyyy">
                                        </cc1:TextBoxWatermarkExtender>
                                        <cc1:MaskedEditExtender ID="uoTextBoxDateTo_MaskedEditExtender" 
                                            runat="server" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                                            CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                            CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                                            TargetControlID="uoTextBoxDateTo" Mask="99/99/9999" UserDateFormat="MonthDayYear" MaskType="Date">
                                        </cc1:MaskedEditExtender>
                                        <cc1:CalendarExtender ID="uoTextBoxDateTo_CalendarExtender" runat="server" 
                                            Enabled="True" PopupButtonID="Image2" TargetControlID="uoTextBoxDateTo"  Format="MM/dd/yyyy">
                                        </cc1:CalendarExtender>
                                        <asp:Image ID="Image2" runat="server" 
                                            ImageUrl="~/Images/Calendar_scheduleHS.png" Height="16px" />
                                        <asp:RequiredFieldValidator ID="rfvEndDate" ControlToValidate="uoTextBoxDateTo"
                                            runat="server" ErrorMessage="Date To required."
                                            ValidationGroup="SaveRoom">*</asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="LeftClass">&nbsp;Currency :</td>
                                    <td class="LeftClass">
                                        <asp:DropDownList runat="server" ID="uoDropDownListCurrency"
                                            AppendDataBoundItems="true" Width="200px"></asp:DropDownList>
                                        <asp:RequiredFieldValidator runat="server" ID="rfvCurrency"
                                            ValidationGroup="SaveRoom" InitialValue="0" ControlToValidate="uoDropDownListCurrency"
                                            ErrorMessage="Currency required.">*</asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="LeftClass">&nbsp;Room Rate :</td>
                                    <td class="LeftClass">
                                        <asp:TextBox runat="server" ID="uoTextBoxRoomRate" Width="200px"></asp:TextBox>
                                        <cc1:MaskedEditExtender runat="server" ID="uoTextBoxRoomRate_MaskedEditExtender"
                                            Enabled="true" InputDirection="LeftToRight" Mask="999,999.99"
                                            MaskType="Number" TargetControlID="uoTextBoxRoomRate"></cc1:MaskedEditExtender>
                                        <asp:RequiredFieldValidator runat="server" ID="rfvRoomRate" 
                                            ValidationGroup="SaveRoom" ErrorMessage="Room rate required." 
                                            ControlToValidate="uoTextBoxRoomRate">*</asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="LeftClass">&nbsp;Room Rate Tax Inclusive :</td>
                                    <td class="LeftClass">
                                        <asp:CheckBox runat="server" ID="uoCheckBoxRoomTaxInclusive" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="LeftClass">&nbsp;Room Rate Tax (%) :</td>
                                    <td class="LeftClass">
                                        <asp:TextBox runat="server" ID="uoTextBoxRoomTax" Width="200px"></asp:TextBox>
                                        <cc1:MaskedEditExtender runat="server" ID="uoTextBoxRoomTax_MaskedEditExtender"
                                            Enabled="true" InputDirection="LeftToRight" Mask="999.99"
                                            TargetControlID="uoTextBoxRoomTax"></cc1:MaskedEditExtender>
                                        <asp:RequiredFieldValidator runat="server" ID="rfvRoomTax"
                                            ValidationGroup="SaveRoom" ControlToValidate="uoTextBoxRoomTax"
                                            ErrorMessage="Room Rate Tax required.">*</asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td class="LeftClass">
                                        <asp:Button runat="server" ID="uoButtonSaveRoom" ValidationGroup="SaveRoom"
                                            Text="Add Room" onclick="uoButtonSaveRoom_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            <table width="100%">
                                <tr>
                                    <td></td>
                                    <td style="font-weight:bold">Daily Room Count</td>
                                </tr>
                                <tr>
                                    <td class="RightClass">Monday :</td>
                                    <td class="LeftClass">
                                        <asp:TextBox runat="server" ID="uoTextBoxMonday"></asp:TextBox>
                                         <asp:RequiredFieldValidator runat="server" ID="rfvMonday"
                                            ValidationGroup="SaveRoom" ControlToValidate="uoTextBoxMonday"
                                            ErrorMessage="Monday Count required.">*</asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="RightClass">Tuesday :</td>
                                    <td class="LeftClass">
                                        <asp:TextBox runat="server" ID="uoTextBoxTuesday"></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" ID="rfvTuesday"
                                            ValidationGroup="SaveRoom" ControlToValidate="uoTextBoxTuesday"
                                            ErrorMessage="Tuesday Count required">*</asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="RightClass">Wednesday :</td>
                                    <td class="LeftClass">
                                        <asp:TextBox ID="uoTextBoxWednesday" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" ID="rfvWednesday"
                                            ValidationGroup="SaveRoom" ControlToValidate="uoTextBoxWednesday"
                                            ErrorMessage="Wednesday Count required">*</asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="RightClass">Thursday :</td>
                                    <td class="LeftClass">
                                        <asp:TextBox runat="server" ID="uoTextBoxThursday"></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" ID="rfvThursday"
                                            ValidationGroup="SaveRoom" ControlToValidate="uoTextBoxThursday"
                                            ErrorMessage="Thursday Count required">*</asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="RightClass">Friday :</td>
                                    <td class="LeftClass">
                                        <asp:TextBox runat="server" ID="uoTextBoxFriday"></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" ID="rfvFriday"
                                            ValidationGroup="SaveRoom" ControlToValidate="uoTextBoxFriday"
                                            ErrorMessage="Friday Count required">*</asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="RightClass">Saturday :</td>
                                    <td class="LeftClass">
                                        <asp:TextBox runat="server" ID="uoTextBoxSaturday"></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" ID="rfvSaturday"
                                            ValidationGroup="SaveRoom" ControlToValidate="uoTextBoxSaturday"
                                            ErrorMessage="Saturday Count required">*</asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="RightClass">Sunday :</td>
                                    <td class="LeftClass">
                                        <asp:TextBox runat="server" ID="uoTextBoxSunday"></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" ID="rfvSunday"
                                            ValidationGroup="SaveRoom" ControlToValidate="uoTextBoxSunday"
                                            ErrorMessage="Sunday Count required">*</asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </td>
    </tr>
    
    <tr><td colspan="2"></td></tr>
    <tr>
        <td colspan="2" class="Module">
            <asp:ListView runat="server" ID="uoRoomsList" 
                onitemcommand="uoRoomsList_ItemCommand" 
                onitemdeleting="uoRoomsList_ItemDeleting">
                <LayoutTemplate>
                    <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                        <tr>
                            <th>Room Type</th>
                            <th>Date From</th>
                            <th>Date To</th>
                            <th>Room Rate</th>
                            <th>Room Tax Rate (%)</th>
                            <th>Currency</th>
                            <th>Room Rate Tax Inclusive</th>
                            <th>Mon</th>
                            <th>Tue</th>
                            <th>Wed</th>
                            <th>Thu</th>
                            <th>Fri</th>
                            <th>Sat</th>
                            <th>Sun</th>
                            <th runat="server" id="DeleteTH" style="width:10%"></th>
                        </tr>
                        <asp:panel runat="server" ID="itemPlaceHolder"></asp:panel>
                    </table>
                </LayoutTemplate>
                <ItemTemplate>
                    <tr>
                        <td class="leftAligned"><%# Eval("colRoomNameVarchar") %></td>
                        <td class="leftAligned"><%# String.Format("{0:dd-MMM-yyyy}", Eval("colDateFromDatetime")) %></td>
                        <td class="leftAligned"><%# String.Format("{0:dd-MMM-yyyy}", Eval("colDateToDatetime")) %></td>
                        <td class="leftAligned"><%# Eval("colServiceRateMoney") %></td>
                        <td class="leftAligned"><%# Eval("colServiceRateTaxDecimal") %></td>
                        <td class="leftAligned"><%# Eval("colCurrencyNameVarchar") %></td>
                        <td class="leftAligned"><%# Eval("colServiceRateTaxInclusiveBit") %></td>
                        <td class="leftAligned"><%# Eval("colMondayCountInt") %></td>
                        <td class="leftAligned"><%# Eval("colTuesdayCountInt")%></td>
                        <td class="leftAligned"><%# Eval("colWednesdayCountInt")%></td>
                        <td class="leftAligned"><%# Eval("colThursdayCountInt")%></td>
                        <td class="leftAligned"><%# Eval("colFridayCountInt")%></td>
                        <td class="leftAligned"><%# Eval("colSaturdayCountInt")%></td>
                        <td class="leftAligned"><%# Eval("colSundayCountInt")%></td>
                        <td class="leftAligned">
                            <asp:LinkButton runat="server" ID="uoHyperLinkDelete" CommandName="Delete"
                                CommandArgument='<%# Eval("colContractPortAgentServiceSpecificationsIdInt") %>' 
                                Text="Delete" OnClientClick="return confirmDelete();">
                            </asp:LinkButton>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:ListView>
        </td>
    </tr>
    <tr><td colspan="2" align="center"><asp:Button runat="server" ID="uoButtonSave" Text="Save" ValidationGroup="Save" 
            onclick="uoButtonSave_Click" />
    </td></tr>
</table>

<asp:HiddenField ID="uoHiddenFieldservicedetailId" runat="server" Value="0" EnableViewState="true" />
</asp:Content>

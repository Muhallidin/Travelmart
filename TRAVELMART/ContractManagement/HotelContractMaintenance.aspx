<%@ Page Title="" Language="C#" MasterPageFile="~/Popup.Master" AutoEventWireup="true"
    CodeBehind="HotelContractMaintenance.aspx.cs" Inherits="TRAVELMART.ContractManagement.HotelContractMaintenance" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="leftAligned">
        <div class="PageTitle">
            Hotel Contract
        </div>
        <hr />
        <table width="80%" style="text-align: left">
            <tr>
                <td colspan="4">
                </td>
            </tr>
            <tr>
                <td style="width: 20%">
                    &nbsp;Vendor Code:
                </td>
                <td colspan="3">
                    <asp:TextBox ID="uoTextBoxVendorCode" runat="server" Width="447px" />
                    &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="uoTextBoxVendorCode"
                        ErrorMessage="Vendor code required." ValidationGroup="Save">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;Vendor Name:
                </td>
                <td colspan="3">
                    <asp:TextBox ID="uoTextBoxVendorName" runat="server" Width="447px" />
                    &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="uoTextBoxVendorName"
                        ErrorMessage="Vendor name required." ValidationGroup="Save">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;Contract Title:
                </td>
                <td colspan="3">
                    <asp:TextBox ID="uotextboxContractTitle" runat="server" Width="447px" />
                    &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="uoTextBoxVendorName"
                        ErrorMessage="Vendor name required." ValidationGroup="Save">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    Remarks :
                </td>
                <td colspan="3">
                    <asp:TextBox ID="uotextboxRemarks" runat="server" Width="445px" TextMode="MultiLine" CssClass=TextBoxInput />
                </td>
            </tr>
            <tr>
                <td>
                    Contract Start Date :
                </td>
                <td>
                    <asp:TextBox ID="uotextboxStartDate" runat="server" />
                    <%--<asp:ImageButton ID="uoimagebuttonCalendar" runat="server" ImageUrl="~/Images/Calendar_scheduleHS.png" />--%>
                    <cc1:CalendarExtender ID="uotextboxStartDate_CalendarExtender" runat="server" Enabled="True"
                        PopupButtonID="uoimagebuttonCalendar" TargetControlID="uotextboxStartDate" Format="MM/dd/yyyy hh:mm">
                    </cc1:CalendarExtender>
                    <cc1:MaskedEditExtender ID="uotextboxStartDate_MaskedEditExtender" runat="server"
                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                        CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                        CultureTimePlaceholder="" Enabled="True" MaskType="DateTime" TargetControlID="uotextboxStartDate"
                        Mask="99/99/9999 99:99" UserDateFormat="MonthDayYear">
                    </cc1:MaskedEditExtender>
                </td>
                <td>
                    Contract End Date :
                </td>
                <td>
                    <asp:TextBox ID="uotextboxEndDate" runat="server" />
                    <%--<asp:ImageButton ID="uoimagebuttonCalendar1" runat="server" ImageUrl="~/Images/Calendar_scheduleHS.png" />--%>
                    <cc1:CalendarExtender ID="uotextboxEndDate_CalendarExtender" runat="server" Enabled="True"
                        PopupButtonID="uoimagebuttonCalendar1" TargetControlID="uotextboxEndDate" Format="MM/dd/yyyy hh:mm">
                    </cc1:CalendarExtender>
                    <cc1:MaskedEditExtender ID="uotextboxEndDate_MaskedEditExtender" runat="server" CultureAMPMPlaceholder=""
                        CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                        CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                        Enabled="True" MaskType="DateTime" TargetControlID="uotextboxEndDate" Mask="99/99/9999 99:99"
                        UserDateFormat="MonthDayYear">
                    </cc1:MaskedEditExtender>
                </td>
            </tr>
            <tr>
                <td>
                    RCCL Personnel :
                </td>
                <td>
                    <asp:TextBox ID="uotextboxRCCLRep" runat="server" />
                </td>
                <td>
                    Vendor Personnel :
                </td>
                <td>
                    <asp:TextBox ID="uotextboxVendorRep" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td colspan="3">
                    <asp:Button ID="uobtnSave" runat="server" Text="Save" Font-Size="X-Small" OnClick="uobtnSave_Click" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <br />
                    <asp:HiddenField ID="uohiddenvID" runat="server" />
                </td>
            </tr>
        </table>
        <div class="PageTitle">
            <asp:Panel ID="uopanelroomhead" runat="server">
                Rooms
                <asp:Label ID="uolabelRoom" runat="server" Text="" Font-Size="Small" />
            </asp:Panel>
        </div>
        <asp:Panel ID="uopanelroom" runat="server" CssClass="CollapsiblePanel">
            <table width="100%">
                <tr>
                    <td colspan="2">
                    </td>
                </tr>
                <tr>
                    <td style="width: 34%">
                        <table>
                            <tr>
                                <td class="leftAligned">
                                    Room Type :
                                </td>
                                <td class="leftAligned">
                                    <asp:TextBox ID="uotextboxRoomType" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Date Range :
                                </td>
                                <td>
                                    <asp:TextBox ID="uotextboxRoomDate" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Room Rate :
                                </td>
                                <td>
                                    <asp:TextBox ID="uotextboxRoomRate" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Room Type :
                                </td>
                                <td>
                                    <asp:TextBox ID="TextBox3" runat="server" />
                                </td>
                            </tr>
                            <tr><td></td>
                                <td>
                                    <b>Daily room count</b>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Monday :
                                </td>
                                <td>
                                    <asp:TextBox ID="uotextboxMondayRoom" Width="60px" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Tuesday :
                                </td>
                                <td>
                                    <asp:TextBox ID="uotextboxTuesdayRoom" Width="60px" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Wednesday :
                                </td>
                                <td>
                                    <asp:TextBox ID="uotextboxWednesdayRoom" Width="60px" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Thursday :
                                </td>
                                <td>
                                    <asp:TextBox ID="uotextboxThursdayRoom" Width="60px" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Friday :
                                </td>
                                <td>
                                    <asp:TextBox ID="uotextboxFridayRoom" Width="60px" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Saturday :
                                </td>
                                <td>
                                    <asp:TextBox ID="uotextboxSaturdayRoom" Width="60px" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Sunday :
                                </td>
                                <td>
                                    <asp:TextBox ID="uotextboxSundayRoom" Width="60px" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                    <asp:Button ID="uobtnAddRoom" runat="server" Text="ADD" Font-Size="X-Small" OnClick="uobtnAddRoom_Click" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" valign="top" class="leftAligned">
                        <asp:ListView runat="server" ID="uoRoomList">
                            <LayoutTemplate>
                                <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                                    <tr>
                                        <th>
                                            Type of Room
                                        </th>
                                        <th>
                                            Date Range
                                        </th>
                                        <th>
                                            Room Rate
                                        </th>
                                        <th runat="server" style="width: 5%" id="EditTH" />
                                        <th runat="server" style="width: 5%" id="DeleteTH" />
                                    </tr>
                                    <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                                </table>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td class="leftAligned">
                                    </td>
                                    <td class="leftAligned">
                                    </td>
                                    <td class="leftAligned">
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <EmptyDataTemplate>
                                <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                                    <tr>
                                        <th>
                                            Type of Room
                                        </th>
                                        <th>
                                            Date Range
                                        </th>
                                        <th>
                                            Room Rate
                                        </th>
                                    </tr>
                                    <tr>
                                        <td colspan="3" class="leftAligned">
                                            No Record
                                        </td>
                                    </tr>
                                </table>
                            </EmptyDataTemplate>
                        </asp:ListView>
                        <asp:DataPager ID="uoRoomListPager" runat="server" PagedControlID="uoRoomList" PageSize="5">
                            <Fields>
                                <asp:NumericPagerField ButtonType="link" NumericButtonCssClass="PagerClass" />
                            </Fields>
                        </asp:DataPager>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <cc1:CollapsiblePanelExtender ID="uocollapsibleRoom" runat="server" TargetControlID="uopanelRoom"
            ExpandControlID="uopanelroomhead" CollapseControlID="uopanelroomhead" TextLabelID="uolabelRoom"
            CollapsedText="(Show List)" ExpandedText="(Hide List)" Collapsed="true" SuppressPostBack="true">
        </cc1:CollapsiblePanelExtender>
        <br />
        <div class="PageTitle">
            <asp:Panel ID="uopanelmealhead" runat="server">
                Meals
                <asp:Label ID="uolabelMeals" runat="server" Text="" Font-Size="Small" />
            </asp:Panel>
        </div>
        <asp:Panel ID="uopanelMeal" runat="server" CssClass="CollapsiblePanel">
            <table width="100%">
                <tr>
                    <td colspan="2">
                    </td>
                </tr>
                <tr>
                    <td class="RightClass" style="width: 32%">
                        Meal Type :
                        <asp:TextBox ID="uotextboxMealType" runat="server" /><br />
                        Date Range :
                        <asp:TextBox ID="uotextboxMealDate" runat="server" /><br />
                        Meal Rate :
                        <asp:TextBox ID="uotextboxMealRate" runat="server" /><br />
                        <asp:Button ID="uobtnAddMeal" runat="server" Text="ADD" Font-Size="X-Small" OnClick="uobtnAddMeal_Click" />
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:ListView runat="server" ID="uoMealList">
                            <LayoutTemplate>
                                <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                                    <tr>
                                        <th>
                                            Meal Type
                                        </th>
                                        <th>
                                            Date Range
                                        </th>
                                        <th>
                                            Meal Rate
                                        </th>
                                        <th runat="server" style="width: 5%" id="EditTH" />
                                        <th runat="server" style="width: 5%" id="DeleteTH" />
                                    </tr>
                                    <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                                </table>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td class="leftAligned">
                                    </td>
                                    <td class="leftAligned">
                                    </td>
                                    <td class="leftAligned">
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <EmptyDataTemplate>
                                <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                                    <tr>
                                        <th>
                                            Type of Room
                                        </th>
                                        <th>
                                            Date Range
                                        </th>
                                        <th>
                                            Room Rate
                                        </th>
                                    </tr>
                                    <tr>
                                        <td colspan="3" class="leftAligned">
                                            No Record
                                        </td>
                                    </tr>
                                </table>
                            </EmptyDataTemplate>
                        </asp:ListView>
                        <asp:DataPager ID="uoMealListPager" runat="server" PagedControlID="uoMealList" PageSize="5">
                            <Fields>
                                <asp:NumericPagerField ButtonType="link" NumericButtonCssClass="PagerClass" />
                            </Fields>
                        </asp:DataPager>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <cc1:CollapsiblePanelExtender ID="uocollapsibleMeal" runat="server" TargetControlID="uopanelMeal"
            ExpandControlID="uopanelmealhead" CollapseControlID="uopanelmealhead" TextLabelID="uolabelMeals"
            CollapsedText="(Show List)" ExpandedText="(Hide List)" Collapsed="true" SuppressPostBack="true">
        </cc1:CollapsiblePanelExtender>
    </div>
</asp:Content>

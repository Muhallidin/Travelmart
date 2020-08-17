<%@ Page Title="" Language="C#" MasterPageFile="~/Popup.Master" AutoEventWireup="true"
    CodeBehind="PortEditor.aspx.cs" Inherits="TRAVELMART.PortEditor" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="PageTitle">
        Port Arrival Confirmation
    </div>
    <hr />
    <table>
        <tr>
            <td colspan="2">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                Seafarer Name :
            </td>
            <td>
                <asp:TextBox ID="uotextboxSFName" runat="server" Width="300px" ReadOnly="true" CssClass="ReadOnly" />
            </td>
        </tr>
        <tr>
            <td>
                Nationality :
            </td>
            <td>
                <asp:TextBox ID="uotextboxSFNationality" runat="server" Width="300px" ReadOnly="true"
                    CssClass="ReadOnly" />
            </td>
        </tr>
        <tr>
            <td>
                Rank :
            </td>
            <td>
                <asp:TextBox ID="uotextboxSFRank" runat="server" Width="300px" ReadOnly="true" CssClass="ReadOnly" />
            </td>
        </tr>
        <tr>
            <td>
                Ship :
            </td>
            <td>
                <asp:TextBox ID="uotextboxSFVessel" runat="server" Width="300px" ReadOnly="true"
                    CssClass="ReadOnly" />
            </td>
        </tr>
        <tr>
            <td>
                Port :
            </td>
            <td>
                <asp:TextBox ID="uoTextboxSFPort" runat="server" Width="300px" ReadOnly="true" CssClass="ReadOnly" />
                <asp:DropDownList ID="uoDropDownListPort" runat="server" Width="305px" AppendDataBoundItems="True"
                    DataTextField="PORT" DataValueField="PORTID">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="uoRequiredFieldValidatorPort" ControlToValidate="uoDropDownListPort"
                    runat="server" ErrorMessage="*" ValidationGroup="Save" />
            </td>
        </tr>
        <tr>
            <td>
                Arrival Date/Time :
            </td>
            <td>
                <asp:TextBox ID="uoTextBoxDropOffDate" runat="server" Width="298px" />
                <asp:ImageButton ID="uoimagebuttonCalendar" runat="server" ImageUrl="~/Images/Calendar_scheduleHS.png" />
                <cc1:CalendarExtender ID="uoTextBoxDropOffDate_CalendarExtender" runat="server" Enabled="True"
                    PopupButtonID="uoimagebuttonCalendar" TargetControlID="uoTextBoxDropOffDate"
                    Format="MM/dd/yyyy hh:mm">
                </cc1:CalendarExtender>
                <cc1:MaskedEditExtender ID="uoTextBoxDropOffDate_MaskedEditExtender" runat="server"
                    CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                    CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                    CultureTimePlaceholder="" Enabled="True" MaskType="DateTime" TargetControlID="uoTextBoxDropOffDate"
                    Mask="99/99/9999 99:99" UserDateFormat="MonthDayYear">
                </cc1:MaskedEditExtender>
                <asp:RequiredFieldValidator ID="uorfvDate" ControlToValidate="uoTextBoxDropOffDate"
                    runat="server" ErrorMessage="*" ValidationGroup="Save" />
            </td>
        </tr>
        <tr>
            <td>
                Status :
            </td>
            <td>
                <asp:DropDownList ID="uoDropdownListPortTravelStatus" runat="server" Width="305px">
                    <asp:ListItem Value="0" Text="--Select Status--" Selected="True" />
                    <asp:ListItem Value="ON" Text="ON" />
                    <asp:ListItem Value="OFF" Text="OFF" />
                    <asp:ListItem Value="NO SHOW" Text="NO SHOW" />
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="uorfvPort" ControlToValidate="uoDropdownListPortTravelStatus"
                    runat="server" InitialValue="0" ErrorMessage="*" ValidationGroup="Save" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td align="left">
                <asp:Button ID="uobuttonSave" runat="server" Text="Save" OnClick="uobuttonSave_Click"
                    Width="59px" ValidationGroup="Save" />
                <asp:HiddenField ID="uoHiddenFieldPortID" runat="server" Value="0"/>
                <asp:HiddenField ID="uoHiddenFieldSeafarerID" runat="server" />
                <asp:HiddenField ID="uoHiddenFieldPortTransID" runat="server" Value="0"/>
                <asp:HiddenField ID="uoHiddenFieldTravelRequestID" runat="server" Value="0"/>
                <asp:HiddenField ID="uoHiddenFieldManualRequestID" runat="server" Value="0"/>
                <asp:HiddenField ID="uoHiddenFieldRecordLocator" runat="server" Value="0"/>            
                <asp:HiddenField ID="uoHiddenFieldStatus" runat="server" Value="0"/>
                <asp:HiddenField ID="uoHiddenFieldContractId" runat="server" />
            </td>
        </tr>
    </table>
</asp:Content>

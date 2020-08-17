<%@ Page Title="" Language="C#" MasterPageFile="~/TravelMart.Master"
    AutoEventWireup="true" CodeBehind="PortAgent.aspx.cs" Inherits="TRAVELMART.WebForm5" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css" media="screen">
        .inputText
        {
            font-family: "trebuchet ms" , Arial, Helvetica, sans-serif;
            font-size: 11px;
            height: 14px;
        }
        .custom-calendar .ajax__calendar_container
        {
            background-color: #B9DCFF; /* pale yellow */
            border: solid 1px #666;
             position: relative;
        }
        .custom-calendar .ajax__calendar_title
        {
            background-color: #35556b; /* pale green */
            height: 20px;
            color: #fff;
            vertical-align: middle;
        }
        .custom-calendar .ajax__calendar_prev, .custom-calendar .ajax__calendar_next
        {
            /*background-color:#aaa; darker gray */
            background-color: #35556b;
            height: 20px;
            width: 20px;
        }
        .custom-calendar .ajax__calendar_today
        {
            background-color: #5382A1; /* pale blue */
            height: 20px;
        }
        .custom-calendar .ajax__calendar_days table thead tr td
        {
            background-color: #5382A1; /* dark yellow */ /* color:#333;*/
        }
        .custom-calendar .ajax__calendar_day
        {
            color: #333; /* normal day - darker gray color */
        }
        .custom-calendar .ajax__calendar_other .ajax__calendar_day
        {
            color: #666; /* day not actually in this
    month - lighter gray color */
        }
        .custom-calendar .ajax__calendar_active .ajax__calendar_day
        {
            font-weight: bold; /* color:#000;*/
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table>
        <tr>
            <td colspan="2">
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="uclabelCompanyName" runat="server" Text="Service Provider Company Name :" />
            </td>
            <td>
                <asp:TextBox ID="uotextboxCompanyName" runat="server" Width="300px" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="uclabelAddress" runat="server" Text="Service Provider Company Address :" />
            </td>
            <td>
                <asp:TextBox ID="uotextboxAddress" runat="server" Width="300px"/>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="uclabelMainContactNum" runat="server" Text="Main Contact Number :" />
            </td>
            <td>
                <asp:TextBox ID="uotextboxMainContactNum" runat="server" Width="300px"/>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="uclabelEmergencyContact" runat="server" Text="Emergency Contact Number :" />
            </td>
            <td>
                <asp:TextBox ID="uotextboxEmergencyContact" runat="server" Width="300px"/>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="uclabelFaxNumber" runat="server" Text="Service Provider Fax Number :" />
            </td>
            <td>
                <asp:TextBox ID="uotextboxFaxNumber" runat="server" Width="300px"/>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="uclabelEmailAddress" runat="server" Text="Service Provider Email Address :" />
            </td>
            <td>
                <asp:TextBox ID="uotextboxEmailAddress" runat="server" Width="300px"/>
                <asp:RegularExpressionValidator ID="uoregexvalEmail" ControlToValidate="uotextboxEmailAddress"
                    runat="server" ErrorMessage="*" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                    ValidationGroup="Save" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="uclabelMainContactPerson" runat="server" Text="Main Contact Person :" />
            </td>
            <td>
                <asp:TextBox ID="uotextboxMainContactPerson" runat="server" Width="300px"/>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="uclabelAlternateContactPerson" runat="server" Text="Alternate Contact Person :" />
            </td>
            <td>
                <asp:TextBox ID="uotextboxAlternateContactPerson" runat="server" Width="300px"/>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="uclabelMainContactPersonPhone" runat="server" Text="Main Contact Person Phone :" />
            </td>
            <td>
                <asp:TextBox ID="uotextboxMainContactPersonPhone" runat="server" Width="300px"/>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="uclabelMainContactPersonEmail" runat="server" Text="Main Contact Person Email :" />
            </td>
            <td>
                <asp:TextBox ID="uotextboxMainContactPersonEmail" runat="server" Width="300px"/>
                <asp:RegularExpressionValidator ID="uoregexvalMainContactEmail" ControlToValidate="uotextboxMainContactPersonEmail"
                    runat="server" ErrorMessage="*" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                    ValidationGroup="Save" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="uclabelAlternateContactPersonPhone" runat="server" Text="Alternate Contact Person Phone :" />
            </td>
            <td>
                <asp:TextBox ID="uotextboxAlternateContactPersonPhone" runat="server" Width="300px"/>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="uclabelAlternateContactPersonEmail" runat="server" Text="Alternate Contact Person Email :" />
            </td>
            <td>
                <asp:TextBox ID="uotextboxAlternateContactPersonEmail" runat="server" Width="300px"/>
                <asp:RegularExpressionValidator ID="uoregexvalAlternateContactEmail" ControlToValidate="uotextboxAlternateContactPersonEmail"
                    runat="server" ErrorMessage="*" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                    ValidationGroup="Save" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="uclabelContractStartDate" runat="server" Text="Service Provider Contract Start Date :" />
            </td>
            <td>
                <asp:TextBox ID="uotextboxContractStartDate" runat="server" Width="300px"/>
                <asp:ImageButton runat="server" ID="imgCalendarStart" ImageUrl="~/Images/Calendar_scheduleHS.png" />
                <asp:CalendarExtender runat="server" ID="calStart" TargetControlID="uotextboxContractStartDate"
                    PopupButtonID="imgCalendarStart" PopupPosition="Right" CssClass="custom-calendar">
                </asp:CalendarExtender>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="uclabelContractEndDate" runat="server" Text="Service Provider Contract End Date :" />
            </td>
            <td>
                <asp:TextBox ID="uotextboxContractEndDate" runat="server" Width="300px"/>
                <asp:ImageButton runat="server" ID="imgCalendarEnd" ImageUrl="~/Images/Calendar_scheduleHS.png" />
                <asp:CalendarExtender runat="server" ID="CalendarExtender1" TargetControlID="uotextboxContractEndDate"
                    PopupButtonID="imgCalendarEnd" PopupPosition="Right" CssClass="custom-calendar">
                </asp:CalendarExtender>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="uclabelServiceRates" runat="server" Text="Service Provider Service Rates and Taxes :" />
            </td>
            <td>
                <asp:TextBox ID="uotextboxServiceRates" runat="server" Width="300px"/>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="uclabelRemarks" runat="server" Text="Remarks :" />
            </td>
            <td>
                <asp:TextBox ID="uotextboxRemarks" runat="server" Width="300px" 
                    TextMode="MultiLine"/>
            </td>
        </tr>
        <tr>
            <td colspan="2">
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td align="left">
                <asp:Button ID="uobuttonSave" runat="server" Text="Save" ValidationGroup="Save" OnClick="uobuttonSave_Click" />
                <asp:Button ID="uobuttonCancel" runat="server" Text="Cancel" />
            </td>
        </tr>
    </table>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/Popup.Master" AutoEventWireup="true" CodeBehind="AddSeafarer.aspx.cs" Inherits="TRAVELMART.Hotel.AddSeafarer" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style2
        {
            width: 338px;
        }
        .style3
        {
            width: 101px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="PageTitle">
        Add Seafarer
    </div>
    <hr />
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
    <table style="width: 444px">
        <tr>
            <td class="style3">&nbsp; 
                Seafarer Name:</td>
            <td>
                <asp:DropDownList ID="uoDropDownListSeafarer" Width="305px" runat="server" 
                    AutoPostBack="True" 
                    onselectedindexchanged="uoDropDownListSeafarer_SelectedIndexChanged">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                    ControlToValidate="uoDropDownListSeafarer" 
                    ErrorMessage="Seafarer name required." InitialValue="0">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="style3">&nbsp;
                Brand:</td>
            <td>
                <asp:TextBox ID="uoTextBoxBrand" runat="server" Width="305px" CssClass="ReadOnly"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style3">&nbsp;
                Vessel:</td>
            <td>
                <asp:TextBox ID="uoTextBoxVessel" runat="server" Width="305px" CssClass="ReadOnly"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style3">&nbsp;
                Rank:</td>
            <td>
                <asp:TextBox ID="uoTextBoxRank" runat="server" Width="305px" CssClass="ReadOnly"></asp:TextBox>
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td class="style3">&nbsp
                Hotel:
            </td>
            <td class="style2">
                <asp:DropDownList ID="uoDropDownListHotel" runat="server" Width="305px" AutoPostBack="True"
                    OnSelectedIndexChanged="uoDropDownListHotel_SelectedIndexChanged">
                </asp:DropDownList>
            </td>          
        </tr>
        <tr>
            <td class="style3">&nbsp
                Location:
            </td>
            <td class="style2">
                <asp:DropDownList ID="uoDropDownListLocation" runat="server" Width="305px">
                </asp:DropDownList>
            </td>            
        </tr>
        <tr>
            <td class="style3">&nbsp
                Room Type:
            </td>
            <td class="style2">
                <asp:DropDownList ID="uoDropDownListRoomType" runat="server" Width="305px">
                </asp:DropDownList>
            </td>            
        </tr>
        <tr>
            <td class="style3">&nbsp
                Checkin Date:
            </td>
            <td class="style2">
                <asp:TextBox ID="uoTextBoxCheckInDate" runat="server" Text="" Width="300px"></asp:TextBox>
                <cc1:textboxwatermarkextender ID="uoTextBoxPickUpDate_TextBoxWatermarkExtender" runat="server"
                    Enabled="True" TargetControlID="uoTextBoxCheckInDate" WatermarkCssClass="fieldWatermark"
                    WatermarkText="MM/dd/yyyy">
                </cc1:textboxwatermarkextender>
                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/Calendar_scheduleHS.png" />
                <cc1:calendarextender ID="uoTextBoxPickUpDate_CalendarExtender" runat="server" Enabled="True"
                    TargetControlID="uoTextBoxCheckInDate" PopupButtonID="ImageButton1" 
                    Format="MM/dd/yyyy">
                </cc1:calendarextender>
                <cc1:maskededitextender ID="uoTextBoxPickUpDate_MaskedEditExtender" runat="server"
                    CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                    CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                    CultureTimePlaceholder="" Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="uoTextBoxCheckInDate"
                    UserDateFormat="MonthDayYear">
                </cc1:maskededitextender>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="uoTextBoxCheckInDate"
                    ErrorMessage="Check In Date Required.">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="style3">&nbsp
                Time:
            </td>
            <td class="style2">
                <asp:TextBox ID="uoTextBoxCheckInTime" runat="server" Text="" Width="300px"></asp:TextBox>
                <cc1:textboxwatermarkextender ID="uoTextBoxCheckInTime_TextBoxWatermarkExtender"
                    runat="server" Enabled="True" TargetControlID="uoTextBoxCheckInTime" 
                    WatermarkCssClass="fieldWatermark" WatermarkText="HH:mm">
                </cc1:textboxwatermarkextender>
                <cc1:maskededitextender ID="uoMaskedEditExtenderCheckInTime" runat="server" CultureAMPMPlaceholder=""
                    CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                    CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                    Enabled="True" Mask="99:99" MaskType="Time" TargetControlID="uoTextBoxCheckInTime"
                    UserDateFormat="None" UserTimeFormat="TwentyFourHour">
                </cc1:maskededitextender>
            </td>          
        </tr>
        <tr>
            <td class="style3">&nbsp No. of Days:</td>
            <td class="style2">
                <asp:TextBox ID="uoTextBoxNoOfdays" runat="server" Width="300px" onkeypress="return validate(event);"></asp:TextBox>
                <asp:NumericUpDownExtender ID="uoTextBoxNoOfdays_NumericUpDownExtender" runat="server"
                    Enabled="True" Maximum="100" Minimum="1" RefValues="" ServiceDownMethod="" ServiceDownPath=""
                    ServiceUpMethod="" Tag="" TargetButtonDownID="" TargetButtonUpID="" TargetControlID="uoTextBoxNoOfdays"
                    Width="50">
                </asp:NumericUpDownExtender>
            </td>            
        </tr>
        <tr>            
            <td class="style3">&nbsp Meals:</td>
            <td class="style2">
                <asp:CheckBox ID="uoCheckBoxBreakfast" runat="server" Text="Breakfast"/>
            </td>          
        </tr>
        <tr>            
            <td class="style3"></td>
            <td class="style2">
                <asp:CheckBox ID="uoCheckBoxLunch" runat="server" Text="Lunch"/>
            </td>            
        </tr>
        <tr>            
            <td class="style3"></td>
            <td class="style2">
                <asp:CheckBox ID="uoCheckBoxDinner" runat="server" Text="Dinner"/>
            </td>            
        </tr>
        <tr>            
            <td class="style3">&nbsp; With Shuttle:&nbsp;</td>
            <td class="style2">
                <asp:CheckBox ID="uoCheckBoxShuttle" runat="server" />
            </td>            
        </tr>
        <tr>
            <td valign=top class="style3">&nbsp Remarks:</td>
            <td class="style2">
                <asp:TextBox ID="uoTextBoxRemarks" runat="server" TextMode="MultiLine" 
                    Width="305px" CssClass=TextBoxInput></asp:TextBox>
            </td>            
        </tr>
        <tr>
            <td class="style3">&nbsp
                Hotel Status:
            </td>
            <td class="style2">
                <asp:DropDownList ID="uoDropDownListHotelStatus" runat="server" Width="305px" 
                    AutoPostBack="True" 
                    onselectedindexchanged="uoDropDownListHotelStatus_SelectedIndexChanged">
                    <asp:ListItem Value="Unused">Unused</asp:ListItem>
                    <asp:ListItem Value="Checked In">Checked In</asp:ListItem>
                    <asp:ListItem Value="Checked Out">Checked Out</asp:ListItem>
                    <asp:ListItem Value="No Show">No Show</asp:ListItem>
                    <asp:ListItem Value="Cancelled">Cancelled</asp:ListItem>
                </asp:DropDownList>
            </td>          
        </tr>
        <tr>
           <%-- <td>
                <asp:Label ID="ucLabelCrewBill" runat="server" Text="Charge to Crew:" 
                    Visible="False"></asp:Label>
            </td>--%>
            <td class="style3"></td>
            <td align=center class="style2">
                &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp                
                <asp:CheckBox ID="uoCheckBoxCrewBill" runat="server" Text="Charge to crew" 
                    Visible="False" ToolTip="Set charges for hotel status cancelled."/>
            </td>            
        </tr>
        <tr><td colspan="2"></td></tr>
        <tr>
            <td class="style3">
            </td>
            <td class="style2">
                <asp:Button ID="uoButtonSave" runat="server" Text="Save" OnClick="uoButtonSave_Click"
                    Width="70px" />
            </td>           
        </tr>
        <tr>
            <td class="style3">
                &nbsp;
            </td>
            <td class="style2">
                <asp:HiddenField ID="uoHiddenFieldSfID" runat="server" />
            </td>          
        </tr>      
    </table>
    
</asp:Content>


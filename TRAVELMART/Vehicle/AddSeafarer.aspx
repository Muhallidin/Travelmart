<%@ Page Title="" Language="C#" MasterPageFile="~/Popup.Master" AutoEventWireup="true" CodeBehind="AddSeafarer.aspx.cs" Inherits="TRAVELMART.Vehicle.AddSeafarer" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">    
    <div class="PageTitle">
         Add Seafarer            
    </div>
    <hr/>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server"/>
<table>
    <tr>
        <td>&nbsp Seafarer Name:</td>
        <td>
            <asp:DropDownList ID="uoDropDownListSeafarerName" runat="server" Width="305px" 
                AutoPostBack="true"
                onselectedindexchanged="uoDropDownListSeafarerName_SelectedIndexChanged">
            </asp:DropDownList>
                &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="uoDropDownListSeafarerName" ErrorMessage="Seafarer name required." InitialValue="0">*</asp:RequiredFieldValidator>
        </td>        
    </tr>    
    <tr>
        <td>&nbsp Brand:</td>
        <td>
            <asp:TextBox ID="uoTextBoxBrand" runat="server" Height="20px" Width="300px" CssClass=ReadOnly></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>&nbsp Vessel:</td>
        <td>
            <asp:TextBox ID="uoTextBoxVessel" runat="server" Height="20px" Width="300px" CssClass=ReadOnly></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>&nbsp Rank:</td>
        <td>
            <asp:TextBox ID="uoTextBoxRank" runat="server" Height="20px" Width="300px" CssClass=ReadOnly></asp:TextBox>
        </td>
    </tr>    
    <tr>
        <td>&nbsp Vehicle Company:</td>
        <td>
            <asp:DropDownList ID="uoDropDownListVehicleCompany" runat="server" Width="305px" AppendDataBoundItems="true">
                <%--<asp:ListItem Value="">- Select a Vehicle Company -</asp:ListItem>--%>
            </asp:DropDownList>
                &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="uoDropDownListVehicleCompany" ErrorMessage="Vehicle company required.">*</asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td>&nbsp Vehicle Type:</td>
        <td>            
            <asp:DropDownList ID="uoDropDownListVehicleType" runat="server" Width="305px" AppendDataBoundItems="true">
            </asp:DropDownList>                
                &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="uoDropDownListVehicleType" ErrorMessage="Vehicle type required.">*</asp:RequiredFieldValidator>            
        </td>
    </tr>
    <tr>
        <td>&nbsp Vehicle Plate No.:</td>
        <td>
            <asp:TextBox ID="uoTextBoxPlateNo" runat="server" Width="300px"></asp:TextBox>                
        </td>
    </tr>
    <tr>
        <td>&nbsp; Pickup Date:</td>
        <td>
            <asp:TextBox ID="uoTextBoxPickUpDate" runat="server" Width="300px"></asp:TextBox>
                <cc1:TextBoxWatermarkExtender ID="uoTextBoxPickUpDate_TextBoxWatermarkExtender" 
                    runat="server" Enabled="True" TargetControlID="uoTextBoxPickUpDate" WatermarkCssClass="fieldWatermark" WatermarkText="MM/dd/yyyy">
                </cc1:TextBoxWatermarkExtender>
                    <asp:ImageButton ID="ImageButton1" runat="server" 
                        ImageUrl="~/Images/Calendar_scheduleHS.png" />
                            <cc1:CalendarExtender ID="uoTextBoxPickUpDate_CalendarExtender" runat="server" 
                                Enabled="True" TargetControlID="uoTextBoxPickUpDate" PopupButtonID="ImageButton1" Format="MM/dd/yyyy">
                            </cc1:CalendarExtender>
                                <cc1:MaskedEditExtender ID="uoTextBoxPickUpDate_MaskedEditExtender" 
                                    runat="server" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                                    CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                    CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                                    Mask="99/99/9999" MaskType="Date" TargetControlID="uoTextBoxPickUpDate" UserDateFormat="MonthDayYear">
                                </cc1:MaskedEditExtender>                                
                                    &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="uoTextBoxPickUpDate" ErrorMessage="Pick up date required.">*</asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td>&nbsp; Pickup Time:</td>
        <td>
            <asp:TextBox ID="uoTextBoxPickupTime" runat="server" Width="300px"></asp:TextBox>
            <cc1:TextBoxWatermarkExtender ID="uoTextBoxPickupTime_TextBoxWatermarkExtender" 
                runat="server" Enabled="True" TargetControlID="uoTextBoxPickupTime" 
                WatermarkText="HH:mm" WatermarkCssClass="fieldWatermark">
            </cc1:TextBoxWatermarkExtender>
            <cc1:MaskedEditExtender ID="uoTextBoxPickupTime_MaskedEditExtender" 
                runat="server" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                Mask="99:99" MaskType="Time" TargetControlID="uoTextBoxPickupTime" UserDateFormat="None" 
                UserTimeFormat="TwentyFourHour">
            </cc1:MaskedEditExtender>
        </td>
    </tr>
    <tr>
        <td>&nbsp; Dropoff Date:</td>
        <td>
            <asp:TextBox ID="uoTextBoxDropOffDate" runat="server" Width="300px"></asp:TextBox>
                <cc1:TextBoxWatermarkExtender ID="uoTextBoxDropOffDate_TextBoxWatermarkExtender" 
                    runat="server" Enabled="True" TargetControlID="uoTextBoxDropOffDate" WatermarkCssClass="fieldWatermark" WatermarkText="MM/dd/yyyy">
                </cc1:TextBoxWatermarkExtender>
                    <asp:ImageButton ID="ImageButton2" runat="server" 
                        ImageUrl="~/Images/Calendar_scheduleHS.png" />
                            <cc1:CalendarExtender ID="uoTextBoxDropOffDate_CalendarExtender" runat="server" 
                                Enabled="True" TargetControlID="uoTextBoxDropOffDate" PopupButtonID="ImageButton2" Format="MM/dd/yyyy">
                            </cc1:CalendarExtender>
                                <cc1:MaskedEditExtender ID="uoTextBoxDropOffDate_MaskedEditExtender" 
                                    runat="server" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                                    CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                    CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" MaskType="Date"
                                    TargetControlID="uoTextBoxDropOffDate" Mask="99/99/9999" UserDateFormat="MonthDayYear">
                                </cc1:MaskedEditExtender>
                                    &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="uoTextBoxDropOffDate" ErrorMessage="Drop off date required.">*</asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td>&nbsp; Dropoff Time:</td>
        <td>
            <asp:TextBox ID="uoTextBoxDropoffTime" runat="server" Width="300px"></asp:TextBox>
            <cc1:TextBoxWatermarkExtender ID="uoTextBoxDropoffTime_TextBoxWatermarkExtender" 
                runat="server" Enabled="True" TargetControlID="uoTextBoxDropoffTime" 
                WatermarkText="HH:mm" WatermarkCssClass="fieldWatermark">
            </cc1:TextBoxWatermarkExtender>
            <cc1:MaskedEditExtender ID="uoTextBoxDropoffTime_MaskedEditExtender" 
                runat="server" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                Mask="99:99" MaskType="Time" TargetControlID="uoTextBoxDropoffTime"  UserDateFormat="None" 
                UserTimeFormat="TwentyFourHour">
            </cc1:MaskedEditExtender>
        </td>
    </tr>
     <tr>
        <td>&nbsp Pick Up Location:</td>
        <td>
            <asp:TextBox ID="uoTextBoxPickUpPlace" runat="server" Width="300px"></asp:TextBox>
                &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="uoTextBoxPickUpPlace" ErrorMessage="Pick up location required.">*</asp:RequiredFieldValidator>
        </td>
    </tr>
      <tr>
        <td>&nbsp Drop off Location:</td>
        <td>
            <asp:TextBox ID="uoTextBoxDropOffPlace" runat="server" Width="300px"></asp:TextBox>
                &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="uoTextBoxDropOffPlace" ErrorMessage="Drop off location required.">*</asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td valign=top>&nbsp Remarks:</td>
        <td>
            <asp:TextBox ID="uoTextBoxRemarks" runat="server" Width="300px" 
                TextMode="MultiLine" Height="50px" CssClass=TextBoxInput></asp:TextBox>                
        </td>
    </tr>
    <tr>
        <td>&nbsp Vehicle Status:</td>
        <td>
            <asp:DropDownList ID="uoDropDownListVehicleStatus" runat="server" width="305px" AutoPostBack=true 
                onselectedindexchanged="uoDropDownListVehicleStatus_SelectedIndexChanged">
                <asp:ListItem Value="Unused">Unused</asp:ListItem>
                <asp:ListItem Value="In Transit">In Transit</asp:ListItem>
                <asp:ListItem Value="Arrived">Arrived</asp:ListItem>
                <asp:ListItem Value="No Show">No Show</asp:ListItem>
                <asp:ListItem Value="Cancelled">Cancelled</asp:ListItem>
            </asp:DropDownList>                
        </td>
    </tr>
    <tr>
        <td></td>
        <td align=center>
            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
            <asp:CheckBox ID="uoCheckBoxBilledToCrew" runat="server" Text="Charge to crew" Visible=false ToolTip="Set charges for vehicle status cancelled."/>
        </td>
    </tr>     
    <tr>
        <td colspan="2"></td>
    </tr> 
    <tr>
        <td></td>
        <td>            
            <asp:Button ID="uoButtonSave" runat="server" Text="Save" Width="80"
                onclick="uoButtonSave_Click" />                
        </td>
    </tr> 
    <tr>
        <td></td>
        <td>
            <asp:HiddenField ID="uoHiddenFieldSeafarerID" runat="server" />
        </td>       
    </tr>      
</table>
</asp:Content>

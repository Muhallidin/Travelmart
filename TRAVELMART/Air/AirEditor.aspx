<%@ Page Title="" Language="C#" MasterPageFile="~/Popup.Master" AutoEventWireup="true" CodeBehind="AirEditor.aspx.cs" Inherits="TRAVELMART.AirEditor" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="PageTitle">
         Air Travel Status            
    </div>
    <hr />    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">        
    <ContentTemplate>
        <table>
            <tr>
                <td>
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" />       
                </td>
            </tr>
            <tr>
                <td>
                    <div runat="server" id="uoDivSabre">
                        <table>
                        <tr>
                            <td >&nbsp Seafarer Name:</td>
                            <td >
                                <asp:TextBox ID="uoTextBoxSeafarer" runat="server" Width="300px" ReadOnly="true" CssClass="ReadOnly"></asp:TextBox>                          
                            </td>        
                        </tr>    
                        <tr>
                            <td>&nbsp Departure Date/Time:</td>
                            <td >
                                <asp:TextBox ID="uoTextBoxDepartureDatetime" runat="server" Width="300px" ReadOnly="true" CssClass="ReadOnly"></asp:TextBox>                          
                            </td>        
                        </tr>
                        <tr>
                            <td>&nbsp Arrival Date/Time:</td>
                            <td >
                                <asp:TextBox ID="uoTextBoxArrivalDatetime" runat="server" Width="300px" ReadOnly="true" CssClass="ReadOnly"></asp:TextBox>                          
                            </td>        
                        </tr>
                        <tr>
                            <td>&nbsp Airline:</td>
                            <td >
                                <asp:TextBox ID="uoTextBoxAirline" runat="server" Width="300px" ReadOnly="true" CssClass="ReadOnly"></asp:TextBox>                          
                            </td>        
                        </tr>
                        <tr>
                            <td>&nbsp Flight No.:</td>
                            <td >
                                <asp:TextBox ID="uoTextBoxFlightNo" runat="server" Width="300px" ReadOnly="true" CssClass="ReadOnly"></asp:TextBox>                          
                            </td>        
                        </tr>
                        <tr>
                            <td>&nbsp Ticket No.:</td>
                            <td >
                                <asp:TextBox ID="uoTextBoxTicketNo" runat="server" Width="300px" ReadOnly="true" CssClass="ReadOnly"></asp:TextBox>                          
                            </td>        
                        </tr>
                        <tr>
                                <td>&nbsp Departure Location:</td>
                                <td >
                                    <asp:TextBox ID="uoTextBoxDepartureLoc" runat="server" Width="300px" ReadOnly="true" CssClass="ReadOnly"></asp:TextBox>                                    
                                </td>        
                            </tr>
                            <tr>
                                <td>&nbsp Arrival Location:</td>
                                <td >
                                    <asp:TextBox ID="uoTextBoxArrivalLoc" runat="server" Width="300px" ReadOnly="true" CssClass="ReadOnly"></asp:TextBox>                                    
                                </td>        
                            </tr>
                        <tr>
                            <td valign="top">&nbsp Remarks:</td>
                            <td >
                                <asp:TextBox ID="uoTextBoxRemarks" runat="server" Width="300px" Height="50px" 
                                    TextMode="MultiLine" CssClass="TextBoxInput"></asp:TextBox>                          
                            </td>        
                        </tr>
                        <tr>
                            <td>&nbsp Air Status:</td>
                            <td >
                                <asp:DropDownList ID="uoDropDownListAirStatus" runat="server" width="305px" 
                                    AutoPostBack="true" 
                                    onselectedindexchanged="uoDropDownListAirStatus_SelectedIndexChanged">
                                <asp:ListItem Value="">--Select Status--</asp:ListItem>                                     
                                    <asp:ListItem>Open</asp:ListItem>
                                    <asp:ListItem>Checked In</asp:ListItem>                    
                                    <asp:ListItem>Used</asp:ListItem>
                                    <asp:ListItem>Exchanged</asp:ListItem>    
                            </asp:DropDownList>
                             &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="uoDropDownListAirStatus" ErrorMessage="Air status required.">*</asp:RequiredFieldValidator>
                            </td>        
                        </tr>
                        <tr>
                            <td></td>
                            <td align="center">
                                &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                                &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                                &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                                &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                                <asp:CheckBox ID="uoCheckBoxChargedToCrew" runat="server" Text="Charge to crew" ToolTip="Set charges for air status cancelled."/>
                            </td>
                        </tr>
                        <tr><td colspan="2"></td></tr>
                        <tr>
                            <td></td>
                            <td>
                                <asp:Button ID="uoButtonSave" runat="server" Text="Save" Width="80" 
                                    onclick="uoButtonSave_Click"/>
                            </td>
                        </tr>
                    </table>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div runat="server" id="uoDivOther">
                        <table>
                            <tr>
                                <td >&nbsp Seafarer Name:</td>
                                <td >
                                    <asp:TextBox ID="uoTextBoxSeafarer2" runat="server" Width="300px" ReadOnly="true" CssClass="ReadOnly"></asp:TextBox>                          
                                </td>        
                            </tr>    
                            <tr>
                                <td>&nbsp Departure Date/Time:</td>
                                <td >
                                    <asp:TextBox ID="uoTextBoxDepartureDatetime2" runat="server" Width="300px" CssClass="TextBoxInput"></asp:TextBox>                          
                                    <%--<cc1:TextBoxWatermarkExtender ID="uoTextBoxDepartureDatetime2_TextBoxWatermarkExtender" runat="server"
                                        Enabled="True" TargetControlID="uoTextBoxDepartureDatetime2" WatermarkCssClass="fieldWatermark"
                                        WatermarkText="MM/dd/yyyy">
                                    </cc1:TextBoxWatermarkExtender>
                                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/Calendar_scheduleHS.png" />
                                    <cc1:CalendarExtender ID="uoTextBoxDepartureDatetime2_CalendarExtender" runat="server" Enabled="True"
                                        TargetControlID="uoTextBoxDepartureDatetime2" PopupButtonID="ImageButton1" Format="MM/dd/yyyy">
                                    </cc1:CalendarExtender>
                                    <cc1:MaskedEditExtender ID="uoTextBoxDepartureDatetime2_MaskedEditExtender" runat="server"
                                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                        CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                        CultureTimePlaceholder="" Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="uoTextBoxDepartureDatetime2"
                                        UserDateFormat="MonthDayYear">
                                    </cc1:MaskedEditExtender>--%>
                                    &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="uoTextBoxDepartureDatetime2" ErrorMessage="Departure datetime required.">*</asp:RequiredFieldValidator>
                                </td>        
                            </tr>
                            <tr>
                                <td>&nbsp Arrival Date/Time:</td>
                                <td >
                                    <asp:TextBox ID="uoTextBoxArrivalDatetime2" runat="server" Width="300px" CssClass="TextBoxInput"></asp:TextBox>                          
                                    &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="uoTextBoxArrivalDatetime2" ErrorMessage="Arrival datetime required.">*</asp:RequiredFieldValidator>
                                </td>        
                            </tr>
                            <tr>
                                <td>&nbsp Airline:</td>
                                <td >
                                    <asp:DropDownList ID="uoDropDownListAirline" runat="server" width="305px" 
                                        AppendDataBoundItems="True" >
                                    </asp:DropDownList>
                                    &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="uoDropDownListAirline" ErrorMessage="Airline required.">*</asp:RequiredFieldValidator>
                                </td>        
                            </tr>
                            <tr>
                                <td>&nbsp Flight No.:</td>
                                <td >
                                    <asp:TextBox ID="uoTextBoxFlightNo2" runat="server" Width="300px" CssClass="TextBoxInput"></asp:TextBox>                          
                                    &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="uoTextBoxFlightNo2" ErrorMessage="Flight no. required.">*</asp:RequiredFieldValidator>
                                </td>        
                            </tr>
                            <tr>
                                <td>&nbsp Ticket No.:</td>
                                <td >
                                    <asp:TextBox ID="uoTextBoxTicketNo2" runat="server" Width="300px" CssClass="TextBoxInput"></asp:TextBox>
                                    &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="uoTextBoxTicketNo2" ErrorMessage="Ticket no. required.">*</asp:RequiredFieldValidator>
                                </td>        
                            </tr>
                            <tr>
                                <td>&nbsp Departure Location:</td>
                                <td >
                                    <asp:TextBox ID="uoTextBoxDepartureLoc2" runat="server" Width="300px" CssClass="TextBoxInput"></asp:TextBox>
                                    &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="uoTextBoxDepartureLoc2" ErrorMessage="Departure location code required.">*</asp:RequiredFieldValidator>
                                </td>        
                            </tr>
                            <tr>
                                <td>&nbsp Arrival Location:</td>
                                <td >
                                    <asp:TextBox ID="uoTextBoxArrivalLoc2" runat="server" Width="300px" CssClass="TextBoxInput"></asp:TextBox>
                                    &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="uoTextBoxArrivalLoc2" ErrorMessage="Arrival location code required.">*</asp:RequiredFieldValidator>
                                </td>        
                            </tr>
                            <tr>
                                <td valign="top">&nbsp Remarks:</td>
                                <td >
                                    <asp:TextBox ID="uoTextBoxRemarks2" runat="server" Width="300px" Height="50px" 
                                        TextMode="MultiLine" CssClass="TextBoxInput"></asp:TextBox>                          
                                </td>        
                            </tr>
                            <tr>
                                <td>&nbsp Air Status:</td>
                                <td >
                                    <asp:DropDownList ID="uoDropDownListAirStatus2" runat="server" width="305px" 
                                        AutoPostBack="true" 
                                        onselectedindexchanged="uoDropDownListAirStatus2_SelectedIndexChanged">
                                    <asp:ListItem Value="">--Select Status--</asp:ListItem> 
                                        <asp:ListItem>Unused</asp:ListItem>
                                    <asp:ListItem Value="Arrived">Arrived</asp:ListItem>                    
                                    <asp:ListItem Value="Delayed">Delayed</asp:ListItem>
                                    <asp:ListItem Value="Flown">Flown</asp:ListItem>    
                                    <asp:ListItem Value="Unflown">Unflown</asp:ListItem>
                                    <asp:ListItem Value="Rebooked">Rebooked</asp:ListItem>
                                    <asp:ListItem Value="Cancelled">Cancelled</asp:ListItem>
                                </asp:DropDownList>
                                 &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="uoDropDownListAirStatus2" ErrorMessage="Air status required.">*</asp:RequiredFieldValidator>
                                </td>        
                            </tr>
                            <tr>
                                <td></td>
                                <td align="center">
                                    &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                                    &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                                    &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                                    &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                                    <asp:CheckBox ID="uoCheckBoxChargedToCrew2" runat="server" Text="Charge to crew" Visible=false ToolTip="Set charges for air status cancelled."/>
                                </td>
                            </tr>
                            <tr><td colspan="2"></td></tr>
                            <tr>
                                <td></td>
                                <td>
                                    <asp:Button ID="uoButtonSave2" runat="server" Text="Save" Width="80" 
                                        onclick="uoButtonSave2_Click"/>
                                </td>
                            </tr>
                            <asp:HiddenField ID="uoHiddenFieldAirID" runat="server" Value="0"/>
                        </table>
                    </div>
                </td>
            </tr>            
        </table>                
    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

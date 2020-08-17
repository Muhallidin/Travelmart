<%@ Page Title="" Language="C#" MasterPageFile="~/Popup.Master" AutoEventWireup="true" CodeBehind="EventsAdd.aspx.cs" Inherits="TRAVELMART.Maintenance.EventsAdd" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="PageTitle">
        Events Editor
    </div>
    <hr />
    
    <table width="100%">
        <tr>
            <td colspan="2" class="LeftClass">
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Save" />
            </td>
            <td style="width:3%;"></td>
           
        </tr>
        <tr>
            <td class="LeftClass" style="width:15%;">
                &nbsp Event Name:
            </td>
            <td colspan="4" class="LeftClass">
                <asp:TextBox runat="server" ID="uoTextBoxEventName"
                    Width="98%" MaxLength="50"></asp:TextBox>
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                        ControlToValidate="uoTextBoxEventName" ErrorMessage="Select Date From."  InitialValue=""
                        ValidationGroup="Save">*
                </asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="LeftClass">&nbsp Remarks:</td>
            <td class="LeftClass" colspan="4">
                <asp:TextBox runat="server" Width="98%" 
                    TextMode="MultiLine" ID="uoTextBoxRemarks" Font-Names="Arial"></asp:TextBox>
            </td>
        </tr>
        <tr><td colspan="5"></td></tr>
        <tr>
            <td class="LeftClass" style="width:15%;">&nbsp Date From:</td>
            <td class="LeftClass" style="width:30%;">
               
                <asp:TextBox ID="uoTextBoxFrom" runat="server" 
                    Width="100%"></asp:TextBox>
                <cc1:TextBoxWatermarkExtender ID="uoTextBoxFrom_TextBoxWatermarkExtender" runat="server"
                    Enabled="True" TargetControlID="uoTextBoxFrom" WatermarkCssClass="fieldWatermarkSmall"
                    WatermarkText="MM/dd/yyyy">
                </cc1:TextBoxWatermarkExtender>
                <cc1:CalendarExtender ID="uoTextBoxFrom_CalendarExtender" runat="server" Enabled="True"
                    TargetControlID="uoTextBoxFrom" Format="MM/dd/yyyy">
                </cc1:CalendarExtender>
                <cc1:MaskedEditExtender ID="uoTextBoxFrom_MaskedEditExtender" runat="server" CultureAMPMPlaceholder=""
                    CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                    CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                    Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="uoTextBoxFrom"
                    UserDateFormat="MonthDayYear">
                </cc1:MaskedEditExtender>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server"
                    ControlToValidate="uoTextBoxFrom" ErrorMessage="Select Date From."
                    ValidationGroup="Save">*
                </asp:RequiredFieldValidator><%--InitialValue=""--%>
                                 
            </td>
            <td style="width:3%;"></td>
            <td class="LeftClass" style="width:15%">&nbsp Region:</td>
            <td class="LeftClass" style="width:35%">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                         <asp:DropDownList runat="server" 
                            id="uoDropDownListRegion" Width="97%"
                            AutoPostBack="true"
                            AppendDataBoundItems="true" 
                             onselectedindexchanged="uoDropDownListRegion_SelectedIndexChanged">
                             <asp:ListItem Value="0">--Select Region--</asp:ListItem>
                         </asp:DropDownList>
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                            ControlToValidate="uoDropDownListRegion" ErrorMessage="Select Region."  InitialValue="0"
                            ValidationGroup="Save">*
                    </asp:RequiredFieldValidator>
                    </ContentTemplate>
                </asp:UpdatePanel>               
            </td>            
        </tr>
        <tr>
            <td class="LeftClass"  style="width:15%">&nbsp Date To:</td>
            <td class="LeftClass"  style="width:30%">
               
                <asp:TextBox ID="uoTextBoxTo" runat="server"  Width="100%"></asp:TextBox>
                <cc1:TextBoxWatermarkExtender ID="uoTextBoxTo_TextBoxWatermarkExtender" runat="server"
                    Enabled="True" TargetControlID="uoTextBoxTo" WatermarkCssClass="fieldWatermarkSmall"
                    WatermarkText="MM/dd/yyyy">
                </cc1:TextBoxWatermarkExtender>
                <cc1:CalendarExtender ID="uoTextBoxTo_CalendarExtender" runat="server" Enabled="True"
                    TargetControlID="uoTextBoxTo" Format="MM/dd/yyyy">
                </cc1:CalendarExtender>
                <cc1:MaskedEditExtender ID="uoTextBoxTo_MaskedEditExtender" runat="server" CultureAMPMPlaceholder=""
                    CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                    CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                    Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="uoTextBoxTo"
                    UserDateFormat="MonthDayYear">
                </cc1:MaskedEditExtender>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server"
                    ControlToValidate="uoTextBoxTo" ErrorMessage="Select Date To."
                    ValidationGroup="Save">*
                </asp:RequiredFieldValidator><%--InitialValue=""--%>
                                   
            </td>
            <td style="width:3%;"></td>
            <td class="LeftClass"  style="width:15%">&nbsp Country:</td>
            <td class="LeftClass"  style="width:35%">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                         <asp:DropDownList runat="server" ID="uoDropDownListCountry"
                            AppendDataBoundItems="true" AutoPostBack="true"
                            Width="97%" 
                             onselectedindexchanged="uoDropDownListCountry_SelectedIndexChanged">
                             <asp:ListItem Value="0">--Select Country--</asp:ListItem>
                         </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                            ControlToValidate="uoDropDownListCountry" ErrorMessage="Select Country."  InitialValue="0"
                            ValidationGroup="Save">*
                        </asp:RequiredFieldValidator>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="uoDropDownListRegion" 
                            EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
               
            </td>
        </tr>
        <tr>
          
            <td colspan="2"></td>
            <td style="width:3%;"></td>
            <td class="LeftClass"  style="width:15%">&nbsp City Name:</td>
            <td class="LeftClass"  style="width:35%">
                <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:TextBox runat="server" ID="uoTextBoxCity" Width="61%"
                            ></asp:TextBox> 
                        <asp:Button runat="server" ID="uoButtonSearch" Width="32%"
                            Text="Search City" onclick="uoButtonSearch_Click" />
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="uoButtonSearch" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="uoDropDownListLetters" 
                            EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
                
            </td>
        </tr>
        <tr>
            <td></td>
            <td></td>
            <td style="width:3%;"></td>
            <td class="LeftClass">&nbsp City:</td>
            <td class="LeftClass">
                <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                         <asp:DropDownList runat="server" ID="uoDropDownListLetters"
                            Width="20%"
                            onselectedindexchanged="uoDropDownListLetters_SelectedIndexChanged" 
                            AppendDataBoundItems="True" AutoPostBack="True">
                            <asp:ListItem Value="0">--</asp:ListItem>
                        </asp:DropDownList> &nbsp;
                        <asp:DropDownList runat="server" ID="uoDropDownListCity"
                            Width="74%" AutoPostBack="true" AppendDataBoundItems="true"
                            onselectedindexchanged="uoDropDownListCity_SelectedIndexChanged">
                            <asp:ListItem Value="0">--select City--</asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"
                            ControlToValidate="uoDropDownListCity" ErrorMessage="Select City." InitialValue="0"
                            ValidationGroup="Save">*
                        </asp:RequiredFieldValidator>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="uoDropDownListCountry" 
                            EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="uoButtonSearch" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="uoDropDownListLetters" 
                            EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td></td>
            <td></td>
            <td style="width:3%;"></td>
            <td class="LeftClass">
                &nbsp Hotel:
            </td>
            <td class="LeftClass">
                <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:DropDownList runat="server" ID="uoDropDownListHotel"
                            AutoPostBack="true" AppendDataBoundItems="true"
                            Width="97%" 
                            onselectedindexchanged="uoDropDownListHotel_SelectedIndexChanged">
                            <asp:ListItem Value="0">--Select Hotel--</asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server"
                            ControlToValidate="uoDropDownListHotel" ErrorMessage="Select Hotel." InitialValue="0"
                            ValidationGroup="Save">*
                        </asp:RequiredFieldValidator>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="uoDropDownListCity" 
                            EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td colspan="5"></td>
        </tr>        
        <tr>
            <td colspan="5" align="center">
                <asp:Button runat="server" ID="uoButtonSave" ValidationGroup="Save"
                    Text="Save" onclick="uoButtonSave_Click" />
            </td>
        </tr>
    </table>
</asp:Content>

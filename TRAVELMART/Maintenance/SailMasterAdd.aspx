<%@ Page Title="" Language="C#" MasterPageFile="~/Popup.Master" AutoEventWireup="true" CodeBehind="SailMasterAdd.aspx.cs" Inherits="TRAVELMART.Maintenance.SailMasterAdd" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<table width="100%"  class="TableView">
    <tr class="PageTitle">
        <td class="LeftClass" colspan="2">Sail Master</td>
    </tr>  
    <tr>
        <td colspan="2" class="LeftClass">
            <asp:ValidationSummary runat="server" ID="ValidationSummary1" 
                ValidationGroup="Save" />
        </td>
    </tr>
    <tr>
        <td class="caption" style="width:20%;">Ship Name:</td>
        <td class="value"><%= Request.QueryString["vName"] %></td>
    </tr>
    <tr>
        <td class="caption" style="width:20%;">Region:</td>
        <td class="value" style="width:80%;">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:DropDownList runat="server" ID="uoDropDownListRegion" Width="91%"
                        
                        onselectedindexchanged="uoDropDownListRegion_SelectedIndexChanged" 
                        AppendDataBoundItems="True" AutoPostBack="True">
                        <asp:ListItem Value="0">--Select Region--</asp:ListItem>
                    </asp:DropDownList>
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                            ControlToValidate="uoDropDownListRegion" ErrorMessage="Select Region." InitialValue="0"
                            ValidationGroup="Save">*
                     </asp:RequiredFieldValidator>
                </ContentTemplate>
            </asp:UpdatePanel>            
        </td>
    </tr>
    <tr>
        <td class="caption" style="width:20%;">Country:</td>
        <td class="value" style="width:80%;">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:DropDownList runat="server" ID="uoDropDownListCountry"
                        Width="91%" 
                        onselectedindexchanged="uoDropDownListCountry_SelectedIndexChanged" 
                        AppendDataBoundItems="True" AutoPostBack="True">
                        <asp:ListItem Value="0">--Select Country--</asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                        ControlToValidate="uoDropDownListCountry" ErrorMessage="Select Country." InitialValue="0"
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
        <td class="caption" style="width:20%;">City Name:</td>
        <td class="value" style="width:80%;">
            <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                     <asp:TextBox runat="server" ID="uoTextBoxCity" Width="49%"
                        ></asp:TextBox>
                    <asp:Button runat="server" ID="uoButtonSearch" Width="40%"
                        Text="Search City" Font-Size ="X-Small" onclick="uoButtonSearch_Click" />
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="uoDropDownListLetters" 
                        EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="uoButtonSearch" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
           
        </td>
    </tr>
    <tr>
        <td class="caption" style="width:20%;">City:</td>
        <td class="value" style="width:80%;">
            <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                     <asp:DropDownList runat="server" ID="uoDropDownListLetters"
                        Width="30%" 
                        onselectedindexchanged="uoDropDownListLetters_SelectedIndexChanged" 
                        AppendDataBoundItems="True" AutoPostBack="True">
                        <asp:ListItem Value="0">--</asp:ListItem>
                    </asp:DropDownList>
                    <asp:DropDownList runat="server" ID="uoDropDownListCity"
                        Width="60%"  AutoPostBack="true" AppendDataBoundItems="true"
                        onselectedindexchanged="uoDropDownListCity_SelectedIndexChanged">
                        <asp:ListItem Value="0">--select City--</asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                        ControlToValidate="uoDropDownListCity" ErrorMessage="Select City." InitialValue="0"
                        ValidationGroup="Save">*
                    </asp:RequiredFieldValidator>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="uoDropDownListLetters" 
                        EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="uoDropDownListCountry" 
                        EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="uoButtonSearch" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
           
        </td>
    </tr>
    <tr>
        <td class="caption" style="width:20%;">Seaport:</td>
        <td class="value" style="width:80%;">
            <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                     <asp:DropDownList runat="server" ID="uoDropDownListSeaport"
                        Width="90.5%"  AppendDataBoundItems="true" 
                        onselectedindexchanged="uoDropDownListSeaport_SelectedIndexChanged" 
                         AutoPostBack="True">
                        <asp:ListItem Value="0">--Select Seaport--</asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"
                        ControlToValidate="uoDropDownListSeaport" ErrorMessage="Select Seaport." InitialValue="0"
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
        <td class="caption" style="width:20%;">Itinerary Code:</td>
        <td class="value" style="width:80%;">
            <asp:TextBox runat="server" ID="uoTextBoxItinerary" Width="89%"
                 MaxLength="10"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="caption" style="width:20%;">Voyage No.:</td>
        <td class="value" style="width:80%;">
            <asp:TextBox runat="server" ID="uoTextBoxVoyage"
                Width="89%"  MaxLength="10"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="caption" style="width:20%;">Schedule Date:</td>
        <td class="value" style="width:80%;">
            <asp:TextBox ID="uoTextBoxScheduleDate" runat="server"  
                Width="89%"></asp:TextBox>
            <cc1:TextBoxWatermarkExtender ID="uoTextBoxScheduleDate_TextBoxWatermarkExtender" runat="server"
                Enabled="True" TargetControlID="uoTextBoxScheduleDate" WatermarkCssClass="fieldWatermarkSmall"
                WatermarkText="MM/dd/yyyy">
            </cc1:TextBoxWatermarkExtender>
            <cc1:CalendarExtender ID="uoTextBoxScheduleDate_CalendarExtender" runat="server" Enabled="True"
                TargetControlID="uoTextBoxScheduleDate" Format="MM/dd/yyyy">
            </cc1:CalendarExtender>
            <cc1:MaskedEditExtender ID="uoTextBoxScheduleDate_MaskedEditExtender" runat="server" CultureAMPMPlaceholder=""
                CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="uoTextBoxScheduleDate"
                UserDateFormat="MonthDayYear">
            </cc1:MaskedEditExtender>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server"
                ControlToValidate="uoTextBoxScheduleDate" ErrorMessage="Select Schedule Date."  InitialValue=""
                ValidationGroup="Save">*
            </asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td class="caption" style="width:20%;">Day Seq.:</td>
        <td class="value" style="width:80%;">
            <asp:TextBox runat="server" ID="uoTextBoxDaySeq"
                Width="89%" MaxLength="10"></asp:TextBox>
            <asp:NumericUpDownExtender ID="uoTextBoxDaySeq_NumericUpDownExtender" runat="server"
            Enabled="True" Maximum="100" Minimum="1" RefValues="" ServiceDownMethod="" ServiceDownPath=""
            ServiceUpMethod="" Tag="" TargetButtonDownID="" TargetButtonUpID="" TargetControlID="uoTextBoxDaySeq" Width="50">
            </asp:NumericUpDownExtender>
        </td>
    </tr>
    <tr>
        <td class="caption" style="width:20%;">Date From:</td>
        <td class="value" style="width:80%;">
            <asp:TextBox ID="uoTextBoxFrom" runat="server"  
                Width="89%"></asp:TextBox>
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
                ControlToValidate="uoTextBoxFrom" ErrorMessage="Select Date From."  InitialValue=""
                ValidationGroup="Save">*
            </asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td class="caption" style="width:20%;">Date To:</td>
        <td class="value" style="width:80%;">
            <asp:TextBox ID="uoTextBoxTo" runat="server"  Width="89%"></asp:TextBox>
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
                ControlToValidate="uoTextBoxTo" ErrorMessage="Select Date To." InitialValue=""
                ValidationGroup="Save">*
            </asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr><td colspan="2"></td></tr>
    <tr>
        <td colspan="2" align="center">
            <asp:Button runat="server" ID="uoButtonSave" Text="Save" 
                onclick="uoButtonSave_Click" ValidationGroup="Save" />
        </td>
    </tr>
</table>
</asp:Content>

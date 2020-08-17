<%@ Page Title="" Language="C#" MasterPageFile="~/Popup.Master" AutoEventWireup="true" CodeBehind="HotelRoomOverride.aspx.cs" Inherits="TRAVELMART.Hotel.HotelRoomOverride" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script type="text/javascript">
    function SaveRecord() {
        var LastRoomBlocks = document.getElementById('<%= uoHiddenFieldLastRoomBlocks.ClientID %>').value;
        var newRoomBlock = document.getElementById('<%= uoTextBoxNumberOfRooms.ClientID %>').value;
        if (LastRoomBlocks > newRoomBlock) {
            if (confirm("New room block is less than previous room block. Would you like to proceed?") == true)
                return true;
            else
                return false;
        }
        else {
            if (confirm("New room blocks will automatically be used for Overflow Bookings. Would you like to proceed?") == true)
                return true;
            else
                return false;
        }
    }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="PageTitle"><%= PageTitle %></div>
<hr />
<br />
<asp:Panel runat="server" ID="uoPanelHotelBranch">
    <table width ="100%">
        <tr>
            <td class="LeftClass" style="width:150px">&nbsp; Region :</td>
            <td class="LeftClass">
                <asp:UpdatePanel runat="server" ID="uoUpdatePanelRegion" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:DropDownList runat="server" ID="uoDropDownListRegion"
                            AppendDataBoundItems="true" AutoPostBack="true"
                            Width="300px" 
                            onselectedindexchanged="uoDropDownListRegion_SelectedIndexChanged"></asp:DropDownList>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td class="LeftClass" style="width:150px">&nbsp; Country :</td>
            <td class="LeftClass">
                <asp:UpdatePanel runat="server" ID="uoUpdatePanelCountry" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:DropDownList runat="server" ID="uoDropDownListCountryPerRegion"
                            AppendDataBoundItems="true" AutoPostBack="true"
                            Width="301px" 
                            onselectedindexchanged="uoDropDownListCountryPerRegion_SelectedIndexChanged">
                            <asp:ListItem Value="0" Text="--Select Country--"></asp:ListItem>    
                        </asp:DropDownList>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="uoDropDownListRegion" 
                            EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>                
            </td>
        </tr>
        <tr>
            <td class="LeftClass" style="width:150px">&nbsp City Name:</td>
            <td class="LeftClass"  >
                <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:TextBox runat="server" ID="uoTextBoxCity" Width="200px"
                            ></asp:TextBox> 
                        <asp:Button runat="server" ID="uoButtonSearch" Width="95px"
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
            <td class="LeftClass" style="width:150px">&nbsp; City :</td>
            <td class="LeftClass">
                <asp:UpdatePanel runat="server" ID="uoUpdatePanelCity" UpdateMode ="Conditional">
                    <ContentTemplate>
                        <asp:DropDownList runat="server" ID="uoDropDownListLetters"
                            Width="90px"
                            onselectedindexchanged="uoDropDownListLetters_SelectedIndexChanged" 
                            AppendDataBoundItems="True" AutoPostBack="True">
                            <asp:ListItem Value="0">--</asp:ListItem>
                        </asp:DropDownList> &nbsp;&nbsp;
                        <asp:DropDownList runat="server" ID="uoDropDownListCity"
                            AppendDataBoundItems="true" AutoPostBack="true"
                            Width="200px" onselectedindexchanged="uoDropDownListCity_SelectedIndexChanged" 
                            >
                            <asp:ListItem Value="0" Text="--Select City--"></asp:ListItem>
                        </asp:DropDownList>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="uoDropDownListCountryPerRegion" 
                            EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="uoButtonSearch" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="uoDropDownListLetters" 
                            EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
       
        <tr>
            <td class="LeftClass" style="width:150px">&nbsp; Hotel :</td>
            <td class="LeftClass">
                <asp:UpdatePanel runat="server" ID="uoUpdatePanelHotel" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:DropDownList runat="server" ID="uoDropDownListHotel"
                            AppendDataBoundItems="true" Width="300px" AutoPostBack="true" 
                            onselectedindexchanged="uoDropDownListHotel_SelectedIndexChanged">
                            <asp:ListItem Value="0" Text="--Select Hotel--"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator runat="server" ValidationGroup="Save"
                            ErrorMessage="Hotel required." ControlToValidate="uoDropDownListHotel"
                            InitialValue="0" ID="rfvHotel">*</asp:RequiredFieldValidator>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="uoDropDownListCity" 
                            EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr><td colspan="2"></td></tr>
    </table>
    
</asp:Panel>

<asp:Panel runat="server" ID="uoEditPanel">
    <table width="100%">
        <tr>
            <td class="LeftClass" style="width:150px;">&nbsp; Hotel :</td>
            <td class="LeftClass" style="font-weight:bold"> <%= Hotel %> </td>
        </tr>
        <tr><td colspan="2"></td></tr>
        <tr>
            <td class="leftAligned" style="width:150px;">&nbsp; Contract Room Blocks :</td>
            <td class="LeftClass"> <%= cBlocks %> </td>
        </tr>
        
    </table>
</asp:Panel>

<table width="100%">
    <tr runat="server" id="trEdate">
        <td class="LeftClass" style="width:150px">&nbsp; Effective Date :</td>
        <td class="LeftClass">
            <asp:TextBox runat="server" ID="uoTextBoxEffectiveDate" Width="295px"></asp:TextBox>
            <cc1:TextBoxWatermarkExtender runat="server" ID="uoTextBoxEffectiveDate_WaterMark" TargetControlID="uoTextBoxEffectiveDate"
                WatermarkText="mm/dd/yyyy" Enabled="true" WatermarkCssClass="fieldWatermark"></cc1:TextBoxWatermarkExtender>
            <cc1:CalendarExtender runat="server" ID="uoTextBoxEffectiveDate_CalendarExender"
                Enabled="true" TargetControlID="uoTextBoxEffectiveDate" Format="MM/dd/yyyy" PopupButtonID="ImageButton1"></cc1:CalendarExtender>
            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/Calendar_scheduleHS.png" />
            <cc1:MaskedEditExtender runat="server" ID="uoTextBoxEffectiveDate_MaskedEditExtender"
                Enabled="true" TargetControlID="uoTextBoxEffectiveDate" Mask="99/99/9999" MaskType="Date"
                UserDateFormat="MonthDayYear"></cc1:MaskedEditExtender>
            <asp:RequiredFieldValidator runat="server" ID="rfvDate" ValidationGroup="Save"
                ErrorMessage="Effective Date required." ControlToValidate="uoTextBoxEffectiveDate">*</asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr runat="server" id="trRoomType">
        <td class="LeftClass" style="width:150px">&nbsp; Room Type :</td>
        <td class="LeftClass" >
           <asp:UpdatePanel runat="server" ID="uoUpdatePanelRoom" UpdateMode="Conditional">
                <ContentTemplate>
                     <asp:DropDownList runat="server" ID="uoDropdownListRoom"
                        AppendDataBoundItems="true" Width="301px">
                        <asp:ListItem Value="0" Text="--Select Room--"></asp:ListItem>    
                     </asp:DropDownList>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="uoDropDownListHotel" 
                        EventName="SelectedIndexChanged" />
                </Triggers>
           </asp:UpdatePanel>
        </td>
    </tr>
    <tr>
        <td class="LeftClass" style="width:150px">&nbsp; No. of Rooms :</td>
        <td class="LeftClass">
            <asp:TextBox runat="server" ID="uoTextBoxNumberOfRooms" Width="300px"></asp:TextBox>
            <cc1:NumericUpDownExtender runat="server" ID="uoTextBoxNumberOfRooms_NumericUpDown"
                Enabled="true"  Width="300" TargetButtonDownID=""
                TargetButtonUpID="" TargetControlID="uoTextBoxNumberOfRooms"
                Minimum="0" RefValues=""></cc1:NumericUpDownExtender>
            <asp:RequiredFieldValidator runat="server" ID="rfvNumOfRooms"
                ErrorMessage="Number of Rooms required." ControlToValidate="uoTextBoxNumberOfRooms"
                InitialValue="0" ValidationGroup="Save">*</asp:RequiredFieldValidator>
        </td>
    </tr>
     <tr>
        <td class="LeftClass" style="width:150px">&nbsp; Currency :</td>
        <td class="LeftClass">
            <asp:UpdatePanel runat="server" ID="uoUpdatePanelCurrency" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:TextBox runat="server" ID="uoTextBoxCurrency" Width="300px"
                        Enabled="false" CssClass="ReadOnly"></asp:TextBox>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="uoDropDownListHotel" 
                        EventName="SelectedIndexChanged" />
                </Triggers>
            </asp:UpdatePanel>
            
        </td>
    </tr>
    <tr>
        <td class="LeftClass" style="width:150px">&nbsp; Rate :</td>
        <td class="LeftClass">
            <asp:TextBox runat="server" ID="uoTextBoxRate" Width="300px"></asp:TextBox>
            <cc1:MaskedEditExtender runat="server" ID="uoTextBoxRate_MaskedEditExtender"
                TargetControlID="uoTextBoxRate" Mask="9,999,999.99" MaskType="Number"
                Enabled="true" InputDirection="RightToLeft">
            </cc1:MaskedEditExtender>
            <asp:RequiredFieldValidator runat="server" ID="rfvRate"
                ControlToValidate="uoTextBoxRate" ErrorMessage="Rate Required."
                ValidationGroup="Save">*</asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td class="LeftClass" style="width:150px">&nbsp;Rate Tax (%) :</td>
        <td class="LeftClass">
            <asp:TextBox runat="server" ID="uoTextBoxRateTax" Width="300px"></asp:TextBox>
            <cc1:MaskedEditExtender runat="server" ID="uoTextBoxRateTax_MaskedEditExtender"
                Enabled="true" Mask="999.99" MaskType="Number" InputDirection="RightToLeft"
                TargetControlID="uoTextBoxRateTax"></cc1:MaskedEditExtender>
            <asp:RequiredFieldValidator runat="server" ID="rfvRateTax"
                ErrorMessage="Rate Tax required." ValidationGroup="Save"
                ControlToValidate="uoTextBoxRateTax">*</asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td class="LeftClass" style="width:150px">&nbsp; Tax Inclusive :</td>
        <td class="LeftClass">
            <asp:CheckBox runat="server" ID="uoCheckBoxTaxBit" />
        </td>
    </tr>
    <tr><td colspan="2"></td></tr>
    <tr>
        <td colspan="2" align="center">
            <asp:Button runat="server" ID="uoButtonSave" ValidationGroup="Save" Text="Save" 
                onclick="uoButtonSave_Click" OnClientClick="return SaveRecord();"/>
        </td>
    </tr>
</table>
<asp:HiddenField runat="server" ID="uoHiddenFieldCurrencyId" Value="0" />
<asp:HiddenField runat="server" ID="uoHiddenFieldLastRoomBlocks" Value="0" />
<asp:HiddenField runat="server" ID="uoHiddenFieldBranchId" Value="0" />
<asp:HiddenField runat="server" ID="uoHiddenFieldCountryId" Value="0" />
<asp:HiddenField runat="server" ID="uoHiddenFieldEffectiveDate" Value="0" />
|<asp:HiddenField runat="server" ID="uoHiddenFieldRoomType"  Value="0"/>
</asp:Content>

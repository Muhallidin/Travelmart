<%@ Page Title="" Language="C#" MasterPageFile="~/Popup.Master" AutoEventWireup="true" CodeBehind="PortAgentContractHotelAdd.aspx.cs" Inherits="TRAVELMART.ContractManagement.PortAgentContractHotelAdd" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script type="text/javascript">
    function confirmDelete() {
        if (confirm("Delete Room?") == true)
            return true;
        else
            return false;
    }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="PageTitle">Service Provider Hotel</div>
<hr />
<table width="100%">
    <tr>
        <td class="LeftClass" style="width:120px;">Hotel Brand Name :</td>
        <td class="LeftClass">
            <asp:UpdatePanel runat="server" ID="uoPanelBrandName" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:DropDownList runat="server" ID="uoDropDownListHotelBrand"
                        AppendDataBoundItems="true" AutoPostBack="true"
                        Width="300px" 
                        onselectedindexchanged="uoDropDownListHotelBrand_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator runat="server" ID="rfvBrand"
                        ValidationGroup="Save" InitialValue="0"
                        ControlToValidate="uoDropDownListHotelBrand">* </asp:RequiredFieldValidator>                        
                </ContentTemplate>
            </asp:UpdatePanel>
        </td>
    </tr>
    <tr>
        <td class="LeftClass" style="width:120px;">Hotel Branch Name :</td>
        <td class="LeftClass">
            <asp:UpdatePanel runat="server" ID="uoPanelBranchName" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:DropDownList runat="server" ID="uoDropDownListHotelBranch"
                        AppendDataBoundItems="true" AutoPostBack="true"
                        Width="300px" 
                        onselectedindexchanged="uoDropDownListHotelBranch_SelectedIndexChanged">
                        <asp:ListItem Text="--Select Hotel Branch--" Value="0"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator runat="server" ID="rfvBranch"
                        ValidationGroup="Save" InitialValue="0"
                        ControlToValidate="uoDropDownListHotelBranch">* </asp:RequiredFieldValidator>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="uoDropDownListHotelBrand" 
                        EventName="SelectedIndexChanged" />
                </Triggers>
            </asp:UpdatePanel>
        </td>
    </tr>
</table>
<br />
<div class="PageTitle">Shuttle</div>
<hr />
<table width="100%">
    <tr>
        <td class="LeftClass" style="width:120px;">With Shuttle :</td>
        <td class="LeftClass">
            <asp:CheckBox runat="server" ID="uoCheckBoxShuttle" />
        </td>
    </tr>
</table>
<br />
<div class="PageTitle">Meals</div>
<hr />
<table width="100%">
    <tr>
        <td class="LeftClass" style="width:120px;">Meal Rate :</td>
        <td class="LeftClass">
            <asp:TextBox runat="server" ID="uoTextBoxMealRate"
                Width="200px"></asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ID="rfvMeal"
                ValidationGroup="Save" ControlToValidate="uoTextBoxMealRate">
            *</asp:RequiredFieldValidator>
            <cc1:MaskedEditExtender runat="server" ID="uoTextBoxMealRate_MaskedEditExtender"
                TargetControlID="uoTextBoxMealRate"
                MaskType="Number" Mask="9,999,999.99" Enabled="true"
                ClearMaskOnLostFocus="true" InputDirection="RightToLeft">
            </cc1:MaskedEditExtender>
            <cc1:TextBoxWatermarkExtender runat="server" ID="uoTextBoxMealRate_Watermark"
                Enabled="true" WatermarkText="0.00" TargetControlID="uoTextBoxMealRate"
                WatermarkCssClass="fieldWatermark">
            </cc1:TextBoxWatermarkExtender>
        </td>
    </tr>
    <tr>
        <td class="LeftClass" style="width:120px;">Meal Tax Rate (%) :</td>
        <td class="LeftClass">
            <asp:TextBox runat="server" ID="uoTextBoxMealTaxRate"
                Width="200px">
            </asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ID="rfvMealTax"
                ControlToValidate="uoTextBoxMealTaxRate"
                ValidationGroup="Save">*</asp:RequiredFieldValidator>
            <cc1:MaskedEditExtender runat="server" ID="uoTextBoxMealTaxRate_MaskedEditExtender"
                TargetControlID="uoTextBoxMealTaxRate"
                Mask="999.99" Enabled="true"
                MaskType="Number"
                ClearMaskOnLostFocus="true" InputDirection="RightToLeft">
            </cc1:MaskedEditExtender>
            <cc1:TextBoxWatermarkExtender runat="server" ID="uoTextBoxMealTaxRate_WaterMark"
                TargetControlID="uoTextBoxMealTaxRate" Enabled="true"
                WatermarkCssClass="fieldWatermark"
                WatermarkText="0.00">
            </cc1:TextBoxWatermarkExtender>
        </td>
    </tr>
    <tr>
        <td class="LeftClass" style="width:120px;">Meal Tax Inclusive :</td>
        <td class="LeftClass">
            <asp:CheckBox runat="server" ID="uoCheckBoxTaxInclusive" />
        </td>
    </tr>
    <tr>
        <td class="LeftClass" style="width:120px;">Meals Included :</td>
        <td class="LeftClass">
            <asp:CheckBox runat="server" ID="uoCheckBoxBfast" 
                Text="Breakfast" />
        </td>
    </tr>
    <tr>
        <td class="LeftClass" style="width:120px;"></td>
        <td class="LeftClass">
            <asp:CheckBox runat="server" ID="uoCheckBoxLunch" 
                Text="Lunch"/>
        </td>
    </tr>
    <tr>
        <td class="LeftClass" style="width:120px;"></td>
        <td class="LeftClass">
            <asp:CheckBox runat="server" ID="uoCheckBoxDinner" 
                Text="Dinner"/>
        </td>
    </tr>
    <tr>
        <td class="LeftClass" style="width:120px"></td>
        <td class="LeftClass">
            <asp:CheckBox runat="server" ID="uoCheckBoxLunchOrDinner" 
                Text="Lunch or Dinner"/>
        </td>
    </tr>
</table>
<br />
<asp:Panel runat="server" ID="uoPanelRoomHeader" CssClass="PageTitle">
    Rooms 
    <asp:Label runat="server" ID="uoLabelRoomHeader" 
        Font-Italic="true" Font-Size="X-Small"></asp:Label>
</asp:Panel>
<asp:Panel runat="server" ID="uoPanelRoomDetails">
    <cc1:CollapsiblePanelExtender runat="server"
        id="uoPanelExtender" AutoCollapse="false"
        AutoExpand="false" CollapseControlID="uoPanelRoomHeader"
        ExpandControlID="uoPanelRoomHeader"
        Collapsed="true" TargetControlID="uoPanelRoomDetails"
        CollapsedText="(Show Details)"
        ExpandedText="(Hide Details)"
        Enabled="true"
        ExpandDirection="Vertical"
        SuppressPostBack="true"
        TextLabelID="uoLabelRoomHeader">
    </cc1:CollapsiblePanelExtender>
    <table width="100%">
        <tr>
            <td class="LeftClass" style="width:120px;">Room Type :</td>
            <td class="LeftClass">
               <asp:UpdatePanel runat="server" ID="uoPanelRoomType" UpdateMode="Conditional">
                    <ContentTemplate>
                         <asp:DropDownList runat="server" ID="uoDropDownListRoomType"
                            AppendDataBoundItems="true" 
                            Width="200px">
                            <asp:ListItem Text="--Select Room Type--" Value="0"></asp:ListItem>
                         </asp:DropDownList>
                         <asp:RequiredFieldValidator runat="server" ID="rfvRoomType"
                            ValidationGroup="SaveRoom" ControlToValidate="uoDropDownListRoomType"
                            InitialValue="0">*</asp:RequiredFieldValidator>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="uoDropDownListHotelBranch" 
                            EventName="SelectedIndexChanged" />
                    </Triggers>
               </asp:UpdatePanel>
            </td>
            <td class="LeftClass" style="width:120px;"></td>
            <td class="LeftClass" style="font-weight:bold;">Daily Room Count</td>
        </tr>
        <tr>
            <td class="LeftClass" style="width:120px;">Date From :</td>
            <td class="LeftClass">
                <asp:TextBox runat="server" ID="uoTextBoxDateFrom"
                    Width="200px"></asp:TextBox>
                <asp:Image runat="server" ID="imageFrom"
                     ImageUrl="~/Images/Calendar_scheduleHS.png" />
                <asp:RequiredFieldValidator runat="server" ID="rfvDateFrom"
                    ValidationGroup="SaveRoom" ControlToValidate="uoTextBoxDateFrom"> *</asp:RequiredFieldValidator>
                <cc1:TextBoxWatermarkExtender runat="server"
                    ID="uoTextBoxDateFrom_WaterMark" TargetControlID="uoTextBoxDateFrom"
                    WatermarkCssClass="fieldWatermark" WatermarkText="MM/dd/yyyy">
                </cc1:TextBoxWatermarkExtender>
                <cc1:MaskedEditExtender runat="server" ID="uoTextBoxDateFrom_MaskedEditExtender"
                    TargetControlID="uoTextBoxDateFrom" Enabled="true" InputDirection="LeftToRight"
                    Mask="99/99/9999" MaskType="Date" UserDateFormat="MonthDayYear">
                </cc1:MaskedEditExtender>
                <cc1:CalendarExtender runat="server" ID="uoTextBoxDateFrom_CalendarExtender"
                    Enabled="true" PopupButtonID="imageFrom" 
                    TargetControlID="uoTextBoxDateFrom" Format="MM/dd/yyyy">
                </cc1:CalendarExtender>
            </td>
            <td class="RightClass" style="width:120px;">Monday :</td>
            <td class="LeftClass">
                <asp:TextBox runat="server" ID="uoTextBoxMon"
                    Width="150px"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ID="rfvMon"
                    ControlToValidate="uoTextBoxMon" ValidationGroup="SaveRoom"> *</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="LeftClass" style="width:120px;">Date To :</td>
            <td class="LeftClass">
                <asp:TextBox runat="server" ID="uoTextBoxDateTo"
                    Width="200px"></asp:TextBox>
                <asp:Image ID="imageTo" ImageUrl="~/Images/Calendar_scheduleHS.png"
                    runat="server" />
                <asp:RequiredFieldValidator runat="server" ID="rfvDateTo"
                    ValidationGroup="SaveRoom" ControlToValidate="uoTextBoxDateTo">*</asp:RequiredFieldValidator>
                <cc1:TextBoxWatermarkExtender runat="server" WatermarkCssClass="fieldWatermark"
                    Enabled="true" TargetControlID="uoTextBoxDateTo"
                    WatermarkText="MM/dd/yyyy" ID="uoTextBoxDateTo_WaterMark">
                </cc1:TextBoxWatermarkExtender>
                <cc1:MaskedEditExtender runat="server" ID="uoTextBoxDateTo_MaskedEditExtender"
                    TargetControlID="uoTextBoxDateTo" Enabled="true" 
                    InputDirection="LeftToRight" Mask="99/99/9999" MaskType="Date"
                    UserDateFormat="MonthDayYear">
                </cc1:MaskedEditExtender>
                <cc1:CalendarExtender runat="server" ID="uoTextBoxDateTo_CalendarExtender"
                    Format="MM/dd/yyyy" PopupButtonID="imageTo" 
                    TargetControlID="uoTextBoxDateTo">
                </cc1:CalendarExtender>                    
            </td>
            <td class="RightClass" style="width:120px;">Tuesday :</td>
            <td class="LeftClass">
                <asp:TextBox runat="server" ID="uoTextBoxTues"
                    Width="150px"> </asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ID="rfvTues"
                    ControlToValidate="uoTextBoxTues"
                    ValidationGroup="SaveRoom">* </asp:RequiredFieldValidator>                    
            </td>
        </tr>
        <tr>
            <td class="LeftClass" style="width:120px;">Currency :</td>
            <td class="LeftClass">
                <asp:UpdatePanel runat="server" ID="uoPanelCurrency" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:TextBox runat="server" ID="uoTextBoxCurrency"
                            Width="200px" Enabled="false" CssClass="ReadOnly"> </asp:TextBox>
                        <asp:HiddenField runat="server" ID="uoHiddenFieldCurrencyId" Value="0" EnableViewState="true" />
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="uoDropDownListHotelBrand" 
                            EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>                
            </td>
            <td class="RightClass" style="width:120px;">Wednesday :</td>
            <td class="LeftClass">
                <asp:TextBox runat="server" ID="uoTextBoxWed"
                    Width="150px"> </asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ID="rfvWed"
                    ValidationGroup="SaveRoom"
                    ControlToValidate="uoTextBoxWed">* </asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
             <td class="LeftClass" style="width:120px;">Room Rate :</td>
             <td class="LeftClass">
                <asp:TextBox runat="server" ID="uoTextBoxRoomRate"
                    Width="200px"> </asp:TextBox>
                <asp:RequiredFieldValidator runat="server" 
                    ID="rfvRoomRate" ControlToValidate="uoTextBoxRoomRate"
                    ValidationGroup="SaveRoom">* </asp:RequiredFieldValidator>
                <cc1:MaskedEditExtender runat="server"
                    ID="uoTextBoxRoomRate_MaskedEditExtender"
                    TargetControlID="uoTextBoxRoomRate"
                    ClearMaskOnLostFocus="true"
                    Enabled="true" InputDirection="RightToLeft"
                    Mask="9,999,999.99" MaskType="Number">
                 </cc1:MaskedEditExtender>
                 <cc1:TextBoxWatermarkExtender runat="server"
                    ID="uoTextBoxRoomRate_WaterMark" Enabled="true"
                    TargetControlID="uoTextBoxRoomRate"
                    WatermarkCssClass="fieldWatermark"
                    WatermarkText="0.00">
                 </cc1:TextBoxWatermarkExtender>
             </td>
             <td class="RightClass" style="width:120px;">Thursday :</td>
             <td class="LeftClass">
                <asp:TextBox runat="server" ID="uoTextBoxThur"
                    Width="150px"> </asp:TextBox>
                <asp:RequiredFieldValidator runat="server" 
                    id="rfvThurs" ControlToValidate="uoTextBoxThur"
                    ValidationGroup="SaveRoom">* </asp:RequiredFieldValidator>
             </td>
        </tr>
        <tr>
            <td class="LeftClass" style="width:120px;">Room Rate Tax (%) :</td>
            <td class="LeftClass">
                <asp:TextBox runat="server" ID="uoTextBoxRoomTax"
                    Width="200px"> </asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ID="rfvRoomTax"
                    ValidationGroup="SaveRoom" ControlToValidate="uoTextBoxRoomTax">* </asp:RequiredFieldValidator>
                <cc1:MaskedEditExtender runat="server" ID="uoTextBoxRoomTax_MaskedEditExtender"
                    TargetControlID="uoTextBoxRoomTax" Enabled="true"
                    Mask="999.99" MaskType="Number"
                    InputDirection="RightToLeft">
                </cc1:MaskedEditExtender>
                <cc1:TextBoxWatermarkExtender runat="server" ID="uoTextBoxRoomTax_WaterMArk"
                    TargetControlID="uoTextBoxRoomTax" Enabled="true"
                    WatermarkCssClass="fieldWatermark"
                    WatermarkText="0.00">
                </cc1:TextBoxWatermarkExtender>
            </td>
            <td class="RightClass" style="width:120px;">Friday :</td>
            <td class="LeftClass">
                <asp:TextBox runat="server" ID="uoTextBoxFri"
                    Width="150px"> </asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ID="rfvFri"
                    ControlToValidate="uoTextBoxFri" 
                    ValidationGroup="SaveRoom">* </asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="LeftClass" style="width:120px;">Room Tax Inclusive :</td>
            <td class="LeftClass">
                <asp:CheckBox runat="server" ID="uoCheckBoxRoomTaxInclusive" />
            </td>
            <td class="RightClass" style="width:120px;">Saturday :</td>
             <td class="LeftClass">
                <asp:TextBox runat="server" ID="uoTextBoxSat"
                    Width="150px"> </asp:TextBox>
                <asp:RequiredFieldValidator runat="server"
                    ID="rfvSat" ControlToValidate="uoTextBoxSat"
                    ValidationGroup="SaveRoom">* </asp:RequiredFieldValidator>
             </td>
        </tr>
        <tr>
             <td class="LeftClass" style="width:120px;"></td>
             <td class="LeftClass">
                <asp:Button runat="server" ID="uoBtnSaveRoom"
                    ValidationGroup="SaveRoom" Text="Add Room"
                    Width="75px" onclick="uoBtnSaveRoom_Click" />
             </td>
             <td class="RightClass" style="width:120px;">Sunday :</td>
             <td class="LeftClass">
                <asp:TextBox runat="server" ID="uoTextBoxSun"
                    Width="150px"> </asp:TextBox>
                <asp:RequiredFieldValidator runat="server"
                    ID="rfvSun" ControlToValidate="uoTextBoxSun"
                    ValidationGroup="SaveRoom">* </asp:RequiredFieldValidator>
             </td>
        </tr>
    </table>
</asp:Panel>
<br />
<table width="100%" class="Module">
    <tr>
        <td>
            <asp:UpdatePanel runat="server" ID="uoPanelRoomList" UpdateMode="Conditional">
                <ContentTemplate>
                     <asp:GridView ID="uoRoomList" runat="server" Width="100%" 
                            CssClass="listViewTable" AutoGenerateColumns="False" 
                            onselectedindexchanged="uoRoomList_SelectedIndexChanged">
                            <Columns>
                                <asp:BoundField DataField="RoomType" HeaderText="Room Type" />
                                <asp:BoundField DataField="DateFrom" HeaderText="Date From" 
                                    DataFormatString="{0:d}" />
                                <asp:BoundField DataField="DateTo" HeaderText="Date To" 
                                    DataFormatString="{0:d}"/>
                                <asp:BoundField DataField="Currency" HeaderText="Currency"/>
                                <asp:BoundField DataField="RoomRate" HeaderText="Room Rate"/>
                                <asp:BoundField DataField="RoomTax" HeaderText="Tax(%)"/>
                                <asp:BoundField DataField="RoomTaxInclusive" HeaderText="Tax Inclusive"/>
                                <asp:BoundField DataField="Mon" HeaderText="Mon"/>
                                <asp:BoundField DataField="Tue" HeaderText="Tues"/>
                                <asp:BoundField DataField="Wed" HeaderText="Wed"/>
                                <asp:BoundField DataField="Thu" HeaderText="Thurs"/>
                                <asp:BoundField DataField="Fri" HeaderText="Fri"/>
                                <asp:BoundField DataField="Sat" HeaderText="Sat"/>
                                <asp:BoundField DataField="Sun" HeaderText="Sun"/>
                                <asp:ButtonField CommandName="Select" Text="Delete" />
                            </Columns>
                    </asp:GridView>                   
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="uoBtnSaveRoom" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
        </td>
    </tr>
    <tr>
        <td align="center">
            <asp:Button runat="server" ID="uoBtnSave" ValidationGroup="Save"
                Text = "Save" onclick="uoBtnSave_Click" />
        </td>
    </tr>
</table>

<asp:HiddenField runat="server" ID="uoHiddenFieldServicedetailId" Value="0" />
<asp:HiddenField runat="server" ID="uoHiddenFieldCountryId" Value="0" />
</asp:Content>

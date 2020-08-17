<%@ Page Title="" Language="C#" MasterPageFile="~/Popup.Master" AutoEventWireup="true" CodeBehind="PortAgentContractVehicleAdd.aspx.cs" Inherits="TRAVELMART.ContractManagement.PortAgentContractVehicleAdd" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script language="javascript" type="text/javascript">
    function confirmDelete() {
        if (confirm("Delete Vehicle?") == true)
            return true;
        else
            return false;
    }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<table width="100%">
    <tr>
        <td class="PageTitle" colspan="2"><%= AddEditLabel %></td>
    </tr>
    <tr><td colspan="2"><hr /></td></tr>
    <tr>
        <td class="LeftClass" style="width:150px;">&nbsp;Vehicle Brand Name :</td>
        <td class="LeftClass">
            <asp:DropDownList runat="server" ID="uoDropDownListVehicleBrandName" Width="95%"
                AutoPostBack="true" AppendDataBoundItems="true" 
                onselectedindexchanged="uoDropDownListVehicleBrandName_SelectedIndexChanged"></asp:DropDownList>
           <asp:RequiredFieldValidator runat="server" ID="rfvBrand"
                ControlToValidate="uoDropDownListVehicleBrandName" InitialValue="0"
                ErrorMessage="Hotel Brand required." ValidationGroup="Save">*</asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td class="LeftClass" style="width:150px;">&nbsp;Vehicle Branch Name :</td>
        <td class="LeftClass">
            <asp:DropDownList runat="server" ID="uoDropDownListVehicleBranch" Width="95%"
                AppendDataBoundItems="true" AutoPostBack="true" 
                onselectedindexchanged="uoDropDownListVehicleBranch_SelectedIndexChanged">
                <asp:ListItem Text="--Select Vendor Branch--" Value="0"></asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator runat="server" ID="rfvBranch"
                ControlToValidate="uoDropDownListVehicleBranch" InitialValue="0"
                ErrorMessage="Hotel Branch required." ValidationGroup="Save">*</asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr><td colspan="2"></td></tr>
    
    <tr runat="server" id="uoTRVehicle" visible="false">
        <td colspan="2">
            <asp:Panel runat="server" ID="panelVehicle">
                <div  class="PageTitle">
                    Vehicles &nbsp;
                    <asp:Label runat="server" ID="uoLabelVehicleHeader" Text="" Font-Size="Smaller" Font-Italic="true"></asp:Label>
                </div>                
            </asp:Panel>
            <asp:Panel runat="server" ID="panelVehicleDetails">
                <cc1:CollapsiblePanelExtender runat="server" ID="CollapsiblePanelExtenderRooms"
                    CollapseControlID="panelVehicle" Collapsed="false"
                    ExpandControlID="panelVehicle"
                    TargetControlID="panelVehicleDetails"
                    CollapsedText="(Show Details)"
                    ExpandedText="(Hide Details)"
                    TextLabelID="uoLabelVehicleHeader"
                    ExpandDirection="Vertical">
                </cc1:CollapsiblePanelExtender>
                <table width="100%">
                    <tr>
                        <td class="LeftClass" style="width:20%">&nbsp; Type :</td>
                        <td class="LeftClass">
                            <asp:DropDownList runat="server" ID="uoDropDownListVehicleType"
                                AppendDataBoundItems="true" Width="200px" AutoPostBack="true" 
                                onselectedindexchanged="uoDropDownListVehicleType_SelectedIndexChanged"></asp:DropDownList>
                            <asp:RequiredFieldValidator runat="server" ID="rfvVehicleType"
                                ErrorMessage="Vehicle Type required." ValidationGroup="SaveVehicle"
                                ControlToValidate="uoDropDownListVehicleType"
                                InitialValue="0">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>  
                     <tr>
                        <td class="LeftClass">&nbsp;Origin :</td>
                        <td class="LeftClass">
                            <asp:DropDownList runat="server" ID="uoDropDownListOrigin" 
                                AutoPostBack="True" Width="200px" 
                                onselectedindexchanged="uoDropDownListOrigin_SelectedIndexChanged">
                                <asp:ListItem Value="0">--Select Origin--</asp:ListItem>
                                <asp:ListItem>Airport</asp:ListItem>
                                <asp:ListItem>Hotel</asp:ListItem>
                                <asp:ListItem>Port</asp:ListItem>
                                <asp:ListItem>Others</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator runat="server" ID="rfvVehicleOrigin"
                                ErrorMessage="Origin required." ValidationGroup="SaveVehicle"
                                ControlToValidate="uoDropDownListOrigin"
                                InitialValue="0">*</asp:RequiredFieldValidator>
                            &nbsp;
                            <asp:TextBox runat="server" ID="uoTextBoxOtherOrigin" Visible="false" Width="30%"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ID="rfvOtherVOrigin"
                                ErrorMessage="Origin required." ValidationGroup="SaveVehicle"
                                ControlToValidate="uoTextBoxOtherOrigin">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="LeftClass">&nbsp;Destination :</td>
                        <td class="LeftClass">
                            <asp:DropDownList runat="server" ID="uoDropDownListDestination" 
                                AutoPostBack="True" Width="200px"
                                onselectedindexchanged="uoDropDownListDestination_SelectedIndexChanged">
                                <asp:ListItem Value="0">--Select Destination--</asp:ListItem>
                                <asp:ListItem>Airport</asp:ListItem>
                                <asp:ListItem>Hotel</asp:ListItem>
                                <asp:ListItem>Port</asp:ListItem>
                                <asp:ListItem>Others</asp:ListItem>
                            </asp:DropDownList>
                             <asp:RequiredFieldValidator runat="server" ID="rfvVehicleDestination"
                                ErrorMessage="Capacity required." ValidationGroup="SaveVehicle"
                                ControlToValidate="uoDropDownListDestination"
                                InitialValue="0">*</asp:RequiredFieldValidator>
                            &nbsp;
                            <asp:TextBox runat="server" ID="uoTextBoxOtherDestination" Visible="false" Width="30%"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ID="rfvVOtherDestination"
                                ErrorMessage="Origin required." ValidationGroup="SaveVehicle"
                                ControlToValidate="uoTextBoxOtherDestination">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="LeftClass">&nbsp;Currency :</td>
                        <td class="LeftClass">
                            <asp:DropDownList runat="server" ID="uoDropDownListVehicleCurrency" AppendDataBoundItems="true"
                             Width="200px">
                            </asp:DropDownList>
                             <asp:RequiredFieldValidator ID="rfvVCurrency" runat="server" 
                            ControlToValidate="uoDropDownListVehicleCurrency" ErrorMessage="currency required." 
                            ValidationGroup="SaveVehicle"
                            InitialValue="0">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="LeftClass">&nbsp;Rate :</td>
                        <td class="LeftClass">
                            <asp:TextBox runat="server" ID="uoTextBoxRate" Width="200px"></asp:TextBox>
                            <cc1:MaskedEditExtender runat="server" ID="uoTextBoxRate_MaskedEditExtender"
                                InputDirection="RightToLeft" Mask="999,999.99" TargetControlID="uoTextBoxRate"
                                MaskType="Number" Enabled="true"></cc1:MaskedEditExtender>
                            <asp:RequiredFieldValidator runat="server" ID="rfvVehicleRate"
                                ControlToValidate="uoTextBoxRate" ValidationGroup="SaveVehicle"
                                ErrorMessage="Vehicle Rate is required.">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="LeftClass">&nbsp; Start Date :</td>
                        <td class="LeftClass">
                            <asp:TextBox ID="uoTextBoxDateFrom" runat="server" Width="200px"></asp:TextBox>
                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" 
                             runat="server" Enabled="True" TargetControlID="uoTextBoxDateFrom" WatermarkCssClass="fieldWatermark" 
                             WatermarkText="MM/dd/yyyy">
                            </cc1:TextBoxWatermarkExtender>  
                            <asp:ImageButton ID="ImageButton3" runat="server" 
                             ImageUrl="~/Images/Calendar_scheduleHS.png" />                                      
                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" 
                             Enabled="True" TargetControlID="uoTextBoxDateFrom" PopupButtonID="ImageButton3" Format="MM/dd/yyyy">
                            </cc1:CalendarExtender>
                            <cc1:MaskedEditExtender ID="MaskedEditExtender1" 
                             runat="server" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                             CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                             CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                             Mask="99/99/9999" MaskType="Date" TargetControlID="uoTextBoxDateFrom" UserDateFormat="MonthDayYear">
                            </cc1:MaskedEditExtender> 
                            <asp:RequiredFieldValidator ID="rfvVStartDate" runat="server" 
                            ControlToValidate="uoTextBoxDateFrom" 
                            ErrorMessage="Start date required." ValidationGroup="SaveVehicle">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="LeftClass">&nbsp;End Date :</td>
                        <td class="LeftClass">
                            <asp:TextBox ID="uoTextBoxDateTo" runat="server" Width="200px"></asp:TextBox>
                            <cc1:TextBoxWatermarkExtender ID="uoTextBoxDateTo_TextBoxWatermarkExtender" 
                             runat="server" Enabled="True" TargetControlID="uoTextBoxDateTo" WatermarkCssClass="fieldWatermark" WatermarkText="MM/dd/yyyy">
                            </cc1:TextBoxWatermarkExtender>    
                            <asp:ImageButton ID="ImageButton4" runat="server" 
                             ImageUrl="~/Images/Calendar_scheduleHS.png" />                                    
                            <cc1:CalendarExtender ID="CalendarExtender2" runat="server" 
                             Enabled="True" TargetControlID="uoTextBoxDateTo" PopupButtonID="ImageButton4" Format="MM/dd/yyyy">
                            </cc1:CalendarExtender>
                            <cc1:MaskedEditExtender ID="MaskedEditExtender2" 
                             runat="server" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                             CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                             CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                             Mask="99/99/9999" MaskType="Date" TargetControlID="uoTextBoxDateTo" UserDateFormat="MonthDayYear">
                            </cc1:MaskedEditExtender>
                            <asp:RequiredFieldValidator ID="rfvVEndDate" runat="server" 
                            ControlToValidate="uoTextBoxDateTo" ErrorMessage="End date required." 
                            ValidationGroup="SaveVehicle">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="LeftClass">&nbsp;Capacity :</td>
                        <td class="LeftClass">
                            <asp:TextBox runat="server" ID="uoTextBoxCapacity" CssClass="ReadOnly" 
                                Width="200px" ReadOnly="true"></asp:TextBox>
                        </td>
                    </tr>             
                    <tr>
                        <td></td>
                        <td class="LeftClass">
                            <asp:Button runat="server" ID="uoButtonSaveVehicle" ValidationGroup="SaveVehicle"
                                Text="Add Vehicle" onclick="uoButtonSaveVehicle_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2"></td>
                    </tr>
                    
                </table>
            </asp:Panel>
        </td>
    </tr>
    
    <tr><td colspan="2"></td></tr>
    <tr>
        <td colspan="2" class="Module">
            <asp:ListView runat="server" ID="uoVehicleList" 
                onitemcommand="uoVehicleList_ItemCommand" onitemdeleting="uoVehicleList_ItemDeleting" 
                >
                <LayoutTemplate>
                    <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                        <tr>
                            <th>Vehicle Type</th>
                            <th>Capacity</th>
                            <th>Date from</th>
                            <th>Date To</th>
                            <th>Origin</th>
                            <th>Destination</th>
                            <th>Rate</th>
                            <th>Currency</th>
                            <th runat="server" id="DeleteTH" style="width:10%"></th>
                        </tr>
                        <asp:panel runat="server" ID="itemPlaceHolder"></asp:panel>
                    </table>
                </LayoutTemplate>
                <ItemTemplate>
                    <tr>
                        <td class="leftAligned"><%# Eval("colVehicleNameVarchar")%></td>
                        <td class="leftAligned"><%# Eval("colVehicleCapacity")%></td>
                        <td class="leftAligned"><%# String.Format("{0:dd-MMM-yyyy}", Eval("colDateFromDatetime")) %></td>
                        <td class="leftAligned"><%# String.Format("{0:dd-MMM-yyyy}", Eval("colDateToDatetime")) %></td>
                        <td class="leftAligned"><%# Eval("colOriginVarchar")%></td>
                        <td class="leftAligned"><%# Eval("colDestinationVarchar")%></td>
                        <td class="leftAligned"><%# Eval("colServiceRateMoney") %></td>
                        <td class="leftAligned"><%# Eval("colCurrencyNameVarchar") %></td>
                        <td class="leftAligned">
                            <asp:LinkButton runat="server" ID="uoHyperLinkDelete" CommandName="Delete"
                                CommandArgument='<%# Eval("colContractPortAgentServiceSpecificationsIdInt") %>' 
                                Text="Delete" OnClientClick="return confirmDelete();">
                            </asp:LinkButton>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:ListView>
        </td>
    </tr>
    <tr><td colspan="2" align="center"><asp:Button runat="server" ID="uoButtonSave" Text="Save" ValidationGroup="Save" 
            onclick="uoButtonSave_Click" />
    </td></tr>
</table>
</asp:Content>

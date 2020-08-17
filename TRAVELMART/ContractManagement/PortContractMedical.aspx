<%@ Page Title="" Language="C#" MasterPageFile="~/Popup.Master" AutoEventWireup="true" CodeBehind="PortContractMedical.aspx.cs" Inherits="TRAVELMART.ContractManagement.PortContractMedical" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script type="text/javascript" language="javascript">
    function confirmDelete() {
        if (confirm("Delete transfer?") == true)
            return true;
        else
            return false;
    }
</script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<table width="100%">
    <tr>
        <td colspan="2" class="PageTitle">
            <%= uoLabelTitle%>
        </td>
    </tr>
    <tr><td colspan="2"><hr /></td></tr>
    <tr>
        <td class="LeftClass">&nbsp;No of Days :</td>
        <td class="LeftClass">
            <asp:TextBox runat="server" ID="uoTextBoxDays" Width="100px"></asp:TextBox>
            <cc1:NumericUpDownExtender runat="server" ID="uoNumeric1"
                TargetControlID="uoTextBoxDays"
                Width="100" TargetButtonDownID=""
                TargetButtonUpID=""
                Minimum="0" Enabled="true" RefValues=""></cc1:NumericUpDownExtender>
        </td>
    </tr>
    <tr>
        <td class="LeftClass">&nbsp;Rate Per Day :</td>
        <td class="LeftClass">
            <asp:TextBox runat="server" ID="uoTextBoxRate" Width="100px"></asp:TextBox>
             <cc1:MaskedEditExtender ID="uoTextBoxRatePerHead_MaskedEditExtender" 
                runat="server"  Enabled="True" 
                Mask="999,999.99" MaskType="Number" TargetControlID="uoTextBoxRate">
            </cc1:MaskedEditExtender>
            <asp:RequiredFieldValidator ID="rfvRatePerHead" runat="server"
                ControlToValidate="uoTextBoxRate" 
                ValidationGroup="Save" ErrorMessage="Rate per Day required." InitialValue=""
             >*</asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr><td colspan="2"></td></tr>
    <tr>
        <td colspan="2">
            <asp:Panel runat="server" ID="panelMedicalHeader">
                <div class="PageTitle">
                    Medical Transfers
                    <asp:Label runat="server" ID="uoLabelShowDetails" Font-Size="Smaller"></asp:Label>
                </div>
            </asp:Panel>
            <hr />
            <asp:Panel runat="server" ID="uoPanelMedicalDetails">
                <cc1:CollapsiblePanelExtender runat="server" ID="uoCollapsiblePanelMedical"
                    Enabled="true" Collapsed="false"
                    TargetControlID="uoPanelMedicalDetails"
                    CollapseControlID="panelMedicalHeader"
                    ExpandControlID="panelMedicalHeader"
                    ExpandedText="(Hide Details)"
                    CollapsedText="(Show Details)"
                    TextLabelID="uoLabelShowDetails">
                </cc1:CollapsiblePanelExtender>
                <table width="100%">
                     <tr>
                        <td class="LeftClass">&nbsp;Transfer Rate (per way) :</td>
                        <td class="LeftClass">
                            <asp:TextBox runat="server" ID="uoTextBoxTrnsferRate" Width="200px"></asp:TextBox>
                            <cc1:MaskedEditExtender runat="server" ID="uoTextBoxTrnsferRate_MaskedEditExtender"
                                TargetControlID="uoTextBoxTrnsferRate" Mask="999,999.99"
                                MaskType="Number" InputDirection="LeftToRight"></cc1:MaskedEditExtender>
                            <asp:RequiredFieldValidator runat="server" ID="rfvRate"
                                ControlToValidate="uoTextBoxTrnsferRate" ValidationGroup="SaveTransfer"
                                ErrorMessage="Transfer Rate required.">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="LeftClass">&nbsp;Currency :</td>
                        <td class="LeftClass">
                            <asp:DropDownList runat="server" ID="uoDropDownListCurrency" AppendDataBoundItems="true"></asp:DropDownList>
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
                                <asp:ListItem>Doctor's Office</asp:ListItem>
                                <asp:ListItem>Medical Clinic</asp:ListItem>
                                <asp:ListItem>Hotel</asp:ListItem>
                                <asp:ListItem>Pharmacy</asp:ListItem>
                                <asp:ListItem>Pier</asp:ListItem>
                                <asp:ListItem>Others</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator runat="server" ID="rfvVehicleOrigin"
                                ErrorMessage="Origin required." ValidationGroup="SaveVehicle"
                                ControlToValidate="uoDropDownListOrigin"
                                InitialValue="0">*</asp:RequiredFieldValidator>
                            &nbsp;
                            <asp:TextBox runat="server" ID="uoTextBoxOtherOrigin" Visible="false" Width="30%"></asp:TextBox>
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
                                <asp:ListItem>Doctor's Office</asp:ListItem>
                                <asp:ListItem>Medical Clinic</asp:ListItem>
                                <asp:ListItem>Hotel</asp:ListItem>
                                <asp:ListItem>Pharmacy</asp:ListItem>
                                <asp:ListItem>Pier</asp:ListItem>
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
                        <td class="LeftClass">&nbsp;Remarks :</td>
                        <td class="LeftClass">
                            <asp:TextBox runat="server" ID="uoTextBoxRemarks" 
                                TextMode="MultiLine" Height="50px" Width="500px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            &nbsp;
                            <asp:Button runat="server" ID="uoButtonSaveTransfer" Text="Save Transfer" 
                                onclick="uoButtonSaveTransfer_Click" />
                        </td>
                    </tr>
                </table>
            </asp:Panel> 
        </td>
    </tr>
   
    <tr><td colspan="2"></td></tr>
    <tr>
        <td colspan="2">
            <asp:ListView runat="server" ID="uoTransferList" 
                onitemcommand="uoTransferList_ItemCommand" 
                onitemdeleting="uoTransferList_ItemDeleting">
                <LayoutTemplate>
                    <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                        <tr>
                            <th>Rate</th>
                            <th>Currency</th>
                            <th>Origin</th>
                            <th>Destination</th>
                            <th>Remarks</th>
                            <th runat="server" id="uoDeleteTH" style="width:15%"></th>
                        </tr>
                        <asp:PlaceHolder runat="server" ID="itemPlaceHolder"></asp:PlaceHolder>
                    </table>
                </LayoutTemplate>
                <ItemTemplate>
                    <tr>
                        <td class="leftAligned">
                            <%# Eval("colServiceRateMoney") %>
                        </td>
                        <td class="leftAligned">
                            <%# Eval("colCurrencyNameVarchar")%>
                        </td>
                        <td class="leftAligned">
                            <%# Eval("colOriginVarchar") %>
                        </td>
                        <td class="leftAligned">
                            <%# Eval("colDestinationVarchar") %>
                        </td>
                        <td class="leftAligned">
                            <%# Eval("colRemarksVarchar") %>
                        </td>
                        <td class="leftAligned">
                            <asp:LinkButton runat="server" ID="uoHyperlinkDelete" CommandName="Delete"
                                CommandArgument='<%# Eval("colContractPortAgentServiceSpecificationsIdInt") %>'
                                Text="Delete"></asp:LinkButton>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:ListView>
        </td>
    </tr>
    <tr>
        <td align="center" colspan="2">
            <asp:Button runat="server" ID="uoButtonSave" Text="Save" 
                onclick="uoButtonSave_Click" />
        </td>
    </tr>
</table>
<asp:HiddenField runat="server" ID="uoHiddenFieldDetailId" Value="0" />
</asp:Content>

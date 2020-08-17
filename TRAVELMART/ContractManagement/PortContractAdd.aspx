<%@ Page Title="" Language="C#" MasterPageFile="~/Popup.Master" AutoEventWireup="true"
    CodeBehind="PortContractAdd.aspx.cs" Inherits="TRAVELMART.ContractManagement.PortContractAdd" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript" src="../FBox/jquery-1.6.1.min.js"></script>

    <script type="text/javascript" src="../FBox/fancybox/jquery.mousewheel-3.0.4.pack.js"></script>

    <script type="text/javascript" src="../FBox/fancybox/jquery.fancybox-1.3.4.pack.js"></script>

    <script src="../Menu/menu.js" type="text/javascript"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="leftAligned">
        <div class="PageTitle">
            Port Contract
        </div>
        <hr />
        <table width="100%" style="text-align: left">
            <tr>
                <td style="width: 130px">
                    Company Name:
                </td>
                <td colspan="3">
                    <asp:DropDownList ID="uoDropDownListPort" runat="server" Width="452px" AppendDataBoundItems="true">
                        <asp:ListItem Text="--Select a Port Company--" Value="0" />
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="uoDropDownListPort"
                        ErrorMessage="Vendor name required." ValidationGroup="Save" InitialValue="0">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    Contract Title:
                </td>
                <td colspan="3">
                    <asp:TextBox ID="uotextboxContractTitle" runat="server" Width="448px" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="uotextboxContractTitle"
                        ErrorMessage="Contract title required." ValidationGroup="Save">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td valign="top">
                    Remarks :
                </td>
                <td colspan="3">
                    <asp:TextBox ID="uotextboxRemarks" runat="server" Width="448px" TextMode="MultiLine"
                        CssClass="TextBoxInput" Height="60px" />
                </td>
            </tr>
            <tr>
                <td>
                    Contract Start Date :
                </td>
                <td>
                    <asp:TextBox ID="uotextboxStartDate" runat="server" Width="130px" />
                    <cc1:TextBoxWatermarkExtender ID="uotextboxStartDate_TextBoxWatermarkExtender" runat="server"
                        Enabled="True" TargetControlID="uotextboxStartDate" WatermarkCssClass="fieldWatermark"
                        WatermarkText="MM/dd/yyyy">
                    </cc1:TextBoxWatermarkExtender>
                    <cc1:MaskedEditExtender ID="uotextboxStartDate_MaskedEditExtender" runat="server"
                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                        CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                        CultureTimePlaceholder="" Enabled="True" TargetControlID="uotextboxStartDate"
                        Mask="99/99/9999" UserDateFormat="MonthDayYear" MaskType="Date">
                    </cc1:MaskedEditExtender>
                    <cc1:CalendarExtender ID="uotextboxStartDate_CalendarExtender" runat="server" Enabled="True"
                        PopupButtonID="Image1" TargetControlID="uotextboxStartDate" Format="MM/dd/yyyy">
                    </cc1:CalendarExtender>
                    <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/Calendar_scheduleHS.png"
                        Height="16px" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="uotextboxStartDate"
                        ErrorMessage="Contract start required." ValidationGroup="Save">*</asp:RequiredFieldValidator>
                </td>
                <td>
                    Rate per Head :
                </td>
                <td>
                    <asp:TextBox ID="uotextboxRateperHead" runat="server" />
                    <cc1:MaskedEditExtender ID="uotextboxRate_MaskedEditExtender" runat="server" CultureAMPMPlaceholder=""
                        CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                        CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                        Enabled="True" Mask="999,999.99" MaskType="Number" TargetControlID="uotextboxRateperHead">
                    </cc1:MaskedEditExtender>
                </td>
            </tr>
            <tr>
                <td style="width: 120px">
                    Contract End Date :
                </td>
                <td>
                    <asp:TextBox ID="uotextboxEndDate" runat="server" Width="130px" />
                    <cc1:TextBoxWatermarkExtender ID="uotextboxEndDate_TextBoxWatermarkExtender1" runat="server"
                        Enabled="True" TargetControlID="uotextboxEndDate" WatermarkCssClass="fieldWatermark"
                        WatermarkText="MM/dd/yyyy">
                    </cc1:TextBoxWatermarkExtender>
                    <cc1:MaskedEditExtender ID="uotextboxEndDate_MaskedEditExtender" runat="server" CultureAMPMPlaceholder=""
                        CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                        CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                        Enabled="True" TargetControlID="uotextboxEndDate" MaskType="Date" UserDateFormat="MonthDayYear"
                        Mask="99/99/9999">
                    </cc1:MaskedEditExtender>
                    <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/Calendar_scheduleHS.png" />
                    <cc1:CalendarExtender ID="uotextboxEndDate_CalendarExtender" runat="server" Enabled="True"
                        PopupButtonID="Image2" TargetControlID="uotextboxEndDate" Format="MM/dd/yyyy">
                    </cc1:CalendarExtender>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="uotextboxEndDate"
                        ErrorMessage="Contract end required." ValidationGroup="Save">*</asp:RequiredFieldValidator>
                </td>
                <td>
                    Tax Rate(%) :
                </td>
                <td>
                    <asp:TextBox ID="uoTextBoxTax" runat="server"></asp:TextBox>
                    <cc1:MaskedEditExtender ID="uoTextBoxTax_MaskedEditExtender" runat="server" CultureAMPMPlaceholder=""
                        CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                        CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                        Enabled="True" Mask="999.99" MaskType="Number" TargetControlID="uoTextBoxTax">
                    </cc1:MaskedEditExtender>
                </td>
            </tr>
            <tr>
                <td>
                    Currency :
                </td>
                <td>
                    <asp:DropDownList ID="uoDropDownListCurrency" runat="server" ValidationGroup="Room"
                        Width="152px" AppendDataBoundItems="true">
                        <asp:ListItem Text="- Select Currency -" Value="0" />
                    </asp:DropDownList>
                </td>
                <td>
                    Rate Tax Inclusive :
                </td>
                <td>
                    <asp:CheckBox ID="uoCheckBoxTaxInclusive" runat="server" Checked="True" />
                </td>
            </tr>
            <tr>
                <td>
                    RCCL Personnel :
                </td>
                <td colspan="3">
                    <asp:TextBox ID="uotextboxRCCLRep" runat="server" Width="150px" />
                </td>
            </tr>
            <tr>
                <td>
                    Vendor Personnel :
                </td>
                <td colspan="3">
                    <asp:TextBox ID="uotextboxVendorRep" runat="server" Width="150px" />
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td colspan="3">
                    <asp:Button ID="uobtnSave" runat="server" Text="Save" OnClick="uobtnSave_Click" ValidationGroup="Save" />
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <br />
                    <asp:HiddenField ID="uohiddenvID" runat="server" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

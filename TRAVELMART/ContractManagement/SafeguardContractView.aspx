<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Popup.Master" CodeBehind="SafeguardContractView.aspx.cs"
    Inherits="TRAVELMART.ContractManagement.SafeguardContractView" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript" src="../FBox/jquery-1.6.1.min.js"></script>

    <script type="text/javascript" src="../FBox/fancybox/jquery.mousewheel-3.0.4.pack.js"></script>

    <script type="text/javascript" src="../FBox/fancybox/jquery.fancybox-1.3.4.pack.js"></script>

    <script src="../Menu/menu.js" type="text/javascript"></script>

    <style type="text/css">
        .style2
        {
            width: 33px;
        }
        .style4
        {
            width: 147px;
        }
        .style6
        {
            width: 147px;
            height: 30px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="leftAligned">
        <div class="PageTitle">
            Safeguard Contract
        </div>
        <hr />
        <table width="100%" style="text-align: left">
            <tr>
                <td colspan="4">
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Header" />
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    &nbsp;Contract Status : &nbsp;<asp:Label ID="ucLabelContractStatus" runat="server"
                        Font-Size="14pt" ForeColor="Red" Font-Bold="true"></asp:Label>
                </td>
            </tr>
           <%-- <tr>
                <td colspan="4">
                    &nbsp;Contract Attachment: &nbsp;<asp:LinkButton ID="LinkButton1" runat="server"
                        OnClick="LinkButton1_Click">View Attachment</asp:LinkButton>
                </td>
            </tr>--%>
            
             <tr runat="server" id="notCrewAssit">
                <td>
                    &nbsp;Contract Attachment :
                </td>
                <td colspan="3">
                    <asp:GridView ID="uoGridViewHotelContractAttachment" runat="server" AutoGenerateColumns="False"
                        CssClass="listViewTable" GridLines="Vertical" Width="452px" RowStyle-HorizontalAlign="Left">
                        <Columns>
                            <asp:TemplateField HeaderText="Filename">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click" CommandArgument='<%# Eval("AttachmentId") %>'
                                        Text='<%# Eval("FileName") %>'></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle BackColor="#CCCCCC" />
                        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Left" />
                        <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />                      
                        <AlternatingRowStyle BackColor="#CCCCCC" />
                        <EmptyDataTemplate>
                            <table cellspacing="0" cellpadding="3" rules="cols" border="0" CssClass="listViewTable">
                                <tr style="color: White; ">
                                    <th scope="col">
                                        Filename
                                    </th>
                                </tr>
                                <tr>
                                    <td>
                                        No Contract Attachment
                                    </td>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
                    </asp:GridView>
                </td>
            </tr>           
            <tr>
                <td class="style4">
                    &nbsp;Contract Title :
                </td>
                <td colspan="2">
                    <asp:TextBox ID="uoTextBoxContractTitle" runat="server" Width="447px" ReadOnly="true"
                        CssClass="ReadOnly" />
                    <%--&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="uoTextBoxContractTitle"
                             ErrorMessage="Contract title required." ValidationGroup="Save">*</asp:RequiredFieldValidator>--%>
                </td>
            </tr>
            <tr>
                <td class="style4">
                    &nbsp;Effective Date :
                </td>
                <td class="style2">
                    <asp:TextBox ID="uoTextBoxContractStartDate" runat="server" ReadOnly="true" CssClass="ReadOnly"></asp:TextBox>
                    <cc1:TextBoxWatermarkExtender ID="uoTextBoxContractStartDate_TextBoxWatermarkExtender"
                        runat="server" Enabled="True" TargetControlID="uoTextBoxContractStartDate" WatermarkCssClass="fieldWatermark"
                        WatermarkText="MM/dd/yyyy">
                    </cc1:TextBoxWatermarkExtender>
                    <%--<asp:ImageButton ID="ImageButton1" runat="server" 
                                     ImageUrl="~/Images/Calendar_scheduleHS.png" />
                                        <cc1:calendarextender ID="uoTextBoxContractStartDate_CalendarExtender" runat="server" 
                                         Enabled="True" 
                        TargetControlID="uoTextBoxContractStartDate" PopupButtonID="ImageButton1" 
                        Format="MM/dd/yyyy">
                                        </cc1:calendarextender>--%>
                    <cc1:MaskedEditExtender ID="uoTextBoxContractStartDate_MaskedEditExtender" runat="server"
                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                        CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                        CultureTimePlaceholder="" Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="uoTextBoxContractStartDate"
                        UserDateFormat="MonthDayYear">
                    </cc1:MaskedEditExtender>
                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" 
                                                    ControlToValidate="uoTextBoxContractStartDate" 
                                                    ErrorMessage="Contract start date required.">*</asp:RequiredFieldValidator>--%>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Expiration Date : &nbsp;&nbsp;&nbsp;
                    <asp:TextBox ID="uoTextBoxContractEndDate" runat="server" ReadOnly="true" CssClass="ReadOnly"></asp:TextBox>
                    <cc1:TextBoxWatermarkExtender ID="uoTextBoxContractEndDate_TextBoxWatermarkExtender"
                        runat="server" Enabled="True" TargetControlID="uoTextBoxContractEndDate" WatermarkCssClass="fieldWatermark"
                        WatermarkText="MM/dd/yyyy">
                    </cc1:TextBoxWatermarkExtender>
                    <%--<asp:ImageButton ID="ImageButton2" runat="server" &nbsp;&nbsp;
                                     ImageUrl="~/Images/Calendar_scheduleHS.png" />
                                        <cc1:calendarextender ID="uoTextBoxContractEndDate_CalendarExtender" runat="server" 
                                         Enabled="True" TargetControlID="uoTextBoxContractEndDate" 
                        PopupButtonID="ImageButton2" Format="MM/dd/yyyy">
                                        </cc1:calendarextender>--%>
                    <cc1:MaskedEditExtender ID="uoTextBoxContractEndDate_MaskedEditExtender" runat="server"
                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                        CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                        CultureTimePlaceholder="" Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="uoTextBoxContractEndDate"
                        UserDateFormat="MonthDayYear">
                    </cc1:MaskedEditExtender>
                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                                                    ControlToValidate="uoTextBoxContractEndDate" 
                                                    ErrorMessage="Contract end date required.">*</asp:RequiredFieldValidator>--%>
                </td>
            </tr>
             <%--<tr>
                <td style="width: 20%">
                    &nbsp;Vendor Code:
                </td>
                <td colspan="3">
                    <asp:TextBox ID="uoTextBoxVendorCode" runat="server" Width="447px" CssClass="TextBoxInputReadOnly"
                        ReadOnly="True" />
                    &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="uoTextBoxVendorCode"
                        ErrorMessage="Vendor code required." ValidationGroup="Save">*</asp:RequiredFieldValidator>
                </td>
            </tr>--%>
            <tr>
                <td class="style4">
                    &nbsp;Vendor Personnel :
                </td>
                <td class="style2" colspan="3">
                    <asp:TextBox ID="uotextboxVendorRep" runat="server" Width="150px" ReadOnly="True"
                        CssClass="TextBoxInputReadOnly" />
               
                   &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;RCCL Personnel :
               &nbsp;&nbsp;&nbsp;
                    <asp:TextBox ID="uotextboxRCCLRep" Width="150px"  runat="server" ReadOnly="True" CssClass="TextBoxInputReadOnly" />
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;Tel. No. :
                </td>
                <td  colspan="3">
                    <asp:TextBox ID="uoTextBoxTelNo" runat="server" Width="150px" ReadOnly="True" CssClass="TextBoxInputReadOnly" />
              
                   &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Fax No&nbsp;&nbsp;:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
               
                    <asp:TextBox ID="uoTextBoxFaxNumber" runat="server" Width="150px" ReadOnly="true"
                        CssClass="TextBoxInputReadOnly" />
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;Website :
                </td>
                <td>
                    <asp:TextBox ID="uoTextBoxWebsite" runat="server" Width="250px" ReadOnly="true" CssClass="TextBoxInputReadOnly" />
                </td>
                <td>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;E-mail To:
                </td>
                <td>
                    <asp:TextBox ID="uoTextBoxEmailTo" runat="server" TextMode="MultiLine" Width="250px"
                        ReadOnly="True" CssClass="TextBoxInputReadOnly" />
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;E-mail Cc:
                </td>
                <td>
                    <asp:TextBox ID="uoTextBoxEmailCc" runat="server" TextMode="MultiLine" Width="250px"
                        ReadOnly="True" CssClass="TextBoxInputReadOnly" />
                </td>
            </tr>
         
            <tr>
                <td colspan="4">
                </td>
            </tr>
            <tr>
                <td class="style4">
                    &nbsp;Remarks :
                </td>
                <td colspan="3">
                    <asp:TextBox ID="uoTextBoxRemarks" runat="server" Width="447px" TextMode="MultiLine"
                        ReadOnly="true" CssClass="TextBoxInputReadOnly" />
                </td>
            </tr>
            <tr>
                <td class="style6">
                    <asp:HiddenField ID="uoHiddenFieldContractId" runat="server" />
                </td>
                <td class="style6">
                    <asp:HiddenField ID="uoHiddenFieldBranchId" runat="server" />
                     <asp:HiddenField ID="uoHiddenFieldAttachmentBytes" runat="server" />
                      <asp:HiddenField ID="uoHiddenFieldAttachmentFile" runat="server" />
                </td>
            </tr>
        </table>
        <div class="PageTitle">
            <asp:Panel ID="uopanelvehiclehead" runat="server">
                Services
                <asp:Label ID="uolabelVehicle" runat="server" Text="" Font-Size="Small" />
            </asp:Panel>
        </div>
        <asp:Panel ID="uopanelvehicle" runat="server" CssClass="CollapsiblePanel">
            <table width="100%">
                <tr>
                    <td align="left">
                        <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="Vehicle" />
                    </td>
                </tr>
                <tr>
                    <td valign="top" align="left">
                        <asp:GridView ID="uoGridViewVehicle" runat="server" Width="100%" AllowPaging="true"
                            AutoGenerateColumns="False" EmptyDataText="No Data" CssClass="listViewTable"
                            OnPageIndexChanging="uoGridViewVehicle_PageIndexChanging">
                            <Columns>
                                <asp:BoundField DataField="ContractDetailID" HeaderText="Contract Detail ID" Visible="false" />
                                <asp:BoundField DataField="ServiceTypeID" HeaderText="Service ID" Visible="false" />
                                <asp:BoundField DataField="ServiceType" HeaderText="Type" />
                                <asp:BoundField DataField="RateAmount" HeaderText="Rate" DataFormatString="{0:#,##0.00}" />
                                <asp:BoundField DataField="Tax" HeaderText="Tax" DataFormatString="{0:#,##0.00}" />
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <cc1:CollapsiblePanelExtender ID="uocollapsibleVehicle" runat="server" TargetControlID="uopanelvehicle"
            ExpandControlID="uopanelvehiclehead" CollapseControlID="uopanelvehiclehead" TextLabelID="uolabelVehicle"
            CollapsedText="(Show List)" ExpandedText="(Hide List)" Collapsed="false" SuppressPostBack="true">
        </cc1:CollapsiblePanelExtender>
        <br />
    </div>
</asp:Content>

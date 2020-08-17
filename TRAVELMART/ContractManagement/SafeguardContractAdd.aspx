<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/TravelMartMaintenanceMaster.Master"
    CodeBehind="SafeguardContractAdd.aspx.cs" Inherits="TRAVELMART.ContractManagement.SafeguardContractAdd" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style24
        {
            width: 212px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="NaviPlaceHolder" runat="server">

    <script type="text/javascript" language="javascript">
        function validate(key) {

            //getting key code of pressed key
            var keycode = (key.which) ? key.which : key.keyCode;
            if ((keycode < 48 || keycode > 57) && (keycode < 45 || keycode > 46)) {
                return false;
            }
            else {
                return true;
            }
            return true;
        }
        function confirmDelete() {
            if (confirm("Remove record?") == true)
                return true;
            else
                return false;
        }


////        function pageLoad(sender, args) {
////            var ina = $("#<%=uoDropDownListServiceTypeDuration.ClientID %>");
////            alert($("option:selected", ina).text());
////        }

////        $("#<%=uoDropDownListServiceTypeDuration.ClientID %>").click(function() {
////        $("#<%=uoDropDownListServiceTypeDuration.ClientID %>").attr("selectedIndex", this.selectedIndex);
////            alert("test");
////        });

//        function OnSelectedIndexChange() {
//            var ina = $("#ctl00_NaviPlaceHolder_DropDownList1");
//            var inw = $("OPTION:selected", ina).val()
//            alert("index changed test " + inw);
//            //alert('Selected Index : ' + $(ina).prop("selectedIndex"));
//        }     
       
    </script>

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
                <td class="contentCaptionOrig">
                    Contract Title :
                </td>
                <td colspan="3">
                    <asp:TextBox ID="uoTextBoxContractTitle" runat="server" Width="447px" />
                    &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="uoTextBoxContractTitle"
                        ErrorMessage="Contract title required." ValidationGroup="Header">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="contentCaptionOrig">
                    Remarks :
                </td>
                <td colspan="3">
                    <asp:TextBox ID="uoTextBoxRemarks" runat="server" Width="447px" TextMode="MultiLine"
                        CssClass="TextBoxInput" />
                </td>
            </tr>
            <tr>
                <td class="contentCaptionOrig">
                    Effective Date :
                </td>
                <td class="contentValueOrig">
                    <asp:TextBox ID="uoTextBoxContractStartDate" runat="server" Width="125px"></asp:TextBox>
                    <cc1:TextBoxWatermarkExtender ID="uoTextBoxContractStartDate_TextBoxWatermarkExtender"
                        runat="server" Enabled="True" TargetControlID="uoTextBoxContractStartDate" WatermarkCssClass="fieldWatermark"
                        WatermarkText="MM/dd/yyyy">
                    </cc1:TextBoxWatermarkExtender>
                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/Calendar_scheduleHS.png" />
                    <cc1:CalendarExtender ID="uoTextBoxContractStartDate_CalendarExtender" runat="server"
                        Enabled="True" TargetControlID="uoTextBoxContractStartDate" PopupButtonID="ImageButton1"
                        Format="MM/dd/yyyy">
                    </cc1:CalendarExtender>
                    <cc1:MaskedEditExtender ID="uoTextBoxContractStartDate_MaskedEditExtender" runat="server"
                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                        CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                        CultureTimePlaceholder="" Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="uoTextBoxContractStartDate"
                        UserDateFormat="MonthDayYear">
                    </cc1:MaskedEditExtender>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ValidationGroup="Header"
                        ControlToValidate="uoTextBoxContractStartDate" ErrorMessage="Contract start date required.">*</asp:RequiredFieldValidator>
                </td>
                <td class="contentCaptionOrig">
                    Expiration Date :
                </td>
                <td class="contentValueOrig">
                    <asp:TextBox ID="uoTextBoxContractEndDate" runat="server" Width="125px"></asp:TextBox>
                    <cc1:TextBoxWatermarkExtender ID="uoTextBoxContractEndDate_TextBoxWatermarkExtender"
                        runat="server" Enabled="True" TargetControlID="uoTextBoxContractEndDate" WatermarkCssClass="fieldWatermark"
                        WatermarkText="MM/dd/yyyy">
                    </cc1:TextBoxWatermarkExtender>
                    <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Images/Calendar_scheduleHS.png" />
                    <cc1:CalendarExtender ID="uoTextBoxContractEndDate_CalendarExtender" runat="server"
                        Enabled="True" TargetControlID="uoTextBoxContractEndDate" PopupButtonID="ImageButton2"
                        Format="MM/dd/yyyy">
                    </cc1:CalendarExtender>
                    <cc1:MaskedEditExtender ID="uoTextBoxContractEndDate_MaskedEditExtender" runat="server"
                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                        CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                        CultureTimePlaceholder="" Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="uoTextBoxContractEndDate"
                        UserDateFormat="MonthDayYear">
                    </cc1:MaskedEditExtender>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="uoTextBoxContractEndDate"
                        ValidationGroup="Header" ErrorMessage="Contract end date required.">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="contentCaptionOrig">
                    Vendor Contract Date Accepted :
                </td>
                <td class="contentValueOrig">
                    <asp:TextBox ID="uoTextBoxVendorDateAccepted" runat="server" Width="125px"></asp:TextBox>
                    <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" Enabled="True"
                        TargetControlID="uoTextBoxVendorDateAccepted" WatermarkCssClass="fieldWatermark"
                        WatermarkText="MM/dd/yyyy">
                    </cc1:TextBoxWatermarkExtender>
                    <cc1:MaskedEditExtender ID="uoTextBoxvendorDateAccepted0_MaskedEditExtender" runat="server"
                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                        CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                        CultureTimePlaceholder="" Enabled="True" MaskType="Date" UserDateFormat="MonthDayYear"
                        Mask="99/99/9999" TargetControlID="uoTextBoxVendorDateAccepted">
                    </cc1:MaskedEditExtender>
                    <cc1:CalendarExtender ID="uoTextBoxvendorDateAccepted0_CalendarExtender" runat="server"
                        Enabled="True" TargetControlID="uoTextBoxVendorDateAccepted" PopupButtonID="Image6"
                        Format="MM/dd/yyyy">
                    </cc1:CalendarExtender>
                    <asp:Image ID="Image6" runat="server" ImageUrl="~/Images/Calendar_scheduleHS.png"
                        Height="16px" />
                </td>
                <td class="contentCaptionOrig">
                    RCCL Contract Date Accepted :
                </td>
                <td class="contentValueOrig">
                    <asp:TextBox ID="uoTextBoxRCCLDateAccepted" runat="server" Width="125px"></asp:TextBox>
                    <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" Enabled="True"
                        TargetControlID="uoTextBoxRCCLDateAccepted" WatermarkCssClass="fieldWatermark"
                        WatermarkText="MM/dd/yyyy">
                    </cc1:TextBoxWatermarkExtender>
                    <cc1:MaskedEditExtender ID="uoTextBoxRCCLDateAccepted_MaskedEditExtender" runat="server"
                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                        CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                        CultureTimePlaceholder="" Enabled="True" MaskType="Date" UserDateFormat="MonthDayYear"
                        Mask="99/99/9999" TargetControlID="uoTextBoxRCCLDateAccepted">
                    </cc1:MaskedEditExtender>
                    <cc1:CalendarExtender ID="uoTextBoxRCCLDateAccepted_CalendarExtender" runat="server"
                        Enabled="True" TargetControlID="uoTextBoxRCCLDateAccepted" PopupButtonID="Image5"
                        Format="MM/dd/yyyy">
                    </cc1:CalendarExtender>
                    <asp:Image ID="Image5" runat="server" ImageUrl="~/Images/Calendar_scheduleHS.png"
                        Height="16px" />
                </td>
            </tr>
            <tr>
                <td class="contentCaptionOrig">
                    Vendor Personnel :
                </td>
                <td class="contentValueOrig">
                    <asp:TextBox ID="uotextboxVendorRep" runat="server" Width="125px" />
                </td>
                <td class="contentCaptionOrig">
                    RCCL Personnel :
                </td>
                <td class="contentValueOrig">
                    <asp:TextBox ID="uotextboxRCCLRep" runat="server" Width="125px" />
                </td>
            </tr>
            <tr>
                <td class="contentCaptionOrig">
                    Currency:
                </td>
                <td class="contentValueOrig">
                    <asp:DropDownList ID="uoDropDownListCurrency" runat="server" Width="300px" AppendDataBoundItems="true">
                    </asp:DropDownList>
                </td>
                <td class="contentCaptionOrig">
                </td>
                <td class="contentValueOrig">
                </td>
            </tr>
            <tr>
                <td colspan="4">
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <asp:HiddenField ID="uoHiddenFieldVendorId" runat="server" />
                </td>
            </tr>
        </table>
        <div class="PageTitle">
            <asp:Panel ID="Panel1" runat="server">
                Safeguard Type and Duration
            </asp:Panel>
        </div>
        <table width="100%">
            <tr>
                <td class="contentCaptionOrig">
                    Service Type
                </td>
                <td class="contentValueOrig" colspan="3">
                    <asp:DropDownList ID="uoDropDownListServiceType" runat="server" Width="300px" AppendDataBoundItems="true">
                    </asp:DropDownList>
                    &nbsp;
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ValidationGroup="SafeguardCapacity"
                        ControlToValidate="uoDropDownListServiceType" InitialValue="0" ErrorMessage="Max Count Required">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="contentCaptionOrig">
                    Service From
                </td>
                <td class="contentValueOrig" colspan="3">
                    <asp:TextBox ID="uoTextBoxMin" runat="server" Width="100px" CssClass="SmallText"
                        onkeypress="return validate(event);"></asp:TextBox>
                    &nbsp;
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ValidationGroup="SafeguardCapacity"
                        ControlToValidate="uoTextBoxMin" ErrorMessage="Min Count Required">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="contentCaptionOrig">
                    Service To
                </td>
                <td class="contentValueOrig">
                    <asp:TextBox ID="uoTextBoxMax" runat="server" Width="100px" CssClass="SmallText"
                        onkeypress="return validate(event);"></asp:TextBox>
                    &nbsp;
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ValidationGroup="SafeguardCapacity"
                        ControlToValidate="uoTextBoxMax" ErrorMessage="Max Count Required">*</asp:RequiredFieldValidator>
                    &nbsp;
                </td>
                <td class="contentCaptionOrig">
                </td>
                <td class="contentValueOrig">
                </td>
            </tr>
            <tr>
                <td class="contentCaptionOrig">
                </td>
                <td class="contentValueOrig" colspan="3">
                    <asp:Button ID="uoButtonServiceTypeDurationAdd" runat="server" Text="Add" CssClass="SmallButton"
                        Width="50px" ValidationGroup="SafeguardCapacity" OnClick="uoButtonServiceTypeDurationAdd_Click" />
                </td>
            </tr>
            <tr>
                <td class="contentCaptionOrig">
                </td>
                <td class="contentValueOrig" valign="top" style="vertical-align: top">
                    <asp:ListView runat="server" ID="uoListViewServiceTypeDuration" OnItemDeleting="uoListViewServiceTypeDuration_ItemDeleting">
                        <LayoutTemplate>
                            <table border="0" cellpadding="0" cellspacing="0" class="listViewTable" width="50%">
                                <tr>
                                    <th style="text-align: center; white-space: normal;">
                                        Service Type
                                    </th>
                                    <th style="text-align: center; white-space: normal;">
                                        Count From
                                    </th>
                                    <th style="text-align: center; white-space: normal;">
                                        Count To
                                    </th>
                                    <th runat="server" style="width: 5%" id="DeleteTH" />
                                </tr>
                                <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                            </table>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <tr>
                                <td class="leftAligned">
                                    <asp:HiddenField ID="uoHiddenFieldContractServiceDurationID" runat="server" Value='<%# Eval("ContractSafeguardDurationIDInt") %>' />
                                    <asp:HiddenField ID="uoHiddenFieldServiceTypeID" runat="server" Value='<%# Eval("ServiceTypeID") %>' />
                                    <asp:Label ID="uoLabelServiceTypeName" runat="server" Text='<%# Eval("ServiceType")%>'></asp:Label>
                                </td>
                                <td class="leftAligned">
                                    <asp:Label ID="uoLabelMinCapacity" runat="server" Text='<%# Eval("From")%>'></asp:Label>
                                </td>
                                <td class="leftAligned">
                                    <asp:Label ID="uoLabelMaxCapacity" runat="server" Text='<%# Eval("To")%>'></asp:Label>
                                </td>
                                <td style="width: 10%">
                                    <asp:LinkButton OnClientClick="return confirmDelete();" ID="uoLinkButtonDelete" runat="server"
                                        CommandName="Delete" CommandArgument='<%# Eval("ServiceTypeID") %>'>Delete</asp:LinkButton>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <EmptyDataTemplate>
                            <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                                <tr>
                                    <th style="text-align: center; white-space: normal;">
                                        Service Type
                                    </th>
                                    <th style="text-align: center; white-space: normal;">
                                        Count From
                                    </th>
                                    <th style="text-align: center; white-space: normal;">
                                        Count To
                                    </th>
                                </tr>
                                <tr>
                                    <td class="leftAligned" colspan="3">
                                        No Record
                                    </td>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
                    </asp:ListView>
                </td>
                <td class="contentCaptionOrig">
                </td>
                <td class="contentValueOrig" valign="top" style="vertical-align: top">
                </td>
            </tr>
        </table>
        <div class="PageTitle">
            Contract Details
            <%-- <asp:Panel ID="uopanelvehiclehead" runat="server">
                Transfer Service
                <asp:Label ID="uolabelVehicle" runat="server" Text="" Font-Size="Small" />
            </asp:Panel>--%>
        </div>
        <%--<asp:Panel ID="uopanelvehicle" runat="server" CssClass="CollapsiblePanel">--%>
        <table width="100%">
           <%-- <tr>
                <td align="left" colspan="4">
                    <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="Service" />
                </td>
            </tr>--%>
            <tr>
                <td class="contentCaptionOrig">
                    Service :
                </td>
                <td align="left" class="contentValueOrig">
                    <asp:DropDownList ID="uoDropDownListServiceTypeDuration" runat="server" 
                        Width="250px" AppendDataBoundItems="true">
                    </asp:DropDownList>
                 
                </td>
                <td colspan="2" style="text-align: left">
                   <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="uoDropDownListServiceTypeDuration"
                        ErrorMessage="Service type required." InitialValue="0" ValidationGroup="Service">*</asp:RequiredFieldValidator>
            
                </td>
            </tr>
            <tr>
                <td class="contentCaptionOrig">
                    Rate:
                </td>
                <td class="contentValueOrig">
                    <asp:TextBox ID="uoTextBoxVehicleRate" runat="server" Width="180px" />
                    <cc1:MaskedEditExtender ID="uoTextBoxVehicleRate_MaskedEditExtender" runat="server"
                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                        CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                        CultureTimePlaceholder="" Enabled="True" Mask="999,999,999.99" MaskType="Number"
                        TargetControlID="uoTextBoxVehicleRate" InputDirection="RightToLeft">
                    </cc1:MaskedEditExtender>
                </td>
                <td colspan="2" style="text-align: left">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="uoTextBoxVehicleRate"
                        ErrorMessage="Rate required." ValidationGroup="Service">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="contentCaptionOrig">
                    Tax:
                </td>
                <td class="contentValueOrig">
                    <asp:TextBox ID="uoTextBoxTax" runat="server" Width="180px"></asp:TextBox>
                    <cc1:MaskedEditExtender ID="uoTextBoxTax_MaskedEditExtender" runat="server" CultureAMPMPlaceholder=""
                        CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                        CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                        Enabled="True" Mask="999.99" MaskType="Number" TargetControlID="uoTextBoxTax">
                    </cc1:MaskedEditExtender>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td align="left">
                    <asp:Button ID="uoButtonAddService" runat="server" Text="Add" ValidationGroup="Service"
                        OnClick="uoButtonAddService_Click" CssClass="SmallButton"/>
                </td>
                <td>
                    <asp:HiddenField ID="uoHiddenFieldDetail" runat="server" Value="0" />
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td valign="top" align="left" colspan="2">
                    <asp:GridView ID="uoGridViewService" runat="server" Width="100%" AutoGenerateColumns="False"
                        CssClass="listViewTable" OnRowDeleting="uoGridViewService_RowDeleting" OnSelectedIndexChanging="uoGridViewService_SelectedIndexChanging">
                        <Columns>
                            <asp:BoundField DataField="ContractDetailID" HeaderText="Contract Detail ID" Visible="false" />
                            <asp:BoundField DataField="ServiceTypeID" HeaderText="Service ID" Visible="false" />
                            <asp:BoundField DataField="ServiceType" HeaderText="Type" />
                            <asp:BoundField DataField="RateAmount" HeaderText="Rate" DataFormatString="{0:#,##0.00}" />
                            <asp:BoundField DataField="Tax" HeaderText="Tax" DataFormatString="{0:#,##0.00}" />
                            <asp:ButtonField CommandName="Delete" Text="Delete" />
                            <asp:ButtonField CommandName="Select" Text="Edit" />
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </div>
   <asp:UpdatePanel ID="UpdatePanel11" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <div class="PageTitle">
                <asp:Panel ID="Panel3" runat="server">
                    Upload Contract</asp:Panel>
            </div>
            <table width="100%">
                <tr>
                    <td align="left">
                        <asp:ValidationSummary ID="ValidationSummaryUpload" runat="server" ValidationGroup="Upload" />
                    </td>
                </tr>
                <tr>
                    <td class="style24" align="left">
                        Contract Attachment (pdf/doc/docx) :
                    </td>
                    <td align="left">
                        <asp:FileUpload ID="uoFileUploadContract" runat="server" Width="500px" />
                        <asp:Label ID="Label1" runat="server" ForeColor="Red"></asp:Label><br />
                        <asp:RegularExpressionValidator ID="uoRegularExpressionValidatorUpload" runat="server"
                            ControlToValidate="uoFileUploadContract" ErrorMessage="Please select a .pdf or .doc or .docx file"
                            ValidationExpression="^([a-zA-Z].*|[1-9].*)\.(((p|P)(d|D)(f|F))|((d|D)(o|O)(c|C))|((d|D)(o|O)(c|C)(x|X)))$"
                            ValidationGroup="Upload">*</asp:RegularExpressionValidator>
                        <asp:LinkButton runat="server" ID="uoLinkAttach" Text="Attach" OnClick="uoButtonUpload_Click">
                        </asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td class="style24" align="left">
                    </td>
                    <td align="left">
                        <asp:ListView runat="server" ID="uoListViewAttachment" OnItemDeleting="uoListViewAttachment_ItemDeleting">
                            <LayoutTemplate>
                                <table border="0" cellpadding="0" cellspacing="0" class="listViewTable" width="200px">
                                    <tr>
                                        <th style="text-align: center; white-space: normal;">
                                            Filename
                                        </th>
                                        <th runat="server" style="width: 5%" id="DeleteTH" />
                                    </tr>
                                    <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                                </table>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td class="leftAligned">
                                        <%--<asp:HiddenField ID="uoHiddenFieldID" runat="server" Value='<%# Eval("AttachmentId") %>' />                                    
                                    <asp:Label ID="uoLabelVehicleTypeName" runat="server" Text='<%# Eval("FileName")%>'></asp:Label>--%>
                                        <asp:LinkButton runat="server" ID="uoLinkButtonFileName" Text='<%# Eval("FileName") %>'
                                            OnClick="ucLabelAttached_Click" CommandArgument='<%# Eval("FileName") %>'></asp:LinkButton>
                                    </td>
                                    <td style="width: 10%">
                                        <asp:LinkButton ID="uoLinkButtonDelete" runat="server" CommandName="Delete">Delete</asp:LinkButton>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <EmptyDataTemplate>
                                <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                                    <tr>
                                        <th style="text-align: center; white-space: normal;">
                                            Filename
                                        </th>
                                    </tr>
                                    <tr>
                                        <td class="leftAligned">
                                            No Record
                                        </td>
                                    </tr>
                                </table>
                            </EmptyDataTemplate>
                        </asp:ListView>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="style24" align="left">
                        <asp:HiddenField ID="uoHiddenFieldFileName" runat="server" />
                    </td>
                    <td align="left">
                        <asp:Label ID="ucLabelAttached" runat="server" ForeColor="Red" Visible="False"></asp:Label>
                        &nbsp;<asp:LinkButton ID="uoLinkButtonRemove" runat="server" Visible="False">Remove</asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td class="style24" align="left">
                        <asp:HiddenField ID="uoHiddenFieldFileType" runat="server" />
                        <asp:HiddenField ID="uoHiddenFieldContractID" runat="server" Value="0" />
                        <asp:HiddenField ID="uoHiddenFieldUserName" runat="server" Value="" />
                    </td>
                    <td align="left">
                        <%--<asp:HiddenField ID="uoHiddenFieldFileData" runat="server" />--%>
                        &nbsp;
                    </td>
                </tr>
            </table>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="uoLinkAttach" />
            <asp:PostBackTrigger ControlID="uoListViewAttachment" />
        </Triggers>
    </asp:UpdatePanel>
    <table width="100%">
        <tr>
            <td align="center">
                <hr />
                <asp:Button ID="uoButtonSave" runat="server" Text="Save" Width="80" OnClick="uoButtonSave_Click"
                    ValidationGroup="Header" />           
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="uoHiddenfieldService" runat="server" />
    
</asp:Content>

<%@ Page Language="C#" MasterPageFile="~/Popup.Master" AutoEventWireup="true"
        CodeBehind="MeetAndGreetContractView.aspx.cs" Inherits="TRAVELMART.ContractManagement.MeetAndGreetContractView" %>
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
            Vehicle Contract
        </div>
        <hr />
        <table width="100%" style="text-align: left">
            <tr>
                <td colspan="4">
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
                        ValidationGroup="Header" />
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    &nbsp;Contract Status : &nbsp;<asp:Label ID="ucLabelContractStatus" runat="server" 
                        Font-Size="14pt" ForeColor="Red" Font-Bold="true"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    &nbsp;Contract Attachment: &nbsp;<asp:LinkButton ID="LinkButton1" 
                        runat="server" onclick="LinkButton1_Click">View Attachment</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 20%">
                    &nbsp;Vendor Code:
                </td>
                <td colspan="3">
                    <asp:TextBox ID="uoTextBoxVendorCode" runat="server" Width="447px" CssClass="TextBoxInputReadOnly"
                        ReadOnly="True" />
                    &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="uoTextBoxVendorCode"
                        ErrorMessage="Vendor code required." ValidationGroup="Save">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="style4">
                   &nbsp;Vehicle Branch Name :
                </td>
                <td colspan="2">
                    <asp:TextBox ID="uoTextBoxVehicleName" runat="server" Width="447px" ReadOnly=true CssClass=ReadOnly></asp:TextBox>
                    <%--<asp:DropDownList ID="uoDropDownListVendorName" runat="server" Width="447px" AutoPostBack="True" 
                    OnSelectedIndexChanged="uoDropDownListVendorName_SelectedIndexChanged">
                    </asp:DropDownList>
                    &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="uoDropDownListVendorName"
                          ErrorMessage="Vendor required.">*
                          </asp:RequiredFieldValidator>--%>
                </td>
            </tr>
            <%--<tr>
                <td class="style4">
                    &nbsp;Country :</td>
                <td colspan="2">
                    <asp:TextBox ID="uoTextBoxCountry" runat="server" Width="447px" ReadOnly=true CssClass=ReadOnly></asp:TextBox>                    
                </td>
            </tr>--%>
            <%--<tr>
                <td class="style4">
                    &nbsp;City :</td>
                <td colspan="2">
                    <asp:TextBox ID="uoTextBoxCity" runat="server" Width="447px" ReadOnly=true CssClass=ReadOnly></asp:TextBox>                    
                </td>
            </tr>--%>
            <%--<tr>
                 <td class="style4">
                     &nbsp;Branch :</td>
                 <td colspan="2">
                    <asp:TextBox ID="uoTextBoxVehicleBranch" runat="server" Width="447px" ReadOnly=true CssClass=ReadOnly></asp:TextBox>
                 </td> 
            </tr> --%>
            <tr>
                <td colspan="4">
                </td>
            </tr>
            <tr>
                <td class="style4">
                    &nbsp;Contract Title :
                </td>
                <td colspan="2">
                    <asp:TextBox ID="uoTextBoxContractTitle" runat="server" Width="447px" ReadOnly=true CssClass=ReadOnly />
                             <%--&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="uoTextBoxContractTitle"
                             ErrorMessage="Contract title required." ValidationGroup="Save">*</asp:RequiredFieldValidator>--%>
                </td>
            </tr>
            <tr>
                <td class="style4">
                    &nbsp;Effective Date :
                </td>
                <td class="style2">
                    <asp:TextBox ID="uoTextBoxContractStartDate" runat="server" ReadOnly=true CssClass=ReadOnly></asp:TextBox>
                                <cc1:textboxwatermarkextender ID="uoTextBoxContractStartDate_TextBoxWatermarkExtender" 
                                 runat="server" Enabled="True" 
                        TargetControlID="uoTextBoxContractStartDate" WatermarkCssClass="fieldWatermark" 
                        WatermarkText="MM/dd/yyyy">
                                </cc1:textboxwatermarkextender>
                                    <%--<asp:ImageButton ID="ImageButton1" runat="server" 
                                     ImageUrl="~/Images/Calendar_scheduleHS.png" />
                                        <cc1:calendarextender ID="uoTextBoxContractStartDate_CalendarExtender" runat="server" 
                                         Enabled="True" 
                        TargetControlID="uoTextBoxContractStartDate" PopupButtonID="ImageButton1" 
                        Format="MM/dd/yyyy">
                                        </cc1:calendarextender>--%>
                                            <cc1:maskededitextender ID="uoTextBoxContractStartDate_MaskedEditExtender" 
                                             runat="server" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                                             CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                             CultureThousandsPlaceholder="" 
                        CultureTimePlaceholder="" Enabled="True" 
                                             Mask="99/99/9999" MaskType="Date" 
                        TargetControlID="uoTextBoxContractStartDate" UserDateFormat="MonthDayYear">
                                            </cc1:maskededitextender>
                                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" 
                                                    ControlToValidate="uoTextBoxContractStartDate" 
                                                    ErrorMessage="Contract start date required.">*</asp:RequiredFieldValidator>--%>                         
                </td>
                <td class="style4">
                    &nbsp;Expiration Date :
                </td>
                <td>
                    <asp:TextBox ID="uoTextBoxContractEndDate" runat="server" ReadOnly=true CssClass=ReadOnly></asp:TextBox>
                                <cc1:textboxwatermarkextender ID="uoTextBoxContractEndDate_TextBoxWatermarkExtender" 
                                 runat="server" Enabled="True" 
                        TargetControlID="uoTextBoxContractEndDate" WatermarkCssClass="fieldWatermark" 
                        WatermarkText="MM/dd/yyyy">
                                </cc1:textboxwatermarkextender>
                                    <%--<asp:ImageButton ID="ImageButton2" runat="server" 
                                     ImageUrl="~/Images/Calendar_scheduleHS.png" />
                                        <cc1:calendarextender ID="uoTextBoxContractEndDate_CalendarExtender" runat="server" 
                                         Enabled="True" TargetControlID="uoTextBoxContractEndDate" 
                        PopupButtonID="ImageButton2" Format="MM/dd/yyyy">
                                        </cc1:calendarextender>--%>
                                            <cc1:maskededitextender ID="uoTextBoxContractEndDate_MaskedEditExtender" 
                                             runat="server" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                                             CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                             CultureThousandsPlaceholder="" 
                        CultureTimePlaceholder="" Enabled="True" 
                                             Mask="99/99/9999" MaskType="Date" 
                        TargetControlID="uoTextBoxContractEndDate" UserDateFormat="MonthDayYear">
                                            </cc1:maskededitextender>   
                                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                                                    ControlToValidate="uoTextBoxContractEndDate" 
                                                    ErrorMessage="Contract end date required.">*</asp:RequiredFieldValidator>--%>                           
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
                    <asp:TextBox ID="uoTextBoxRemarks" runat="server" Width="447px" TextMode="MultiLine" ReadOnly=true CssClass=TextBoxInputReadOnly />
                </td>
            </tr>            
            <tr>
                <td class="style6">
                    <asp:HiddenField ID="uoHiddenFieldContractId" runat="server" />
                </td>   
                <td class="style6">
                    <asp:HiddenField ID="uoHiddenFieldBranchId" runat="server" />
                </td>              
            </tr>            
        </table>
        <div class="PageTitle">
            <asp:Panel ID="uopanelvehiclehead" runat="server">
                Transfer Service
                <asp:Label ID="uolabelVehicle" runat="server" Text="" Font-Size="Small" />
            </asp:Panel>
        </div>
        <asp:Panel ID="uopanelvehicle" runat="server" CssClass="CollapsiblePanel">
            <table width="100%">
                <tr>
                    <td align=left>
                        <asp:ValidationSummary ID="ValidationSummary2" runat="server" 
                            ValidationGroup="Vehicle" />
                    </td>
                </tr>               
                <tr>
                    <td valign="top" align=left>
                        <asp:GridView ID="uoGridViewVehicle" runat="server" Width="100%" AllowPaging=true 
                        AutoGenerateColumns="False" EmptyDataText="No Data" CssClass=listViewTable OnPageIndexChanging="uoGridViewVehicle_PageIndexChanging" >
                            <Columns>                                
                                <asp:BoundField DataField="colVehicleNameVarchar" HeaderText="Type"/>
                                <asp:BoundField DataField="colVehicleCapacityInt" HeaderText="Capacity"/>
                                <asp:BoundField DataField="colOriginVarchar" HeaderText="Origin"/>
                                <asp:BoundField DataField="colDestinationVarchar" HeaderText="Destination"/>
                                <%--<asp:BoundField DataField="colStartDate" HeaderText="Start Date"/>--%>
                                <%--<asp:BoundField DataField="colEndDate" HeaderText="End Date"/>--%>
                                <asp:BoundField DataField="colCurrencyCodeVarchar" HeaderText="Currency"/>
                                <asp:BoundField DataField="colRentalRatePerDayMoney" HeaderText="Rate" DataFormatString="{0:#,##0.00}"/>                               
                                <%--<asp:ButtonField CommandName="Select" Text="Delete" />--%>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>               
            </table>
        </asp:Panel>
        <cc1:collapsiblepanelextender ID="uocollapsibleVehicle" runat="server" TargetControlID="uopanelvehicle"
            ExpandControlID="uopanelvehiclehead" CollapseControlID="uopanelvehiclehead" TextLabelID="uolabelVehicle"
            CollapsedText="(Show List)" ExpandedText="(Hide List)" Collapsed="false" 
            SuppressPostBack="true">
        </cc1:collapsiblepanelextender>
        <br />
        <%--</div>--%>
        
        <div class="PageTitle">
            <asp:Panel ID="uopanelluggagevanhead" runat="server">
                Luggage Van
                <asp:Label ID="uolabelLuggageVan" runat="server" Text="" Font-Size="Small" />
            </asp:Panel>
        </div>
        <asp:Panel ID="uopanelluggagevan" runat="server" CssClass="CollapsiblePanel">
            <table width="100%">
                <tr>
                    <td align=left>
                        <asp:ValidationSummary ID="ValidationSummary3" runat="server" 
                            ValidationGroup="LuggageVan" />
                    </td>
                </tr>                
                <tr>
                    <td valign="top" align=left>
                        <asp:GridView ID="uoGridViewLuggageVan" runat="server" Width="100%" AllowPaging=true 
                        AutoGenerateColumns="False" EmptyDataText="No Data" CssClass=listViewTable OnPageIndexChanging="uoGridViewLuggageVan_PageIndexChanging" >
                            <Columns>                                
                                <asp:BoundField DataField="colLVVehicleNameVarchar" HeaderText="Type"/>
                                <asp:BoundField DataField="colLVBagstinyint" HeaderText="Bags"/>
                                <asp:BoundField DataField="colLVOriginVarchar" HeaderText="Origin"/>
                                <asp:BoundField DataField="colLVDestinationVarchar" HeaderText="Destination"/>                                
                                <asp:BoundField DataField="colLVCurrencyCodeVarchar" HeaderText="Currency"/>
                                <asp:BoundField DataField="colLVRateMoney" HeaderText="Rate" DataFormatString="{0:#,##0.00}"/>                               
                                <%--<asp:ButtonField CommandName="Select" Text="Delete" />--%>
                            </Columns>                            
                        </asp:GridView>
                    </td>
                </tr>                
            </table>
        </asp:Panel>
        <cc1:collapsiblepanelextender ID="uocollapsibleLuggageVan" runat="server" TargetControlID="uopanelluggagevan"
            ExpandControlID="uopanelluggagevanhead" CollapseControlID="uopanelluggagevanhead" TextLabelID="uolabelLuggageVan"
            CollapsedText="(Show List)" ExpandedText="(Hide List)" Collapsed="false" 
            SuppressPostBack="true">
        </cc1:collapsiblepanelextender>
        <br />
        
        <div class="PageTitle">
            <asp:Panel ID="uopanelserviceratehead" runat="server">
                Service Rate
                <asp:Label ID="uolabelServiceRate" runat="server" Text="" Font-Size="Small" />
            </asp:Panel>
        </div>
        <asp:Panel ID="uopanelservicerate" runat="server" CssClass="CollapsiblePanel">
            <table width="100%">
                <tr>
                    <td align=left>
                        <asp:ValidationSummary ID="ValidationSummary4" runat="server" 
                            ValidationGroup="ServiceRate" />
                    </td>
                </tr>                
                <tr>
                    <td valign="top" align=left>
                        <asp:GridView ID="uoGridViewServiceRate" runat="server" Width="100%" AllowPaging=true 
                        AutoGenerateColumns="False" EmptyDataText="No Data" CssClass=listViewTable OnPageIndexChanging="uoGridViewServiceRate_PageIndexChanging" >
                            <Columns>                                
                                <asp:BoundField DataField="colSRSeamansVisaCurrencyCodeVarchar" HeaderText="Seaman’s Visa (Currency)"/>
                                <asp:BoundField DataField="colSRSeamansVisaRateMoney" HeaderText="Seaman’s Visa (Rate)" DataFormatString="{0:#,##0.00}"/>
                                <asp:BoundField DataField="colSRBaggageTraceCurrencyCodeVarchar" HeaderText="Airline lost baggage tracing (Currency)"/>
                                <asp:BoundField DataField="colSRBaggageTraceRateMoney" HeaderText="Airline lost baggage tracing (Rate)" DataFormatString="{0:#,##0.00}"/>                                
                                <asp:BoundField DataField="colSRAgencyFeesCurrencyCodeVarchar" HeaderText="Agency fees (Currency)"/>
                                <asp:BoundField DataField="colSRAgencyFeesRateMoney" HeaderText="Agency fees (Rate)" DataFormatString="{0:#,##0.00}"/>
                                <asp:BoundField DataField="colSROkToBoardCurrencyCodeVarchar" HeaderText="Airline okay to board ea sign on crew (Currency)"/>
                                <asp:BoundField DataField="colSROkToBoardRateMoney" HeaderText="Airline okay to board ea sign on crew (Rate)" DataFormatString="{0:#,##0.00}"/>                                
                                <%--<asp:ButtonField CommandName="Select" Text="Delete" />--%>
                            </Columns>                            
                        </asp:GridView>
                    </td>
                </tr>                
            </table>
        </asp:Panel>
        <cc1:collapsiblepanelextender ID="uocollapsibleServiceRate" runat="server" TargetControlID="uopanelservicerate"
            ExpandControlID="uopanelserviceratehead" CollapseControlID="uopanelserviceratehead" TextLabelID="uolabelServiceRate"
            CollapsedText="(Show List)" ExpandedText="(Hide List)" Collapsed="false" 
            SuppressPostBack="true">
        </cc1:collapsiblepanelextender>
        <br />
        </div>       
</asp:Content>
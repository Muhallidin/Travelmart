<%@ Page Title="" Language="C#" MasterPageFile="~/TravelMartMaster.Master" AutoEventWireup="true" CodeBehind="HotelContractAdd_old.aspx.cs" 
Inherits="TRAVELMART.ContractManagement.HotelContractAdd_old" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    
    <style type="text/css">
        .style9
        {
            width: 164px;
        }
        .style10
        {
            width: 19px;
        }
        .style11
        {
            width: 142px;
            height: 26px;
        }
        .style12
        {
            width: 164px;
            height: 26px;
        }
        .style13
        {
            width: 19px;
            height: 26px;
        }
        .style14
        {
            height: 26px;
        }
        .style15
        {
            width: 148px;
        }
        .style17
        {
            width: 29px;
        }
        .style18
        {
            width: 119px;
        }
        .style19
        {
            width: 150px;
        }
        .style20
        {
            width: 181px;
        }
        .style22
        {
        }
        .style23
        {
            width: 220px;
        }
        .style24
        {
            width: 212px;
        }
        .style25
        {
            width: 148px;
            height: 24px;
        }
        .style26
        {
            width: 119px;
            height: 24px;
        }
        .style27
        {
            width: 29px;
            height: 24px;
        }
        .style28
        {
            height: 24px;
        }
        .style29
        {
            width: 214px;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="leftAligned">
        <div class="PageTitle">
            Hotel Contract
        </div>
        <hr />
        <table width="120%" style="text-align: left">
            <tr>
                <td colspan="4">
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
                        ValidationGroup="Header" />
                </td>
            </tr>            
                <td class="style29">
                    Contract Title :
                </td>
                <td colspan="3">
                    <asp:TextBox ID="uotextboxContractTitle" runat="server" Width="535px" />
                    &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="uotextboxContractTitle"
                        ErrorMessage="Contract title required." ValidationGroup="Header">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="style29">
                    Remarks :
                </td>
                <td colspan="3">
                    <asp:TextBox ID="uotextboxRemarks" runat="server" Width="535px" 
                        TextMode="MultiLine" CssClass=TextBoxInput />
                </td>
            </tr>

<%--            <tr>
                <td class="style22">
                    Contract Attachment :</td>
                <td colspan="3">
                    <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                   <ContentTemplate>
                    <asp:FileUpload ID="uoFileUploadContract" runat="server" Width="70%" />
                                        <asp:Button ID="uoButtonUpload" runat="server" onclick="uoButtonUpload_Click" 
                        Text="Upload" />
                    <asp:Label ID="Label1" runat="server" ForeColor="Red"></asp:Label>
                    </ContentTemplate>
                    <Triggers>
                    <asp:PostBackTrigger ControlID="uoButtonUpload" />
                    </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>--%>
<%--            <tr>
                <td class="style22">
                    <asp:HiddenField ID="uoHiddenFieldFileName" runat="server" />
                </td>
                <td colspan="3">
                    <asp:Button ID="uoButtonUpload" runat="server" onclick="uoButtonUpload_Click" 
                        Text="Upload" />
                    <asp:Label ID="Label1" runat="server" ForeColor="Red"></asp:Label>
                    <asp:Label ID="ucLabelAttached" runat="server" ForeColor="Red" Visible="False"></asp:Label>
&nbsp;<asp:LinkButton ID="uoLinkButtonRemove" runat="server" onclick="uoLinkButtonRemove_Click" 
                        Visible="False">Remove</asp:LinkButton>
                </td>
            </tr>--%>

<%--            <tr>
                <td class="style22">
                    <asp:HiddenField ID="uoHiddenFieldFileType" runat="server" />
                </td>
                <td colspan="3">
                    <asp:HiddenField ID="uoHiddenFieldFileData" runat="server" />
                </td>
            </tr>--%>

            <tr>
                <td class="style29">
                    Contract Start Date :
                </td>
                <td class="style20">
                    <asp:TextBox ID="uotextboxStartDate" runat="server" Width="125px" />
                <cc1:TextBoxWatermarkExtender ID="uotextboxStartDate_TextBoxWatermarkExtender" runat="server"
                    Enabled="True" TargetControlID="uotextboxStartDate" WatermarkCssClass="fieldWatermark"
                    WatermarkText="MM/dd/yyyy">
                </cc1:TextBoxWatermarkExtender>
                    <cc1:MaskedEditExtender ID="uotextboxStartDate_MaskedEditExtender" 
                        runat="server" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                        CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                        CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                        TargetControlID="uotextboxStartDate" Mask="99/99/9999" UserDateFormat="MonthDayYear" MaskType="Date">
                    </cc1:MaskedEditExtender>
                    <cc1:CalendarExtender ID="uotextboxStartDate_CalendarExtender" runat="server" 
                        Enabled="True" PopupButtonID="Image1" TargetControlID="uotextboxStartDate"  Format="MM/dd/yyyy">
                    </cc1:CalendarExtender>
                    <asp:Image ID="Image1" runat="server" 
                        ImageUrl="~/Images/Calendar_scheduleHS.png" Height="16px" />      
                        
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="uotextboxStartDate"
                        ErrorMessage="Contract start date required." ValidationGroup="Header">*</asp:RequiredFieldValidator>
                        
                </td>
                <td class="style23">
                    Contract End Date :
                </td>
                <td>
                    <asp:TextBox ID="uotextboxEndDate" runat="server" Width="125px" />
                <cc1:TextBoxWatermarkExtender ID="uotextboxEndDate_TextBoxWatermarkExtender1" runat="server"
                    Enabled="True" TargetControlID="uotextboxEndDate" WatermarkCssClass="fieldWatermark"
                    WatermarkText="MM/dd/yyyy">
                </cc1:TextBoxWatermarkExtender>
                    <cc1:MaskedEditExtender ID="uotextboxEndDate_MaskedEditExtender" runat="server" 
                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                        CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                        CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                        TargetControlID="uotextboxEndDate" MaskType="Date" UserDateFormat="MonthDayYear" Mask="99/99/9999">
                    </cc1:MaskedEditExtender>
                    <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/Calendar_scheduleHS.png" />
                    <cc1:calendarextender ID="uotextboxEndDate_CalendarExtender" runat="server" Enabled="True"
                        PopupButtonID="Image2" TargetControlID="uotextboxEndDate" 
                        Format="MM/dd/yyyy">
                    </cc1:calendarextender>
                              
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="uotextboxEndDate"
                        ErrorMessage="Contract end date required." ValidationGroup="Header">*</asp:RequiredFieldValidator>
                              
                </td>
            </tr>
           
            <tr>
                <td class="style29">
                    Vendor Contract Date Accepted :<td class="style20">
                    <asp:TextBox ID="uoTextBoxVendorDateAccepted" runat="server" Width="125px"></asp:TextBox>
                <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server"
                    Enabled="True" TargetControlID="uoTextBoxVendorDateAccepted" WatermarkCssClass="fieldWatermark"
                    WatermarkText="MM/dd/yyyy">
                </cc1:TextBoxWatermarkExtender>
                    <cc1:MaskedEditExtender ID="uoTextBoxvendorDateAccepted0_MaskedEditExtender" 
                        runat="server" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                        CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                        CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                        MaskType="Date" UserDateFormat="MonthDayYear" Mask="99/99/9999"
                        TargetControlID="uoTextBoxVendorDateAccepted">
                    </cc1:MaskedEditExtender>
                    <cc1:CalendarExtender ID="uoTextBoxvendorDateAccepted0_CalendarExtender" 
                        runat="server" Enabled="True" TargetControlID="uoTextBoxVendorDateAccepted" PopupButtonID="Image6"
                        Format="MM/dd/yyyy">
                    </cc1:CalendarExtender>
                    <asp:Image ID="Image6" runat="server" 
                        ImageUrl="~/Images/Calendar_scheduleHS.png" Height="16px" />      
                        
                </td>
                <td class="style23">
                    RCCL Contract Date Accepted :</td>
                <td>
                    <asp:TextBox ID="uoTextBoxRCCLDateAccepted" runat="server" Width="125px"></asp:TextBox>
                <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server"
                    Enabled="True" TargetControlID="uoTextBoxRCCLDateAccepted" WatermarkCssClass="fieldWatermark"
                    WatermarkText="MM/dd/yyyy">
                </cc1:TextBoxWatermarkExtender>
                    <cc1:MaskedEditExtender ID="uoTextBoxRCCLDateAccepted_MaskedEditExtender" 
                        runat="server" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                        CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                        CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                        MaskType="Date" UserDateFormat="MonthDayYear" Mask="99/99/9999"
                        TargetControlID="uoTextBoxRCCLDateAccepted">
                    </cc1:MaskedEditExtender>
                    <cc1:CalendarExtender ID="uoTextBoxRCCLDateAccepted_CalendarExtender" 
                        runat="server" Enabled="True" TargetControlID="uoTextBoxRCCLDateAccepted" PopupButtonID="Image5"
                        Format="MM/dd/yyyy">
                    </cc1:CalendarExtender>
                    <asp:Image ID="Image5" runat="server" ImageUrl="~/Images/Calendar_scheduleHS.png" Height="16px" />                        
                </td>                
            </tr>
             <tr>
                <td class="style29">
                    Vendor Personnel :
                </td>
                <td class="style20">
                    <asp:TextBox ID="uotextboxVendorRep" runat="server" Width="125px" />
                </td>
                <td class="style23">
                    RCCL Personnel :
                </td>
                <td>
                    <asp:TextBox ID="uotextboxRCCLRep" runat="server" Width="125px" />
                </td>                
            </tr>
            <tr>
                <td class="style29">
                    <asp:HiddenField ID="uoHiddenFieldContractID" runat="server" />
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            </table>
            <div class="PageTitle">
            <asp:Panel ID="Panel2" runat="server">
                Hotel Branch
            </asp:Panel>
            </div>
                                  
            <table Width="100%" style="text-align: left">  
            <tr>
            <td colspan="2">
            <asp:ValidationSummary ID="ValidationSummary3" runat="server" 
                    ValidationGroup="branch" Width="100%" />  
            </td> 
            </tr> 
            <tr>
                <td class="style29">
                    Hotel Brand Name:</td>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                        <ContentTemplate> 
                        <asp:DropDownList ID="uoDropDownListVendorMain" runat="server"           
                            Width="500px" AutoPostBack="True" onselectedindexchanged="uoDropDownListVendorMain_SelectedIndexChanged" 
                            >
                        </asp:DropDownList>  
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" 
                                ControlToValidate="uoDropDownListVendorMain" 
                                ErrorMessage="Hotel name required." InitialValue="0" ValidationGroup="branch">*</asp:RequiredFieldValidator>
                        </ContentTemplate>  
                    </asp:UpdatePanel>               
                </td>
            </tr>
            <tr>
                <td class="style29">
                    Hotel Branch Name :
                </td>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>  
                        <asp:DropDownList ID="uoDropDownListVendor" runat="server"           
                            Width="500px" AutoPostBack="True" 
                            onselectedindexchanged="uoDropDownListVendor_SelectedIndexChanged">
                        </asp:DropDownList>                
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="uoDropDownListVendor"
                            ErrorMessage="Hotel branch name required." 
                            InitialValue="0" ValidationGroup="branch">*</asp:RequiredFieldValidator>
                        </ContentTemplate>  
                    </asp:UpdatePanel> 
                </td>
            </tr>
            <tr>
                <td class="style29">
                    Country :</td>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>  
                    <asp:DropDownList ID="uoDropDownCountry" runat="server"   
                        Width="500px" AutoPostBack="True" 
                        onselectedindexchanged="uoDropDownCountry_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="uoDropDownCountry"
                        ErrorMessage="Country required." 
                        InitialValue="0" ValidationGroup="branch">*</asp:RequiredFieldValidator>
                    </ContentTemplate>  
                    </asp:UpdatePanel> 
                </td>
            </tr>

            <tr>
                <td class="style29"><%--class="style22"--%>
                    City :</td>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>  
                    <asp:DropDownList ID="uoDropDownListCity" runat="server"                
                        Width="500px">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="uoDropDownListCity"
                        ErrorMessage="City required." 
                        InitialValue="0" ValidationGroup="branch">*</asp:RequiredFieldValidator>
                    </ContentTemplate>  
                    </asp:UpdatePanel> 
                </td>
            </tr>

            <tr>                 
                <td colspan="2">
                    <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                    <ContentTemplate >
                        <asp:GridView ID="uoGridViewBranch" runat="server" AutoGenerateColumns="False" 
                            CssClass="listViewTable" Width="100%"
                            onselectedindexchanged="uoGridViewBranch_SelectedIndexChanged" 
                            Visible="False">
                            <Columns>
                                <asp:BoundField DataField="Hotel Branch" HeaderText="Hotel Branch" />
                                <asp:BoundField DataField="Country" HeaderText="Country" />
                                <asp:BoundField DataField="City" HeaderText="City" />
                                <asp:ButtonField CommandName="Select" Text="Delete" >
                                <ItemStyle Width="70px" />
                                </asp:ButtonField>
                            </Columns>
                        </asp:GridView>
                    </ContentTemplate>
                    <Triggers >
                    <asp:AsyncPostBackTrigger ControlID="uoButtonBranch" />
                    </Triggers>
                    </asp:UpdatePanel>
                    </td>
                <tr>
                <td>
                    <asp:Button ID="uoButtonBranch" runat="server" onclick="uoButtonBranch_Click" 
                        Text="Add Branch" ValidationGroup="branch" Visible="False" />
                    </td>
                </tr>
            </table>

        <div class="PageTitle">
            <asp:Panel ID="uoPanelShuttleHeader" runat="server">
                Shuttle
                <asp:Label ID="uoLabelShuttle" runat="server" Text="" Font-Size="Small" />
            </asp:Panel>
        </div>
        <asp:Panel ID="uoPanelShuttle" runat="server"  CssClass="CollapsiblePanel">
        <table width="100%"> 
        <tr align=left>
        <td align=left class="style29"> With shuttle :</td><%--class="style19"--%>
            <td align=left><asp:CheckBox ID="uoCheckBoxShuttle" runat="server" /></td>
        </tr>
        </table> 
         <cc1:CollapsiblePanelExtender ID="CollapsiblePanelExtender1" runat="server" TargetControlID="uoPanelShuttle"
            ExpandControlID="uoPanelShuttleHeader" CollapseControlID="uoPanelShuttleHeader" TextLabelID="uoLabelShuttle"
            CollapsedText="(Show List)" ExpandedText="(Hide List)" Collapsed="false" SuppressPostBack="true">
        </cc1:CollapsiblePanelExtender>
        </asp:Panel> 
         <div class="PageTitle">
            <asp:Panel ID="Panel1" runat="server">
                Meals
                <asp:Label ID="uoLabelMeal" runat="server" Text="" Font-Size="Small" />
            </asp:Panel>
        </div>
        <asp:Panel ID="uopanelMeal" runat="server"  CssClass="CollapsiblePanel">
        <table width="100%">
            <tr>
                <td align="left" class="style29"><%--class="style15"--%>
                    Meal Rate :</td>
                <td align="left" class="style18">
                    <asp:TextBox ID="uoTextBoxMealRate" runat="server"></asp:TextBox>
                    <cc1:MaskedEditExtender ID="uoTextBoxMealRate_MaskedEditExtender" 
                        runat="server" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                        CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                        CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                        Mask="9,999.99" MaskType="Number" TargetControlID="uoTextBoxMealRate">
                    </cc1:MaskedEditExtender>
                </td>
                <td align="left" class="style17">
                    &nbsp;</td>
                <td align="left">
                    &nbsp;</td>
            </tr>
            <tr>
                <td align="left" class="style29"><%--class="style15"--%>
                    Meal Rate Tax(%) :</td>
                <td align="left" class="style18">
                    <asp:TextBox ID="uoTextBoxMealRateTax" runat="server"></asp:TextBox>
                    <cc1:MaskedEditExtender ID="uoTextBoxMealRateTax_MaskedEditExtender" 
                        runat="server" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                        CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                        CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                        Mask="999.99" MaskType="Number" TargetControlID="uoTextBoxMealRateTax">
                    </cc1:MaskedEditExtender>
                </td>
                <td align="left" class="style17">
                    &nbsp;</td>
                <td align="left">
                    &nbsp;</td>
            </tr>
            <tr>
                <td align="left" class="style29"><%--class="style15"--%>
                    Meal Rate Tax Inclusive :</td>
                <td align="left" class="style18">
                    <asp:CheckBox ID="uoCheckBoxMealRateTaxInclusive" runat="server" />
                </td>
                <td align="left" class="style17">
                    &nbsp;</td>
                <td align="left">
                    &nbsp;</td>
            </tr>
            <tr>
                <td align="left" class="style15">
                    &nbsp;</td>
                <td align="left" class="style18">
                    <asp:CheckBox ID="uoCheckBoxBreakfast" runat="server" Text="Breakfast" 
                        oncheckedchanged="uoCheckBoxBreakfast_CheckedChanged" />
                </td>
                <td align="left" class="style17">
                    &nbsp;</td>
                <td align="left">
                    &nbsp;</td>
            </tr>
            <tr>
                <td align="left" class="style15">
                    &nbsp;</td>
                <td align="left" class="style18">
                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                    <ContentTemplate >
                    <asp:CheckBox ID="uoCheckBoxLunch" runat="server" Text="Lunch" 
                        AutoPostBack="True" oncheckedchanged="uoCheckBoxLunch_CheckedChanged" />
                    </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
                <td align="left" class="style17">
                    &nbsp;</td>
                <td align="left">
                    &nbsp;</td>
            </tr>
            <tr>
                <td align="left" class="style25">
                    </td>
                <td align="left" class="style26">
                    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                    <ContentTemplate >
                    <asp:CheckBox ID="uoCheckBoxDinner" runat="server" Text="Dinner" 
                        AutoPostBack="True" oncheckedchanged="uoCheckBoxDinner_CheckedChanged" />
                    </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
                <td align="left" class="style27">
                    </td>
                <td align="left" class="style28">
                    </td>
            </tr>
            <tr>
                <td align="left" class="style15">
                    &nbsp;</td>
                <td align="left" class="style18">
                    <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                    <ContentTemplate >
                    <asp:CheckBox ID="uoCheckBoxLunchDinner" runat="server" 
                        Text="Lunch or Dinner" AutoPostBack="True" 
                        oncheckedchanged="uoCheckBoxLunchDinner_CheckedChanged" />
                    </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
                <td align="left" class="style17">
                    &nbsp;</td>
                <td align="left">
                    &nbsp;</td>
            </tr>
            <tr>
                <td align="left" class="style15">
                    &nbsp;</td>
                <td align="left" class="style18">
                    &nbsp;</td>
                <td align="left" class="style17">
                    &nbsp;</td>
                <td align="left">
                    &nbsp;</td>
            </tr>
        </table> 
         <cc1:CollapsiblePanelExtender ID="uocollapsibleMeal" runat="server" TargetControlID="uopanelMeal"
            ExpandControlID="Panel1" CollapseControlID="Panel1" TextLabelID="uolabelMeal"
            CollapsedText="(Show List)" ExpandedText="(Hide List)" Collapsed="false" SuppressPostBack="true">
        </cc1:CollapsiblePanelExtender>
        </asp:Panel>
        <div class="PageTitle">
            <asp:Panel ID="uopanelroomhead" runat="server">
                Rooms
                <asp:Label ID="uolabelRoom" runat="server" Text="" Font-Size="Small" />
            </asp:Panel>
        </div>
        <asp:Panel ID="uopanelroom" runat="server" CssClass="CollapsiblePanel">
            <table width="100%">
                <tr>
                    <td align=left>
                        <asp:ValidationSummary ID="ValidationSummary2" runat="server" 
                            ValidationGroup="Room" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <table style="width: 100%" class="LeftAligned" >
                            <tr>
                                <td align="left" class="style29">
                                    Room Type :
                                </td>
                                <td align="left" class="style9">
                                    <asp:DropDownList ID="uoDropDownListRoomType" runat="server" Width="180px">
                                    </asp:DropDownList>
                                </td>
                                <td align="left" class="style10">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" 
                                        ControlToValidate="uoDropDownListRoomType" ErrorMessage="Room type required." 
                                        InitialValue="0" ValidationGroup="Room">*</asp:RequiredFieldValidator>
                                </td>
                                <td class="LeftAligned">
                                    &nbsp;</td>
                                <td class="LeftAligned">
                                    &nbsp;</td>
                                <td align="left">
                                    <b>Daily room count</b>&nbsp;</td>
                            </tr>
                            <tr>
                                <td align=left class="style29">
                                    Date From :
                                </td>
                                <td align=left class="style9">
                                    <asp:TextBox ID="uotextboxRoomDateFrom" runat="server" Width="180px" 
                                        ValidationGroup="Room" />
                                    <cc1:TextBoxWatermarkExtender ID="uotextboxRoomDateFrom_TextBoxWatermarkExtender" 
                                        runat="server" Enabled="True" TargetControlID="uotextboxRoomDateFrom"
                                        WatermarkCssClass="fieldWatermark" WatermarkText="MM/dd/yyyy">
                                    </cc1:TextBoxWatermarkExtender>
                                    <cc1:MaskedEditExtender ID="uotextboxRoomDateFrom_MaskedEditExtender" 
                                        runat="server" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                                        CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                        CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                                        MaskType="Date" UserDateFormat="MonthDayYear" Mask="99/99/9999"
                                        TargetControlID="uotextboxRoomDateFrom">
                                    </cc1:MaskedEditExtender>
                                    <cc1:CalendarExtender ID="uotextboxRoomDateFrom_CalendarExtender" 
                                        runat="server" Enabled="True" PopupButtonID="Image3" 
                                        TargetControlID="uotextboxRoomDateFrom" Format="MM/dd/yyyy">
                                    </cc1:CalendarExtender>
                                </td>
                                <td align=left class="style10">
                                    <asp:Image ID="Image3" runat="server" 
                                        ImageUrl="~/Images/Calendar_scheduleHS.png" />
                                </td>
                                <td align=left>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" 
                                        ControlToValidate="uotextboxRoomDateFrom" 
                                        ErrorMessage="Room date from required." ValidationGroup="Room">*</asp:RequiredFieldValidator>
                                </td>
                                <td align="right">
                                    Monday :
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="uotextboxMondayRoom" runat="server" Width="180px" />
                                    <cc1:FilteredTextBoxExtender ID="uotextboxMondayRoom_FilteredTextBoxExtender" 
                                        runat="server" Enabled="True" FilterType="Numbers" 
                                        TargetControlID="uotextboxMondayRoom">
                                    </cc1:FilteredTextBoxExtender>
                                </td>
                            </tr>
                            <tr>
                                <td align=left class="style29">
                                    Date To&nbsp; :</td>
                                <td align=left class="style9">
                                    <asp:TextBox ID="uotextboxRoomDateTo" runat="server" Width="180px" />
                                    <cc1:TextBoxWatermarkExtender ID="uotextboxRoomDateTo_TextBoxWatermarkExtender" 
                                        runat="server" Enabled="True" TargetControlID="uotextboxRoomDateTo"
                                        WatermarkCssClass="fieldWatermark" WatermarkText="MM/dd/yyyy">
                                    </cc1:TextBoxWatermarkExtender>
                                    <cc1:MaskedEditExtender ID="uotextboxRoomDateTo_MaskedEditExtender" 
                                        runat="server" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                                        CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                        CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                                        MaskType="Date" UserDateFormat="MonthDayYear" Mask="99/99/9999"
                                        TargetControlID="uotextboxRoomDateTo">
                                    </cc1:MaskedEditExtender>
                                    <cc1:CalendarExtender ID="uotextboxRoomDateTo_CalendarExtender" runat="server" 
                                        Enabled="True" PopupButtonID="Image4" TargetControlID="uotextboxRoomDateTo" Format="MM/dd/yyyy">
                                    </cc1:CalendarExtender>
                                </td>
                                <td align=left class="style10">
                                    <asp:Image ID="Image4" runat="server" 
                                        ImageUrl="~/Images/Calendar_scheduleHS.png" />
                                </td>
                                <td align=left>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" 
                                        ControlToValidate="uotextboxRoomDateTo" ErrorMessage="Room date to required." 
                                        ValidationGroup="Room">*</asp:RequiredFieldValidator>
                                </td>
                                <td align="right">
                                    Tuesday : </td>
                                <td align="left">
                                    <asp:TextBox ID="uotextboxTuesdayRoom" runat="server" Width="180px" />
                                    <cc1:FilteredTextBoxExtender ID="uotextboxTuesdayRoom_FilteredTextBoxExtender" 
                                        runat="server" Enabled="True" FilterType="Numbers" 
                                        TargetControlID="uotextboxTuesdayRoom">
                                    </cc1:FilteredTextBoxExtender>
                                </td>
                            </tr>
                            <tr>
                                <td align=left class="style29">
                                    Currency :</td>
                                <td align=left class="style9">
                                    <asp:DropDownList ID="uoDropDownListCurrency" runat="server" 
                                        ValidationGroup="Room" Width="180px">
                                    </asp:DropDownList>
                                </td>
                                <td align=left class="style10">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" 
                                        ControlToValidate="uoDropDownListCurrency" ErrorMessage="Currency required." 
                                        InitialValue="0" ValidationGroup="Room">*</asp:RequiredFieldValidator>
                                </td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    Wednesday : </td>
                                <td align=left>
                                    <asp:TextBox ID="uotextboxWednesdayRoom" runat="server" Width="180px" />
                                    <cc1:FilteredTextBoxExtender ID="uotextboxWednesdayRoom_FilteredTextBoxExtender" 
                                        runat="server" Enabled="True" FilterType="Numbers" 
                                        TargetControlID="uotextboxWednesdayRoom">
                                    </cc1:FilteredTextBoxExtender>
                                </td>
                            </tr>
                            <tr>
                                <td align=left class="style29">
                                    Room Rate :
                                </td>
                                <td align=left class="style9">
                                    <asp:TextBox ID="uotextboxRoomRate" runat="server" Width="180px" />
                                    <cc1:MaskedEditExtender ID="uotextboxRoomRate_MaskedEditExtender" 
                                        runat="server" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                                        CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                        CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                                        Mask="999,999.99" MaskType="Number" TargetControlID="uotextboxRoomRate">
                                    </cc1:MaskedEditExtender>
                                </td>
                                <td align=left class="style10">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" 
                                        ControlToValidate="uotextboxRoomRate" ErrorMessage="Room rate required." 
                                        ValidationGroup="Room">*</asp:RequiredFieldValidator>
                                </td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    Thursday : </td>
                                <td align=left>
                                    <asp:TextBox ID="uotextboxThursdayRoom" runat="server" Width="180px" />
                                    <cc1:FilteredTextBoxExtender ID="uotextboxThursdayRoom_FilteredTextBoxExtender" 
                                        runat="server" Enabled="True" FilterType="Numbers" 
                                        TargetControlID="uotextboxThursdayRoom">
                                    </cc1:FilteredTextBoxExtender>
                                </td>
                            </tr>
                            <tr>
                                <td align=left class="style29">
                                    Room Rate Tax Inclusive :</td>
                                <td align="left" class="style9">
                                    <asp:CheckBox ID="uoCheckBoxTaxInclusive" runat="server" Checked="True" />
                                </td>
                                <td class="style10">
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    Friday : </td>
                                <td align=left>
                                    <asp:TextBox ID="uotextboxFridayRoom" runat="server" Width="180px" />
                                    <cc1:FilteredTextBoxExtender ID="uotextboxFridayRoom_FilteredTextBoxExtender" 
                                        runat="server" Enabled="True" FilterType="Numbers" 
                                        TargetControlID="uotextboxFridayRoom">
                                    </cc1:FilteredTextBoxExtender>
                                </td>
                            </tr>
                            <tr>
                                <td align=left class="style29">
                                    Room Rate Tax(%) :</td>
                                <td align=left class="style9">
                                    <asp:TextBox ID="uoTextBoxTax" runat="server" Width="180px"></asp:TextBox>
                                    <cc1:MaskedEditExtender ID="uoTextBoxTax_MaskedEditExtender" runat="server" 
                                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                                        CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                        CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                                        Mask="999.99" MaskType="Number" TargetControlID="uoTextBoxTax">
                                    </cc1:MaskedEditExtender>
                                </td>
                                <td class="style10">
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    Saturday : </td>
                                <td align=left>
                                    <asp:TextBox ID="uotextboxSaturdayRoom" runat="server" Width="180px" />
                                    <cc1:FilteredTextBoxExtender ID="uotextboxSaturdayRoom_FilteredTextBoxExtender" 
                                        runat="server" Enabled="True" FilterType="Numbers" 
                                        TargetControlID="uotextboxSaturdayRoom">
                                    </cc1:FilteredTextBoxExtender>
                                </td>
                            </tr>
                            <tr><td class="style11"></td>
                                <td align=left class="style12">

                                    <asp:Button ID="uobtnAddRoom0" runat="server" OnClick="uobtnAddRoom_Click" 
                                        Text="Add Room Type" ValidationGroup="Room" />

                                </td>
                                <td class="style13">
                                    </td>
                                <td class="style14">
                                    </td>
                                <td class="style14">
                                    Sunday : </td>
                                <td align=left class="style14">
                                    <asp:TextBox ID="uotextboxSundayRoom" runat="server" Width="180px" />
                                    <cc1:FilteredTextBoxExtender ID="uotextboxSundayRoom_FilteredTextBoxExtender" 
                                        runat="server" Enabled="True" FilterType="Numbers" 
                                        TargetControlID="uotextboxSundayRoom">
                                    </cc1:FilteredTextBoxExtender>
                                </td>
                            </tr>
                            <tr>
                                <td align=left>
                                    &nbsp;</td>
                                <td align=left class="style9">
                                    &nbsp;</td>
                                <td class="style10">
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                        </table>
                    </td>

                </tr>
                <tr>
                    <td valign="top" align=left>
                       <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                    <ContentTemplate >
                        <asp:GridView ID="uoGridViewRooms" runat="server" Width="100%" 
                            CssClass=listViewTable AutoGenerateColumns="False" 
                            onselectedindexchanged="uoGridViewRooms_SelectedIndexChanged">
                            <Columns>
                                <asp:BoundField DataField="Room Type" HeaderText="Room Type" />
                                <asp:BoundField DataField="Date From" HeaderText="Date From" 
                                    DataFormatString="{0:d}" />
                                <asp:BoundField DataField="Date To" HeaderText="Date To" 
                                    DataFormatString="{0:d}"/>
                                <asp:BoundField DataField="Currency" HeaderText="Currency"/>
                                <asp:BoundField DataField="Room Rate" HeaderText="Room Rate"/>
                                <asp:BoundField DataField="Tax Inclusive" HeaderText="Tax Inclusive"/>
                                <asp:BoundField DataField="Tax(%)" HeaderText="Tax(%)"/>
                                <asp:BoundField DataField="Monday" HeaderText="Mon"/>
                                <asp:BoundField DataField="Tuesday" HeaderText="Tues"/>
                                <asp:BoundField DataField="Wednesday" HeaderText="Wed"/>
                                <asp:BoundField DataField="Thursday" HeaderText="Thurs"/>
                                <asp:BoundField DataField="Friday" HeaderText="Fri"/>
                                <asp:BoundField DataField="Saturday" HeaderText="Sat"/>
                                <asp:BoundField DataField="Sunday" HeaderText="Sun"/>                                
                                <asp:ButtonField CommandName="Select" Text="Delete" />
                            </Columns>
                        </asp:GridView>
                        </ContentTemplate>
                        <Triggers> 
                        <asp:AsyncPostBackTrigger ControlID="uobtnAddRoom0" />
                        </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <cc1:CollapsiblePanelExtender ID="uocollapsibleRoom" runat="server" TargetControlID="uopanelRoom"
            ExpandControlID="uopanelroomhead" CollapseControlID="uopanelroomhead" TextLabelID="uolabelRoom"
            CollapsedText="(Show List)" ExpandedText="(Hide List)" Collapsed="false" SuppressPostBack="true">
        </cc1:CollapsiblePanelExtender>
        </div>
        <div class="PageTitle">
            <asp:Panel ID="Panel3" runat="server">
                Upload Contract</asp:Panel>
        </div>
        <asp:Panel ID="Panel4" runat="server" >
        <table width="100%">
            <tr>
                <td align="left">
                    <asp:ValidationSummary ID="ValidationSummaryUpload" runat="server" 
                        ValidationGroup="Upload" />
                </td>
            </tr>
                <tr>
                    <td align="left" class="style29"><%--class="style24"--%>
                        Contract Attachment (pdf/doc/docx) :
                    </td>
                    <td align="left">
                        <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                            <ContentTemplate>
                                <asp:FileUpload ID="uoFileUploadContract" runat="server" Width="70%" />
                                <asp:Button ID="uoButtonUpload" runat="server" onclick="uoButtonUpload_Click" 
                                    Text="Upload" Visible="False" />
                                <asp:Label ID="Label1" runat="server" ForeColor="Red"></asp:Label>
                                <asp:RegularExpressionValidator ID="uoRegularExpressionValidatorUpload" 
                                    runat="server" ControlToValidate="uoFileUploadContract" 
                                    ErrorMessage="Please select a .pdf or .doc or .docx file" 
                                    ValidationExpression="^([a-zA-Z].*|[1-9].*)\.(((p|P)(d|D)(f|F))|((d|D)(o|O)(c|C))|((d|D)(o|O)(c|C)(x|X)))$" 
                                    ValidationGroup="Upload">*</asp:RegularExpressionValidator>
                            </ContentTemplate>
                            <triggers>
                                <asp:PostBackTrigger ControlID="uobtnSave" />
                            </triggers>
                        </asp:UpdatePanel>
                    </td>                    
                </tr>            
            <tr>
                <td class="style24" align="left">
                    <asp:HiddenField ID="uoHiddenFieldFileName" runat="server" />
                </td>
                <td align="left" align="left">
                    <asp:Label ID="ucLabelAttached" runat="server" ForeColor="Red" Visible="False"></asp:Label>
                    &nbsp;<asp:LinkButton ID="uoLinkButtonRemove" runat="server" 
                        onclick="uoLinkButtonRemove_Click" Visible="False">Remove</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td class="style24" align="left">
                    <asp:HiddenField ID="uoHiddenFieldFileType" runat="server" />
                </td>
                <td align="left">
                    &nbsp;</td>
            </tr>
        </table> 
        </asp:Panel> 
        <table width="100%">
        <tr>
        <td align="center">
          <hr />
          <asp:Button ID="uobtnSave" runat="server" onclick="uobtnSave_Click" Text="Save" 
           Width="70px" /> 
        </td>
        </tr>
        </table>
 
</asp:Content>

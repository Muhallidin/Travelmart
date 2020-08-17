<%@ Page Language="C#" MasterPageFile="~/TravelMartMaintenanceMaster.Master" AutoEventWireup="true"
    CodeBehind="VehicleContractAdd.aspx.cs" Inherits="TRAVELMART.ContractManagement.VehicleContractAdd" %>

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
    </script>

    <%--<script type="text/javascript" src="../FBox/jquery-1.6.1.min.js"></script>   

<script type ="text/javascript" language="javascript">
    $(document).ready(function() {
        Settings();
    });

    function pageLoad(sender, args) {
        var isAsyncPostback = Sys.WebForms.PageRequestManager.getInstance().get_isInAsyncPostBack();
        if (isAsyncPostback) {
            Settings();
        }
    }

    function Settings() {

            if ($("#<%=uoDropDownListOrigin.ClientID %>").val() == "Other") {
                $("#<%=uoTextBoxOrigin.ClientID %>").show();
                ValidatorEnable(document.getElementById('<%=RequiredFieldValidator_uoTextBoxOrigin.ClientID %>'), true);
            }
            else {
                $("#<%=uoTextBoxOrigin.ClientID %>").hide();
                ValidatorEnable(document.getElementById('<%=RequiredFieldValidator_uoTextBoxOrigin.ClientID %>'), false);
            }
            if ($("#<%=uoDropDownListDestination.ClientID %>").val() == "Other") {
                $("#<%=uoTextBoxDestination.ClientID %>").show();
                ValidatorEnable(document.getElementById('<%=RequiredFieldValidator_uoTextBoxDestination.ClientID %>'), true);
            }
            else {
                $("#<%=uoTextBoxDestination.ClientID %>").hide();
                ValidatorEnable(document.getElementById('<%=RequiredFieldValidator_uoTextBoxDestination.ClientID %>'), false);
            }        

        if ($("#<%=uoDropDownListOrigin.ClientID %>").val() == "Other") {
            $("#<%=uoTextBoxOrigin.ClientID %>").show();
            ValidatorEnable(document.getElementById('<%=RequiredFieldValidator_uoTextBoxOrigin.ClientID %>'), true);
        }
        else {
            $("#<%=uoTextBoxOrigin.ClientID %>").hide();
            ValidatorEnable(document.getElementById('<%=RequiredFieldValidator_uoTextBoxOrigin.ClientID %>'), false);
        }
        if ($("#<%=uoDropDownListDestination.ClientID %>").val() == "Other") {
            $("#<%=uoTextBoxDestination.ClientID %>").show();
            ValidatorEnable(document.getElementById('<%=RequiredFieldValidator_uoTextBoxDestination.ClientID %>'), true);
        }
        else {
            $("#<%=uoTextBoxDestination.ClientID %>").hide();
            ValidatorEnable(document.getElementById('<%=RequiredFieldValidator_uoTextBoxDestination.ClientID %>'), false);
        }
        
        //--Events        
        $("#<%=uoDropDownListOrigin.ClientID %>").click(function() {
            if ($(this).val() == "Other") {
                $("#<%=uoTextBoxOrigin.ClientID %>").fadeIn();
                ValidatorEnable(document.getElementById('<%=RequiredFieldValidator_uoTextBoxOrigin.ClientID %>'), true);
            }
            else {
                $("#<%=uoTextBoxOrigin.ClientID %>").fadeOut();
                ValidatorEnable(document.getElementById('<%=RequiredFieldValidator_uoTextBoxOrigin.ClientID %>'), false);
            }
        });
        $("#<%=uoDropDownListDestination.ClientID %>").click(function() {
            if ($(this).val() == "Other") {
                $("#<%=uoTextBoxDestination.ClientID %>").fadeIn();
                ValidatorEnable(document.getElementById('<%=RequiredFieldValidator_uoTextBoxDestination.ClientID %>'), true);
            }
            else {
                $("#<%=uoTextBoxDestination.ClientID %>").fadeOut();
                ValidatorEnable(document.getElementById('<%=RequiredFieldValidator_uoTextBoxDestination.ClientID %>'), false);
            }
        });        
    }    
</script>--%>
    <div class="leftAligned">
        <div class="PageTitle">
            Vehicle Contract
        </div>
        <hr />
        <table width="100%" style="text-align: left">
            <tr>
                <td colspan="4">
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Header" />
                </td>
            </tr>
            <%--<tr>
                <td class="style4">
                    Vehicle Brand :
                </td>
                <td colspan="3">
                    <asp:DropDownList ID="uoDropDownListVendorName" runat="server" Width="447px" AutoPostBack="True" 
                    OnSelectedIndexChanged="uoDropDownListVendorName_SelectedIndexChanged">
                    </asp:DropDownList>
                    &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="uoDropDownListVendorName"
                          ErrorMessage="Vendor required.">*
                          </asp:RequiredFieldValidator>
                </td>
            </tr>--%>
            <%--<tr>
                                <td class="style4">
                                    Country :
                                </td>
                                <td colspan="3">
                                    <asp:DropDownList ID="uoDropDownListCountry" runat="server" Width="447px" AutoPostBack="True" 
                                    OnSelectedIndexChanged="uoDropDownListCountry_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="uoDropDownListCountry"
                                    InitialValue="" ErrorMessage="Country required.">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="style4">
                                    City :
                                 </td>
                                <td colspan="3">
                                    <asp:DropDownList ID="uoDropDownListCity" runat="server" Width="447px" >
                                    </asp:DropDownList>
                                    &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="uoDropDownListCity"
                                    InitialValue="" ErrorMessage="City required.">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>--%>
            <%--<tr>
                                <td class="style4">
                                    Vehicle Branch :
                                </td>
                                <td colspan="3">
                                    <asp:DropDownList ID="uoDropDownListVehicleBranch" runat="server" Width="447px"
                                    onselectedindexchanged="uoDropDownListVehicleBranch_SelectedIndexChanged" AutoPostBack="True">
                                    </asp:DropDownList>
                                    &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="uoDropDownListVehicleBranch"
                                    ErrorMessage="Branch required." InitialValue="0">*</asp:RequiredFieldValidator>
                                </td> 
                            </tr> --%>
            <%--<tr>
                                <td colspan="4"></td>
                            </tr>--%>
            <tr>
                <td class="contentCaptionOrig">
                    Contract Title :
                </td>
                <td colspan="3">
                    <asp:TextBox ID="uoTextBoxContractTitle" runat="server" Width="447px" />
                    &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="uoTextBoxContractTitle"
                        ErrorMessage="Contract title required." ValidationGroup="Header">*</asp:RequiredFieldValidator></td>
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
                    <asp:TextBox ID="uoTextBoxContractStartDate" runat="server"></asp:TextBox>
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
                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ValidationGroup="Header"
                        ControlToValidate="uoTextBoxContractStartDate" ErrorMessage="Contract start date required.">*</asp:RequiredFieldValidator>--%>
                </td>
                <td class="contentCaptionOrig">
                    Expiration Date :
                </td>
                <td class="contentValueOrig">
                    <asp:TextBox ID="uoTextBoxContractEndDate" runat="server"></asp:TextBox>
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
                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="uoTextBoxContractEndDate"
                        ValidationGroup="Header" ErrorMessage="Contract end date required.">*</asp:RequiredFieldValidator>--%>
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
                <%-- Modified By: Michael Evangelista
                            Date: 10/06/2014
                     Description: Added Airport to Hotel and Hotel to Ship checkboxes
                 --%>
                <td class="contentCaptionOrig">
               <asp:CheckBox runat="server" ID="uoCheckBoxAirportToHotel" Text="Airport To Hotel"/>
                </td>
                <td class="contentValueOrig">
                <asp:CheckBox runat="server" ID="uoCheckBoxHotelToShip" Text="Hotel To Ship"/>
                </td>
            </tr>
            <tr>
                <td class="contentCaptionOrig">                    
                    Brand:
                </td>
                <td class="contentValueOrig">
                    <asp:CheckBoxList ID="uoCheckBoxListBrand" runat="server" 
                        RepeatDirection="Horizontal">
                        <asp:ListItem Value="1">RCI</asp:ListItem>
                        <asp:ListItem Value="2">AZA</asp:ListItem>
                        <asp:ListItem Value="3">CEL</asp:ListItem>
                        <asp:ListItem Value="4">PUL</asp:ListItem>
                        <asp:ListItem Value="5">SKS</asp:ListItem>
                    </asp:CheckBoxList>
                </td>
                <td class="contentCaptionOrig">                    
                </td>
                <td class="contentValueOrig">
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <asp:HiddenField ID="uoHiddenFieldVendorId" runat="server" />
                </td>
            </tr>
        </table>
        <div class="PageTitle">
            <asp:Panel ID="Panel2" runat="server">
                Vehicle Branch
            </asp:Panel>
        </div>
        <table width="100%" style="text-align: left">
            <%--<tr>
                <td colspan="4">
                <asp:ValidationSummary ID="ValidationSummary3" runat="server" 
                        ValidationGroup="Branch" Width="100%" />  
                </td> 
            </tr> 
            <tr>                        
                <td class="contentCaptionOrig">
                    Vehicle Brand Name:</td>
                <td class="contentValueOrig" colspan="3">
                    <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                    <ContentTemplate> 
                    <asp:DropDownList ID="uoDropDownListVendorName" runat="server"           
                        Width="500px" AutoPostBack="True" onselectedindexchanged="uoDropDownListVendorName_SelectedIndexChanged" 
                        >
                    </asp:DropDownList>  
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" 
                            ControlToValidate="uoDropDownListVendorName" 
                            ErrorMessage="Vehicle brand required." InitialValue="0" ValidationGroup="Branch">*</asp:RequiredFieldValidator>
                    </ContentTemplate>  
                    </asp:UpdatePanel>               
                </td>
            </tr>--%>
            <tr>
                <td class="contentCaptionOrig">
                    Vehicle Branch Name :
                </td>
                <td class="contentValueOrig" colspan="2">
                    <asp:TextBox ID="uoTextBoxVendorBranch" runat="server" CssClass="ReadOnly" ReadOnly="true"
                        Width="447px"></asp:TextBox>
                    <%-- <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>  
                    <asp:DropDownList ID="uoDropDownListVehicleBranch" runat="server"           
                        Width="500px" AutoPostBack="True" 
                        onselectedindexchanged="uoDropDownListVehicleBranch_SelectedIndexChanged">
                    </asp:DropDownList>                
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="uoDropDownListVehicleBranch"
                        ErrorMessage="Vehicle branch name required." 
                        InitialValue="0" ValidationGroup="Branch">*</asp:RequiredFieldValidator>
                    </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="uoDropDownListVendorName" 
                                EventName="SelectedIndexChanged" />
                        </Triggers> 
                    </asp:UpdatePanel> --%>
                </td>
                <td></td>
            </tr>
            <tr>
                <td class="contentCaptionOrig">
                    Country :
                </td>
                <td class="contentValueOrig" colspan="2">
                    <asp:TextBox ID="uoTextBoxCountry" runat="server" CssClass="ReadOnly" ReadOnly="true"
                        Width="300px"></asp:TextBox>
                    <%--<asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate> 
                    <asp:DropDownList ID="uoDropDownListCountry" runat="server"   
                        Width="500px" AutoPostBack="True" 
                        onselectedindexchanged="uoDropDownListCountry_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ControlToValidate="uoDropDownListCountry"
                        ErrorMessage="Country required." 
                        InitialValue="0" ValidationGroup="Branch">*</asp:RequiredFieldValidator>
                    </ContentTemplate>                                                
                    </asp:UpdatePanel> --%>
                </td>
                <td></td>
            </tr>
            <tr>
                <td class="contentCaptionOrig">
                    City :
                </td>
                <td class="contentValueOrig" colspan="2">
                    <asp:TextBox ID="uoTextBoxCity" runat="server" Width="300px" ReadOnly="true" CssClass="ReadOnly"></asp:TextBox>
                </td>
                <td></td>
            </tr>
        </table>
        <br />
        <%--</div>--%>
        <div class="PageTitle">
            Assigned Airport & Seaport
        </div>
        <table width="100%" style="text-align: left">
            <tr>
                <td class="contentCaptionOrig">
                    Airport Filter By:
                </td>
                <td class="contentValueOrig">
                    <asp:DropDownList ID="uoDropDownListAirportFilter" runat="server" Width="300px">
                        <asp:ListItem Text="By Code" Value="0"></asp:ListItem>
                        <asp:ListItem Text="By Name" Value="1"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="contentCaptionOrig">
                    Seaport Filter By:
                </td>
                <td class="contentValueOrig">
                    <asp:DropDownList ID="uoDropDownListSeaportFilter" runat="server" Width="300px">
                        <asp:ListItem Text="By Code" Value="0"></asp:ListItem>
                        <asp:ListItem Text="By Name" Value="1"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="contentCaptionOrig">
                </td>
                <td class="contentValueOrig">
                    <asp:TextBox ID="uoTextBoxAirportFilter" runat="server" Width="295px" CssClass="SmallText"></asp:TextBox>
                    &nbsp; &nbsp; &nbsp;
                    <asp:Button ID="uoButtonAirportFilter" runat="server" Text="Filter" CssClass="SmallButton"
                        Width="50px" OnClick="uoButtonAirportFilter_Click" />
                </td>
                <td class="contentCaptionOrig">
                </td>
                <td class="contentValueOrig">
                    <asp:TextBox ID="uoTextBoxSeaportFilter" runat="server" Width="295px" CssClass="SmallText"></asp:TextBox>
                    &nbsp; &nbsp; &nbsp;
                    <asp:Button ID="uoButtonSeaportFilter" runat="server" Text="Filter" CssClass="SmallButton"
                        Width="50px" OnClick="uoButtonSeaportFilter_Click" />
                </td>
            </tr>
            <tr>
                <td class="contentCaptionOrig">
                    Airport:
                </td>
                <td class="contentValueOrig">
                    <asp:DropDownList ID="uoDropDownListAirport" runat="server" Width="300px" AppendDataBoundItems="true">
                    </asp:DropDownList>
                    &nbsp;
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="Airport"
                        ControlToValidate="uoDropDownListAirport" InitialValue="0" ErrorMessage="Aiport Required">*</asp:RequiredFieldValidator>
                    &nbsp;
                    <asp:Button ID="uoButtonAirportAdd" runat="server" Text="Add" ValidationGroup="Airport"
                        CssClass="SmallButton" Width="50px" OnClick="uoButtonAirportAdd_Click" />
                </td>
                <td class="contentCaptionOrig">
                    Seaport:
                </td>
                <td class="contentValueOrig">
                    <asp:DropDownList ID="uoDropDownListSeaport" runat="server" Width="300px" AppendDataBoundItems="true">
                    </asp:DropDownList>
                    &nbsp;
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="Seaport"
                        ControlToValidate="uoDropDownListSeaport" InitialValue="0" ErrorMessage="Seaport Required">*</asp:RequiredFieldValidator>
                    &nbsp;
                    <asp:Button ID="uoButtonSeaportAdd" runat="server" Text="Add" ValidationGroup="Seaport"
                        CssClass="SmallButton" Width="50px" OnClick="uoButtonSeaportAdd_Click" />
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <asp:ListView runat="server" ID="uoListViewAirport" OnItemDeleting="uoListViewAirport_ItemDeleting">
                        <LayoutTemplate>
                            <table border="0" cellpadding="0" cellspacing="0" class="listViewTable" width="50%">
                                <tr>
                                    <th style="text-align: center; white-space: normal;">
                                        Airport
                                    </th>
                                    <th runat="server" style="width: 5%" id="DeleteTH" />
                                </tr>
                                <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                            </table>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <tr>
                                <td class="leftAligned">
                                    <asp:HiddenField ID="uoHiddenFieldContractAirportID" runat="server" Value='<%# Eval("AirportSeaportID") %>' />
                                    <asp:HiddenField ID="uoHiddenFieldAirportID" runat="server" Value='<%# Eval("AirportID") %>' />
                                    <asp:Label ID="uoLabelAirport" runat="server" Text='<%# Eval("AirportName")%>'></asp:Label>
                                </td>
                                <td style="width: 10%">
                                    <asp:LinkButton OnClientClick="return confirmDelete();" ID="uoLinkButtonDelete" runat="server"
                                        CommandName="Delete">Delete</asp:LinkButton>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <EmptyDataTemplate>
                            <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                                <tr>
                                    <th>
                                        Airport
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
                <td>
                    <asp:ListView runat="server" ID="uoListViewSeaport" OnItemDeleting="uoListViewSeaport_ItemDeleting">
                        <LayoutTemplate>
                            <table border="0" cellpadding="0" cellspacing="0" class="listViewTable" width="50%">
                                <tr>
                                    <th style="text-align: center; white-space: normal;">
                                        Seaport
                                    </th>
                                    <th runat="server" style="width: 5%" id="DeleteTH" />
                                </tr>
                                <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                            </table>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <tr>
                                <td class="leftAligned">
                                    <asp:HiddenField ID="uoHiddenFieldContractSeaportID" runat="server" Value='<%# Eval("ID") %>' />
                                    <asp:HiddenField ID="uoHiddenFieldSeaportID" runat="server" Value='<%# Eval("SeaportID") %>' />
                                    <asp:Label ID="uoLabelSeaport" runat="server" Text='<%# Eval("SeaportName")%>'></asp:Label>
                                </td>
                                <td style="width: 10%">
                                    <asp:LinkButton OnClientClick="return confirmDelete();" ID="uoLinkButtonDelete" runat="server"
                                        CommandName="Delete">Delete</asp:LinkButton>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <EmptyDataTemplate>
                            <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                                <tr>
                                    <th>
                                        Seaport
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
            </tr>
            <tr>
                <td colspan="4">
                    <br />
                </td>
            </tr>
        </table>
        <%--<div class="PageTitle">
            <asp:Panel ID="uopanelbranchhead" runat="server">
                Branch
                <asp:Label ID="uolabelBranch" runat="server" Text="" Font-Size="Small" />
            </asp:Panel>
        </div>--%>
        <%--<asp:Panel ID="uopanelbranch" runat="server" CssClass="CollapsiblePanel">
            <table width="100%">
                <tr>
                    <td align=left>
                        <asp:ValidationSummary ID="ValidationSummary3" runat="server" 
                            ValidationGroup="Vehicle" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <table style="width: 100%" class="LeftAligned" >                            
                            <tr>
                                <td align=left>
                                    Vehicle Branch :
                                </td>
                                <td align=left>
                                    <asp:DropDownList ID="uoDropDownListVehicleBranch" runat="server" Width="447px">
                                    </asp:DropDownList>
                                </td>
                            </tr>                                                                 
                            <tr><td class="style11"></td>
                                <td align=left class="style12">
                                    <asp:Button ID="uoButtonAddBranch" runat="server" OnClick="uoButtonAddBranch_Click" 
                                        Text="Add" ValidationGroup="Vehicle" />
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
                        <asp:GridView ID="uoGridViewBranch" runat="server" Width="100%" 
                        AutoGenerateColumns="False" CssClass=listViewTable OnSelectedIndexChanged="uoGridViewBranch_SelectedIndexChanged" >
                            <Columns> 
                                <asp:BoundField DataField="Branch ID" HeaderText="Branch ID"/>
                                <asp:BoundField DataField="Vehicle Branch" HeaderText="Vehicle Branch"/>                                                                                             
                                <asp:ButtonField CommandName="Select" Text="Delete"/>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>                
            </table>
        </asp:Panel>--%>
        <%--<cc1:collapsiblepanelextender ID="uocollapsibleBranch" runat="server" TargetControlID="uopanelbranch"
            ExpandControlID="uopanelbranchhead" CollapseControlID="uopanelbranchhead" TextLabelID="uolabelBranch"
            CollapsedText="(Show List)" ExpandedText="(Hide List)" Collapsed="false" 
            SuppressPostBack="true">
        </cc1:collapsiblepanelextender>
        <br />--%>
        <div class="PageTitle">
            <asp:Panel ID="Panel1" runat="server">
                Vehicle Type and Capacity
            </asp:Panel>
        </div>
        <table width="100%">
            <tr>
                <td class="contentCaptionOrig">
                    Vehicle Type
                </td>
                <td class="contentValueOrig" colspan="3">
                    <asp:DropDownList ID="uoDropDownListVehicleType" runat="server" Width="300px" AppendDataBoundItems="true">
                    </asp:DropDownList>
                    &nbsp;
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ValidationGroup="VehicleCapacity"
                        ControlToValidate="uoDropDownListVehicleType" InitialValue="0" ErrorMessage="Max Count Required">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="contentCaptionOrig">
                    Min/Max Capacity:
                </td>
                <td class="contentValueOrig">
                    Min:&nbsp;
                    <asp:TextBox ID="uoTextBoxMin" runat="server" Width="100px" CssClass="SmallText"
                        onkeypress="return validate(event);"></asp:TextBox>
                    &nbsp;
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ValidationGroup="VehicleCapacity"
                        ControlToValidate="uoTextBoxMin" ErrorMessage="Min Count Required">*</asp:RequiredFieldValidator>
                    &nbsp;&nbsp;Max:&nbsp;
                    <asp:TextBox ID="uoTextBoxMax" runat="server" Width="100px" CssClass="SmallText"
                        onkeypress="return validate(event);"></asp:TextBox>
                    &nbsp;
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ValidationGroup="VehicleCapacity"
                        ControlToValidate="uoTextBoxMax" ErrorMessage="Max Count Required">*</asp:RequiredFieldValidator>
                    &nbsp;
                    <asp:Button ID="uoButtonVehicleTypeCapacityAdd" runat="server" Text="Add" CssClass="SmallButton"
                        Width="50px" ValidationGroup="VehicleCapacity" OnClick="uoButtonVehicleTypeCapacityAdd_Click" />
                </td>
                <td class="contentCaptionOrig">
                </td>
                <td class="contentValueOrig">
                </td>
            </tr>
            <tr>
                <td class="contentCaptionOrig">
                </td>
                <td class="contentValueOrig" valign="top" style="vertical-align: top">
                    <asp:ListView runat="server" ID="uoListViewVehicleTypeCapacity" OnItemDeleting="uoListViewVehicleTypeCapacity_ItemDeleting">
                        <LayoutTemplate>
                            <table border="0" cellpadding="0" cellspacing="0" class="listViewTable" width="50%">
                                <tr>
                                    <th style="text-align: center; white-space: normal;">
                                        Vehicle Type
                                    </th>
                                    <th style="text-align: center; white-space: normal;">
                                        Min Capacity
                                    </th>
                                    <th style="text-align: center; white-space: normal;">
                                        Max Capacity
                                    </th>
                                    <th runat="server" style="width: 5%" id="DeleteTH" />
                                </tr>
                                <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                            </table>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <tr>
                                <td class="leftAligned">
                                    <asp:HiddenField ID="uoHiddenFieldContractVehicleCapacityID" runat="server" Value='<%# Eval("ContractVehicleCapacityIDInt") %>' />
                                    <asp:HiddenField ID="uoHiddenFieldVehicleTypeID" runat="server" Value='<%# Eval("VehicleTypeID") %>' />
                                    <asp:Label ID="uoLabelVehicleTypeName" runat="server" Text='<%# Eval("VehicleType")%>'></asp:Label>
                                </td>
                                <td class="leftAligned">
                                    <asp:Label ID="uoLabelMinCapacity" runat="server" Text='<%# Eval("MinCapacity")%>'></asp:Label>
                                </td>
                                <td class="leftAligned">
                                    <asp:Label ID="uoLabelMaxCapacity" runat="server" Text='<%# Eval("MaxCapacity")%>'></asp:Label>
                                </td>
                                <td style="width: 10%">
                                    <asp:LinkButton OnClientClick="return confirmDelete();" ID="uoLinkButtonDelete" runat="server"
                                        CommandName="Delete" CommandArgument='<%# Eval("VehicleTypeID") %>'>Delete</asp:LinkButton>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <EmptyDataTemplate>
                            <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                                <tr>
                                    <th style="text-align: center; white-space: normal;">
                                        Vehicle Type
                                    </th>
                                    <th style="text-align: center; white-space: normal;">
                                        Min Capacity
                                    </th>
                                    <th style="text-align: center; white-space: normal;">
                                        Max Capacity
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
            <tr>
                <td align="left" colspan="4">
                    <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="Vehicle" />
                </td>
            </tr>
            <tr>
                <td class="contentCaptionOrig">
                    Vehicle :
                </td>
                <td align="left" class="contentValueOrig">
                    <asp:DropDownList ID="uoDropDownListVehicleTypeCapacity" runat="server" Width="250px"
                        AppendDataBoundItems="true">
                    </asp:DropDownList>
                </td>
                <td colspan="2" style="text-align: left">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="uoDropDownListVehicleTypeCapacity"
                        ErrorMessage="Vehicle type required." InitialValue="0" ValidationGroup="Vehicle">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="contentCaptionOrig">
                    Route From:
                </td>
                <td class="contentValueOrig">
                    <asp:DropDownList ID="uoDropDownListRouteFrom" runat="server" Width="250px" AutoPostBack="true" AppendDataBoundItems="true"
                        OnSelectedIndexChanged="uoDropDownListRouteFrom_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td colspan="2" style="text-align: left">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="uoDropDownListRouteFrom" 
                        ErrorMessage="Route From required." ValidationGroup="Vehicle" InitialValue="0">*</asp:RequiredFieldValidator>
                </td>               
            </tr>
            <tr>
                <td class="contentCaptionOrig">
                    Route To:
                </td>
                <td class="contentValueOrig">
                    <asp:DropDownList ID="uoDropDownListRouteTo" runat="server" Width="250px" AutoPostBack="true" AppendDataBoundItems="true"
                        OnSelectedIndexChanged="uoDropDownListRouteTo_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td colspan="2" style="text-align: left">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="uoDropDownListRouteTo"
                        ErrorMessage="Route To required." ValidationGroup="Vehicle" InitialValue="0">*</asp:RequiredFieldValidator>                       
                </td>               
            </tr>
            
            <tr>
                <td class="contentCaptionOrig">
                    Origin :
                </td>
                <td class="contentValueOrig">
                    <asp:DropDownList ID="uoDropDownListOrigin" runat="server" Width="250px" AppendDataBoundItems="true">
                    </asp:DropDownList>
                </td>
                <td colspan="2" style="text-align: left">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorOrigin" runat="server" ControlToValidate="uoDropDownListOrigin"
                        ErrorMessage="Origin required." ValidationGroup="Vehicle" InitialValue="0">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="contentCaptionOrig">
                    Destination :
                </td>
                <td class="contentValueOrig">
                    <asp:DropDownList ID="uoDropDownListDestination" runat="server" Width="250px" AppendDataBoundItems="true">
                    </asp:DropDownList>
                </td>
                <td colspan="2" style="text-align: left">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorDestination" runat="server" ControlToValidate="uoDropDownListDestination"
                        ErrorMessage="Destination required." ValidationGroup="Vehicle" InitialValue="0">*</asp:RequiredFieldValidator>
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
                        ErrorMessage="Rate required." ValidationGroup="Vehicle">*</asp:RequiredFieldValidator>
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
                    <asp:Button ID="uoButtonAddVehicle" runat="server" Text="Add" ValidationGroup="Vehicle"
                        OnClick="uoButtonAddVehicle_Click" CssClass="SmallButton"/>
                </td>
                <td>
                    <asp:HiddenField ID="uoHiddenFieldDetail" runat="server" Value="0" />
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td valign="top" align="left" colspan="2">
                    <asp:GridView ID="uoGridViewVehicle" runat="server" Width="100%" AutoGenerateColumns="False"
                        CssClass="listViewTable" OnRowDeleting="uoGridViewVehicle_RowDeleting" OnSelectedIndexChanging="uoGridViewVehicle_SelectedIndexChanging">
                        <Columns>
                            <asp:BoundField DataField="ContractDetailID" HeaderText="Contract Detail ID" Visible="false" />
                            <asp:BoundField DataField="VehicleTypeID" HeaderText="Vehicle ID" Visible="false" />
                            <asp:BoundField DataField="RouteFrom" HeaderText="Route ID From" Visible="false" />
                            <asp:BoundField DataField="RouteTo" HeaderText="Route ID To" Visible="false" />
                            <asp:BoundField DataField="VehicleType" HeaderText="Type" />
                            <asp:BoundField DataField="RouteFrom" HeaderText="Route From" />
                            <asp:BoundField DataField="RouteTo" HeaderText="Route To" />
                            <asp:BoundField DataField="Origin" HeaderText="Origin" />
                            <asp:BoundField DataField="Destination" HeaderText="Destination" />
                            <%--<asp:BoundField DataField="StartDate" HeaderText="Start Date"/>--%>
                            <%--<asp:BoundField DataField="EndDate" HeaderText="End Date"/>--%>
                            <%--<asp:BoundField DataField="CurrencyID" HeaderText="Currency ID" Visible="false"/>--%>
                            <%--<asp:BoundField DataField="Currency" HeaderText="Currency"/>--%>
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
                        <%--<asp:FileUpload ID="uoFileUploadContract" runat="server" Width="70%" />
                    <asp:Button ID="uoButtonUpload" runat="server" onclick="uoButtonUpload_Click" 
                        Text="Upload"  />
                    <asp:Label ID="Label1" runat="server" ForeColor="Red"></asp:Label>
                    <asp:RegularExpressionValidator ID="uoRegularExpressionValidatorUpload" 
                                    runat="server" ControlToValidate="uoFileUploadContract" 
                                    ErrorMessage="Please select a .pdf or .doc or .docx file" 
                                    ValidationExpression="^([a-zA-Z].*|[1-9].*)\.(((p|P)(d|D)(f|F))|((d|D)(o|O)(c|C))|((d|D)(o|O)(c|C)(x|X)))$" 
                                    ValidationGroup="Upload">*</asp:RegularExpressionValidator>--%>
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
                        &nbsp;<asp:LinkButton ID="uoLinkButtonRemove" runat="server" Visible="False">Remove</asp:LinkButton></td>
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
            &nbsp;
                <asp:Button ID="uoButtonBack" runat="server" Text="Back" Width="80" OnClick="uoButtonBack_Click"/>
            </td>
        </tr>
    </table>
</asp:Content>

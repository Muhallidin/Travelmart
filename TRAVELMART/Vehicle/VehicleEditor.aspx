<%@ Page Title="" Language="C#" MasterPageFile="~/Popup.Master" AutoEventWireup="true" CodeBehind="VehicleEditor.aspx.cs" Inherits="TRAVELMART.VehicleEditor" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">    
<script type="text/javascript" src="../FBox/jquery-1.6.1.min.js"></script>   
<script language="javascript" type="text/javascript">
    if ($("#<%=uoDropDownListVehicleCompany.ClientID %>").val() == "0") {
        $("#<%=uoLinkButtonContract.ClientID %>").hide();
    }
    else {
        $("#<%=uoLinkButtonContract.ClientID %>").show();
    }
</script>

 <script type="text/javascript">
     function OpenContract(vendorID) {
         window.parent.OpenContract(vendorID, 'VE');
         return false;
     }         
</script>

    <div class="PageTitle">
         Vehicle            
    </div>
    <hr/>    
<table>
    <tr>
        <td colspan="3">
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" BackColor="Beige"/>
        </td>
    </tr>
    <tr>
        <td>&nbsp Seafarer Name:</td>
        <td>
            <asp:TextBox ID="uoTextBoxSeafarer" runat="server" Width="300px" ReadOnly="true" CssClass="ReadOnly"></asp:TextBox>                          
        </td>        
        <td>
            &nbsp;</td>        
    </tr>
    <tr>
        <td>&nbsp Category:</td>
        <td>
            <asp:DropDownList ID="uoDropDownListCategory" runat="server" Width="305px" AutoPostBack="True" 
            onselectedindexchanged="uoDropDownListCategory_SelectedIndexChanged">
                <asp:ListItem Selected="True" Value="1">Accredited</asp:ListItem>
                <asp:ListItem Value="0">Non Accredited</asp:ListItem>
            </asp:DropDownList>
        </td>
        <td>
            &nbsp;</td>
    </tr>    
    <tr>
        <td>&nbsp Vehicle Brand:</td>
        <td>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>                        
            <asp:DropDownList ID="uoDropDownListVehicleCompany" runat="server" Width="305px" AutoPostBack="True"
            OnSelectedIndexChanged="uoDropDownListVehicleCompany_SelectedIndexChanged" AppendDataBoundItems="true">
            </asp:DropDownList>
                &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="uoDropDownListVehicleCompany" ErrorMessage="Vehicle company required.">*</asp:RequiredFieldValidator>
            </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="uoDropDownListCategory" 
                            EventName="SelectedIndexChanged" />                        
                    </Triggers>
            </asp:UpdatePanel>
        </td>        
    </tr>    
    <tr>
        <td>&nbsp; Country:</td>
        <td>
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
            <asp:DropDownList ID="uoDropDownListCountry" runat="server" Width="305px" 
            onselectedindexchanged="uoDropDownListCountry_SelectedIndexChanged" 
                    AutoPostBack="True" AppendDataBoundItems="True">
            </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="uoDropDownListCountry"
                ErrorMessage="Country required." InitialValue="0">*</asp:RequiredFieldValidator>
            </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="uoDropDownListCategory" 
                        EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="uoDropDownListVehicleCompany" 
                        EventName="SelectedIndexChanged" />
                </Triggers>
            </asp:UpdatePanel>
        </td>            
        <td>
            &nbsp;</td>            
    </tr>
    <tr>
        <td>&nbsp; City:</td>
        <td>
            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
            <ContentTemplate>
            <asp:DropDownList ID="uoDropDownListCity" runat="server" Width="305px" AppendDataBoundItems="true"
            onselectedindexchanged="uoDropDownListCity_SelectedIndexChanged" AutoPostBack="True">
            </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="uoDropDownListCity"
                ErrorMessage="City required." InitialValue="0">*</asp:RequiredFieldValidator>
            </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="uoDropDownListCategory" 
                        EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="uoDropDownListVehicleCompany" 
                        EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="uoDropDownListCountry" 
                        EventName="SelectedIndexChanged" />
                </Triggers>
            </asp:UpdatePanel>
        </td>            
        <td>
            &nbsp;</td>            
    </tr>   
    <tr>
        <td>&nbsp; Branch:</td>
        <td>
            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                <ContentTemplate>
            <asp:DropDownList ID="uoDropDownListVehicleBranch" runat="server" Width="305px" AppendDataBoundItems="True"
            onselectedindexchanged="uoDropDownListVehicleBranch_SelectedIndexChanged" AutoPostBack="True">
            </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="uoDropDownListVehicleBranch"
                ErrorMessage="Branch required." InitialValue="0">*</asp:RequiredFieldValidator>
            </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="uoDropDownListCategory" 
                            EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="uoDropDownListVehicleCompany" 
                            EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="uoDropDownListCountry" 
                            EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="uoDropDownListCity" 
                            EventName="SelectedIndexChanged" />
                    </Triggers>
            </asp:UpdatePanel>
        </td>            
        <td>
            <asp:LinkButton ID="uoLinkButtonContract" runat="server">Contract</asp:LinkButton>
        </td>           
    </tr>   
    <tr>
        <td>&nbsp; Vehicle Type:</td>
        <td>
            <asp:UpdatePanel ID="UpdatePanel5" runat="server" >
            <ContentTemplate>
            <asp:DropDownList ID="uoDropDownListVehicleType" runat="server" Width="305px" 
                AppendDataBoundItems="True"> 
                <asp:ListItem Value="0">--Select Vehicle Type--</asp:ListItem>
            </asp:DropDownList>                
                &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="uoDropDownListVehicleType" ErrorMessage="Vehicle type required.">*</asp:RequiredFieldValidator>
            <%--<asp:TextBox ID="uoTextBoxVehicleBrand" runat="server" Height="20px" Width="300px"></asp:TextBox>            --%>
            </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="uoDropDownListCategory" 
                            EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="uoDropDownListVehicleCompany" 
                            EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="uoDropDownListCountry" 
                            EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="uoDropDownListCity" 
                            EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="uoDropDownListVehicleBranch" 
                            EventName="SelectedIndexChanged" />
                        <asp:PostBackTrigger ControlID="uoDropDownListVehicleType"/>
                    </Triggers>
                </asp:UpdatePanel>
        </td>
        <td>
            &nbsp;</td>
    </tr>
    <%--<tr>
        <td>&nbsp Vehicle Year:</td>
        <td>
            <asp:TextBox ID="uoTextBoxYear" runat="server" Width="300px"></asp:TextBox>                
        </td>
    </tr>--%>
    <%--<tr>
        <td>&nbsp Vehicle Plate No.:</td>
        <td>
            <asp:TextBox ID="uoTextBoxPlateNo" runat="server" Width="300px"></asp:TextBox>                
        </td>
    </tr>--%>
    <tr>
        <td>&nbsp; Pickup Date:</td>
        <td>
            <asp:TextBox ID="uoTextBoxPickUpDate" runat="server" Width="300px"></asp:TextBox>
                <cc1:TextBoxWatermarkExtender ID="uoTextBoxPickUpDate_TextBoxWatermarkExtender" 
                    runat="server" Enabled="True" TargetControlID="uoTextBoxPickUpDate" WatermarkCssClass="fieldWatermark" WatermarkText="MM/dd/yyyy">
                </cc1:TextBoxWatermarkExtender>
                    <asp:ImageButton ID="ImageButton1" runat="server" 
                        ImageUrl="~/Images/Calendar_scheduleHS.png" />
                            <cc1:CalendarExtender ID="uoTextBoxPickUpDate_CalendarExtender" runat="server" 
                                Enabled="True" TargetControlID="uoTextBoxPickUpDate" PopupButtonID="ImageButton1" Format="MM/dd/yyyy">
                            </cc1:CalendarExtender>
                                <cc1:MaskedEditExtender ID="uoTextBoxPickUpDate_MaskedEditExtender" 
                                    runat="server" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                                    CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                    CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                                    Mask="99/99/9999" MaskType="Date" TargetControlID="uoTextBoxPickUpDate" UserDateFormat="MonthDayYear">
                                </cc1:MaskedEditExtender>                                
                                    &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="uoTextBoxPickUpDate" ErrorMessage="Pick up date required.">*</asp:RequiredFieldValidator>
        </td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td>&nbsp; Pickup Time:</td>
        <td>
            <asp:TextBox ID="uoTextBoxPickupTime" runat="server" Width="300px"></asp:TextBox>
            <cc1:TextBoxWatermarkExtender ID="uoTextBoxPickupTime_TextBoxWatermarkExtender" 
                runat="server" Enabled="True" TargetControlID="uoTextBoxPickupTime" 
                WatermarkText="HH:mm" WatermarkCssClass="fieldWatermark">
            </cc1:TextBoxWatermarkExtender>
            <cc1:MaskedEditExtender ID="uoTextBoxPickupTime_MaskedEditExtender" 
                runat="server" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                Mask="99:99" MaskType="Time" TargetControlID="uoTextBoxPickupTime" UserDateFormat="None" 
                UserTimeFormat="TwentyFourHour">
            </cc1:MaskedEditExtender>
        </td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td>&nbsp; Dropoff Date:</td>
        <td>
            <asp:TextBox ID="uoTextBoxDropOffDate" runat="server" Width="300px"></asp:TextBox>
                <cc1:TextBoxWatermarkExtender ID="uoTextBoxDropOffDate_TextBoxWatermarkExtender" 
                    runat="server" Enabled="True" TargetControlID="uoTextBoxDropOffDate" WatermarkCssClass="fieldWatermark" WatermarkText="MM/dd/yyyy">
                </cc1:TextBoxWatermarkExtender>
                    <asp:ImageButton ID="ImageButton2" runat="server" 
                        ImageUrl="~/Images/Calendar_scheduleHS.png" />
                            <cc1:CalendarExtender ID="uoTextBoxDropOffDate_CalendarExtender" runat="server" 
                                Enabled="True" TargetControlID="uoTextBoxDropOffDate" PopupButtonID="ImageButton2" Format="MM/dd/yyyy">
                            </cc1:CalendarExtender>
                                <cc1:MaskedEditExtender ID="uoTextBoxDropOffDate_MaskedEditExtender" 
                                    runat="server" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                                    CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                    CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" MaskType="Date"
                                    TargetControlID="uoTextBoxDropOffDate" Mask="99/99/9999" UserDateFormat="MonthDayYear">
                                </cc1:MaskedEditExtender>
                                    &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="uoTextBoxDropOffDate" ErrorMessage="Drop off date required.">*</asp:RequiredFieldValidator>
        </td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td>&nbsp; Dropoff Time:</td>
        <td>
            <asp:TextBox ID="uoTextBoxDropoffTime" runat="server" Width="300px"></asp:TextBox>
            <cc1:TextBoxWatermarkExtender ID="uoTextBoxDropoffTime_TextBoxWatermarkExtender" 
                runat="server" Enabled="True" TargetControlID="uoTextBoxDropoffTime" 
                WatermarkText="HH:mm" WatermarkCssClass="fieldWatermark">
            </cc1:TextBoxWatermarkExtender>
            <cc1:MaskedEditExtender ID="uoTextBoxDropoffTime_MaskedEditExtender" 
                runat="server" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                Mask="99:99" MaskType="Time" TargetControlID="uoTextBoxDropoffTime"  UserDateFormat="None" 
                UserTimeFormat="TwentyFourHour">
            </cc1:MaskedEditExtender>
        </td>
        <td>
            &nbsp;</td>
    </tr>
     <tr>
        <td>&nbsp Pick Up Location:</td>
        <td>
            <asp:TextBox ID="uoTextBoxPickUpPlace" runat="server" Width="300px"></asp:TextBox>
                <%--&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="uoTextBoxPickUpPlace" ErrorMessage="Pick up location required.">*</asp:RequiredFieldValidator>--%>
        </td>
        <td>
            &nbsp;</td>
    </tr>
      <tr>
        <td>&nbsp Drop off Location:</td>
        <td>
            <asp:TextBox ID="uoTextBoxDropOffPlace" runat="server" Width="300px"></asp:TextBox>
                <%--&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="uoTextBoxDropOffPlace" ErrorMessage="Drop off location required.">*</asp:RequiredFieldValidator>--%>
        </td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td valign=top>&nbsp Remarks:</td>
        <td>
            <asp:TextBox ID="uoTextBoxRemarks" runat="server" Width="300px" 
                TextMode="MultiLine" Height="50px" CssClass=TextBoxInput></asp:TextBox>                           
        </td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td>&nbsp Vehicle Status:</td>
        <td>
            <asp:DropDownList ID="uoDropDownListVehicleStatus" runat="server" width="305px" AutoPostBack=true 
                onselectedindexchanged="uoDropDownListVehicleStatus_SelectedIndexChanged">
                <asp:ListItem Value="Unused">Unused</asp:ListItem>
                <asp:ListItem Value="In Transit">In Transit</asp:ListItem>
                <asp:ListItem Value="Arrived">Arrived</asp:ListItem>
                <asp:ListItem Value="No Show">No Show</asp:ListItem>
                <asp:ListItem Value="Cancelled">Cancelled</asp:ListItem>
            </asp:DropDownList>                
        </td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td></td>
        <td align=center>
            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
            <asp:CheckBox ID="uoCheckBoxBilledToCrew" runat="server" Text="Charge to crew" Visible=false ToolTip="Set charges for vehicle status cancelled."/>
        </td>
        <td align=center>
            &nbsp;</td>
    </tr>     
    <tr>
        <td></td>
        <td>            
            <asp:Button ID="uoButtonSave" runat="server" Text="Save" Width="80"
                onclick="uoButtonSave_Click" style="height: 26px" />                
        </td>
        <td>            
            &nbsp;</td>
    </tr> 
    <tr>
        <td></td>
        <td>
            <asp:HiddenField ID="uoHiddenFieldSeafarerID" runat="server" />
            <asp:HiddenField ID="uoHiddenFieldVehicleBookingID" runat="server" Value="0"/>
            <asp:HiddenField ID="uoHiddenFieldTravelRequestID" runat="server" Value="0"/>
            <asp:HiddenField ID="uoHiddenFieldManualRequestID" runat="server" Value="0"/>
            <asp:HiddenField ID="uoHiddenFieldRecordLocator" runat="server" Value="0"/>            
            <asp:HiddenField ID="uoHiddenFieldStatus" runat="server" Value="0"/>
            <asp:HiddenField ID="uoHiddenFieldRole" runat="server" Value=""/>
            <asp:HiddenField ID="uoHiddenFieldPort" runat="server" Value="0"/>
            <asp:HiddenField ID="uoHiddenFieldPortCountry" runat="server" Value="0"/>
            <asp:HiddenField ID="uoHiddenFieldPortCity" runat="server" Value="0"/>
            <asp:HiddenField ID="uoHiddenFieldPendingId" runat="server" Value="0"/>
            <asp:HiddenField ID="uoHiddenFieldAction" runat="server" />
            <asp:HiddenField ID="uoHiddenFieldCreatedBy" runat="server" />
            <asp:HiddenField ID="uoHiddenFieldCreatedDate" runat="server" />            
        </td>       
        <td>
            &nbsp;</td>       
    </tr>      
</table>
</asp:Content>

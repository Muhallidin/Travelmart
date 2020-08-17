<%@ Page Title="" Language="C#" MasterPageFile="~/Popup.Master" AutoEventWireup="true" CodeBehind="HotelRoomOverrideEdit2.aspx.cs" Inherits="TRAVELMART.HotelRoomOverrideEdit2" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript" src="../FBox/jquery-1.6.1.min.js"></script>   

<script type="text/javascript" language="javascript">
    function validate(key) 
    {
        //getting key code of pressed key
        var keycode = (key.which) ? key.which : key.keyCode;
        //if (((keycode < 48 || keycode > 57) && (keycode < 45 || keycode > 46)) && !isNaN(keycode)) {
        if (keycode >= 48 && keycode <= 57 || keycode == 45) {
            return true;
        }
        else {
            return false;
        }
    }           
</script>
<script type="text/javascript" language="javascript">
     $(document).ready(function() {
         Settings();
     });

     function pageLoad(sender, args) {
         var isAsyncPostback = Sys.WebForms.PageRequestManager.getInstance().get_isInAsyncPostBack();
         if (isAsyncPostback) {
             Settings();
         }
     }

     function SaveRecord(iWithOverflow) {
         var reservedCount = document.getElementById('<%= uoHiddenFieldReservedCount.ClientID %>').value;
         var OverrideOld = document.getElementById('<%= uoLabelOverrideRoomBlocks.ClientID %>').value;
         var OverrideNew = document.getElementById('<%= uoTextBoxOverrideRoomCount.ClientID %>').value;
         var Override = OverrideOld + OverrideNew;
         if (Override < reservedCount) {
             alert("Total room block(s) must not be less than the total no. of reserved room!");
             return false;
         }
         if (OverrideNew < 0) { 
          if (confirm("Confirming this amount will lessen the number of room blocks reserved. Would you like to proceed?") == true)
                 return true;
             else
                 return false;
         }
         else {
             if (iWithOverflow == 1) {
                 if (confirm("New room blocks will automatically be used for Overflow Bookings. Would you like to proceed?") == true)
                     return true;
                 else
                     return false;
             }
             else {
                 return true;          
             }
             
         }
     }
     
     function Settings() {
         $("#<%=uoCheckOverrideBoxTaxInclusive.ClientID %>").click(function() {
             if ($(this).attr('checked')) {
                 $("#<%=uoTextBoxOverrideTax.ClientID %>").removeAttr("disabled");
                 ValidatorEnable(document.getElementById('<%= RequiredFieldValidator_uoTextBoxOverrideTax.ClientID %>'), true);
             }
             else {
                 $("#<%=uoTextBoxOverrideTax.ClientID %>").attr("disabled", true);
                 $("#<%=uoTextBoxOverrideTax.ClientID %>").val("0.0");
                 ValidatorEnable(document.getElementById('<%= RequiredFieldValidator_uoTextBoxOverrideTax.ClientID %>'), false);
             }
         });

         $("#<%=uoDropDownListRoomType.ClientID %>").change(function(ev) {

             BranchID = $("#<%=uoHiddenFieldBranchID.ClientID %>").val();
             RoomTypeID = $("#<%=uoDropDownListRoomType.ClientID %>").val();
             EffectiveDate = $("#<%=uoHiddenFieldDate.ClientID %>").val();

             $.ajax({
                 type: "POST",
                 contentType: "application/json; charset=utf-8",
                 url: "/PageMethods.aspx/GetHotelRoomBlocks",
                 data: "{'BranchID': '" + BranchID + "', 'RoomTypeID': '" + RoomTypeID + "','EffectiveDate': '" + EffectiveDate + "'}",
                 dataType: "json",
                 success: function(data) {
                     //if there is data

                     if (data.d.length > 0) {

                         $("#<%=uoLabelContractRoomCount.ClientID %>").text(data.d[0].ContractRoomBlocks);
                         $("#<%=uoLabelContractCurrency.ClientID %>").text(data.d[0].ContractCurrency);
                         $("#<%=uoLabelContractAmount.ClientID %>").text(data.d[0].ContractRate);
                         $("#<%=uoLabelContractTax.ClientID %>").text(data.d[0].ContractTaxPercent);

                         //$("#<%=uoTextBoxOverrideRoomCount.ClientID %>").val(data.d[0].OverrideRoomBlocks);
                         $("#<%=uoLabelOverrideRoomBlocks.ClientID %>").text(data.d[0].OverrideRoomBlocks);
                         $("#<%=uoTextBoxOverrideAmount.ClientID %>").val(data.d[0].OverrideRate);
                         $("#<%=uoHiddenFieldReservedCount.ClientID %>").val(data.d[0].ReservedRooms);
                         $("#<%=uoDropDownListCurrency.ClientID %>").val(data.d[0].OverrideCurrentcyID);
                         $("#<%=uoTextBoxOverrideTax.ClientID %>").val(data.d[0].OverrideTaxPercent);
                         $("#<%=uoHiddenFieldReservedCount.ClientID %>").val(data.d[0].OverrideReservedRoom);

                         if (data.d[0].OverrideIsTaxInclusive) {
                             $("#<%= uoTextBoxOverrideTax.ClientID %>").removeAttr("disabled");
                         }
                         else {
                             $("#<%= uoTextBoxOverrideTax.ClientID %>").attr("disabled", true);
                         }
                         $("#<%=uoCheckContractBoxTaxInclusive.ClientID %>").attr('checked', data.d[0].ContractIsTaxInclusive);
                         $("#<%=uoCheckOverrideBoxTaxInclusive.ClientID %>").attr('checked', data.d[0].OverrideIsTaxInclusive);
                     }
                     // no data found
                     else {
                         $("#<%=uoLabelContractRoomCount.ClientID %>").text(0);
                         $("#<%=uoLabelContractCurrency.ClientID %>").text(0);
                         $("#<%=uoLabelContractAmount.ClientID %>").text(0);
                         $("#<%=uoLabelContractTax.ClientID %>").text(0);
                         $("#<%=uoCheckContractBoxTaxInclusive.ClientID %>").attr('checked', false);

                         //$("#<%=uoTextBoxOverrideRoomCount.ClientID %>").val(0);
                         $("#<%=uoLabelOverrideRoomBlocks.ClientID %>").text(0);
                         $("#<%=uoTextBoxOverrideAmount.ClientID %>").val(0);
                         $("#<%=uoDropDownListCurrency.ClientID %>").val(0);
                         $("#<%=uoCheckOverrideBoxTaxInclusive.ClientID %>").attr('checked', false);
                         $("#<%= uoTextBoxOverrideTax.ClientID %>").val(0);

                         $("#<%= uoTextBoxOverrideTax.ClientID %>").attr("disabled", true);
                     }
                 },
                 error: function(objXMLHttpRequest, textStatus, errorThrown) {
                     alert(errorThrown);
                 }
             });
         });
    }
 </script>
<div class="PageTitle">
         Hotel Override Room Blocks
</div>
<hr/>  
<table class="TableEditor">      
    <tr>
        <td class="caption">Hotel Branch Name:</td>
        <td class="value">
            <asp:Label ID="uoLabelBranchName" runat="server" Font-Bold="True"></asp:Label>
        </td>
        <td ></td>
    </tr>
    <tr>
        <td class="caption">Room Type:</td>
        <td class="value">
            <asp:DropDownList ID="uoDropDownListRoomType" runat="server"  CssClass="TextBoxInput" Width="205px">
                <asp:ListItem Value="0">--Select Room Type--</asp:ListItem>
                <asp:ListItem Value="1">Single</asp:ListItem>
                <asp:ListItem Value="2">Double</asp:ListItem>
            </asp:DropDownList>
            &nbsp; <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="uoDropDownListRoomType"
            ErrorMessage="Room Type is required." ValidationGroup="Validate" InitialValue="0">*</asp:RequiredFieldValidator>
            <%--<asp:Label ID="uoLabelRoomTypeName" runat="server" ></asp:Label>--%>
        </td>
        <td></td>
    </tr>
     <tr>
        <td class="caption">Date:</td>
        <td class="value">
            <asp:Label ID="uoLabelDate" runat="server" ></asp:Label>
        </td>
        <td></td>
    </tr>
    <tr>
        <td><br /></td>
    </tr>
</table>

<table class="TableEditor">
    <tr>
        <th colspan="3">Contracted Room Block(s)</th>
    </tr>
     <tr>
        <td class="caption">Room Block(s):</td>
        <td class="value">
            <asp:Label ID="uoLabelContractRoomCount" runat="server" ></asp:Label>
        </td>
        <td></td>
    </tr>
    <tr>
        <td class="caption">Amount per day:</td>
        <td class="value">
            <asp:Label ID="uoLabelContractAmount" runat="server"></asp:Label>
        </td>
        <td></td>
    </tr>   
    <tr>
        <td class="caption">Currency:</td>
        <td class="value">
            <asp:Label ID="uoLabelContractCurrency" runat="server"></asp:Label>
        </td>
        <td></td>
    </tr>
    <tr>
        <td class="caption">Room Rate Tax Inclusive :</td>
        <td class="value">
            <asp:CheckBox ID="uoCheckContractBoxTaxInclusive"   runat="server" Checked="True" Enabled="false"/>
        </td>
        <td></td>
    </tr>
    <tr>
        <td class="caption">Room Rate Tax(%) :</td>
        <td class="value">
            <asp:Label ID="uoLabelContractTax" runat="server"></asp:Label>
            
                                    <%--<cc1:MaskedEditExtender ID="uoTextBoxTax_MaskedEditExtender" runat="server" 
                                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                                        CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                        CultureThousandsPlaceholder="" 
                CultureTimePlaceholder="" Enabled="True" 
                                        Mask="999.99" MaskType="Number" 
                TargetControlID="uoTextBoxTax">
                                    </cc1:MaskedEditExtender>--%>
        </td>
        <td></td>
    </tr>
    <tr>
        <td><br /></td>
    </tr>
    <tr>
        <th colspan="3">Additional Room Block(s)</th>
    </tr>
    <tr>
        <td class="caption">Room Block(s) :</td>
        <td class="value">
            <asp:Label ID="uoLabelOverrideRoomBlocks" runat="server"></asp:Label>        
        </td>
    </tr>
    <tr>
        <td><br /></td>
    </tr>
</table>

<table class="TableEditor">        
    <tr>
        <th colspan="3">Add Additional Room Block(s)</th>
    </tr>
     <tr>
        <td class="caption">Room Block(s):</td>
        <td class="value">
            <asp:TextBox ID="uoTextBoxOverrideRoomCount" runat="server" Width="200px" 
                onkeypress="return validate(event);" MaxLength="5"></asp:TextBox>
            &nbsp; <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="uoTextBoxOverrideRoomCount"
            ErrorMessage="Count is required." ValidationGroup="Validate" >*</asp:RequiredFieldValidator>            
        </td>
        <td class="warning">            
        </td>
    </tr>
    <tr>
        <td class="caption">Amount per day:</td>
        <td class="value">
             <asp:TextBox ID="uoTextBoxOverrideAmount" runat="server" Width="200px" onkeypress="return validate(event);"></asp:TextBox>
             &nbsp;  <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="uoTextBoxOverrideAmount"
             ErrorMessage="Amount is required." ValidationGroup="Validate" >*</asp:RequiredFieldValidator>
             <cc1:MaskedEditExtender ID="uoTextBoxOverrideAmount_MaskedEditExtender" 
                        runat="server" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                        CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                        CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                        Mask="99,999.99" MaskType="Number" InputDirection=RightToLeft TargetControlID="uoTextBoxOverrideAmount">
                    </cc1:MaskedEditExtender>
        </td>
        <td class="warning">
            
        </td>
    </tr>   
    <tr>
        <td class="caption">Currency:</td>
        <td class="value">
           <asp:DropDownList ID="uoDropDownListCurrency" runat="server" CssClass="TextBoxInput" Width="205px">
            </asp:DropDownList>
            &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="uoDropDownListCurrency"
            ErrorMessage="Currency is required." ValidationGroup="Validate" InitialValue="0">*</asp:RequiredFieldValidator>
        </td>
        <td class="warning">           
        </td>
    </tr>
    <tr>
        <td class="caption">Room Rate Tax Inclusive :</td>
        <td class="value">
            <asp:CheckBox ID="uoCheckOverrideBoxTaxInclusive"   runat="server" Checked="True" /></td>
        <td>            
        </td>
    </tr>
    <tr>
        <td class="caption">Room Rate Tax(%) :</td>
        <td class="value">
            <asp:TextBox ID="uoTextBoxOverrideTax" runat="server" onkeypress="return validate(event);" Width="200px"></asp:TextBox>
        &nbsp; <asp:RequiredFieldValidator ID="RequiredFieldValidator_uoTextBoxOverrideTax" runat="server" ControlToValidate="uoTextBoxOverrideTax"
             ErrorMessage="Tax is required." ValidationGroup="Validate" >*</asp:RequiredFieldValidator>
             <cc1:MaskedEditExtender ID="uoTextBoxOverrideTax_MaskedEditExtender" 
                        runat="server" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                        CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                        CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                        Mask="999.9" MaskType="Number" InputDirection="RightToLeft" TargetControlID="uoTextBoxOverrideTax">
                    </cc1:MaskedEditExtender>
        </td>
        <td class="warning">
             
        </td>
    </tr>
    <tr>
        <td class="caption">&nbsp;</td>
        <td class="value">
            <asp:Button ID="uoButtonSave" runat="server" Text="Save" 
                onclick="uoButtonSave_Click"  ValidationGroup="Validate" />
        </td>
        <td></td>
    </tr>
</table>

    
    <asp:HiddenField ID="uoHiddenFieldBranchID" runat="server" Value="0" EnableViewState="true"/>
    <%--<asp:HiddenField ID="uoHiddenFieldRoomID" runat="server" Value="0"/>--%>
    <asp:HiddenField ID="uoHiddenFieldDate" runat="server" Value="0" EnableViewState="true"/>    
    <asp:HiddenField ID="uoHiddenFieldUser" runat="server" EnableViewState="true"/>
    <asp:HiddenField ID="uoHiddenFieldReservedCount" runat="server" Value="0" EnableViewState="true"/>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/Popup.Master" AutoEventWireup="true" CodeBehind="HotelRoomEmergencyEdit.aspx.cs" Inherits="TRAVELMART.HotelRoomEmergencyEdit" %>
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
        if (keycode >= 48 && keycode <= 57) {
            return true;
        }
        else {
            return false;
        }
        
//        var keycode = (key.which) ? key.which : key.keyCode;
//        if ((keycode < 48 || keycode > 57) && (keycode < 45 || keycode > 46)) {
//            return false;
//        }
//        else {
//            return true;
//        }
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
     function Settings() {
         $("#<%=uoCheckEmergencyBoxTaxInclusive.ClientID %>").click(function() {
             if ($(this).attr('checked')) {
                 $("#<%=uoTextBoxEmergencyTax.ClientID %>").removeAttr("disabled");
                 ValidatorEnable(document.getElementById('<%= RequiredFieldValidator_uoTextBoxEmergencyTax.ClientID %>'), true);
             }
             else {
                 $("#<%=uoTextBoxEmergencyTax.ClientID %>").attr("disabled", true);
                 $("#<%=uoTextBoxEmergencyTax.ClientID %>").val("0.0");
                 ValidatorEnable(document.getElementById('<%= RequiredFieldValidator_uoTextBoxEmergencyTax.ClientID %>'), false);
             }
         });

         $("#<%=uoDropDownListRoomType.ClientID %>").change(function(ev) {

             BranchID = $("#<%=uoHiddenFieldBranchID.ClientID %>").val();
             RoomTypeID = $("#<%=uoDropDownListRoomType.ClientID %>").val();
             EffectiveDate = $("#<%=uoHiddenFieldDate.ClientID %>").val();

             $.ajax({
                 type: "POST",
                 contentType: "application/json; charset=utf-8",
                 url: "/PageMethods.aspx/GetHotelRoomBlocksEmergency",
                 data: "{'BranchID': '" + BranchID + "', 'RoomTypeID': '" + RoomTypeID + "','EffectiveDate': '" + EffectiveDate + "'}",
                 dataType: "json",
                 success: function(data) {
                     //if there is data
                     if (data.d.length > 0) {

                         $("#<%=uoTextBoxEmergencyRoomCount.ClientID %>").val(data.d[0].RoomBlocks);
                         $("#<%=uoTextBoxEmergencyAmount.ClientID %>").val(data.d[0].Rate);
                         $("#<%=uoDropDownListCurrency.ClientID %>").val(data.d[0].Currency);
                         $("#<%=uoTextBoxEmergencyTax.ClientID %>").val(data.d[0].Tax);

                         if (data.d[0].IsTaxInclusive) {
                             $("#<%= uoTextBoxEmergencyTax.ClientID %>").removeAttr("disabled");
                         }
                         else {
                             $("#<%= uoTextBoxEmergencyTax.ClientID %>").attr("disabled", true);
                         }
                         $("#<%=uoCheckEmergencyBoxTaxInclusive.ClientID %>").attr('checked', data.d[0].IsTaxInclusive);                         
                     }
                     // no data found
                     else {
                         $("#<%=uoTextBoxEmergencyRoomCount.ClientID %>").val(0);
                         $("#<%=uoTextBoxEmergencyAmount.ClientID %>").val(0);
                         $("#<%=uoDropDownListCurrency.ClientID %>").val(0);
                         $("#<%=uoTextBoxEmergencyTax.ClientID %>").val(0);

                         $("#<%= uoTextBoxEmergencyTax.ClientID %>").attr("disabled", true);
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
         Hotel Emergency Room Blocks            
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
    
</table>

<table class="TableEditor">        
    <tr>
        <th colspan="3">&nbsp;</th>
    </tr>
     <tr>
        <td class="caption">Room Block(s):</td>
        <td class="value">
            <asp:TextBox ID="uoTextBoxEmergencyRoomCount" runat="server" Width="200px" onkeypress="return validate(event);"></asp:TextBox>
            &nbsp; <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="uoTextBoxEmergencyRoomCount"
            ErrorMessage="Count is required." ValidationGroup="Validate" >*</asp:RequiredFieldValidator>                        
        </td>
        <td class="warning">            
        </td>
    </tr>
    <tr>
        <td class="caption">Amount per day:</td>
        <td class="value">
             <asp:TextBox ID="uoTextBoxEmergencyAmount" runat="server" Width="200px" onkeypress="return validate(event);"></asp:TextBox>
             &nbsp;  <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="uoTextBoxEmergencyAmount"
             ErrorMessage="Amount is required." ValidationGroup="Validate" >*</asp:RequiredFieldValidator>
             <cc1:MaskedEditExtender ID="uoTextBoxEmergencyAmount_MaskedEditExtender" 
                        runat="server" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                        CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                        CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                        Mask="99,999.99" MaskType="Number" InputDirection=RightToLeft TargetControlID="uoTextBoxEmergencyAmount">
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
            <asp:CheckBox ID="uoCheckEmergencyBoxTaxInclusive"   runat="server" Checked="True" /></td>
        <td>            
        </td>
    </tr>
    <tr>
        <td class="caption">Room Rate Tax(%) :</td>
        <td class="value">
            <asp:TextBox ID="uoTextBoxEmergencyTax" runat="server" onkeypress="return validate(event);" Width="200px"></asp:TextBox>
        &nbsp; <asp:RequiredFieldValidator ID="RequiredFieldValidator_uoTextBoxEmergencyTax" runat="server" ControlToValidate="uoTextBoxEmergencyTax"
             ErrorMessage="Tax is required." ValidationGroup="Validate" >*</asp:RequiredFieldValidator>
             <cc1:MaskedEditExtender ID="uoTextBoxEmergencyTax_MaskedEditExtender" 
                        runat="server" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                        CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                        CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                        Mask="999.9" MaskType="Number" InputDirection=RightToLeft TargetControlID="uoTextBoxEmergencyTax">
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


    <asp:HiddenField ID="uoHiddenFieldBranchID" runat="server" Value="0"/>
    <%--<asp:HiddenField ID="uoHiddenFieldRoomID" runat="server" Value="0"/>--%>
    <asp:HiddenField ID="uoHiddenFieldDate" runat="server" Value="0"/>    
    <asp:HiddenField ID="uoHiddenFieldUser" runat="server"/>
    <asp:HiddenField ID="uoHiddenFieldRoomTypeID" runat="server" Value="0"/>
</asp:Content>

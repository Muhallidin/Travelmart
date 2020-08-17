<%@ Page Title="" Language="C#" MasterPageFile="~/Popup.Master" AutoEventWireup="true" CodeBehind="HotelMaintenance.aspx.cs" Inherits="TRAVELMART.Maintenance.HotelMaintenance" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">    
    <div class="PageTitle">           
        &nbsp;Hotel Brand</div>
    <hr/>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
    <table class="TableEditor">
    <tr>
        <td class="caption">&nbsp Vendor Code:</td>
        <td class="value">
            <asp:TextBox ID="uoTextBoxVendorCode" runat="server" Width="300px"></asp:TextBox>                                          
        </td>        
        <td>
            <asp:RequiredFieldValidator ID="uorfvVendorCode" runat="server" ControlToValidate="uoTextBoxVendorCode" ErrorMessage="Vendor code required." ValidationGroup="Save">*</asp:RequiredFieldValidator>
        </td>
    </tr>    
    <tr>
        <td class="caption">&nbsp Vendor Name:</td>
        <td class="value">
            <asp:TextBox ID="uoTextBoxVendorName" runat="server" Width="300px"></asp:TextBox>                
        </td>
        <td>
            <asp:RequiredFieldValidator ID="uorfvVendorName" runat="server" ControlToValidate="uoTextBoxVendorName" ErrorMessage="Vendor name required." ValidationGroup="Save">*</asp:RequiredFieldValidator>
        </td>
    </tr>  
    <%--<tr>
        <td>&nbsp Hotel Category:</td>
        <td>
            <asp:DropDownList ID="uoDropDownListHotelCategory" runat="server" Width="305px">
                <asp:ListItem Value="">- Select a Hotel Category -</asp:ListItem>
                <asp:ListItem Value="Employee Shipboard">Employee Shipboard</asp:ListItem>
                <asp:ListItem Value="Employee Shoreside">Employee Shoreside</asp:ListItem>
                <asp:ListItem Value="Guests Hotels">Guests Hotels</asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="uorfvHotelCategory" runat="server" 
                    ControlToValidate="uoDropDownListHotelCategory" 
                    ErrorMessage="Hotel category required." InitialValue="" ValidationGroup="Save">*</asp:RequiredFieldValidator>
        </td>
    </tr>--%>
    <tr>
        <td class="caption">&nbsp Vendor Address:</td>
        <td class="value">
            <asp:TextBox ID="uoTextBoxVendorAddress" runat="server" Width="305px" 
                TextMode="MultiLine" CssClass=TextBoxInput></asp:TextBox> 
                <%--&nbsp;<asp:RequiredFieldValidator ID="uorfvVendorAddress" runat="server" ControlToValidate="uoTextBoxVendorAddress" ErrorMessage="Vendor address required." ValidationGroup="Save">*</asp:RequiredFieldValidator>--%>               
        </td>
    </tr>
    <tr>
        <td class="caption">&nbsp; Country:</td>
        <td class="value">
            <asp:DropDownList ID="uoDropDownListCountry" runat="server" Width="305px" AutoPostBack=true
                onselectedindexchanged="uoDropDownListCountry_SelectedIndexChanged">                
            </asp:DropDownList>            
        </td>
        <td>
            <asp:RequiredFieldValidator ID="uorfvCountry" runat="server" 
                    ControlToValidate="uoDropDownListCountry" 
                    ErrorMessage="Country required." InitialValue="" ValidationGroup="Save">*</asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td class="caption">&nbsp; Filter City:</td>
        <td class="value">
              <asp:TextBox ID="uoTextBoxCityName" runat="server" Width="265px"></asp:TextBox>                
              <asp:Button ID="uoButtonFilterCity" runat="server" Text="Filter" 
                  CssClass="SmallButton" onclick="uoButtonFilterCity_Click"/>
        </td>
    </tr>
    <tr>
        <td class="caption">&nbsp; City:</td>
        <td class="value">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>                                
                <asp:DropDownList ID="uoDropDownListCity" runat="server" Width="305px" 
                    onselectedindexchanged="uoDropDownListCity_SelectedIndexChanged">                
                </asp:DropDownList>
            </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="uoButtonFilterCity" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>                      
        </td>
        <td>
          <asp:RequiredFieldValidator ID="uorfvCity" runat="server" 
                ControlToValidate="uoDropDownListCity" 
                ErrorMessage="City required." InitialValue="" ValidationGroup="Save">*</asp:RequiredFieldValidator>
        </td>
    </tr>    
    <tr>
        <td class="caption">&nbsp; Vendor Contact No:</td>
        <td class="value">
            <asp:TextBox ID="uoTextBoxContactNo" runat="server" Width="300px"></asp:TextBox>
            <cc1:MaskedEditExtender ID="uoTextBoxContactNo_MaskedEditExtender" 
                runat="server" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                Mask="(999) 999-9999" MaskType="Number" 
                TargetControlID="uoTextBoxContactNo">
            </cc1:MaskedEditExtender>
        </td>
        <td></td>
    </tr>
    <tr>
        <td class="caption">&nbsp; Vendor Contact Person: </td>
        <td class="value">
            <asp:TextBox ID="uoTextBoxPerson" runat="server" Width="300px"></asp:TextBox>
        </td>
        <td></td>
    </tr>
   <%-- <tr>
        <td class="caption">&nbsp; Vendor Accredited:</td>
        <td class="value">
            <asp:CheckBox ID="uoCheckBoxVendorAccredited" runat="server" Checked=true/>
        </td>
    </tr>    --%>   
    <tr>
        <td colspan="3"></td>
    </tr> 
    <tr>
        <td></td>
        <td>            
            <asp:Button ID="uoButtonSave" runat="server" Text="Save" Width="80"
                onclick="uoButtonSave_Click" ValidationGroup="Save"/>                
        </td>
        <td></td>
    </tr> 
    <tr>
        <td></td>
        <td>            
            <asp:HiddenField ID="uoHiddenFieldVendorIdInt" runat="server" />
            <asp:HiddenField ID="uoHiddenFieldVendorType" runat="server" />
        </td>  
        <td></td>     
    </tr>    
</table>
</asp:Content>



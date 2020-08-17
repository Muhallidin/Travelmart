<%@ Page Language="C#" MasterPageFile="~/Popup.Master" AutoEventWireup="true" CodeBehind="VehicleTypeBranch.aspx.cs" Inherits="TRAVELMART.Maintenance.VehicleTypeBranch" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--<style type="text/css">
        .style1
        {
            width: 135px;
        }
        .style2
        {
            width: 316px;
        }
        .style3
        {
            width: 320px;
        }
    </style>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="PageTitle">
        &nbsp;Vehicle
    </div>
    <hr/>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
    <table style="width: 483px">
        <tr>
                    <td>
                        <table style="width: 100%" class="LeftAligned">                                                      
                            <tr>
                                <td align=left>
                                    Vehicle Type :
                                </td>
                                <td align=left class="style9">
                                    <asp:DropDownList ID="uoDropDownListVehicleType" runat="server" Width="250px" >
                                    </asp:DropDownList>
                                </td>
                                <td align=left class="style10">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" 
                                        ControlToValidate="uoDropDownListVehicleType" ErrorMessage="Vehicle type required." 
                                        InitialValue="0" ValidationGroup="Vehicle">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>     
                            <tr>
                                <td align=left>
                                    Name :
                                </td>
                                <td align=left class="style9">
                                    <asp:TextBox ID="uoTextBoxVehicleTypeName" runat="server" Width="250px"></asp:TextBox>
                                </td>
                                <td align=left class="style10">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                                        ControlToValidate="uoTextBoxVehicleTypeName" ErrorMessage="Vehicle type name required." 
                                        InitialValue="0" ValidationGroup="Vehicle">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>  
                            <tr>
                                <td align=left>
                                    Capacity :
                                </td>
                                <td align=left class="style9">
                                    <asp:TextBox ID="uoTextBoxVehicleCapacity" runat="server" Width="50px"></asp:TextBox>
                                    <cc1:MaskedEditExtender ID="uoTextBoxVehicleCapacity_MaskedEditExtender" 
                                        runat="server" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                                        CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                        CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                                        TargetControlID="uoTextBoxVehicleCapacity" MaskType=Number Mask="99" InputDirection=RightToLeft>
                                    </cc1:MaskedEditExtender>
                                </td>
                                <td align=left class="style10">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                                        ControlToValidate="uoTextBoxVehicleCapacity" ErrorMessage="Vehicle capacity required." 
                                        InitialValue="0" ValidationGroup="Vehicle">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>                                                
                            <tr>                                
                                <td class="style6">
                                    <asp:HiddenField ID="uoHiddenFieldBranchId" runat="server" Value="0"/>
                                </td>                                
                                <td align=left class="style12">
                                    <asp:Button ID="uoButtonSaveVehicleType" runat="server" OnClick="uoButtonSaveVehicleType_Click" 
                                        Text="Save" ValidationGroup="Vehicle" />
                                </td>
                                <td class="style6">
                                    <asp:HiddenField ID="uoHiddenFieldVehicleId" runat="server" Value="0"/>
                                </td> 
                            </tr>                            
                        </table>
                    </td>

                </tr>
    </table>    
    </asp:Content>
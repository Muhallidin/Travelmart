<%@ Page Title="" Language="C#" MasterPageFile="~/Popup.Master" AutoEventWireup="true" CodeBehind="RegionAdd.aspx.cs" Inherits="TRAVELMART.RegionAdd" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style1
        {
            text-align: left;
            vertical-align: middle;
            white-space: nowrap;
            height: 53px;
        }        
        .style5
        {
            text-align:left;
            width: 310px;
            white-space:nowrap;
        }
        .style7
        {
            width: 100px;
            text-align:left;
            white-space:nowrap;
        }        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">   
    <script type="text/javascript" language="javascript">
        function confirmDelete() {
            if (confirm("Delete record?") == true)
                return true;
            else
                return false;
        }
    </script>
    <table  width="100%">
        <tr class="PageTitle">
            <td colspan="2">Region</td>
        </tr>
        <tr>
            <td colspan="2" class=LeftClass>
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
                    ValidationGroup="Save" />
            </td>
        </tr>
        <tr>
            <td class="LeftClass" style="width:15%;">&nbsp Region Name:</td>
            <td class="LeftClass">
                <asp:TextBox ID="uoTextBoxRegionName" runat="server" Width="95%" MaxLength="50"></asp:TextBox>
                <%--<cc1:maskededitextender ID="uoTextBoxRegionName_MaskedEditExtender" 
                                        runat="server" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                                        CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                        CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                                        Mask="999" MaskType="Number" TargetControlID="uoTextBoxRegionName">
                                        </cc1:maskededitextender>--%>
                &nbsp;
                <asp:RequiredFieldValidator ID="uorfvRegionName" runat="server" 
                    ControlToValidate="uoTextBoxRegionName" ErrorMessage="Region Name is required."
                    ValidationGroup="Save">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class=LeftClass style="width:15%;">&nbsp Seaport:</td>
            <td class=LeftClass>
                <asp:DropDownList ID="uoDropDownListSeaport" runat="server" Width="305px">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="uorfvSeaport" runat="server" 
                    ControlToValidate="uoDropDownListSeaport" ErrorMessage="Required Seaport" 
                    InitialValue="0" ValidationGroup="Seaport">*</asp:RequiredFieldValidator>
                <cc1:ListSearchExtender ID="ListSearchExtender_uoDropDownListSeaport" runat="server"
                    TargetControlID="uoDropDownListSeaport" PromptText="Type to search" PromptPosition="Top"
                    IsSorted="true" PromptCssClass="dropdownSearch" /> 
            </td>
        </tr>    
        <tr>
            <td class="style7">
            &nbsp;</td>
            <td class="style5">
                <asp:Button ID="uoButtonAddSeaport" runat="server" onclick="uoButtonAddSeaport_Click"  CssClass="SmallButton"
                Text="Add" ValidationGroup="Seaport" Width="47px" />
            </td>
        </tr>
        <tr>
            <td class="style7">
            &nbsp;</td>
            <td class="style5">
                <asp:GridView ID="uoGridViewSeaport" runat="server" AutoGenerateColumns="False" 
                CssClass="listViewTable" EmptyDataText="No data." 
                onselectedindexchanged="uoGridViewSeaport_SelectedIndexChanged" 
                DataKeyNames="SeaportName" Width="310px">
                    <Columns>
                        <asp:BoundField DataField="RegionSeaportID" HeaderText="RegionSeaportID" />
                        <asp:BoundField DataField="RegionID" HeaderText="Region ID" />
                        <asp:BoundField DataField="SeaportID" HeaderText="Seaport ID" />
                        <asp:BoundField DataField="SeaportName" HeaderText="Seaport Name" />
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="uoLinkButtonDelete" runat="server" CommandName="Select" OnClientClick="javascript:return confirmDelete();">Delete</asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>    
        <tr><td colspan="2"></td></tr>
        <tr><td colspan="2"></td></tr>
        <tr>
            <td colspan="2" align="center">
                <asp:Button ID="uoButtonSave" runat="server" Text="Save" Width="35%"
                    ValidationGroup="Save" onclick="uoButtonSave_Click"/>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="uoHiddenFieldNew" runat="server" />
</asp:Content>

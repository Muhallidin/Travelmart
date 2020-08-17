<%@ Page Title="" Language="C#" MasterPageFile="~/Popup.Master" AutoEventWireup="true"
    CodeBehind="RegionAdd2.aspx.cs" Inherits="TRAVELMART.RegionAdd2" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style5
        {
            text-align: left;
            width: 310px;
            white-space: nowrap;
            vertical-align: top;
        }
        .style7
        {
            width: 100px;
            text-align: left;
            white-space: nowrap;
            vertical-align: top;
        }
    </style>
    
    <script type="text/javascript" language="javascript">
        $(document).ready(function() {
            filterSettings();

        });

        function pageLoad(sender, args) {
            var isAsyncPostback = Sys.WebForms.PageRequestManager.getInstance().get_isInAsyncPostBack();
            if (isAsyncPostback) {
                filterSettings();
            }
        }

        function filterSettings() {
                   
        }
    </script>
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

<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
    <table width="100%">
        <tr class="PageTitle">
            <td colspan="3">
                Region
            </td>
        </tr>
        <tr>
            <td class="LeftClass" colspan="3">
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Save" />
            </td>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td class="style7">
                Region Name:
            </td>
            <td class="style5">
                <asp:TextBox ID="uoTextBoxRegionName" runat="server" Width="301px" MaxLength="50"></asp:TextBox>
            </td>
            <td style="text-align: left">
                <asp:RequiredFieldValidator ID="uorfvRegionName" runat="server" ControlToValidate="uoTextBoxRegionName"
                    ErrorMessage="Region Name is required." ValidationGroup="Save">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="caption">
                Continent:
            </td>
            <td class="value">
                <asp:DropDownList ID="uoDropDownListContinent" runat="server" Width="305px" AutoPostBack="True"
                    OnSelectedIndexChanged="uoDropDownListContinent_SelectedIndexChanged">
                    <asp:ListItem Value="0">--Select Continent--</asp:ListItem>
                </asp:DropDownList>
                <cc1:ListSearchExtender ID="ListSearchExtender_uoDropDownListContinent" runat="server"
                    TargetControlID="uoDropDownListContinent" PromptText="Type to search" PromptPosition="Top"
                    IsSorted="true" PromptCssClass="dropdownSearch" />
            </td>
            <td>
            </td>
            <td class="caption">
                Region Seaport Continent:
            </td>
            <td class="value">
                <asp:DropDownList ID="uoDropDownListContinent2" runat="server" Width="305px" AutoPostBack="True"
                    OnSelectedIndexChanged="uoDropDownListContinent2_SelectedIndexChanged">
                    <asp:ListItem Value="0">--Select Continent--</asp:ListItem>
                </asp:DropDownList>
                <cc1:ListSearchExtender ID="ListSearchExtender1" runat="server" TargetControlID="uoDropDownListContinent2"
                    PromptText="Type to search" PromptPosition="Top" IsSorted="true" PromptCssClass="dropdownSearch" />
            </td>
        </tr>
        <tr>
            <td class="caption">
                Country:
            </td>
            <td class="value">
                <asp:DropDownList ID="uoDropDownListCountry" runat="server" Width="305px" OnSelectedIndexChanged="uoDropDownListCountry_SelectedIndexChanged">
                    <asp:ListItem Value="0">--Select Country--</asp:ListItem>
                </asp:DropDownList>
                <cc1:ListSearchExtender ID="ListSearchExtender_uoDropDownListCountry" runat="server"
                    TargetControlID="uoDropDownListCountry" PromptText="Type to search" PromptPosition="Top"
                    IsSorted="true" PromptCssClass="dropdownSearch" />
            </td>
            <td>
            </td>
            <td class="caption">
                Region Seaport Country:
            </td>
            <td class="value">
                <asp:DropDownList ID="uoDropDownListCountry2" runat="server" Width="305px">
                    <asp:ListItem Value="0">--Select Country--</asp:ListItem>
                </asp:DropDownList>
                <cc1:ListSearchExtender ID="ListSearchExtender2" runat="server" TargetControlID="uoDropDownListCountry2"
                    PromptText="Type to search" PromptPosition="Top" IsSorted="true" PromptCssClass="dropdownSearch" />
            </td>
        </tr>
        <tr>
            <td class="caption">
                Seaport Name:
            </td>
            <td class="value">
                <asp:TextBox ID="uoTextBoxSeaport" runat="server" Width="301px"></asp:TextBox>
            </td>
            <td class="LeftClass">                
            </td>
            <td class="caption">
                Region Seaport Name:
            </td>
            <td class="value">
                <asp:TextBox ID="uoTextBoxSeaport2" runat="server" Width="301px"></asp:TextBox>
            </td>
            <td class="LeftClass">
                
            </td>
        </tr>
        <tr>
            <td></td>
            <td class="LeftClass">
            <asp:Button ID="uoButtonView" runat="server" CssClass="SmallButton" Text="View" ValidationGroup="Seaport"
                    Width="47px" OnClick="uoButtonView_Click" />
            </td>
            <td></td>
            <td></td>
            <td class="LeftClass">
            <asp:Button ID="uoButtonView2" runat="server" CssClass="SmallButton" Text="Filter"
                    ValidationGroup="Seaport" Width="47px" OnClick="uoButtonView2_Click" />
                &nbsp;
                <asp:Button runat="server" CssClass="SmallButton" Text="Remove Filter" ID="uoButtonRemoveFilter"
                    OnClick="uoButtonRemoveFilter_Click" />
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td style="vertical-align: top">
                <asp:ListView runat="server" ID="uoSeaportList">
                    <LayoutTemplate>
                        <table border="0" cellpadding="0" cellspacing="0" class="listViewTable2">
                            <tr>
                                <th>
                                    
                                </th>
                                <th>
                                    Seaport
                                </th>
                            </tr>
                            <asp:PlaceHolder runat="server" ID="itemPlaceHolder"></asp:PlaceHolder>
                        </table>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <tr>
                            <td class="leftAligned">
                                <asp:CheckBox ID="uoCheckBoxSelect" runat="server" />
                            </td>
                            <td class="leftAligned">
                                <asp:HiddenField ID="uoHiddenFieldSeaport" runat="server" Value='<%# Eval("SeaportID") %>' />
                                <asp:HiddenField ID="uoHiddenFieldCountry" runat="server" Value='<%# Eval("CountryID") %>' />
                                <asp:Label ID="uoLabelPort" runat="server" Text='<%# Eval("SeaportName")%>' />
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
                                <td colspan="3" class="leftAligned">
                                    No Record
                                </td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                </asp:ListView>
            </td>
            <td style="vertical-align: top" colspan="2" align="center">
                <table>
                    <tr>
                        <td>
                            <asp:Button ID="uoButtonAdd" runat="server" CssClass="SmallButton" Text="Add >>"
                                ValidationGroup="Seaport" Width="65px" OnClick="uoButtonAdd_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="uoButtonDelete" runat="server" CssClass="SmallButton" Text="<< Remove"
                                ValidationGroup="Seaport" Width="65px" OnClick="uoButtonDelete_Click" />
                        </td>
                    </tr>
                </table>
            </td>
            <td colspan="2" style="vertical-align: top">
                <%--Added list--%>
                <asp:ListView runat="server" ID="uoSeaportAddedList">
                    <LayoutTemplate>
                        <table border="0" cellpadding="0" cellspacing="0" class="listViewTable2">
                            <tr>
                                <th>                                    
                                </th>
                                <th>
                                   Region Seaport
                                </th>
                            </tr>
                            <asp:PlaceHolder runat="server" ID="itemPlaceHolder"></asp:PlaceHolder>
                        </table>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <tr>
                            <td class="leftAligned">
                                <asp:CheckBox ID="uoCheckBoxSelect" runat="server" />
                            </td>
                            <td class="leftAligned">
                                <asp:HiddenField ID="uoHiddenFieldSeaport" runat="server" Value='<%# Eval("SeaportID") %>' />
                                <asp:HiddenField ID="uoHiddenFieldRegionSeaport" runat="server" Value='<%# Eval("RegionSeaportID") %>' />
                                <%# Eval("SeaportName")%>
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
                                <td colspan="3" class="leftAligned">
                                    No Record
                                </td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                </asp:ListView>
            </td>
        </tr>
        <tr>
            <td></td>
            <td>
                <asp:Button ID="uoButtonSave" runat="server" Text="Save"  ValidationGroup="Save"
                    OnClick="uoButtonSave_Click" />
            </td>
            <td></td>
            <td></td>
        </tr>
    </table>
<%--    <table width="100%">
        <tr>            
            <td style="text-align: left">
                <asp:Button ID="uoButtonSave" runat="server" Text="Save"  ValidationGroup="Save"
                    OnClick="uoButtonSave_Click" />
            </td>
        </tr>
    </table>--%>
    <asp:HiddenField runat="server" ID="uoHiddenFieldRegion" Value="0" />
    <asp:HiddenField runat="server" ID="uoHiddenFieldRegionName" Value="" />
    <asp:HiddenField ID="uoHiddenFieldNew" runat="server" />
    
</ContentTemplate>
</asp:UpdatePanel>
    
</asp:Content>

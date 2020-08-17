<%@ Page Title="" Language="C#" MasterPageFile="~/Popup.Master" AutoEventWireup="true"
    CodeBehind="SafeguardMaintenance.aspx.cs" Inherits="TRAVELMART.Maintenance.SafeguardMaintenance" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript" language="javascript">

        function confirmDelete() {
            if (confirm("Remove record?") == true)
                return true;
            else
                return false;
        }
    </script>

    <div class="PageTitle">
        Safeguard Vendor
    </div>
    <hr />
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
    <table width="100%">
        <%--<tr>
        <td>&nbsp Code:</td>
        <td>
            <asp:TextBox ID="uoTextBoxVendorCode" runat="server" Width="300px"></asp:TextBox>
                &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="uoTextBoxVendorCode" ErrorMessage="Vendor code required." ValidationGroup="Save">*</asp:RequiredFieldValidator>                          
        </td>        
    </tr>    --%>
        <tr>
            <td class="LeftClass">
                Name:
            </td>
            <td class="LeftClass" colspan="3">
                <asp:TextBox ID="uoTextBoxVendorName" runat="server" Width="300px"></asp:TextBox>
                &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="uoTextBoxVendorName"
                    ErrorMessage="Vendor name required." ValidationGroup="Save">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="LeftClass">
                Address:
            </td>
            <td class="LeftClass">
                <asp:TextBox ID="uoTextBoxVendorAddress" runat="server" Width="300px" TextMode="MultiLine"
                    CssClass="TextBoxInput"></asp:TextBox>
                <%--&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="uoTextBoxVendorAddress" ErrorMessage="Vendor address required." ValidationGroup="Save">*</asp:RequiredFieldValidator>--%>
            </td>
            <td class="LeftClass" style="vertical-align: bottom">
                Website
            </td>
            <td class="LeftClass" style="vertical-align: bottom">
                <asp:TextBox ID="uoTextBoxWebsite" runat="server" Width="300px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="LeftClass">
                Email To:
            </td>
            <td class="LeftClass">
                <asp:TextBox ID="uoTextBoxEmailTo" runat="server" Width="300px"></asp:TextBox>
            </td>
            <td class="LeftClass">
                Email Cc:
            </td>
            <td class="LeftClass">
                <asp:TextBox ID="uoTextBoxEmailCc" runat="server" Width="300px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="LeftClass">
                Country:
            </td>
            <td class="LeftClass">
                <asp:DropDownList ID="uoDropDownListCountry" runat="server" Width="305px" AutoPostBack="true"
                    AppendDataBoundItems="true" OnSelectedIndexChanged="uoDropDownListCountry_SelectedIndexChanged">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="uoDropDownListCountry"
                    ErrorMessage="Country required." InitialValue="" ValidationGroup="Save">*</asp:RequiredFieldValidator>
            </td>
            <td class="LeftClass">
                Contact Person:
            </td>
            <td class="LeftClass">
                <asp:TextBox ID="uoTextBoxContactPerson" runat="server" Width="300px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="LeftClass">
                City Filter:
            </td>
            <td class="LeftClass">
                <asp:TextBox ID="uoTextBoxCity" runat="server" Width="245px"></asp:TextBox>&nbsp;
                <asp:Button ID="uoButtonFilterCity" runat="server" Text="Filter" CssClass="SmallButton"
                    Width="50px" OnClick="uoButtonFilterCity_Click" />
            </td>
            <td class="LeftClass">
                Contact No:
            </td>
            <td class="LeftClass">
                <asp:TextBox ID="uoTextBoxContactNo" runat="server" Width="300px"></asp:TextBox>
                <cc1:MaskedEditExtender ID="uoTextBoxContactNo_MaskedEditExtender" runat="server"
                    CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                    CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                    CultureTimePlaceholder="" Enabled="True" Mask="(999) 999-9999" MaskType="Number"
                    TargetControlID="uoTextBoxContactNo">
                </cc1:MaskedEditExtender>
            </td>
        </tr>
        <tr>
            <td class="LeftClass">
            </td>
            <td class="LeftClass">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="uoDropDownListCity" runat="server" Width="305px" AppendDataBoundItems="true">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="uoDropDownListCity"
                            ErrorMessage="City required." InitialValue="" ValidationGroup="Save">*</asp:RequiredFieldValidator>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="uoDropDownListCountry" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
            <td class="LeftClass">
                Fax No.
            </td>
            <td class="LeftClass">
                <asp:TextBox ID="uoTextBoxFaxNo" runat="server" Width="300px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <hr />
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <div class="PageTitle">
                    Assigned Seaport
                </div>
                <table>
                    <tr>
                        <td style="width: 50%">
                            <table width="100%" style="text-align: left">
                                <tr>
                                    <%--<td class="contentCaptionOrig">
                            Airport Filter By:
                        </td>
                        <td class="contentValueOrig">
                            <asp:DropDownList ID="uoDropDownListAirportFilter" runat="server" Width="300px">
                                <asp:ListItem Text="By Code" Value="0"></asp:ListItem>
                                <asp:ListItem Text="By Name" Value="1"></asp:ListItem>
                            </asp:DropDownList>
                        </td>--%>
                                    <td class="contentCaptionOrig">
                                        Filter By: &nbsp;
                                    </td>
                                    <td class="contentValueOrig">
                                        <asp:DropDownList ID="uoDropDownListSeaportFilter" runat="server" Width="300px">
                                            <asp:ListItem Text="By Code" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="By Name" Value="1"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <%-- <td class="contentCaptionOrig">
                        </td>
                        <td class="contentValueOrig">
                            <asp:TextBox ID="uoTextBoxAirportFilter" runat="server" Width="295px" CssClass="SmallText"></asp:TextBox>
                            &nbsp; &nbsp; &nbsp;
                            <asp:Button ID="uoButtonAirportFilter" runat="server" Text="Filter" CssClass="SmallButton"
                                Width="50px" OnClick="uoButtonAirportFilter_Click" />
                        </td>--%>
                                    <td class="contentCaptionOrig">
                                    </td>
                                    <td class="contentValueOrig">
                                        <asp:TextBox ID="uoTextBoxSeaportFilter" runat="server" Width="295px" CssClass="SmallText"></asp:TextBox>
                                        &nbsp; &nbsp; &nbsp;
                                        <asp:Button ID="uoButtonSeaportFilter" runat="server" Text="Filter" CssClass="SmallButton"
                                            Width="50px" OnClick="uoButtonSeaportFilter_Click" />
                                        <asp:HiddenField ID="uoHiddenFieldContractID" runat="server" Value="0" />
                                    </td>
                                </tr>
                                <tr>
                                    <%--<td class="contentCaptionOrig">
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
                        </td>--%>
                                    <td class="contentCaptionOrig">
                                        Seaport:
                                    </td>
                                    <td class="contentValueOrig">
                                        <asp:DropDownList ID="uoDropDownListSeaport" runat="server" Width="300px" AppendDataBoundItems="true">
                                        </asp:DropDownList>
                                        &nbsp;
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="Seaport"
                                            ControlToValidate="uoDropDownListSeaport" InitialValue="0" ErrorMessage="Seaport Required">*</asp:RequiredFieldValidator>
                                        &nbsp;
                                        <asp:Button ID="uoButtonSeaportAdd" runat="server" Text="Add" ValidationGroup="Seaport"
                                            CssClass="SmallButton" Width="100px" OnClick="uoButtonSeaportAdd_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td valign="bottom" style="width: 50%">
                            <asp:ListView runat="server" ID="uoListViewSeaport" OnItemDeleting="uoListViewSeaport_ItemDeleting">
                                <LayoutTemplate>
                                    <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
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
                </table>
            </td>
        </tr>
        <tr>
            <td class="LeftClass">
            </td>
            <td class="RightClass" colspan="3">
                <asp:Button ID="uoButtonSave" runat="server" Text="Save" Width="80" OnClick="uoButtonSave_Click"
                    ValidationGroup="Save" />
                <asp:HiddenField ID="uoHiddenFieldSafeguardVendorIdInt" runat="server" />
            </td>
        </tr>
    </table>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/TravelMart.Master" AutoEventWireup="true"
    CodeBehind="MaintenanceView.aspx.cs" Inherits="TRAVELMART.MaintenanceView" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <%--<script type="text/javascript" src="../FBox/jquery-1.6.1.min.js"></script>
    <script type="text/javascript" src="../FBox/fancybox/jquery.mousewheel-3.0.4.pack.js"></script>
    <script type="text/javascript" src="../FBox/fancybox/jquery.fancybox-1.3.4.pack.js"></script>--%>

    <script type="text/javascript" language="javascript">
        function confirmDelete() {
            if (confirm("Delete record?") == true)
                return true;
            else
                return false;
        }
    </script>

    <script type="text/javascript">
        $(document).ready(function() {
            $("a#<%=uoHyperLinkHotelAdd.ClientID %>").fancybox(
                {
                    'width': '45%',
                    'height': '70%',
                    'autoScale': false,
                    'transitionIn': 'fadeIn',
                    'transitionOut': 'fadeOut',
                    'type': 'iframe',
                    'onClosed': function() {
                        var a = $("#<%=uoHiddenFieldPopupHotel.ClientID %>").val();
                        if (a == '1')
                            $("#aspnetForm").submit();
                    }
                });

            $(".HotelLink").fancybox(
                {
                    'width': '45%',
                    'height': '70%',
                    'autoScale': false,
                    'transitionIn': 'fadeIn',
                    'transitionOut': 'fadeOut',
                    'type': 'iframe',
                    'onClosed': function() {
                        var a = $("#<%=uoHiddenFieldPopupHotel.ClientID %>").val();
                        if (a == '1')
                            $("#aspnetForm").submit();
                    }
                });

            $("a#<%=uoHyperLinkVehicleAdd.ClientID %>").fancybox({
                'width': '45%',
                'height': '70%',
                'autoScale': false,
                'transitionIn': 'fadeIn',
                'transitionOut': 'fadeOut',
                'type': 'iframe',
                'onClosed': function() {
                    var a = $("#<%=uoHiddenFieldPopupVehicle.ClientID %>").val();
                    if (a == '1')
                        $("#aspnetForm").submit();
                }

            });
            $(".VehicleLink").fancybox({
                'width': '45%',
                'height': '70%',
                'autoScale': false,
                'transitionIn': 'fadeIn',
                'transitionOut': 'fadeOut',
                'type': 'iframe',
                'onClosed': function() {
                    var a = $("#<%=uoHiddenFieldPopupVehicle.ClientID %>").val();
                    if (a == '1')
                        $("#aspnetForm").submit();
                }

            });
            $("a#<%=uoHyperLinkPortAdd.ClientID %>").fancybox(
                {
                    'width': '35%',
                    'height': '50%',
                    'autoScale': false,
                    'transitionIn': 'fadeIn',
                    'transitionOut': 'fadeOut',
                    'type': 'iframe',
                    'onClosed': function() {
                        var a = $("#<%=uoHiddenFieldPopupPort.ClientID %>").val();
                        if (a == '1')
                            $("#aspnetForm").submit();
                    }
                });

            $(".PortLink").fancybox(
                {
                    'width': '35%',
                    'height': '50%',
                    'autoScale': false,
                    'transitionIn': 'fadeIn',
                    'transitionOut': 'fadeOut',
                    'type': 'iframe',
                    'onClosed': function() {
                        var a = $("#<%=uoHiddenFieldPopupPort.ClientID %>").val();
                        if (a == '1')
                            $("#aspnetForm").submit();
                    }
                });
                
            $(".HotelContract").fancybox({
                'width': '60%',
                'height': '100%',
                'autoScale': false,
                'transitionIn': 'fadeIn',
                'transitionOut': 'fadeOut',
                'type': 'iframe',
                'onClosed': function() {
                    var a = $("#<%=uoHiddenFieldHotelContract.ClientID %>").val();
                    if (a == '1')
                        $("#aspnetForm").submit();
                }

            });

            $(".VehicleContract").fancybox({
                'width': '60%',
                'height': '100%',
                'autoScale': false,
                'transitionIn': 'fadeIn',
                'transitionOut': 'fadeOut',
                'type': 'iframe',
                'onClosed': function() {
                    var a = $("#<%=uoHiddenFieldVehicleContract.ClientID %>").val();
                    if (a == '1')
                        $("#aspnetForm").submit();
                }

            });

        });                       
    </script>

    <table style="width: 100%" cellspacing="0">
        <tr>
            <td class="RightClass">
                <span>Region:</span>
                <asp:DropDownList ID="uoDropDownListRegion" runat="server" Width="250px" AutoPostBack="True"
                    AppendDataBoundItems="True" OnSelectedIndexChanged="uoDropDownListRegion_SelectedIndexChanged">
                </asp:DropDownList>
                <cc1:ListSearchExtender ID="ListSearchExtender_uoDropDownListRegion" runat="server"
                    TargetControlID="uoDropDownListRegion" PromptText="Type to search" PromptPosition="Top"
                    IsSorted="true" PromptCssClass="dropdownSearch" />&nbsp;
            </td>           
        </tr>
    </table>
    <table width="100%" id="uotableHotelVendorList" runat="server">
        <tr>
            <td style="padding-left: 3px; padding-right: 3px;" colspan="2">
                <div>
                    <table style="width: 100%" cellspacing="0">
                        <tr>
                            <td style="width: 30%; text-align: left;" class="PageTitle">
                                <asp:Panel ID="uopanelhotelhead" runat="server">
                                    Hotel Vendor List
                                    <asp:Label ID="uolabelHotel" runat="server" Text="" Font-Size="Small" />
                                </asp:Panel>
                            </td>
                            <td style="text-align: right; background-color: #82ABB8; padding-right: 3px;">
                                <a id="uoHyperLinkHotelAdd" runat="server">
                                    <asp:Button ID="uoBtnHotelAdd" runat="server" Text="Add" Font-Size="X-Small" />
                                </a>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
        <tr>
            <td class="Module" align="right">
                <asp:Panel ID="uopanelHotel" runat="server" CssClass="CollapsiblePanel">
                    <asp:ListView runat="server" ID="uoHotelVendorList" OnItemCommand="uoHotelVendorList_ItemCommand"
                        OnItemDeleting="uoHotelVendorList_ItemDeleting">
                        <LayoutTemplate>
                            <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                                <tr>
                                    <th>
                                        Code
                                    </th>
                                    <th>
                                        Company
                                    </th>
                                    <th>
                                        Address
                                    </th>
                                    <th>
                                        Country
                                    </th>
                                    <th>
                                        City
                                    </th>
                                    <th>
                                        Contact No.
                                    </th>
                                    <th runat="server" style="width: 5%" id="EditTH" />
                                    <th runat="server" style="width: 5%" id="DeleteTH" />
                                </tr>
                                <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                            </table>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <tr>
                                <td class="leftAligned">
                                    <%# Eval("code")%>
                                </td>
                                <td class="leftAligned">
                                  <%-- <%# Eval("company")%>--%>
                                   <a runat="server" class="HotelContract" id="A1" href='<%# "~/ContractManagement/HotelContractView.aspx?vmId=" + Eval("hvID") + "&vmType=" + Eval("colVendorTypeVarchar")%>'>
                                        <%# Eval("company")%></a>
                                </td>
                                <td class="leftAligned">
                                    <%# Eval("address")%>
                                </td>
                                <td class="leftAligned">
                                    <%# Eval("country")%>
                                </td>
                                <td class="leftAligned">
                                    <%# Eval("city")%>
                                </td>
                                <td class="leftAligned">
                                    <%--<%# Eval("contact")%>--%>
                                    <%# String.Format(FormatUSContactNo(Eval("contact")))%>
                                </td>
                                <td>
                                    <a runat="server" class="HotelLink" id="uoAEditHotel" href='<%# "~/Maintenance/HotelMaintenance.aspx?vmId=" + Eval("hvID") + "&vmType=" + Eval("colVendorTypeVarchar") %>'>
                                        Edit</a>
                                </td>
                                <td>
                                    <asp:LinkButton ID="uoLinkButtonDelete" runat="server" OnClientClick="return confirmDelete();"
                                        CommandArgument='<%#Eval("hvID") %>' CommandName="Delete">Delete</asp:LinkButton>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <EmptyDataTemplate>
                            <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                                <tr>
                                    <th>
                                        CODE
                                    </th>
                                    <th>
                                        Company
                                    </th>
                                    <th>
                                        Address
                                    </th>
                                    <th>
                                        Country
                                    </th>
                                    <th>
                                        City
                                    </th>
                                    <th>
                                        Contact No.
                                    </th>
                                </tr>
                                <tr>
                                    <td colspan="6" class="leftAligned">
                                        No Record
                                    </td>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
                    </asp:ListView>
                    <asp:DataPager ID="uoHotelVendorListPager" runat="server" PagedControlID="uoHotelVendorList"
                        PageSize="5" OnPreRender="uoHotelVendorListPager_PreRender">
                        <Fields>
                            <asp:NumericPagerField ButtonType="link" NumericButtonCssClass="PagerClass" />
                            <%-- <asp:NextPreviousPagerField ButtonType="Image"  ButtonCssClass="PagerClass" NextPageImageUrl="~/Images/next.jpg" PreviousPageImageUrl="~/Images/prev.jpg" />--%>
                        </Fields>
                    </asp:DataPager>
                </asp:Panel>
                <cc1:CollapsiblePanelExtender ID="uocollapsibleHotel" runat="server" TargetControlID="uopanelHotel"
                    ExpandControlID="uopanelhotelhead" CollapseControlID="uopanelhotelhead" TextLabelID="uolabelHotel"
                    CollapsedText="(Show List)" ExpandedText="(Hide List)" Collapsed="false" SuppressPostBack="true">
                </cc1:CollapsiblePanelExtender>
            </td>
        </tr>
    </table>
    <table width="100%" id="uotableVehicleVendorList" runat="server">
        <tr>
            <td style="padding-left: 3px; padding-right: 3px;" colspan="2">
                <div>
                    <table style="width: 100%" cellspacing="0">
                        <tr>
                            <td style="width: 30%; text-align: left;" class="PageTitle">
                                <asp:Panel ID="uopanelvehiclehead" runat="server">
                                    Vehicle Vendor List
                                    <asp:Label ID="uolabelVehicle" runat="server" Text="" Font-Size="Small" />
                                </asp:Panel>
                            </td>
                            <td style="text-align: right; background-color: #82ABB8; padding-right: 3px;">
                                <a id="uoHyperLinkVehicleAdd" runat="server">
                                    <asp:Button ID="uoBtnVehicleAdd" runat="server" Text="Add" Font-Size="X-Small" />
                                </a>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
        <tr>
            <td class="Module" align="right">
                <asp:Panel ID="uopanelVehicle" runat="server" CssClass="CollapsiblePanel">
                    <asp:ListView runat="server" ID="uoVehicleVendorList" OnItemCommand="uoVehicleVendorList_ItemCommand"
                        OnItemDeleting="uoVehicleVendorList_ItemDeleting">
                        <LayoutTemplate>
                            <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                                <tr>
                                    <th>
                                        Code
                                    </th>
                                    <th>
                                        Company
                                    </th>
                                    <th>
                                        Address
                                    </th>
                                    <th>
                                        Country
                                    </th>
                                    <th>
                                        City
                                    </th>
                                    <th>
                                        Contact No.
                                    </th>
                                    <th runat="server" style="width: 5%" id="EditTH" />
                                    <th runat="server" style="width: 5%" id="DeleteTH" />
                                </tr>
                                <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                            </table>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <tr>
                                <td class="leftAligned">
                                    <%# Eval("code")%>
                                </td>
                                <td class="leftAligned">
                                    <%--<%# Eval("company")%>--%>
                                    <a runat="server" class="VehicleContract" id="A1" href='<%# "~/ContractManagement/VehicleContractView.aspx?vmId=" + Eval("hvID") + "&vmType=" + Eval("colVendorTypeVarchar")%>'>
                                        <%# Eval("company")%></a>
                                </td>
                                <td class="leftAligned">
                                    <%# Eval("address")%>
                                </td>
                                <td class="leftAligned">
                                    <%# Eval("country")%>
                                </td>
                                <td class="leftAligned">
                                    <%# Eval("city")%>
                                </td>
                                <td class="leftAligned">
                                    <%--<%# Eval("contact")%>--%>
                                    <%# String.Format(FormatUSContactNo(Eval("contact")))%>
                                </td>
                                <td>
                                    <a runat="server" class="VehicleLink" id="uoAEditvehicle" href='<%# "~/Maintenance/VehicleMaintenance.aspx?vmId=" + Eval("hvID") + "&vmType=" + Eval("colVendorTypeVarchar") %>'>
                                        Edit</a>
                                </td>
                                <td>
                                    <asp:LinkButton ID="uoLinkButtonDelete" runat="server" OnClientClick="return confirmDelete();"
                                        CommandArgument='<%#Eval("hvID") %>' CommandName="Delete">Delete</asp:LinkButton>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <EmptyDataTemplate>
                            <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                                <tr>
                                    <th>
                                        CODE
                                    </th>
                                    <th>
                                        Company
                                    </th>
                                    <th>
                                        Address
                                    </th>
                                    <th>
                                        Country
                                    </th>
                                    <th>
                                        City
                                    </th>
                                    <th>
                                        Contact No.
                                    </th>
                                </tr>
                                <tr>
                                    <td colspan="6" class="leftAligned">
                                        No Record
                                    </td>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
                    </asp:ListView>
                    <asp:DataPager ID="uoVehicleVendorListPager" runat="server" PagedControlID="uoVehicleVendorList"
                        PageSize="5" OnPreRender="uoVehicleVendorListPager_PreRender">
                        <Fields>
                            <asp:NumericPagerField ButtonType="link" NumericButtonCssClass="PagerClass" />
                        </Fields>
                    </asp:DataPager>
                </asp:Panel>
                <cc1:CollapsiblePanelExtender ID="uocollapsibleVehicle" runat="server" TargetControlID="uopanelVehicle"
                    ExpandControlID="uopanelvehiclehead" CollapseControlID="uopanelvehiclehead" TextLabelID="uolabelVehicle"
                    CollapsedText="(Show List)" ExpandedText="(Hide List)" Collapsed="True" SuppressPostBack="true">
                </cc1:CollapsiblePanelExtender>
            </td>
        </tr>
    </table>
    <table width="100%" id="uotablePortList" runat="server">
        <tr>
            <td style="padding-left: 3px; padding-right: 3px;" colspan="2">
                <div>
                    <table style="width: 100%" cellspacing="0">
                        <tr>
                            <td style="width: 30%; text-align: left;" class="PageTitle">
                                <asp:Panel ID="uopanelporthead" runat="server">
                                    Port List
                                    <asp:Label ID="uolabelPort" runat="server" Text="" Font-Size="Small" />
                                </asp:Panel>
                            </td>
                            <td style="text-align: right; background-color: #82ABB8; padding-right: 3px;">
                                <a id="uoHyperLinkPortAdd" runat="server">
                                    <asp:Button ID="Button1" runat="server" Text="Add" Font-Size="X-Small" />
                                </a>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
        <tr>
            <td class="Module" align="right">
                <asp:Panel ID="uopanelPort" runat="server" CssClass="CollapsiblePanel">
                    <asp:ListView runat="server" ID="uoPortList" OnItemCommand="uoPortList_ItemCommand"
                        OnItemDeleting="uoPortList_ItemDeleting">
                        <LayoutTemplate>
                            <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                                <tr>
                                    <th>
                                        Port
                                    </th>
                                    <th>
                                        Country
                                    </th>
                                    <th>
                                        City
                                    </th>
                                    <th runat="server" style="width: 10%" id="EditTH" />
                                    <th runat="server" style="width: 10%" id="DeleteTH" />
                                </tr>
                                <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                            </table>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <tr>
                                <td class="leftAligned">
                                    <%# Eval("port")%>
                                </td>
                                <td class="leftAligned">
                                    <%# Eval("country")%>
                                </td>
                                <td class="leftAligned">
                                    <%# Eval("city")%>
                                </td>
                                <td>
                                    <a runat="server" class="PortLink" id="uoAEditHotel" href='<%# "~/Maintenance/PortMaintenance.aspx?vmId=" + Eval("PORTID") %>'>
                                        Edit</a>
                                </td>
                                <td>
                                    <asp:LinkButton ID="uoLinkButtonDelete" runat="server" OnClientClick="return confirmDelete();"
                                        CommandArgument='<%#Eval("PORTID") %>' CommandName="Delete">Delete</asp:LinkButton>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <EmptyDataTemplate>
                            <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                                <tr>
                                    <th>
                                        Port
                                    </th>
                                    <th>
                                        Country
                                    </th>
                                    <th>
                                        City
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
                    <asp:DataPager ID="uoPortListPager" runat="server" PagedControlID="uoPortList" PageSize="5"
                        OnPreRender="uoPortListPager_PreRender">
                        <Fields>
                            <asp:NumericPagerField ButtonType="link" NumericButtonCssClass="PagerClass" />
                        </Fields>
                    </asp:DataPager>
                </asp:Panel>
                <cc1:CollapsiblePanelExtender ID="uocollapsiblePort" runat="server" TargetControlID="uopanelPort"
                    ExpandControlID="uopanelporthead" CollapseControlID="uopanelporthead" TextLabelID="uolabelPort"
                    CollapsedText="(Show List)" ExpandedText="(Hide List)" Collapsed="True" SuppressPostBack="true">
                </cc1:CollapsiblePanelExtender>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="uoHiddenFieldPopupHotel" runat="server" Value="0" />
    <asp:HiddenField ID="uoHiddenFieldPopupVehicle" runat="server" Value="0" />
    <asp:HiddenField ID="uoHiddenFieldPopupPort" runat="server" Value="0" />
    <asp:HiddenField ID="uoHiddenFieldHotelContract" runat="server" Value="0" />
    <asp:HiddenField ID="uoHiddenFieldVehicleContract" runat="server" Value="0" />
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/TravelMartMaster.Master" AutoEventWireup="true"
    CodeBehind="HotelMaintenanceView_old.aspx.cs" Inherits="TRAVELMART.HotelMaintenanceView_old" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <%--<script type="text/javascript" src="../FBox/jquery-1.6.1.min.js"></script>
    <script type="text/javascript" src="../FBox/fancybox/jquery.mousewheel-3.0.4.pack.js"></script>
    <script type="text/javascript" src="../FBox/fancybox/jquery.fancybox-1.3.4.pack.js"></script>

    <script src="../Menu/menu.js" type="text/javascript"></script>        
    --%>
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

        });                       
    </script>

    <table width="100%" id="uotableHotelVendorList" runat="server">
        <tr>
            <td style="padding-left: 3px; padding-right: 3px;" colspan="2">
                <div>
                    <table style="width: 100%" cellspacing="0">
                        <tr>
                            <td style="width: 40%; text-align: left;" class="PageTitle">
                                <asp:Panel ID="uopanelhotelhead" runat="server">
                                    Hotel Brand
                                    <asp:Label ID="uolabelHotel" runat="server" Text="" Font-Size="Small" />
                                </asp:Panel>
                            </td>
                            <td style="text-align: right; background-color: #82ABB8; padding-right: 3px;">                                
                                <a id="uoHyperLinkHotelAdd" runat="server">
                                    <asp:Button ID="uoBtnHotelAdd" runat="server" Text="Add" 
                                    Font-Size="X-Small" />
                                </a>
                            </td>
                        </tr>
                        <tr><td colspan="2"></td></tr>
                        <tr>
                            <td class="LeftClass">
                                <%--<span>Region:</span>--%>
                                <asp:DropDownList ID="uoDropDownListRegion" runat="server" Width="250px" AutoPostBack="True"
                                    AppendDataBoundItems="True" 
                                    OnSelectedIndexChanged="uoDropDownListRegion_SelectedIndexChanged" 
                                    Visible="False">
                                </asp:DropDownList>
                                <cc1:ListSearchExtender ID="ListSearchExtender_uoDropDownListRegion" runat="server"
                                    TargetControlID="uoDropDownListRegion" PromptText="Type to search" PromptPosition="Top"
                                    IsSorted="true" PromptCssClass="dropdownSearch" />
                            </td>
                            <td class="RightClass">
                                &nbsp; <asp:TextBox ID="uoTextBoxSearchParam" runat="server" Font-Size="7.5pt" 
                                    Width="180px" Visible="False" />
                                <asp:Button ID="uoButtonSearch" runat="server" Text="Search" 
                                    Font-Size="X-Small" onclick="uoButtonSearch_Click" Visible="False" />
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
        <tr>
            <td class="Module" align="right">
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
                                    Contact No.
                                </th>
                                <th runat="server" style="width: 5%" id="EditTH" />                               
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
<%--                            <a runat="server" class="HotelContract" id="A1" href='<%# "~/ContractManagement/HotelContractView.aspx?vmId=" + Eval("hvID") + "&vmType=" + Eval("colVendorTypeVarchar") + "&st=" + Request.QueryString["st"] + "&dt=" + Request.QueryString["dt"] + "&ufn=" + Request.QueryString["ufn"]%>'>--%>
                                <%# Eval("COMPANY")%></a>
                            </td>                                 
                            <td class="leftAligned">
                                <%--<%# Eval("contact")%>--%>
                                <%# String.Format(FormatUSContactNo(Eval("contact")))%>
                            </td>
                            <td>
                                <a runat="server" class="HotelLink" id="uoAEditHotel" href='<%# "~/Maintenance/HotelMaintenance.aspx?vmId=" + Eval("hvID") + "&vmType=" + Eval("colVendorTypeVarchar") %>'
                                    visible='<%# (Convert.ToBoolean(uoHiddenFieldVendor.Value)) %>'>
                                    Edit</a>
                            </td>
                           <%-- <td>
                                <asp:LinkButton ID="uoLinkButtonDelete" runat="server" OnClientClick="return confirmDelete();"
                                    CommandArgument='<%#Eval("hvID") %>' CommandName="Delete" >Delete</asp:LinkButton>
                            </td>--%>
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
            </td>
        </tr>
        <tr>
            <td class="LeftClass">
                <asp:DataPager ID="uoHotelVendorListPager" runat="server" PagedControlID="uoHotelVendorList"
                    PageSize="20" OnPreRender="uoHotelVendorListPager_PreRender">
                    <Fields>
                        <asp:NumericPagerField ButtonType="link" NumericButtonCssClass="PagerClass" />
                        <%-- <asp:NextPreviousPagerField ButtonType="Image"  ButtonCssClass="PagerClass" NextPageImageUrl="~/Images/next.jpg" PreviousPageImageUrl="~/Images/prev.jpg" />--%>
                    </Fields>
                </asp:DataPager>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="uoHiddenFieldPopupHotel" runat="server" Value="0" />
    <asp:HiddenField ID="uoHiddenFieldVendor" runat="server" Value="true" />
</asp:Content>

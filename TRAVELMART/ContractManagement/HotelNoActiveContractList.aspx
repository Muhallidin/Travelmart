<%@ Page Title="" Language="C#" MasterPageFile="~/TravelMartMaintenanceMaster.Master"
    AutoEventWireup="true" CodeBehind="HotelNoActiveContractList.aspx.cs" Inherits="TRAVELMART.ContractManagement.HotelNoActiveContractList" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">
    <table width="100%" cellpadding="0" cellspacing="0" class="PageTitle">
        <div class="ViewTitlePadding">
            <tr>
                <td align="Left">
                    Hotel Branch With No Active Contract List
                </td>
            </tr>
    </table>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NaviPlaceHolder" runat="server">
     <script type="text/javascript" language="javascript">
        $(document).ready(function() {
            SetResolution();
        });
        function pageLoad(sender, args) {
            var isAsyncPostback = Sys.WebForms.PageRequestManager.getInstance().get_isInAsyncPostBack();
            if (isAsyncPostback) {
                SetResolution();               
            }
        }
        function SetResolution() {
            var ht = $(window).height();

            var wd = $(window).width() * 0.90;

            if (screen.height <= 600) {
                ht = ht * 0.45;

            }
            else if (screen.height <= 720) {
                ht = ht * 0.60;
            }
            else {
                ht = ht * 0.65;
            }

            $("#Bv").height(ht);
            $("#Bv").width(wd);
        }    
    </script>
    <script type="text/javascript" language="javascript">
        function OpenContract(vendorID) {
            var URLString = "../ContractManagement/HotelContractView.aspx?vId=" + vendorID;

            var screenWidth = screen.availwidth;
            var screenHeight = screen.availheight;

            screenWidth = 800;
            screenHeight = 650;

            window.open(URLString, 'Contract_Management', 'top=10, left=200, width=' + screenWidth + ', height=' + screenHeight + ', scrollbars=yes, menubar=no,toolbar=no,status=no,resizable=yes,addressbar=no');
            return false;
        }
    </script>
    
<div id="Bv" style="overflow: auto; width: 100%; overflow-x: auto; overflow-y: auto;">
    <table width="100%">
        <td class="Module" align="right">
            <asp:ListView runat="server" ID="uoHotelVendorList" OnItemCommand="uoHotelVendorList_ItemCommand"
                OnItemDeleting="uoHotelVendorList_ItemDeleting">
                <LayoutTemplate>
                    <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                        <tr>
                            <th>
                                Branch
                            </th>
                            <%--                                <th>
                                    Address
                                </th>--%>
                            <th>
                                Region
                            </th>
                            <th>
                                Country
                            </th>
                            <th>
                                City
                            </th>
                            <%--                          <th>
                                    Contact No.
                                </th>--%>
                            <%--<th runat="server" style="width: 5%" id="EditTH" />  </th> --%>
                            <%--<th runat="server" style="width: 12%" id="ContractList" />  </th> --%>
                            <th runat="server" style="width: 12%" id="ContractTH" />
                            </th>
                        </tr>
                        <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                    </table>
                </LayoutTemplate>
                <ItemTemplate>
                    <tr>
                        <td class="leftAligned">
                            <%# Eval("colVendorBranchNameVarchar")%>
                        </td>
                        <td class="leftAligned">
                            <%# Eval("colMapNameVarchar")%>
                        </td>
                        <td class="leftAligned">
                            <%# Eval("country")%>
                        </td>
                        <td class="leftAligned">
                            <%# Eval("city")%>
                        </td>
                        <%--                          <td class="leftAligned">
                                <%# String.Format(FormatUSContactNo(Eval("contact")))%>
                            </td>--%>
                        <%--                     <td>
                                <a runat="server" class="HotelLink" id="uoAEditHotel" href='<%# "~/Maintenance/HotelMaintenanceBranch.aspx?vmId=" + Eval("colBranchIDInt") %>'>
                                    Edit</a>
                            </td>
                            <td>
                                <a runat="server" id="A1" href='<%# "~/ContractManagement/HotelContractListView.aspx?vmId=" + Eval("colBranchIDInt") + "&hvID=" + Eval("hvID") + "&hbName=" + Eval("colVendorBranchNameVarchar") + "&st=" + Request.QueryString["st"] + "&dt=" + Request.QueryString["dt"] + "&ufn=" + Request.QueryString["ufn"] %>'>
                                    Contract List</a>
                            </td>--%>
                        <td class='<%# uoHiddenFieldAddContractClass.Value  %>'>
                            <asp:HyperLink runat="server" ID="uoHyperLinkAddContract" NavigateUrl='<%# "~/ContractManagement/HotelContractAdd.aspx?vmId=" + Eval("colBranchIDInt") + "&hvID=" + Eval("hvID") + "&st=" + Request.QueryString["st"] + "&ufn=" + Request.QueryString["ufn"] + "&dt=" + Request.QueryString["dt"] %>'
                                Visible='<%# (Convert.ToBoolean(uoHiddenFieldAddContract.Value)) %>'>
                                    Add Contract</asp:HyperLink>
                        </td>
                        <%--<td>
                                <a runat="server" id="uoAddContract" href='<%# "~/ContractManagement/HotelContractAdd.aspx?vmId=" + Eval("colBranchIDInt") + "&hvID=" + Eval("hvID") + "&st=" + Request.QueryString["st"] + "&ufn=" + Request.QueryString["ufn"] %>'>
                                    Add Contract</a>
                            </td>--%>
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
                                Branch
                            </th>
                            <th>
                                Region
                            </th>
                            <th>
                                Country
                            </th>
                            <th>
                                City
                            </th>
                        </tr>
                        <tr>
                            <td colspan="4" class="leftAligned">
                                No Record
                            </td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
            </asp:ListView>
            <asp:DataPager ID="uoHotelVendorListPager" runat="server" PagedControlID="uoHotelVendorList"
                PageSize="20" OnPreRender="uoHotelVendorListPager_PreRender">
                <Fields>
                    <asp:NumericPagerField ButtonType="link" NumericButtonCssClass="PagerClass" />
                </Fields>
            </asp:DataPager>
            <asp:HiddenField ID="uoHiddenFieldAddContractClass" runat="server" Value="" />
            <asp:HiddenField ID="uoHiddenFieldAddContract" runat="server" Value="true" />
        </td>
        </tr>
    </table>
</div>
</asp:Content>

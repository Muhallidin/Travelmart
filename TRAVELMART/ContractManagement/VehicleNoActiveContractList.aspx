<%@ Page Title="" Language="C#" MasterPageFile="~/TravelmartMaster.Master" AutoEventWireup="true" CodeBehind="VehicleNoActiveContractList.aspx.cs" Inherits="TRAVELMART.ContractManagement.VehicleNoActiveContractList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript" language="javascript">
        function OpenContract(branchID, contractID) {
            var URLString = "../ContractManagement/VehicleContractView.aspx?bId=" + branchID;
                URLString += "&cId=" + contractID;

            var screenWidth = screen.availwidth;
            var screenHeight = screen.availheight;

            screenWidth = 800;
            screenHeight = 650;

            window.open(URLString, 'Contract_Management', 'top=10, left=200, width=' + screenWidth + ', height=' + screenHeight + ', scrollbars=yes, menubar=no,toolbar=no,status=no,resizable=yes,addressbar=no');
            return false;
        }
</script>
<div class="ViewTitlePadding"> 
<table width="100%" cellpadding="0" cellspacing="0" class="PageTitle">
<tr>
<td align="Left">Vehicle Branch With No Active Contract List</td>    
</tr>
</table>
</div>
<table width="100%">
<tr>
<td>
</td>
</tr>
<tr>
<td>
                <asp:ListView runat="server" ID="uoVehicleVendorList" OnItemCommand="uoVehicleVendorList_ItemCommand"
                    OnItemDeleting="uoVehicleVendorList_ItemDeleting">
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
                                <th runat="server" style="width: 12%" id="ContractTH" /> </th>                            
                            </tr>
                            <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                        </table>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <tr>
                            <td class="leftAligned">
<%--                            <a runat="server" class="HotelContract" id="A1" href='<%# "~/ContractManagement/HotelContractView.aspx?vmId=" + Eval("hvID") + "&vmType=" + Eval("colVendorTypeVarchar") + "&st=" + Request.QueryString["st"] + "&dt=" + Request.QueryString["dt"] + "&ufn=" + Request.QueryString["ufn"]%>'>
                                <%# Eval("colVendorBranchNameVarchar")%></a>--%>
                                <%--<asp:LinkButton runat="server" id="uoLinkButtonContract" Text='<%# Eval("colVendorBranchNameVarchar") %>'
                                OnClientClick = '<%# "return OpenContract(\"" + Eval("colBranchIDInt") + "\")" %>'
                                >
                                </asp:LinkButton>--%>
                                <%# Eval("colVendorBranchNameVarchar") %>
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
                            <td>
                                <a runat="server" id="uoAddContract" href='<%# "~/ContractManagement/VehicleContractAdd.aspx?vmId=" + Eval("colBranchIDInt") + "&vvID=" + Eval("vvID") + "&st=" + Request.QueryString["st"] + "&ufn=" + Request.QueryString["ufn"] + "&dt=" + Request.QueryString["dt"] %>'>
                                    Add Contract</a>
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
                                <th></th>                                
                            </tr>
                            <tr>
                                <td colspan="5" class="leftAligned">
                                    No Record
                                </td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                </asp:ListView>
                <asp:DataPager ID="uoVehicleVendorListPager" runat="server" PagedControlID="uoVehicleVendorList"
                    PageSize="20" OnPreRender="uoVehicleVendorListPager_PreRender">
                    <Fields>
                        <asp:NumericPagerField ButtonType="link" NumericButtonCssClass="PagerClass" />
                    </Fields>
                </asp:DataPager>

</td>
</tr>
</table>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/TravelMartMaintenanceMaster.Master"
    AutoEventWireup="true" CodeBehind="HotelContractApproval.aspx.cs" Inherits="TRAVELMART.Maintenance.HotelContractApproval" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">
    <div class="ViewTitlePadding">
        <table width="100%" cellpadding="0" cellspacing="0" class="PageTitle">
            <tr>
                <td align="Left">
                    Hotel Contract For Approval List
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
                ht = ht * 0.70;
            }

            $("#Bv").height(ht);
            $("#Bv").width(wd);
        }    
    </script>

    <script type="text/javascript" language="javascript">
        function OpenContract(vendorID, ContractID) {
            var URLString = "../ContractManagement/HotelContractView.aspx?vId=" + vendorID + "&cId=" + ContractID;

            var screenWidth = screen.availwidth;
            var screenHeight = screen.availheight;

            screenWidth = 800;
            screenHeight = 650;

            window.open(URLString, 'Contract_Management', 'top=10, left=200, width=' + screenWidth + ', height=' + screenHeight + ', scrollbars=yes, menubar=no,toolbar=no,status=no,resizable=yes,addressbar=no');
            return false;
        }
    </script>

    <script type="text/javascript" language="javascript">
        function confirmApprove(IsWithActiveContract, ActiveContractID) {
            if (IsWithActiveContract == 1) {
                var xContinue = confirmReplace(ActiveContractID);
                return xContinue;
                //return false;
            }
            else {
                if (confirm("Confirm approval?") == true)
                    return true;
                else
                    return false;
            }
        }
        function confirmReplace(ActiveContractID) {
            var x = confirm("There is a current live contract.\nIt will be replaced if you proceed.\nDo you want to continue?");
            if (x == true) {
                $("#<%=uoHiddenFieldInactivePrevious.ClientID %>").val("1");
                $("#<%=uoHiddenFieldPrevActiveContractID.ClientID %>").val(ActiveContractID);
                return true;
            }
            else {
                return false;
            }
        }
    </script>
    <div id="Bv" style="overflow: auto; width: 100%; overflow-x: auto; overflow-y: auto;">
    <div class="Module">
        <asp:ListView runat="server" ID="uoHotelVendorList" 
            OnItemCommand="uoHotelVendorList_ItemCommand" 
            onselectedindexchanging="uoHotelVendorList_SelectedIndexChanging">
            <LayoutTemplate>
                <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                    
                        <th>
                            Branch
                        </th>
                        <%--                                <th>
                                    Address
                                </th>
                        <th>
                            Region
                        </th>--%>
                        <th>
                            Country
                        </th>
                        <th>
                            City
                        </th>
                        <th>
                            Status
                        </th>
                        <%--                          <th>
                                    Contact No.
                                </th>--%>
                        <%--<th runat="server" style="width: 5%" id="EditTH" />  </th> --%>
                        <%--<th runat="server" style="width: 12%" id="ContractList" />  </th> --%>
                        <th runat="server" style="width: 12%" id="ContractTH" />
                        </th>
                    
                    <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                </table>
            </LayoutTemplate>
            <ItemTemplate>
                <tr>
                    <td class="leftAligned">
                        <asp:LinkButton runat="server" ID="uoLinkButtonContract" Text='<%# Eval("colVendorBranchNameVarchar") %>'
                            OnClientClick='<%# "return OpenContract(\"" + Eval("colBranchIDInt") + "\", \""+ Eval("colContractIdInt") +"\")" %>'> </asp:LinkButton>
                    </td>
                   <%-- <td class="leftAligned">
                        <%# Eval("colMapNameVarchar")%>
                    </td>--%>
                    <td class="leftAligned">
                        <%# Eval("country")%>
                    </td>
                    <td class="leftAligned">
                        <%# Eval("city")%>
                    </td>
                    <td class="leftAligned">
                        <%# Eval("colContractStatusVarchar")%>
                    </td>
                    <%--                            <td>
                                <a runat="server" id="uoAddContract" href='<%# "~/ContractManagement/HotelContractAdd.aspx?vmId=" + Eval("colBranchIDInt") + "&hvID=" + Eval("colVendorIDInt") + "&st=" + Request.QueryString["st"] + "&dt=" + Request.QueryString["dt"] + "&ufn=" + Request.QueryString["ufn"] %>'>
                                    Approve Contract</a>
                            </td>--%>
                    <td>
                        <asp:LinkButton ID="uoLinkButtonApprove" runat="server" OnClientClick='<%#"return confirmApprove(" + Eval("IsWithActiveContract") +", "+ Eval("ActiveContractID") +");" %>'
                            CommandArgument='<%#Eval("colContractIdInt") %>' CommandName="Select">Approve Contract</asp:LinkButton>
                    </td>
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
                        <th>
                            Status
                        </th>
                        <th>
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
        <div align="left">
            <asp:DataPager ID="uoHotelVendorListPager" runat="server" PagedControlID="uoHotelVendorList"
                PageSize="20" OnPreRender="uoHotelVendorListPager_PreRender">
                <Fields>
                    <asp:NumericPagerField ButtonType="link" NumericButtonCssClass="PagerClass" />
                </Fields>
            </asp:DataPager>
        </div>
        <asp:HiddenField ID="uoHiddenFieldInactivePrevious" runat="server" Value="0" />
        <asp:HiddenField ID="uoHiddenFieldPrevActiveContractID" runat="server" Value="0" />
    </div>
    </div>
    </asp:Content>

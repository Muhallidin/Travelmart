<%@ Page Title="" Language="C#" MasterPageFile="~/TravelMartMaintenanceMaster.Master" AutoEventWireup="true" 
CodeBehind="SafeguardContractApproval.aspx.cs" Inherits="TRAVELMART.Maintenance.SafeguardContractApproval" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">
    <div class="ViewTitlePadding">
        <table width="100%" cellpadding="0" cellspacing="0" class="PageTitle">
            <tr>
                <td align="Left">
                    Safeguard Contract For Approval List
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

    function OpenContract(branchID, contractID) {
        var URLString = "../ContractManagement/VehicleContractView.aspx?bId=" + branchID + "&cId=" + contractID;           

        var screenWidth = screen.availwidth;
        var screenHeight = screen.availheight;

        screenWidth = 800;
        screenHeight = 650;

        window.open(URLString, 'Contract_Management', 'top=10, left=200, width=' + screenWidth + ', height=' + screenHeight + ', scrollbars=yes, menubar=no,toolbar=no,status=no,resizable=yes,addressbar=no');
        return false;
    }
</script>

    <script type="text/javascript" language="javascript">
        function confirmApprove() 
        {
            if (confirm("Confirm approved?") == true)
                return true;
            else
                return false;
        }
  
        function confirmReplace() 
            {
                if (confirm("There is a current live contract, if you continue it will replace the current live contract.") == true)
                    return true;
                else
                    return false;
            }
    </script>

<%--<div class="ViewTitlePadding"> 
<table width="100%" cellpadding="0" cellspacing="0" class="PageTitle">
<tr>
<td align="Left">Vehicle Contract Approval List</td>    
</tr>
</table>
</div>--%>
<div id="Bv" style="overflow: auto; width: 100%; overflow-x: auto; overflow-y: auto;">
        <table width="100%">
        <tr>
        <td>
        </td>
        </tr>
        <tr>
        <td>

                <asp:ListView runat="server" ID="uoVehicleVendorList" OnItemCommand="uoVehicleVendorList_ItemCommand"
                    OnItemDeleting="uoVehicleVendorList_ItemDeleting" 
                    onselectedindexchanged="uoVehicleVendorList_SelectedIndexChanged" 
                    onselectedindexchanging="uoVehicleVendorList_SelectedIndexChanging">
                    <LayoutTemplate>
                        <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                            <tr>
                                <th>
                                    Contract Name
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
                                    Date Created
                                </th>
                                <th runat="server" style="width: 12%" id="ContractTH" /> </th>                            
                            </tr>
                            <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                        </table>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <tr>
                            <td class="leftAligned">
                                <asp:LinkButton runat="server" id="uoLinkButtonContract" Text='<%# Eval("ContractName") %>'
                                OnClientClick = '<%# "return OpenContract(\"" + Eval("colBranchIDInt") + "\", \""+ Eval("colContractIdInt") +"\")" %>'
                                > </asp:LinkButton>
                            </td>                           
                            <td class="leftAligned">
                                <%# Eval("country")%>
                            </td>
                            <td class="leftAligned">
                                <%# Eval("city")%>
                            </td>
                            <td class="leftAligned">
                                <%# Eval("colContractStatusVarchar")%>
                            </td>
                            <td class="leftAligned">
                                <%# String.Format("{0:dd-MMM-yyyy HHmm}", Eval("DateCreated"))%>
                            </td>
                            <td>
                                <asp:LinkButton ID="uoLinkButtonApprove" runat="server" OnClientClick="return confirmApprove();"
                                    CommandArgument='<%#Eval("colContractIdInt") %>' CommandName="Select" >Approve Contract</asp:LinkButton>
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
                                <th></th>
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
        </table>
        <div>
            <table width="100%" style="text-align:left">
                <asp:DataPager ID="uoVehicleVendorListPager" runat="server" PagedControlID="uoVehicleVendorList"
                    PageSize="50" OnPreRender="uoVehicleVendorListPager_PreRender">
                    <Fields>
                        <asp:NumericPagerField ButtonType="link" NumericButtonCssClass="PagerClass" />
                    </Fields>
                </asp:DataPager>
            </table>
        </div>
      <asp:HiddenField ID="uoHiddenFieldUser" runat="server" />
        
</div>
</asp:Content>

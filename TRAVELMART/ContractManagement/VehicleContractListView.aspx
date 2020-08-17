<%@ Page Language="C#" MasterPageFile="~/TravelMartMaintenanceMaster.Master" AutoEventWireup="true" CodeBehind="VehicleContractListView.aspx.cs" Inherits="TRAVELMART.ContractManagement.VehicleContractListView" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" runat="server">
<script type="text/javascript" language="javascript">
    function OpenContract(branchID, contractID) {
        var URLString = "../ContractManagement/VehicleContractView.aspx?";
        URLString += "bId=" + branchID + "&cId=" + contractID;

        var screenWidth = screen.availwidth;
        var screenHeight = screen.availheight;

        screenWidth = 800;
        screenHeight = 650;

        window.open(URLString, 'Contract_Management', 'top=10, left=200, width=' + screenWidth + ', height=' + screenHeight + ', scrollbars=yes, menubar=no,toolbar=no,status=no,resizable=yes,addressbar=no');
        return false;
    }
</script>

    <script type="text/javascript" language="javascript">
        function confirmDelete() {
            if (confirm("Confirm inactive?") == true)
                return true;
            else
                return false;
        }
    </script>       
    
    <%--<script type="text/javascript" language="javascript">
        function confirmDelete() {
            if (confirm("Delete record?") == true)
                return true;
            else
                return false;
        }
    </script>--%>

    <%--<script type="text/javascript">
        $(document).ready(function() {
        $("a#<%=uoHyperLinkVehicleAdd.ClientID %>").fancybox(
            {
                'width': '60%',
                'height': '120%',
                'autoScale': false,
                'transitionIn': 'fadeIn',
                'transitionOut': 'fadeOut',
                'type': 'iframe',
                'onClosed': function()
                    {
                        var a = $("#<%=uoHiddenFieldPopupVehicleContract.ClientID %>").val();
                        if (a == '1')
                             $("#aspnetForm").submit();
                    }
            });
            
            $(".VehicleContractLink").fancybox(
                {
                    'width': '60%',
                    'height': '120%',
                    'autoScale': false,
                    'transitionIn': 'fadeIn',
                    'transitionOut': 'fadeOut',
                    'type': 'iframe',
                    'onClosed': function()
                        {
                            var a = $("#<%=uoHiddenFieldPopupVehicleContract.ClientID %>").val();
                            if (a == '1')
                                $("#aspnetForm").submit();
                        }
                });

        });                       
    </script>--%>
    <%--<script type="text/javascript" language="javascript">
        function OpenContract(branchID, contractID) {
        var URLString = "../ContractManagement/VehicleContractView.aspx?bId=";
        URLString += branchID;
        URLString += "&cId=" + contractID;

        var screenWidth = screen.availwidth;
        var screenHeight = screen.availheight;

        screenWidth = 800;
        screenHeight = 650;

        window.open(URLString, 'Contract_Management', 'top=10, left=200, width=' + screenWidth + ', height=' + screenHeight + ', scrollbars=yes, menubar=no,toolbar=no,status=no,resizable=yes,addressbar=no');
        return false;
    }
</script>--%>
     <table style="width: 100%" cellspacing="0" >  <%--id="uoTableVehicleContractList" runat="server"--%>   
        <tr>
            <td style="padding-left: 3px; padding-right: 3px;" colspan="2">
                <div>
                    <table style="width: 100%" cellspacing="0">                        
                        <tr>
                            <td style="text-align: left;" class="PageTitle" colspan="3">                                
                                <asp:Label ID="ucLabelVehicle" runat="server" Font-Bold="True" Font-Size="12pt" 
                                    Font-Names="Calibri"></asp:Label>
                                <asp:Label ID="uoLabelVehicle" runat="server" Text="" Font-Size="Small" />                            
                            </td>                            
                            <%--<td style="text-align: right; background-color: #82ABB8; padding-right: 3px;">                                
                                <a id="uoHyperLinkVehicleAdd" runat="server">
                                    <asp:Button ID="uoBtnVehicleAdd" runat="server" Text="Add" 
                                    Font-Size="X-Small" onclick="uoBtnVehicleAdd_Click" Visible=false />
                                </a>
                            </td>--%>
                        </tr>
                        <tr>
                            <td class="LeftClass" style="width: 230px; white-space:nowrap">
                            <span>Group By :</span>
                                <asp:DropDownList ID="upDropDownListSort" runat="server" Width="200px" 
                                    AutoPostBack="True" 
                                    onselectedindexchanged="upDropDownListSort_SelectedIndexChanged">
                                    <asp:ListItem Value="colIsActiveBit">Active</asp:ListItem>
                                    <asp:ListItem Value="CONTRACTNAME">Contract Title</asp:ListItem>
                                    <asp:ListItem Value="CONTRACTSTARTDATE">Contract Start Date</asp:ListItem>
                                    <asp:ListItem Value="CONTRACTENDDATE">Contract End Date</asp:ListItem>
                                    <asp:ListItem Value="CONTRACTSTATUS">Status</asp:ListItem></asp:DropDownList>
                            </td>
                            <td  class="LeftClass">
                                <asp:Button ID="uoButtonBack" runat="server" Text="Back" CssClass="SmallButton" OnClick="uoButtonBack_Click" Width="100px"/> 
                            </td>                          
                        </tr>                        
                    </table>
                </div>
            </td>
        </tr>
        <tr>
            <td class="Module" align="right">
                <asp:ListView runat="server" ID="uoVehicleContractList" 
                    onselectedindexchanging="uoVehicleContractList_SelectedIndexChanging" 
                    onitemcommand="uoVehicleContractList_ItemCommand"  >
                    <LayoutTemplate>
                        <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                            <tr>
                                <th>
                                    Contract Title
                                </th>                               
                                <%--<th>
                                    Vehicle Brand
                                </th>--%>
                                <th>
                                    Contract Start Date
                                </th>
                                <th>
                                    Contract End Date
                                </th>
                                <th>
                                    Status
                                </th>  
                                <th>
                                    Active
                                </th>                                 
                                <th runat="server" style="width:30px; white-space:nowrap" id="AmendTh" class='<%# Eval("CssContractAmendVisible") %>' > Amend Contract </th>   
                                <th runat="server" style="width:30px; white-space:nowrap" id="DeleteTh" class='<%# Eval("CssContractAmendVisible") %>' > Delete Contract </th>  
                                <th>
                                    Created Date
                                </th>                              
                            </tr>
                            <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                        </table>
                    </LayoutTemplate>
                    <ItemTemplate>
                    <%# ContractAddGroup()%>
                        <tr>
                            <td class="leftAligned">
                                <%--<a runat="server" class="VehicleContractLink" id="uoViewVehicle" href='<%# "~/ContractManagement/VehicleContractView.aspx?vId=" + Eval("vId") + "&st=" + Request.QueryString["st"] + "&dt=" + Request.QueryString["dt"] + "&ufn=" + Request.QueryString["ufn"] %>'>
                                   <%# Eval("contractname")%> </a>   --%>                             
                                <asp:LinkButton runat="server" id="uoLinkButtonContract" Text='<%# Eval("contractname") %>'
                                OnClientClick = '<%# "return OpenContract(\"" + Eval("BranchID") + "\",\"" + Eval("ContractID")  + "\");" %>'
                                >
                                </asp:LinkButton>
                            </td>                           
                            <%--<td class="leftAligned">
                                <%# Eval("colVendorBranchNameVarchar")%>
                            </td>--%>
                            <td class="leftAligned">
                                <%--<%# Eval("contractstartdate")%>--%>
                                <%# String.Format("{0:dd-MMM-yyyy}", Eval("ContractDateStart"))%>
                            </td>
                            <td class="leftAligned">
                                <%--<%# Eval("contractenddate")%>--%>
                                <%# String.Format("{0:dd-MMM-yyyy}", Eval("ContractDateEnd"))%>
                            </td>
                            <td class="leftAligned">
                             <%# Eval("ContractStatus")%> 
                            </td>
                            <td class="leftAligned">
                             <%# Eval("IsActive")%> 
                            </td>
                            <td class='<%# Eval("CssContractAmendVisible") %>' >
                               <asp:HyperLink runat="server" id="uoHyperLinkAmendContract" NavigateUrl='<%# "~/ContractManagement/VehicleContractAdd.aspx?vmId=" + Eval("BranchID") + "&cID=" + Eval("ContractID") + "&ufn=" + Request.QueryString["ufn"] + "&dt=" + Request.QueryString["dt"] %>'>
                                    Amend Contract</asp:HyperLink>  
                            </td> 
                           <td class='<%# Eval("CssContractAmendVisible") %>' >
                                <asp:LinkButton ID="uoLinkButtonDelete" runat="server" Enabled='<%# !InactiveControl( Eval("IsCurrent")) %>' OnClientClick="return confirmDelete();"
                                    CommandArgument='<%#Eval("ContractID") %>' CommandName="Select" >Inactive Contract</asp:LinkButton>
                            </td>
                            <td class="leftAligned">
                                <%# String.Format("{0:dd-MMM-yyyy HHmm}", Eval("DateCreated"))%>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <EmptyDataTemplate>
                        <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                            <tr>
                                <th>
                                    Contract Title
                                </th>                               
                                <%--<th>
                                    Vehicle Brand
                                </th>--%>
                                <th>
                                    Contract Start Date
                                </th>
                                <th>
                                    Contract End Date
                                </th>  
                                <th>
                                    Status
                                </th>  
                                <th>
                                    Active
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
                <asp:DataPager ID="uoVehicleContractListPager" runat="server" PagedControlID="uoVehicleContractList"
                    PageSize="20" OnPreRender="uoVehicleContractList_PreRender">
                    <Fields>
                        <asp:NumericPagerField ButtonType="link" NumericButtonCssClass="PagerClass" />
                    </Fields>
                </asp:DataPager>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="uoHiddenFieldPopupVehicleContract" runat="server" Value="0" />
    <asp:HiddenField ID="uoHiddenFieldUser" runat="server" />    
</asp:Content>

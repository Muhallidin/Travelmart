<%@ Page Language="C#" MasterPageFile="~/TravelMartMaintenanceMaster.Master" AutoEventWireup="true" CodeBehind="SafeguardContractListView.aspx.cs" Inherits="TRAVELMART.ContractManagement.SafeguardContractListView" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" runat="server">

    <script type="text/javascript" language="javascript">
    function OpenContract(branchID, contractID) {
        var URLString = "../ContractManagement/SafeguardContractView.aspx?";
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
                            <td style="width: 30%; text-align: left;" class="PageTitle">
                                <asp:Panel ID="uoPanelVehicleHead" runat="server" Width="364px">
                                    <%--Vehicle Contract List--%>
                                    <asp:Label ID="ucLabelSafeguard" runat="server" Font-Bold="True" Font-Size="12pt" 
                                        Font-Names="Calibri"></asp:Label>
                                   <%-- <asp:Label ID="uoLabelVehicle" runat="server" Text="" Font-Size="Small" />--%></asp:Panel>
                            </td>
                            <td style="text-align: right; background-color: #82ABB8; padding-right: 3px;">                                
                                <a id="uoHyperLinkVehicleAdd" runat="server">
                                    <asp:Button ID="uoBtnVehicleAdd" runat="server" Text="Add" 
                                    Font-Size="X-Small" onclick="uoBtnVehicleAdd_Click" Visible=false />
                                </a>
                            </td>
                        </tr>
                        <tr>
                            <td align=left>
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
                             <td class="RightClass">
                                <%--<span>Contract Title:</span>--%>
                                    <asp:TextBox ID="uoTextBoxVehicle" runat="server" Width="200" CssClass="TextBoxInput" Visible="False"></asp:TextBox>
                                        <asp:Button ID="uoButtonSearch" runat="server" CssClass="SmallButton"  Text="Search" onclick="uoButtonSearch_Click" Visible="False" />
                            </td> 
                        </tr>
                        <%--<tr><td colspan="2"></td></tr>--%>
                         <%--<tr>
                            <td class="LeftClass">
                                <span>Region:</span>
                                <asp:DropDownList ID="uoDropDownListRegion" runat="server" Width="250px" AutoPostBack="True"
                                    AppendDataBoundItems="True" OnSelectedIndexChanged="uoDropDownListRegion_SelectedIndexChanged">
                                </asp:DropDownList>
                                <cc1:ListSearchExtender ID="ListSearchExtender_uoDropDownListRegion" runat="server"
                                    TargetControlID="uoDropDownListRegion" PromptText="Type to search" PromptPosition="Top"
                                    IsSorted="true" PromptCssClass="dropdownSearch" />
                            </td>
                            <td class="RightClass">
                                Vehicle Name : <asp:TextBox ID="uoTextBoxSearchParam" runat="server" Font-Size="7.5pt" Width="180px" />
                                <asp:Button ID="uoButtonSearch" runat="server" Text="Search" Font-Size="X-Small" />
                            </td>
                        </tr>--%>
                    </table>
                </div>
            </td>
        </tr>
        <tr>
            <td class="Module" align="right">
                <asp:ListView runat="server" ID="uoSafeguardContractList" 
                    onselectedindexchanging="uoSafeguardContractList_SelectedIndexChanging" 
                    onitemcommand="uoSafeguardContractList_ItemCommand" >
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
                                <th runat="server" style="width: 12%" id="AmendTh" /> </th>   
                                <th runat="server" style="width: 12%" id="DeleteTh" /> </th>  
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
                            <td >
                               <asp:HyperLink runat="server" id="uoHyperLinkAmendContract" NavigateUrl='<%# "~/ContractManagement/SafeguardContractAdd.aspx?vmId=" + Eval("BranchID") + "&cID=" + Eval("ContractID") + "&ufn=" + Request.QueryString["ufn"] + "&dt=" + Request.QueryString["dt"] %>'>
                                    Amend Contract</asp:HyperLink>  
                            </td> 
                           <td>
                                <asp:LinkButton ID="uoLinkButtonDelete" runat="server" Enabled='<%# !InactiveControl( Eval("IsCurrent")) %>' OnClientClick="return confirmDelete();"
                                    CommandArgument='<%#Eval("ContractID") %>' CommandName="Select" >Inactive 
                                Contract</asp:LinkButton>
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
                <asp:DataPager ID="uoSafeguardContractListPager" runat="server" PagedControlID="uoSafeguardContractList"
                    PageSize="20" OnPreRender="uoSafeguardContractList_PreRender">
                    <Fields>
                        <asp:NumericPagerField ButtonType="link" NumericButtonCssClass="PagerClass" />
                    </Fields>
                </asp:DataPager>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="uoHiddenFieldPopupVehicleContract" runat="server" Value="0" />
</asp:Content>

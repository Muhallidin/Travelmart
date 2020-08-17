<%@ Page Language="C#" MasterPageFile="~/TravelMartMaster.Master" AutoEventWireup="true" CodeBehind="VehicleMaintenanceBranchView.aspx.cs" Inherits="TRAVELMART.Maintenance.VehicleMaintenanceBranchView" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript" src="../FBox/jquery-1.6.1.min.js"></script>
    <script type="text/javascript" src="../FBox/fancybox/jquery.mousewheel-3.0.4.pack.js"></script>
    <script type="text/javascript" src="../FBox/fancybox/jquery.fancybox-1.3.4.pack.js"></script>
    
    <script src="../Menu/menu.js" type="text/javascript"></script>        
    
    <script type="text/javascript" language="javascript">
        function confirmDelete() {
            if (confirm("Delete record?") == true)
                return true;
            else
                return false;
        }
    </script>

    <%--<script type="text/javascript">
        $(document).ready(function() {
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

        });                       
    </script>--%>
    
    <script type="text/javascript" language="javascript">
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
    </script>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">   
    <ContentTemplate>
    <table width="100%" id="uotableVehicleVendorList" runat="server">
        <tr>
            <td style="padding-left: 3px; padding-right: 3px;" colspan="2">
                <div>
                    <table style="width: 100%" cellspacing="0">
                        <tr>
                            <td style="width: 40%; text-align: left;" class="PageTitle">
                                <asp:Panel ID="uopanelvehiclehead" runat="server" Width="305px">
                                    Vehicle Branch List
                                    <asp:Label ID="uolabelVehicle" runat="server" Text="" Font-Size="Small" />
                                </asp:Panel>
                            </td>
                            <td style="text-align: right; background-color: #82ABB8; padding-right: 3px;">                                
                                <a id="uoHyperLinkVehicleAdd" runat="server">
                                    <asp:Button ID="uoButtonVehicleAdd" runat="server" Text="Add Vehicle Branch" Font-Size="X-Small" 
                                    onclick="uoButtonVehicleAdd_Click"/>
                                </a>
                            </td>
                        </tr>
                        <tr><td colspan="2"></td></tr>
                         <tr>
                            <td class="LeftClass">
                                <%--<span>Region:</span>--%>
                                <asp:DropDownList ID="uoDropDownListRegion" runat="server" Width="250px" AutoPostBack="True"
                                    AppendDataBoundItems="True" OnSelectedIndexChanged="uoDropDownListRegion_SelectedIndexChanged" Visible="False">
                                </asp:DropDownList>
                                <cc1:ListSearchExtender ID="ListSearchExtender_uoDropDownListRegion" runat="server"
                                    TargetControlID="uoDropDownListRegion" PromptText="Type to search" PromptPosition="Top"
                                    IsSorted="true" PromptCssClass="dropdownSearch" />
                            </td>
                            <td class="RightClass">
                                Vehicle Branch: <asp:TextBox ID="uoTextBoxSearchParam" runat="server" Font-Size="7.5pt" Width="180px" />
                                <asp:Button ID="uoButtonSearch" runat="server" Text="Search" Font-Size="X-Small" />
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
        <tr>
            <td class="Module" align="right">
                <asp:ListView runat="server" ID="uoVehicleVendorList" OnItemCommand="uoVehicleVendorList_ItemCommand"
                    OnItemDeleting="uoVehicleVendorList_ItemDeleting">
                    <LayoutTemplate>
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
                                <th runat="server" style="width: 5%" id="EditTH" />
                                <th runat="server" style="width: 12%" id="ContractListTH" />  </th> 
                                <th runat="server" style="width: 12%" id="ContractTH" /> </th>   
                            </tr>
                            <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                        </table>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <tr>
                            <td class="leftAligned">
                                <%--<%# Eval("colVendorBranchNameVarchar")%>--%>
                                <asp:LinkButton runat="server" id="uoLinkButtonContract" Text='<%# Eval("colVendorBranchNameVarchar")%>'
                                OnClientClick = '<%# "return OpenContract(\"" + Eval("colBranchIDInt") + "\",\"" + Eval("colContractIdInt") + "\");" %>' 
                                Visible ='<%# Eval("IsWithContract") %>'>
                                </asp:LinkButton>
                                <asp:Label ID="uoLabelVendor" runat="server" Text='<%# Eval("colVendorBranchNameVarchar") %>' Visible ='<%# !(Boolean)Eval("IsWithContract") %>'></asp:Label>
                            </td>
                            <%--<td class="leftAligned">
                                <%# Eval("address")%>
                            </td>--%>
                            <td class="leftAligned">
                                <%# Eval("colMapNameVarchar")%>
                            </td>
                            <td class="leftAligned">
                                <%# Eval("country")%>
                            </td>
                            <td class="leftAligned">
                                <%# Eval("city")%>
                            </td>
                            <%--<td class="leftAligned">                                
                                <%# String.Format(FormatUSContactNo(Eval("contact")))%>
                            </td>--%>
                            <td  class = '<%# uoHiddenFieldVendorClass.Value  %>'>
                                <a runat="server" class="VehicleLink" id="uoAEditvehicle" href='<%# "~/Maintenance/VehicleMaintenanceBranch.aspx?vmId=" + Eval("colBranchIDInt") + "&vmType=" + Eval("colVendorTypeVarchar") + "&st=" + Request.QueryString["st"] + "&ufn=" + Request.QueryString["ufn"] + "&dt=" + Request.QueryString["dt"] %>'
                                visible='<%# (Convert.ToBoolean(uoHiddenFieldVendor.Value)) %>'>  
                                    Edit</a>
                            </td>
                           <td class = '<%# uoHiddenFieldViewContractClass.Value  %>'>
                                <a runat="server" visible='<%# (Convert.ToBoolean(uoHiddenFieldViewContract.Value)) %>' id="A1" href='<%# "~/ContractManagement/VehicleContractListView.aspx?vmId=" + Eval("colBranchIDInt") + "&vvID=" + Eval("vvID") + "&vbName=" + Eval("colVendorBranchNameVarchar") + "&st=" + Request.QueryString["st"] + "&ufn=" + Request.QueryString["ufn"] + "&dt=" + Request.QueryString["dt"] %>'>
                                    Contract List</a>
                            </td>
                            <td class = '<%# uoHiddenFieldEditAddContractClass.Value  %>'>
                                <a runat="server" id="uoAddContract" href='<%# "~/ContractManagement/VehicleContractAdd.aspx?vmId=" + Eval("colBranchIDInt") + "&vvID=" + Eval("vvID") + "&st=" + Request.QueryString["st"] + "&ufn=" + Request.QueryString["ufn"] + "&dt=" + Request.QueryString["dt"] %>'
                                 visible='<%# (Convert.ToBoolean(uoHiddenFieldEditAddContract.Value)) %>'>
                                    Add Contract</a>
                            </td>
                           <%-- <td>
                                <asp:LinkButton ID="uoLinkButtonDelete" runat="server" OnClientClick="return confirmDelete();"
                                    CommandArgument='<%#Eval("hvID") %>' CommandName="Delete">Delete</asp:LinkButton>
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
                                <td colspan="6" class="leftAligned">
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
    </ContentTemplate>
        <Triggers>                            
            <asp:AsyncPostBackTrigger ControlID="uoVehicleVendorListPager" 
                EventName="PreRender" />
        </Triggers>    
    </asp:UpdatePanel>
    <asp:HiddenField ID="uoHiddenFieldPopupVehicle" runat="server" Value="0" />
    
    <asp:HiddenField ID="uoHiddenFieldVendor" runat="server" Value="true" />
    <asp:HiddenField ID="uoHiddenFieldVendorClass" runat="server" Value="" />
    
    <asp:HiddenField ID="uoHiddenFieldViewContract" runat="server" Value="true" />
    <asp:HiddenField ID="uoHiddenFieldViewContractClass" runat="server" Value="" />
    
    <asp:HiddenField ID="uoHiddenFieldEditAddContract" runat="server" Value="true" />
    <asp:HiddenField ID="uoHiddenFieldEditAddContractClass" runat="server" Value="" />
</asp:Content>
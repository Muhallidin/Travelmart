<%@ Page Title="" Language="C#" MasterPageFile="~/TravelMartMaintenanceMaster.Master" AutoEventWireup="True"
    CodeBehind="PortAgentMaintenanceView.aspx.cs" Inherits="TRAVELMART.PortAgentMaintenanceView" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" runat="server">

    <script type="text/javascript" language="javascript">
        function confirmDelete() {
            if (confirm("Delete record?") == true)
                return true;
            else
                return false;
        }
    </script>

    <script type="text/javascript">
        Sys.Application.add_load(function() {
            SetBranchResolution();
            filterSettings();
        });

        function setupPopup() {
//            $(".VehicleLink").fancybox({
//                'width': '70%',
//                'height': '90%',
//                'autoScale': false,
//                'transitionIn': 'fadeIn',
//                'transitionOut': 'fadeOut',
//                'type': 'iframe',
//                'onClosed': function() {
//                var a = $("#<%=uoHiddenFieldPopupPortAgent.ClientID %>").val();
//                    if (a == '1')
//                        $("#aspnetForm").submit();
//                }
//            });
        }
        function SetBranchResolution() {
            var ht = $(window).height(); //550;
            var ht2 = $(window).height(); //550;
            var wd = $(window).width() * 0.90;
            if (screen.height <= 600) {
                ht = ht * 0.47;
                ht2 = ht2 * 0.47;
            }
            else if (screen.height <= 720) {
                ht = ht * 0.52;
                ht2 = ht2 * 0.52;
            }
            else {
                ht = ht * 0.6;
                ht2 = ht2 * 0.77;
            }

            $("#Bv").width(wd);
            $("#Bv").height(ht);

            $("#PG").width(wd);
            $("#PG").height(ht2);
        }

        function filterSettings() {

            var ddlRegion = $("#<%=uoDropDownListRegion.ClientID %>");
            var ddlSeaport = $("#<%=uoDropDownListSeaport.ClientID %>");
            var txtSeaport = $("#<%=uoTextBoxSearchSeaport.ClientID %>");
            var btnSeaportFilter = $("#<%=uoButtonFilterSeaport.ClientID %>");
            var btnViewBranch = $("#<%=uoButtonViewBranch.ClientID %>");


            if ($("#<%=uoCheckBoxAdvanceSearch.ClientID %>").attr('checked')) {
                $("#<%=uoTableAdvanceSearch.ClientID %>").show();
            }
            else {
                $("#<%=uoTableAdvanceSearch.ClientID %>").hide();
            }

            $("#<%=uoCheckBoxAdvanceSearch.ClientID %>").click(function() {
                if ($(this).attr('checked')) {
                    $("#<%=uoTableAdvanceSearch.ClientID %>").fadeIn();
                }
                else {
                    $("#<%=uoTableAdvanceSearch.ClientID %>").fadeOut();
                    $("#<%=uoDropDownListRegion.ClientID %>").val('0');
                    $("#<%=uoDropDownListSeaport.ClientID %>").val('0');
                }
            });
        }

        function OpenContract(branchID, contractID) {
            var URLString = "../ContractManagement/portAgentContractView.aspx?";
            URLString += "bId=" + branchID + "&cId=" + contractID;

            var screenWidth = screen.availwidth;
            var screenHeight = screen.availheight;

            screenWidth = 800;
            screenHeight = 650;

            window.open(URLString, 'Contract_Management', 'top=10, left=200, width=' + screenWidth + ', height=' + screenHeight + ', scrollbars=yes, menubar=no,toolbar=no,status=no,resizable=yes,addressbar=no');
            return false;
        }
    </script>
    
    <div id="PG" style="overflow: auto; overflow-x: auto; overflow-y: auto;">
    <table width="100%" id="uotablePortAgentList" runat="server">
        <tr>
            <td style="padding-left: 3px; padding-right: 3px;" colspan="2">
                <div>
                    <table style="width: 100%" cellspacing="0">
                        <tr>
                            <td style="width: 40%; text-align: left;" class="PageTitle">
                                <asp:Panel ID="uopanelvehiclehead" runat="server">
                                    Service Provider List
                                    <asp:Label ID="uolabelVehicle" runat="server" Text="" Font-Size="Small" />
                                </asp:Panel>
                            </td>
                            <td style="text-align: right; background-color: #82ABB8; padding-right: 3px;">                                
                                <%--<a id="uoHyperLinkVehicleAdd" runat="server">--%>
                                    <asp:Button ID="uoBtnPortAgentAdd" runat="server" Text="Add" 
                                    Font-Size="X-Small" Width="100px" onclick="uoBtnPortAgentAdd_Click"/>
                                <%--</a>--%>
                            </td>
                        </tr>
                        <tr><td colspan="2"></td></tr>                        
                    </table>
                     <table style="width: 100%" cellspacing="0" runat="server" id="uoTableSearch">
                         <tr>                           
                            <td class="LeftClass" style="width:50px">
                                Vendor Name: 
                            </td>
                            <td class="LeftClass" >
                                <asp:TextBox ID="uoTextBoxSearchParam" runat="server" Font-Size="7.5pt" Width="180px" Text=""/>
                                <asp:Button ID="uoButtonSearch" runat="server" Text="Search" Width="100px"
                                    Font-Size="X-Small" onclick="uoButtonSearch_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td class="LeftClass">
                                 
                            </td>
                            <td class="LeftClass">
                                <asp:CheckBox ID="uoCheckBoxAdvanceSearch" Text="View Advance Search" runat="server" />
                            </td>
                        </tr>
                    </table>
                    
                    <table style="width: 100%" cellspacing="0" runat="server" id="uoTableAdvanceSearch">
                    <tr>
                        <td class="LeftClass" style="width:50px">
                            Region:
                        </td>
                        <td class="contentValue">
                            <asp:DropDownList ID="uoDropDownListRegion" runat="server" Width="250px" 
                                AppendDataBoundItems="True" AutoPostBack="true" 
                                onselectedindexchanged="uoDropDownListRegion_SelectedIndexChanged">
                            </asp:DropDownList>
                            <cc1:ListSearchExtender ID="ListSearchExtender_uoDropDownListRegion" runat="server"
                                TargetControlID="uoDropDownListRegion" PromptText="Type to search" PromptPosition="Top"
                                IsSorted="true" PromptCssClass="dropdownSearch" />
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="LeftClass" style="width:50px">
                            Seaport:
                        </td>
                        <td class="contentValue">
                            <asp:DropDownList ID="uoDropDownListSeaport" runat="server" Width="250px" AppendDataBoundItems="True">
                            </asp:DropDownList>
                            <cc1:ListSearchExtender ID="ListSearchExtender_uoDropDownListSeaport" runat="server"
                                TargetControlID="uoDropDownListSeaport" PromptText="Type to search" PromptPosition="Top"
                                IsSorted="true" PromptCssClass="dropdownSearch" />
                        </td>
                        <td class="LeftClass">
                            <asp:TextBox ID="uoTextBoxSearchSeaport" runat="server" CssClass="TextBoxInput" Width="249px" />
                            <asp:Button ID="uoButtonFilterSeaport" runat="server" Text="Filter Seaport" CssClass="SmallButton" 
                             OnClick="uoButtonFilterSeaport_Click"/>
                            <asp:Button ID="uoButtonViewBranch" runat="server" Text="View Branch" CssClass="SmallButton" 
                            OnClick="uoButtonViewBranch_Click"/>
                        </td>
                        <td class="RightClass">
                        </td>
                    </tr>
                </table>
                </div>
            </td>
        </tr>
        </table>
        
        <div id="Bv" style="overflow: auto; width: 100%; overflow-x: auto; overflow-y: auto;" align="right">
        <table width="100%" style="text-align:left">
        <tr>
            <td class="Module" align="right">
                <asp:ListView runat="server" ID="uoListViewPortAgent" OnItemCommand="uoListViewPortAgent_ItemCommand"
                    OnItemDeleting="uoListViewPortAgent_ItemDeleting" >
                    <LayoutTemplate>
                        <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                            <tr>
                                 <th style="text-align: center; white-space: normal;">
                                        <asp:LinkButton ID="uoLinkButtonHeaderPortAgentName" runat="server" Width="300px"
                                            CommandName="SortByName">Vendor Name</asp:LinkButton>
                                </th>                            
                                 <th style="text-align: center; white-space: normal;">
                                        <asp:LinkButton  ID="LinkButton1" runat="server" Width="150px"
                                            CommandName="SortByCOuntry">Country</asp:LinkButton>
                                </th>                            
                                <th style="text-align: center; white-space: normal;">
                                        <asp:LinkButton ID="LinkButton2" runat="server" Width="150px"
                                            CommandName="SortByCity">City</asp:LinkButton>                                
                                </th>
                                <th style="text-align: center; white-space: normal; Width:100px">
                                    Contact No.
                                </th>
                                <th style="text-align: center; white-space: normal; Width:100px">
                                    Fax No.
                                </th>
                                 <th style="text-align: left; white-space: normal; width:200px">
                                    Email Address
                                </th>                                
                                <th runat="server" style="width: 100px" id="EditTH" />         
                                <th runat="server" style="width: 100px" id="ContractTH" />
                                <th runat="server" style="width: 100px" id="AddContractTH" />
                            </tr>
                            <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                        </table>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <tr>
                            <td class="leftAligned">                               
                                <asp:LinkButton runat="server" Width="300px" id="uoLinkButtonContract" Text ='<%# Eval("PortAgentName") %>'
                                OnClientClick = '<%# "return OpenContract(\"" + Eval("PortAgentID") + "\",\"" + Eval("ContractID")  + "\");" %>'
                                Visible = '<%# Eval("IsWithContract") %>'
                                ></asp:LinkButton>                                
                                <asp:Label runat="server" Width="300px" ID ="uoLabelPortAgentName" Text='<%# Eval("PortAgentName") %>' Visible = '<%# !Convert.ToBoolean(Eval("IsWithContract")) %>'  ></asp:Label>
                            </td>
                            <td class="leftAligned" style="width: 150px">
                                <%# Eval("Country")%>
                            </td>                           
                            <td class="leftAligned" style="width: 150px">
                                <%# Eval("City")%>
                            </td>
                            <td class="leftAligned" style="width: 100px">
                                <%# Eval("ContactNo")%>
                            </td>
                            <td class="leftAligned" style="width: 100px">
                                <%# Eval("FaxNo")%>
                            </td>
                            <td class="leftAligned" style="width: 200px; white-space:normal" >
                                <%# Eval("EmailTo")%>                        
                            </td>  
                            <td  style="width:100px">
                                 <a runat="server" class="VehicleLink" id="uoAEditvehicle" href='<%# "PortAgentMaintenance.aspx?vmId=" + Eval("PortAgentID") + "&ufn=" + Request.QueryString["ufn"] + "&dt=" + Request.QueryString["dt"]  %>'
                                visible='<%# (Convert.ToBoolean(uoHiddenFieldVendor.Value)) %>' style="width: 80px" >
                                    Edit</a>
                            </td>     
                            <td>
                                 <a runat="server" visible='<%# (Convert.ToBoolean(uoHiddenFieldContractList.Value)) %>'
                                    id="A1" href='<%# "../ContractManagement/PortAgentContractListView.aspx?vmId=" + Eval("PortAgentID") + "&hbName=" + Eval("PortAgentName") + "&st=" + Request.QueryString["st"] + "&ufn=" + Request.QueryString["ufn"] + "&dt=" + Request.QueryString["dt"] %>'>
                                    Contract List</a>
                            </td>    
                            <td>
                                <a runat="server" id="uoAddContract" href='<%# "../ContractManagement/PortAgentContractAdd.aspx?vmId=" + Eval("PortAgentID") +  "&st=" + Request.QueryString["st"] + "&ufn=" + Request.QueryString["ufn"] + "&dt=" + Request.QueryString["dt"] %>'
                                    visible='<%# (Convert.ToBoolean(uoHiddenFieldVendor.Value)) %>'>Add Contract</a>
                            </td>                 
                        </tr>
                    </ItemTemplate>
                    <EmptyDataTemplate>
                        <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                            <tr>
                                <th>
                                    Vendor Name
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
                                <th>
                                    Fax No.
                                </th>
                                 <th>
                                    Email Address
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
                 <asp:ObjectDataSource ID="uoObjectDataSourcePortAgent" runat="server" MaximumRowsParameterName="iMaxRow"
                        SelectCountMethod="GetPortAgentCount" SelectMethod="GetPortAgentList"
                        StartRowIndexParameterName="iStartRow" TypeName="TRAVELMART.BLL.MaintenanceViewBLL"
                        OldValuesParameterFormatString="oldcount_{0}" EnablePaging="True"  >
                        <SelectParameters>
                            <asp:ControlParameter Name="sUserID" Type="String" ControlID="uoHiddenFieldUser" />
                            <asp:ControlParameter Name="sRole" Type="String" ControlID="uoHiddenFieldRole" />
                            
                            <asp:ControlParameter Name="sPortAgentVendor" Type="String" ControlID="uoTextBoxSearchParam" />
                            <asp:ControlParameter Name="sOrder" Type="String" ControlID="uoHiddenFieldSortBy" />
                            <asp:ControlParameter Name="iRegionID" Type="Int32" ControlID="uoDropDownListRegion" />
                            <asp:ControlParameter Name="iPortID" Type="Int32" ControlID="uoDropDownListSeaport" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
            </td>
        </tr>
        <tr>
            <td class="LeftClass">
                <asp:DataPager ID="uoListViewPortAgentPager" runat="server" PagedControlID="uoListViewPortAgent"
                    PageSize="20" OnPreRender="uoListViewPortAgentPager_PreRender">
                    <Fields>
                        <asp:NumericPagerField ButtonType="link" NumericButtonCssClass="PagerClass" />
                    </Fields>
                </asp:DataPager>
            </td>
        </tr>
    </table>
    </div>
    <asp:HiddenField ID="uoHiddenFieldPopupPortAgent" runat="server" Value="0" />
    <asp:HiddenField ID="uoHiddenFieldVendor" runat="server" Value="true" />
    <asp:HiddenField ID="uoHiddenFieldContractList" runat="server" Value="true" />
    
    <asp:HiddenField ID="uoHiddenFieldSortBy" runat="server" Value="SortByName" />
    <asp:HiddenField ID="uoHiddenFieldUser" runat="server" Value="" />
    <asp:HiddenField ID="uoHiddenFieldRole" runat="server" Value="" />
    
    </div>
</asp:Content>

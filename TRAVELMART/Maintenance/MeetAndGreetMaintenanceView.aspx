<%@ Page Title="" Language="C#" MasterPageFile="~/TravelMartMaintenanceMaster.Master" AutoEventWireup="True"
    CodeBehind="MeetAndGreetMaintenanceView.aspx.cs" Inherits="TRAVELMART.MeetAndGreetMaintenanceView" %>

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
            setupPopup();
            SetBranchResolution();
        });
        
        function setupPopup() {
            $("a#<%=uoHyperLinkVehicleAdd.ClientID %>").fancybox({
                'width': '70%',
                'height': '90%',
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
                'width': '70%',
                'height': '90%',
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
                ht = ht * 0.55;
                ht2 = ht2 * 0.68;
            }

            $("#Bv").width(wd);
            $("#Bv").height(ht);

            $("#PG").width(wd);
            $("#PG").height(ht2);
        }       
    </script>

    <div id="PG" style="overflow: auto; overflow-x: auto; overflow-y: auto;">
    <table width="100%" id="uotableVehicleVendorList" runat="server">
        <tr>
            <td style="padding-left: 3px; padding-right: 3px;" colspan="2">
                <div>
                    <table style="width: 100%" cellspacing="0">
                        <tr>
                            <td style="width: 40%; text-align: left;" class="PageTitle">
                                <asp:Panel ID="uopanelvehiclehead" runat="server">
                                    Meet and Greet Vendor List
                                    <asp:Label ID="uolabelVehicle" runat="server" Text="" Font-Size="Small" />
                                </asp:Panel>
                            </td>
                            <td style="text-align: right; background-color: #82ABB8; padding-right: 3px;">                                
                                <a id="uoHyperLinkVehicleAdd" runat="server">
                                    <asp:Button ID="uoBtnVehicleAdd" runat="server" Text="Add" Font-Size="X-Small" />
                                </a>
                            </td>
                        </tr>
                        <tr><td colspan="2"></td></tr>
                         <tr>                           
                            <td class="RightClass" colspan="2">
                                Meet and Greet Vendor Name: <asp:TextBox ID="uoTextBoxSearchParam" runat="server" Font-Size="7.5pt" Width="180px" Text=""/>
                                <asp:Button ID="uoButtonSearch" runat="server" Text="Search" 
                                    Font-Size="X-Small" onclick="uoButtonSearch_Click" />
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
                <asp:ListView runat="server" ID="uoVehicleVendorList" OnItemCommand="uoVehicleVendorList_ItemCommand"
                    OnItemDeleting="uoVehicleVendorList_ItemDeleting" >
                    <LayoutTemplate>
                        <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                            <tr>
                                 <th style="text-align: center; white-space: normal;">
                                        <asp:LinkButton ID="uoLinkButtonHeaderVendorName" runat="server"
                                            CommandName="SortByName">Vendor Name</asp:LinkButton>
                                </th>                            
                                 <th style="text-align: center; white-space: normal;">
                                        <asp:LinkButton  ID="LinkButton1" runat="server"
                                            CommandName="SortByCOuntry">City</asp:LinkButton>
                                </th>                            
                                <th style="text-align: center; white-space: normal;">
                                        <asp:LinkButton ID="LinkButton2" runat="server"
                                            CommandName="SortByCity">Country</asp:LinkButton>                                
                                </th>
                                <th style="text-align: center; white-space: normal;">
                                    Contact No.
                                </th>
                                <th style="text-align: center; white-space: normal;">
                                    Fax No.
                                </th>
                                 <th style="text-align: center; white-space: normal;">
                                    Email Address
                                </th>                                
                                <th runat="server" style="width: 5%" id="EditTH" />         
                                <th runat="server" style="width: 10%" id="ContractTH" />
                                <th runat="server" style="width: 10%" id="AddContractTH" />
                            </tr>
                            <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                        </table>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <tr>
                            <td class="leftAligned">
                                <%# Eval("MeetAndGreetVendorName")%>
                            </td>
                            <td class="leftAligned">
                                <%# Eval("CountryName")%>
                            </td>                           
                            <td class="leftAligned">
                                <%# Eval("CityName")%>
                            </td>
                            <td class="leftAligned">
                                <%# Eval("ContactNo")%>
                            </td>
                            <td class="leftAligned">
                                <%# Eval("FaxNo")%>
                            </td>
                            <td class="leftAligned">
                                <%# Eval("EmailTo")%>                        
                            </td>  
                            <td>
                                 <a runat="server" class="VehicleLink" id="uoAEditvehicle" href='<%# "~/Maintenance/MeetandGreetMaintenance.aspx?vmId=" + Eval("MeetandGreetVendorId")%> '
                                visible='<%# (Convert.ToBoolean(uoHiddenFieldVendor.Value)) %>'>
                                    Edit</a>
                            </td>     
                            <td>
                                 <a runat="server" visible='<%# (Convert.ToBoolean(uoHiddenFieldVendor.Value)) %>'
                                    id="A1" href='<%# "../ContractManagement/MeetAndGreetContractListView.aspx?vmId=" + Eval("MeetandGreetVendorId") + "&hbName=" + Eval("MeetAndGreetVendorName") + "&st=" + Request.QueryString["st"] + "&ufn=" + Request.QueryString["ufn"] + "&dt=" + Request.QueryString["dt"] %>'>
                                    Contract List</a>
                            </td>    
                            <td>
                                <a runat="server" id="uoAddContract" href='<%# "../ContractManagement/MeetAndGreetContractAdd.aspx?vmId=" + Eval("MeetandGreetVendorId") +  "&st=" + Request.QueryString["st"] + "&ufn=" + Request.QueryString["ufn"] + "&dt=" + Request.QueryString["dt"] %>'
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
                 <asp:ObjectDataSource ID="uoObjectDataSourceVehicle" runat="server" MaximumRowsParameterName="iMaxRow"
                        SelectCountMethod="MeetAndGreetVendorsGetCount" SelectMethod="MeetAndGreetVendorsGet"
                        StartRowIndexParameterName="iStartRow" TypeName="TRAVELMART.BLL.MeetAndGreetBLL"
                        OldValuesParameterFormatString="oldcount_{0}" EnablePaging="True" OnSelecting="uoObjectDataSourceVehicle_Selecting" >
                        <SelectParameters>
                            <asp:Parameter Name="sVehicleVendorName" Type="String"/>
                            <asp:ControlParameter Name="sOrderyBy" Type="String" ControlID="uoHiddenFieldSortBy" />                            
                        </SelectParameters>
                    </asp:ObjectDataSource>
            </td>
        </tr>
        <tr>
            <td class="LeftClass">
                <asp:DataPager ID="uoVehicleVendorListPager" runat="server" PagedControlID="uoVehicleVendorList"
                    PageSize="20" OnPreRender="uoVehicleVendorListPager_PreRender">
                    <Fields>
                        <asp:NumericPagerField ButtonType="link" NumericButtonCssClass="PagerClass" />
                    </Fields>
                </asp:DataPager>
            </td>
        </tr>
    </table>
    </div>
    <asp:HiddenField ID="uoHiddenFieldPopupVehicle" runat="server" Value="0" />
    <asp:HiddenField ID="uoHiddenFieldVendor" runat="server" Value="true" />
    <asp:HiddenField ID="uoHiddenFieldSortBy" runat="server" Value="SortByName" />
    </div>
</asp:Content>

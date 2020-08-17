<%@ Page Title="" Language="C#" MasterPageFile="~/TravelMartMaintenanceMaster.Master"  AutoEventWireup="true"
    CodeBehind="PortMaintenanceView.aspx.cs" Inherits="TRAVELMART.PortMaintenanceView" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NaviPlaceHolder" runat="server">

    <%--<script type="text/javascript" src="../FBox/jquery-1.6.1.min.js"></script>
    <script type="text/javascript" src="../FBox/fancybox/jquery.mousewheel-3.0.4.pack.js"></script>
    <script type="text/javascript" src="../FBox/fancybox/jquery.fancybox-1.3.4.pack.js"></script>

    <script src="../Menu/menu.js" type="text/javascript"></script>        --%>
     <script type="text/javascript">
         Sys.Application.add_load(function() {
             SetBranchResolution();
         });
 
 
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

         function OpenContract(vendorID, ContractID) {
             var URLString = "../ContractManagement/VehicleContractView.aspx?bId=" + vendorID + "&cId=" + ContractID;

             var screenWidth = screen.availwidth;
             var screenHeight = screen.availheight;

             screenWidth = 800;
             screenHeight = 650;

             window.open(URLString, 'Contract_Management', 'top=10, left=200, width=' + screenWidth + ', height=' + screenHeight + ', scrollbars=yes, menubar=no,toolbar=no,status=no,resizable=yes,addressbar=no');
             return false;
         }  
    </script>   
    <script type="text/javascript" language="javascript">
        function confirmDelete(Data) {
            if (confirm("Deactivate record?") == true) {
                $("#ctl00_NaviPlaceHolder_uoHiddenFieldActivationID").val(Data);
                return true;
            }
            else
                return false;
        }
        function confirmActivate(Data) {
            if (confirm("Activate record?") == true) {
                $("#ctl00_NaviPlaceHolder_uoHiddenFieldActivationID").val(Data);
                return true;
            }
            else
                return false 
        }
    </script>

    <script type="text/javascript">
    $(document).ready(function() {
        ShowPopup();
    });

    function pageLoad(sender, args) {
        var isAsyncPostback = Sys.WebForms.PageRequestManager.getInstance().get_isInAsyncPostBack();
        if (isAsyncPostback) {
            ShowPopup();
        }
    }
            
    function ShowPopup() {
        $("a#<%=uoHyperLinkPortAdd.ClientID %>").fancybox(
        {
            'width': '50%',
            'height': '70%',
            'autoScale': false,
            'transitionIn': 'fadeIn',
            'transitionOut': 'fadeOut',
            'type': 'iframe',
            'onClosed': function() {
                var a = $("#<%=uoHiddenFieldPopupPort.ClientID %>").val();
                if (a == '1')
                    $("#aspnetForm").submit();
            }
        });
        $(".PortLink").fancybox(
        {
            'width': '50%',
            'height': '70%',
            'autoScale': false,
            'transitionIn': 'fadeIn',
            'transitionOut': 'fadeOut',
            'type': 'iframe',
            'onClosed': function() {
                var a = $("#<%=uoHiddenFieldPopupPort.ClientID %>").val();
                if (a == '1')
                    $("#aspnetForm").submit();
            }
        });
    }
    function DeletePort(SeaportID) {
        if (confirm("Delete record?") == true) {
            $("#<%=uoHiddenFieldPortId.ClientID %>").val(SeaportID);
            $("#<%=uoHiddenFieldIsDelete.ClientID %>").val(1);
            $("#aspnetForm").submit();
        }
        else {
            $("#<%=uoHiddenFieldIsDelete.ClientID %>").val(0);
            $("#<%=uoHiddenFieldPortId.ClientID %>").val(0);
            return false;
        }
    }                  
    </script>
 
       
    <div id="PG" style="overflow: auto; overflow-x: auto; overflow-y: hidden;height:101%;">
    <table width="100%" id="uotablePortList" runat="server">
        <tr>
            <td style="padding-left: 3px; padding-right: 3px;">
                <div>
                    <table style="width: 100%" cellspacing="0">
                        <tr>
                            <td style="width: 40%; text-align: left;" class="PageTitle" colspan="2">
                                <asp:Panel ID="uopanelporthead" runat="server">
                                    Seaport
                                    <asp:Label ID="uolabelPort" runat="server" Text="" Font-Size="Small" />
                                </asp:Panel>
                            </td>                            
                            <td style="text-align: right; background-color: #82ABB8; padding-right: 3px;" colspan="2">
                                <a id="uoHyperLinkPortAdd" runat="server">
                                    <asp:Button ID="Button1" runat="server" Text="Add" Font-Size="X-Small" />
                                </a>
                            </td>
                        </tr>
                        <tr><td colspan="4"></td></tr>                        
                        <%--<tr>   
                            <td></td> 
                            <td class=RightClass runat="server" id="uoSeaportSearch">
                                Port Name :
                                <asp:TextBox ID="uoTextBoxSearchParam" runat="server" Font-Size="7.5pt" Width="180px" />                                
                            </td>                           
                        </tr>--%>
                        <tr>         
                            <td class="contentCaptionOrig" style="width:100px" runat="server" id="uoCountrySearch">
                                Region :

                            </td>
                            <td class="contentValueOrig">
                                <asp:DropDownList ID="uoDropDownListRegion" runat="server"  Width="300px" AppendDataBoundItems="true"
                                    AutoPostBack="true" onselectedindexchanged="uoDropDownListRegion_SelectedIndexChanged">
                                </asp:DropDownList >                                                       
                            </td>                   
                            <%--<td class="LeftClass" visible="false">
                                <span>Region:</span>
                                <asp:DropDownList ID="uoDropDownListRegion" runat="server" Width="250px" AutoPostBack="True"
                                    AppendDataBoundItems="True" >
                                </asp:DropDownList>
                                <cc1:ListSearchExtender ID="ListSearchExtender_uoDropDownListRegion" runat="server"
                                    TargetControlID="uoDropDownListRegion" PromptText="Type to search" PromptPosition="Top"
                                    IsSorted="true" PromptCssClass="dropdownSearch" />
                            </td>--%>                            
                            <td class="contentCaptionOrig" style="width:100px" runat=server id="uoSeaportSearch">
                                 Seaport Name :           
                            </td>
                            <td class="contentValueOrig">
                            <asp:TextBox ID="uoTextBoxSearchParam" runat="server" Font-Size="7.5pt" 
                                    Width="300px" Text="" /> &nbsp;
                               <asp:Button ID="uoButtonSearch" runat="server" Text="Search" Width="70px"
                                    Font-Size="X-Small" OnClick="uoButtonSearch_Click" />   
               
                            </td>                            
                        </tr>
                        <tr>
                            <td class="contentCaptionOrig" style="width:130px">
                                            
                            </td>
                            <td class="contentValueOrig" style="width:400px; white-space:nowrap">
                            </td>
                            <td colspan="2">
                            </td>
                        
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
        </table>
        <div id="Bv" style="overflow: auto; width: 100%; overflow-x: auto; overflow-y: auto;" align="right">
        <table width="100%" runat="server">       
        <tr>
            <td class="Module" align="right">
                <asp:ListView runat="server" ID="uoPortList" OnItemCommand="uoPortList_ItemCommand">
                    <LayoutTemplate>
                        <table border="0" cellpadding="0" cellspacing="0" class="listViewTable2">
                            <tr>
                                <th>
                                   Seaport Code
                                </th>
                                <th>
                                    Seaport Name
                                </th>
                                <th>
                                    Activation
                                </th>                                
                                <%--<th runat="server" style="width: 10%" id="EditTH" />
                                <th runat="server" style="width: 10%" id="DeleteTH" />--%>
                            </tr>
                            <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                        </table>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <tr>
                            <td class="leftAligned">
                              <%# Eval("SeaportCode")%>
                            </td>
                            <td class="leftAligned">
                               <%# Eval("SeaportName")%> 
                               
                            </td>
                            <td class="leftAligned">
                              
                               <asp:LinkButton id="uoLinkButtonActivate" CommandName="Select" runat="server" OnClientClick='<%# "return confirmActivate(\"" + Eval("SeaportId") + "\");" %>' Visible='<%# !Convert.ToBoolean(Eval("SeaportActivated")) %>' >Activate</asp:LinkButton>
                              <asp:LinkButton id="uoLinkButtonDeactivate" CommandName="Remove" runat="server" OnClientClick='<%# "return confirmDelete(\"" + Eval("SeaportId") + "\");" %>' Visible='<%# Convert.ToBoolean(Eval("SeaportActivated")) %>'>Deactivate</asp:LinkButton>
                           </td>                          
                           <%-- <td
                                <a runat="server" class="PortLink" id="uoAEditHotel" href='<%# "~/Maintenance/PortMaintenance.aspx?vmId=" + Eval("PORTID") %>'
                                    visible='<%# (Convert.ToBoolean(uoHyperLinkPortAdd.Visible)) %>'>
                                    Edit</a>
                            </td>
                            <td></td>--%>
                            <%--<td>
                                <asp:LinkButton ID="uoLinkButtonDelete" runat="server" OnClientClick="return confirmDelete();"
                                    CommandArgument='<%#Eval("PORTID") %>' CommandName="Delete">Delete</asp:LinkButton>
                            </td>--%>
                        </tr>
                    </ItemTemplate>
                    <EmptyDataTemplate>
                        <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                            <tr>
                                <th>
                                    SeaPort Code
                                </th>
                                <th>
                                    Seaport Name
                                </th>
                                <th>
                                    Activation
                                </th>
                                <th></th>
                            </tr>
                            <tr>
                                <td colspan="4" class="leftAligned">
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
                <asp:DataPager ID="uoPortListPager" runat="server" PagedControlID="uoPortList"
                    PageSize="20" OnPreRender="uoPortListPager_PreRender">
                    <Fields>
                        <asp:NumericPagerField ButtonType="link" NumericButtonCssClass="PagerClass" />
                    </Fields>
                </asp:DataPager>
            </td>        
        </tr>        
        </table>
        </div>
        <table width="100%" id="Table1" runat="server">
        <tr>
            <td class="LeftClass">   
                 <asp:ObjectDataSource ID="uoObjectDataSourceVehicle" runat="server" MaximumRowsParameterName="iMaxRow"
                        SelectCountMethod="SeaportGetListCount" SelectMethod="SeaportGetList"
                        StartRowIndexParameterName="iStartRow" TypeName="TRAVELMART.BLL.MaintenanceViewBLL"
                        OldValuesParameterFormatString="" EnablePaging="True" OnSelecting="uoObjectDataSourceVehicle_Selecting">
                        <SelectParameters>
                            <asp:ControlParameter Name="LoadType" Type="Int16" ControlID="uoHiddenFieldLoadType" />
                            <asp:ControlParameter Name="RegionID" Type="Int32" ControlID="uoDropDownListRegion" />
                            <asp:ControlParameter Name="SeaportID" Type="Int32" ControlID="uoHiddenFieldSeaportDefault" />
                            <asp:Parameter Name="SeaportName" Type="String"/>
                            <asp:ControlParameter Name="sOrderBy" Type="String" ControlID="uoHiddenFieldSortBy" />
                            <asp:ControlParameter Name="sUserID" Type="String" ControlID="uoHiddenFieldUser" />
                        </SelectParameters>
                    </asp:ObjectDataSource>                     
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="uoHiddenFieldPopupPort" runat="server" Value="0" />   
    <asp:HiddenField ID="uoHiddenFieldRegionID" runat="server" Value="0" />
    
    <asp:HiddenField ID="uoHiddenFieldUser" runat="server" Value="" />
    <asp:HiddenField ID="uoHiddenFieldRole" runat="server" Value="" />
    <asp:HiddenField ID="uoHiddenFieldPortAgentID" runat="server" Value="0" />
    <asp:HiddenField ID="uoHiddenFieldLoadType" runat="server" Value="0" />
    
    <asp:HiddenField ID="uoHiddenFieldPortId" runat="server" Value="0" />
    <asp:HiddenField ID="uoHiddenFieldSortBy" runat="server" Value="0" />
    <asp:HiddenField ID="uoHiddenFieldIsDelete" runat="server" Value="0" />
    <asp:HiddenField ID="uoHiddenFieldSeaportDefault" runat="server" Value="0" />
    <asp:HiddenField ID="uoHiddenFieldActivationID" runat="server" Value="0" />
     </div> 
  
</asp:Content>

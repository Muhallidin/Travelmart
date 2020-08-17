<%@ Page Title="" Language="C#" MasterPageFile="~/TravelMart.Master" AutoEventWireup="true" CodeBehind="PortContractListView.aspx.cs" Inherits="TRAVELMART.PortContractListView" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
 <%--<script type="text/javascript" src="../FBox/jquery-1.6.1.min.js"></script>
    <script type="text/javascript" src="../FBox/fancybox/jquery.mousewheel-3.0.4.pack.js"></script>
    <script type="text/javascript" src="../FBox/fancybox/jquery.fancybox-1.3.4.pack.js"></script>
    <script src="../Menu/menu.js" type="text/javascript"></script>    --%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<script type="text/javascript">
    $(document).ready(function() {
    $("a#<%=uoHyperLinkPortAdd.ClientID %>").fancybox({
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
        $(".PortLink").fancybox({
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

        $(".PortCompanyLink").fancybox(
                {
                    'width': '38%',
                    'height': '50%',
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

    });                       
    </script>
 <table style="width: 100%" cellspacing="0">
        
            <tr>
            <td style="padding-left: 3px; padding-right: 3px;" colspan="2">
                <div>
                    <table style="width: 100%" cellspacing="0">
                        <tr>
                            <td  text-align: left;" class="PageTitle">
                                <asp:Panel ID="uopanelhotelhead" runat="server" Width="364px">
                                    Port Contract List 
                                    <asp:Label ID="uolabelHotel" runat="server" Text="" Font-Size="Small" />
                                </asp:Panel>
                            </td>                                     
                            <td style="text-align: right; background-color: #82ABB8; padding-right: 3px;">
                                <a id="uoHyperLinkPortAdd" runat="server">
                                    <asp:Button ID="uoBtnPortAdd" runat="server" Text="Add" 
                                    CssClass="SmallButton"/>
                                </a>
                            </td>
                        </tr>
                        <tr>
                            <td class="LeftClass"  colspan="2">
                                Port Company:
                                <asp:TextBox ID="uoTextBoxPortCompany" runat="server" Width="300"></asp:TextBox>
                                <asp:Button ID="uoButtonSearch" runat="server" Text="Search" CssClass="SmallButton"/>
                            </td>                              
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
        <tr>
            <td class="Module" align="right">
                <asp:ListView runat="server" ID="uoPortContractList">
                    <LayoutTemplate>
                        <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                            <tr>
                                <th style="width:25%">Port Company</th>
                                <th style="width:25%">Contract Person</th>                                
                                <th style="width:25%">Contract No.</th>
                                <th style="width:10%">Contract Date Start</th>
                                <th style="width:10%">Contract Date End</th>  
                                <th runat="server" style="width: 10%" id="EditTH" />                                                          
                            </tr>
                            <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                        </table>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <tr>
                            <td class="leftAligned">                           
                              <a runat="server" class="PortCompanyLink" id="uoAEditPortCompany" href='<%# "~/Maintenance/PortCompanyMaintenance.aspx?cmId=" + Eval("colPortAgentCompanyIdInt") %>'>
                                <%# Eval("colCompanyNameVarchar")%></a>                          
                            </td>
                            <td class="leftAligned">
                                <%# Eval("colContractNameVarchar")%>
                            </td>  
                             <td class="leftAligned">
                                <%# Eval("colMainContactNumberVarchar")%>
                            </td>                           
                            <td class="leftAligned">
                                <%# Eval("colContractDateStartedDate")%>
                            </td>
                            <td class="leftAligned">
                                <%# Eval("colContractDateEndDate")%>
                            </td>
                            <td >
                              <a runat="server" class="PortLink" id="uoAEditPortContract" href='<%# "~/ContractManagement/PortContractAdd.aspx?cmId=" + Eval("colContractIdInt") %>'>
                                    Edit</a>
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
                                <th>Port Company</th>
                                <th>Contract</th>                                
                                <th>Contract Date Start</th>
                                <th>Contract Date End</th> 
                            </tr>
                            <tr>
                                <td colspan="6" class="leftAligned">
                                    No Record
                                </td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                </asp:ListView>
                    <asp:DataPager ID="uoPortContractListPager" runat="server" PagedControlID="uoPortContractList"
                    PageSize="20" OnPreRender="uoPortContractList_PreRender">
                    <Fields>
                        <asp:NumericPagerField ButtonType="link" NumericButtonCssClass="PagerClass" />
                    </Fields>
                </asp:DataPager>
                </td>
            </tr> 
    </table>
    <asp:HiddenField ID="uoHiddenFieldPopupPort" runat="server" Value="0" />
</asp:Content>

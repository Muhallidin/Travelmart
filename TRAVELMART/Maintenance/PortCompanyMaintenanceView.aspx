<%@ Page Title="" Language="C#" MasterPageFile="~/TravelmartMaster.Master" AutoEventWireup="true"
    CodeBehind="PortCompanyMaintenanceView.aspx.cs" Inherits="TRAVELMART.PortCompanyMaintenanceView" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <%--<script type="text/javascript" src="../FBox/jquery-1.6.1.min.js"></script>
    <script type="text/javascript" src="../FBox/fancybox/jquery.mousewheel-3.0.4.pack.js"></script>
    <script type="text/javascript" src="../FBox/fancybox/jquery.fancybox-1.3.4.pack.js"></script>

    <script src="../Menu/menu.js" type="text/javascript"></script>        --%>
    <script type="text/javascript" language="javascript">
        function confirmDelete() {
            if (confirm("Delete record?") == true)
                return true;
            else
                return false;
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
        $("a#<%=uoHyperLinkPortCompanyAdd.ClientID %>").fancybox(
        {
            'width': '35%',
            'height': '57%',
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
            'width': '35%',
            'height': '57%',
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
    </script>

    <table width="100%" id="uotablePortCompanyList" runat="server">
        <tr>
            <td style="padding-left: 3px; padding-right: 3px;" colspan="2">
                <div>
                    <table style="width: 100%" cellspacing="0">
                        <tr>
                            <td style="width: 40%; text-align: left;" class="PageTitle">
                                <asp:Panel ID="uopanelporthead" runat="server">
                                    Port Company List
                                    <asp:Label ID="uolabelPort" runat="server" Text="" Font-Size="Small" />
                                </asp:Panel>
                            </td>
                            <td style="text-align: right; background-color: #82ABB8; padding-right: 3px;">
                                <a id="uoHyperLinkPortCompanyAdd" runat="server">
                                    <asp:Button ID="Button1" runat="server" Text="Add" Font-Size="X-Small" />
                                </a>
                            </td>
                        </tr>
                        <tr><td colspan="2"></td></tr>
                        <tr>
                            <td></td>
                            <%--<td class="LeftClass" visible="false">
                                <span>Region:</span>
                                <asp:DropDownList ID="uoDropDownListRegion" runat="server" Width="250px" AutoPostBack="True"
                                    AppendDataBoundItems="True" >
                                </asp:DropDownList>
                                <cc1:ListSearchExtender ID="ListSearchExtender_uoDropDownListRegion" runat="server"
                                    TargetControlID="uoDropDownListRegion" PromptText="Type to search" PromptPosition="Top"
                                    IsSorted="true" PromptCssClass="dropdownSearch" />
                            </td>--%>
                            <td class="RightClass" runat="server" id="uoPortCompanySearch">
                                Port Company Name :
                                <asp:TextBox ID="uoTextBoxSearchParam" runat="server" Font-Size="7.5pt" Width="180px" />
                                <asp:Button ID="uoButtonSearch" runat="server" Text="Search" Font-Size="X-Small" />
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
        <tr>
            <td class="Module" align="right">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                                
                <asp:ListView runat="server" ID="uoPortCompanyList" OnItemCommand="uoPortCompanyList_ItemCommand"
                    OnItemDeleting="uoPortCompanyList_ItemDeleting">
                    <LayoutTemplate>
                        <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                            <tr>                               
                                <th>Company Name</th>                               
                                <th>Country</th> 
                                <th>Contact</th>                               
                                <th runat="server" style="width: 10%" id="EditTH" />
                            </tr>
                            <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                        </table>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <tr>
                            <td class="leftAligned">
                          <%--   <a runat="server" class="PortCompanyLink" id="uoAEditPortCompany" href='<%# "~/ContractManagement/PortContractAdd.aspx?cmId=" + Eval("COMPANYID") %>'>
                             --%>   <%# Eval("company")%><%--</a>  --%>     
                               
                            </td>                             
                            <td class="leftAligned">
                                <%# Eval("country")%>
                            </td>    
                            <td class="leftAligned">                           
                                <%# String.Format(FormatUSContactNo(Eval("contact")))%>
                            </td>                        
                            <td>
                                <a runat="server" class="PortLink" id="uoAEditHotel" href='<%# "~/Maintenance/PortCompanyMaintenance.aspx?ufn=" + Request.QueryString["ufn"] +  "&cmId=" + Eval("COMPANYID") %>'
                                    Visible='<%# (Convert.ToBoolean(uoPortCompanySearch.Visible)) %>'>
                                    Edit</a>
                            </td>
                            <%--<td>
                                <asp:LinkButton ID="uoLinkButtonDelete" runat="server" OnClientClick="return confirmDelete();"
                                    CommandArgument='<%#Eval("PORTID") %>' CommandName="Delete">Delete</asp:LinkButton>
                            </td>--%>
                        </tr>
                    </ItemTemplate>
                    <EmptyDataTemplate>
                        <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                            <tr>
                               <th>Company Name</th>
                                <th>Company Address</th>
                                <th>Country</th>                             
                            </tr>
                            <tr>
                                <td colspan="4" class="leftAligned">
                                    No Record
                                </td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                </asp:ListView>
                <asp:DataPager ID="uoPortCompanyListPager" runat="server" PagedControlID="uoPortCompanyList" PageSize="20"
                    OnPreRender="uoPortCompanyListPager_PreRender">
                    <Fields>
                        <asp:NumericPagerField ButtonType="link" NumericButtonCssClass="PagerClass" />
                    </Fields>
                </asp:DataPager>
                <asp:HiddenField ID="uoHiddenFieldPopupPort" runat="server" Value="0" />
                
                </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="uoDropDownListRegion" 
                            EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="uoButtonSearch" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>    
</asp:Content>

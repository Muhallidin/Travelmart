<%@ Page Title="" Language="C#" MasterPageFile="~/TravelMartMaintenanceMaster.Master"
    AutoEventWireup="true" CodeBehind="RoleView.aspx.cs" Inherits="TRAVELMART.Administration.RoleView" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content runat="server" ID="Conetent3" ContentPlaceHolderID="HeaderContent">

    <script type="text/javascript">
        function SetResolution() {
            var ht = $(window).height();
            var wd = $(window).width() * 0.90;
            if (screen.height <= 600) {
                ht = ht * 0.48;
            }
            else if (screen.height <= 720) {
                ht = ht * 0.65;
            }
            else {
                ht = ht * 0.68;
            }

            $("#Bv").height(ht);
            $("#Bv").width(wd);
            

        }

        function pageLoad(sender, args) {
            var isAsyncPostback = Sys.WebForms.PageRequestManager.getInstance().get_isInAsyncPostBack();
            if (isAsyncPostback) {
                SetResolution();
                PageSettings();
            }

        }
        
        $(document).ready(function() {
            SetResolution();
            PageSettings();
        });

            function PageSettings() {
                $("a#<%=uoHyperLinkRoleAdd.ClientID %>").fancybox(
                {
                    'width': '37%',
                    'height': '35%',
                    'autoScale': false,
                    'transitionIn': 'fadeIn',
                    'transitionOut': 'fadeOut',
                    'type': 'iframe',
                    'onClosed': function() {
                        var a = $("#<%=uoHiddenFieldPopupRole.ClientID %>").val();
                        if (a == '1')
                            $("#aspnetForm").submit();
                    }
                });

                $(".RoleLink").fancybox(
                {
                    'width': '37%',
                    'height': '35%',
                    'autoScale': false,
                    'transitionIn': 'fadeIn',
                    'transitionOut': 'fadeOut',
                    'type': 'iframe',
                    'onClosed': function() {
                        var a = $("#<%=uoHiddenFieldPopupRole.ClientID %>").val();
                        if (a == '1')
                            $("#aspnetForm").submit();
                    }
                });
            }                   
    </script>

    <script type="text/javascript" language="javascript">
        function confirmDelete() {
            if (confirm("Delete record?") == true)
                return true;
            else
                return false;
        }
    </script>

    <div style="padding-left: 3px; padding-right: 3px;">
        <table style="width: 100%" cellspacing="0">
            <tr>
                <td style="width: 40%; text-align: left;" class="PageTitle">
                    <asp:Panel ID="uopanelrolehead" runat="server">
                        User Roles
                        <asp:Label ID="uclabelRole" runat="server" Text="" Font-Size="Small" />
                    </asp:Panel>
                </td>
                <td style="text-align: right; background-color: #82ABB8; padding-right: 3px;">
                    <a id="uoHyperLinkRoleAdd" runat="server">
                        <asp:Button ID="uoButtonRoleAdd" runat="server" Text="Add" CssClass="SmallButton" />
                    </a>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NaviPlaceHolder" runat="server">
    <div id="Bv" style="overflow: auto; overflow-x: auto; overflow-y: auto;">
        <table width="100%" id="uotableRoles" runat="server">
            <tr>
                <td class="Module" align="right">
                    <asp:ListView runat="server" ID="uoRoleList" OnItemCommand="uoRoleList_ItemCommand"
                        OnItemDeleting="uoRoleList_ItemDeleting">
                        <LayoutTemplate>
                            <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                                <tr>
                                    <th style="display: none">
                                        Role ID
                                    </th>
                                    <th>
                                        Role Name
                                    </th>
                                    <th>
                                        Description
                                    </th>
                                    <th runat="server" style="width: 5%" id="EditTH" />
                                    <th runat="server" id="DeleteTH">
                                    </th>
                                </tr>
                                <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                            </table>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <tr>
                                <td style="display: none">
                                    <%# Eval("rid")%>
                                </td>
                                <td class="leftAligned">
                                    <%# Eval("role")%>
                                </td>
                                <td class="leftAligned">
                                    <%# Eval("roledesc")%>
                                </td>
                                <td class='<%# (HideElemet()==""?"leftAligned":HideElemet()) %>'>
                                    <a runat="server" class="RoleLink" id="uoEditRole" href='<%# "~/Administration/AddRole.aspx?rId=" + Eval("rid") %>'>
                                        Edit</a>
                                </td>
                                <td class='<%# (HideElemet()==""?"leftAligned":HideElemet()) %>'>
                                    <asp:LinkButton ID="uoLinkButtonDelete" runat="server" OnClientClick="return confirmDelete();"
                                        CommandArgument='<%#Eval("role") %>' CommandName="Delete">Delete</asp:LinkButton>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <EmptyDataTemplate>
                            <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                                <tr>
                                    <th>
                                        Role ID
                                    </th>
                                    <th>
                                        Role Name
                                    </th>
                                    <th>
                                        Description
                                    </th>
                                </tr>
                                <tr>
                                    <td colspan="3" class="leftAligned">
                                        No Record
                                    </td>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
                    </asp:ListView>
                </td>
            </tr>
            <tr align="left">
                <td>
                    <asp:DataPager ID="uoRoleListPager" runat="server" PagedControlID="uoRoleList" PageSize="20"
                        OnPreRender="uoRoleListPager_PreRender">
                        <Fields>
                            <asp:NumericPagerField ButtonType="link" NumericButtonCssClass="PagerClass" />
                        </Fields>
                    </asp:DataPager>
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="uoHiddenFieldPopupRole" runat="server" Value="0" />
    </div>
</asp:Content>

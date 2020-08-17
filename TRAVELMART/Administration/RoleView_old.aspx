<%@ Page Title="" Language="C#" MasterPageFile="~/TravelmartMaster.Master" AutoEventWireup="true"
    CodeBehind="RoleView_old.aspx.cs" Inherits="TRAVELMART.Administration.RoleView_old" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

   <%-- <script type="text/javascript" src="../FBox/jquery-1.6.1.min.js"></script>
    <script type="text/javascript" src="../FBox/fancybox/jquery.mousewheel-3.0.4.pack.js"></script>
    <script type="text/javascript" src="../FBox/fancybox/jquery.fancybox-1.3.4.pack.js"></script>--%>

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
        });                       
    </script>


<table width="100%" id="uotableRoles" runat="server">
        <tr>
            <td style="padding-left: 3px; padding-right: 3px;" colspan="2">
                <div>
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
            </td>
        </tr>
        <tr>
            <td class="Module" align="right">
                <asp:ListView runat="server" ID="uoRoleList" OnItemCommand="uoRoleList_ItemCommand"
                    OnItemDeleting="uoRoleList_ItemDeleting">
                    <LayoutTemplate>
                        <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                            <tr>
                                <th style="display:none">Role ID</th>
                                <th>Role Name</th>
                                <th>Description</th>                                                       
                                <th runat="server" style="width: 5%" id="EditTH" />  
                                <th runat="server" id = "DeleteTH"></th>                              
                            </tr>
                            <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                        </table>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <tr>
                            <td style="display:none">
                             <%# Eval("rid")%>
                            </td>
                            <td class="leftAligned">
                             <%# Eval("role")%>
                            </td>
                            <td class="leftAligned">
                             <%# Eval("roledesc")%>
                            </td>                                      
                            <td class='<%# (HideElemet()==""?"leftAligned":HideElemet()) %>' >
                                <a runat="server" class="RoleLink" id="uoEditRole" href='<%# "~/Administration/AddRole.aspx?rId=" + Eval("rid") %>'>
                                    Edit</a>
                            </td>
                            <td class='<%# (HideElemet()==""?"leftAligned":HideElemet()) %>'>
                                <asp:LinkButton ID="uoLinkButtonDelete" runat="server" OnClientClick="return confirmDelete();"
                                    CommandArgument='<%#Eval("role") %>' CommandName="Delete" >Delete</asp:LinkButton>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <EmptyDataTemplate>
                        <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                            <tr>
                                <th>Role ID</th>
                                <th>Role Name</th>
                                <th>Description</th>       
                            </tr>
                            <tr>
                                <td colspan="3" class="leftAligned">
                                    No Record
                                </td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                </asp:ListView>
                <asp:DataPager ID="uoRoleListPager" runat="server" PagedControlID="uoRoleList"
                    PageSize="20" OnPreRender="uoRoleListPager_PreRender">
                    <Fields>
                        <asp:NumericPagerField ButtonType="link" NumericButtonCssClass="PagerClass" />
                    </Fields>
                </asp:DataPager>
            </td>
        </tr>
    </table>   
     <asp:HiddenField ID="uoHiddenFieldPopupRole" runat="server" Value="0" />
</asp:Content>

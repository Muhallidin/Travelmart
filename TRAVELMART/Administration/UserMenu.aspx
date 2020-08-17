<%@ Page Title="" Language="C#" MasterPageFile="~/TravelMartMaintenanceMaster.Master"
    AutoEventWireup="true" CodeBehind="UserMenu.aspx.cs" Inherits="TRAVELMART.UserMenu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content runat="server" ID="Content3" ContentPlaceHolderID="HeaderContent">
    <div class="ViewTitlePadding">
        <table width="100%" cellpadding="0" cellspacing="0" class="PageTitle">
            <tr>
                <td>
                    User Menu
                </td>
                <td class="RightClass">
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NaviPlaceHolder" runat="server">
    <%--<script type="text/javascript" src="../FBox/jquery-1.6.1.min.js"></script>
    <script type="text/javascript" src="../FBox/fancybox/jquery.mousewheel-3.0.4.pack.js"></script>
    <script type="text/javascript" src="../FBox/fancybox/jquery.fancybox-1.3.4.pack.js"></script>--%>

    <script type="text/javascript">
        $(document).ready(function() {
            SetResolution();
            pageSettings();
        });

        function pageLoad(sender, args) {
            var isAsyncPostback = Sys.WebForms.PageRequestManager.getInstance().get_isInAsyncPostBack();
            if (isAsyncPostback) {
                SetResolution();
                pageSettings();
            }

        }

        function SetResolution() {
            var ht = $(window).height();
            var wd = $(window).width() * 0.90;
            if (screen.height <= 600) {
                ht = ht * 0.40;
            }
            else if (screen.height <= 720) {
                ht = ht * 0.60;
            }
            else {
                ht = ht * 0.63;
            }

            $("#Bv").height(ht);
            $("#Bv").width(wd);
            $("#Av").width(wd);

        }
        
        function pageSettings() {
            $(".EditLink").fancybox(
                {
                    'width': '35%',
                    'height': '60%',
                    'autoScale': false,
                    'transitionIn': 'fadeIn',
                    'transitionOut': 'fadeOut',
                    'type': 'iframe',
                    'onClosed': function() {
                        var a = $("#<%=uoHiddenFieldEdit.ClientID %>").val();
                        if (a == '1')
                            $("#aspnetForm").submit();
                    }
                });
            $("a#<%=uoHyperLinkAdd.ClientID %>").fancybox(
        {
            'width': '35%',
            'height': '20%',
            'autoScale': false,
            'transitionIn': 'fadeIn',
            'transitionOut': 'fadeOut',
            'type': 'iframe',
            'onClosed': function() {
                var a = $("#<%=uoHiddenFieldAdd.ClientID %>").val();
                if (a == '1')
                    $("#aspnetForm").submit();
            }
        });

            $("#<%=uoButtonAdd.ClientID %>").click(function() {
                if ($("#<%=uoDropDownListRole.ClientID %>").val() == "0") {
                    alert("Select Role");
                    return false;
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

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div id="Av" style="overflow: auto; overflow-x: hidden; overflow-y: hidden;">
                <table width="100%">
                    <tr>
                        <td class="LeftClass">
                            Role:&nbsp;
                            <asp:DropDownList ID="uoDropDownListRole" runat="server" Width="200px" DataTextField="role"
                                DataValueField="rid" AppendDataBoundItems="True" AutoPostBack="True" OnSelectedIndexChanged="uoDropDownListRole_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td class="RightClass">
                            <a id="uoHyperLinkAdd" runat="server" href="UserMenuAdd.aspx">
                                <asp:Button ID="uoButtonAdd" runat="server" Text="Add" CssClass="SmallButton" />
                            </a>
                        </td>
                    </tr>
                </table>
            </div>
            <div id="Bv" style="overflow: auto; overflow-x: auto; overflow-y: auto;">
                <asp:GridView ID="uoGridViewMenu" runat="server" CssClass="listViewTable" AutoGenerateColumns="False"
                    OnRowCommand="uoGridViewMenu_RowCommand" OnRowDeleting="uoGridViewMenu_RowDeleting">
                    <Columns>
                        <asp:BoundField DataField="colMenuIDInt" HeaderText="MenuID" />
                        <asp:BoundField DataField="colDisplayNameVarchar" HeaderText="Menu" ItemStyle-CssClass="leftAligned">
                            <ItemStyle CssClass="leftAligned"></ItemStyle>
                        </asp:BoundField>
                        <asp:TemplateField ItemStyle-Width="100px" HeaderText="Submenu">
                            <ItemTemplate>
                                <asp:HyperLink CssClass="EditLink" Visible='<%#ViewIsVisible(Eval("colPageIDInt")) %>'
                                    runat="server" ID="uoHyperLinkView" Text="View" NavigateUrl='<%# "UserSubmenu.aspx?mID=" + Eval("colPageIDInt") + "&mName=" + Eval("colDisplayNameVarchar") %>'></asp:HyperLink>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-Width="100px">
                            <ItemTemplate>
                                <asp:LinkButton CommandName="Delete" CommandArgument='<%#Eval("colMenuIDInt") %>'
                                    runat="server" ID="uoLinkButtonDelete" Text="Delete" OnClientClick="return confirmDelete()"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <asp:HiddenField ID="uoHiddenFieldEdit" runat="server" Value="0" />
                <asp:HiddenField ID="uoHiddenFieldAdd" runat="server" Value="0" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

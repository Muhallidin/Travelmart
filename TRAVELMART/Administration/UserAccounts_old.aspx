<%@ Page Title="" Language="C#" MasterPageFile="~/TravelmartMaster.Master" AutoEventWireup="true"
    CodeBehind="UserAccounts_old.aspx.cs" Inherits="TRAVELMART.Administration.UserAccounts_old" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   
        
    <%--<script type="text/javascript" src="../FBox/jquery-1.6.1.min.js"></script>
    <script type="text/javascript" src="../FBox/fancybox/jquery.mousewheel-3.0.4.pack.js"></script>
    <script type="text/javascript" src="../FBox/fancybox/jquery.fancybox-1.3.4.pack.js"></script>--%>

    <script type="text/javascript" language="javascript">
        function confirmDelete() {
            if (confirm("Delete record?") == true)
                return true;
            else
                return false;
        }

        function confirmUnlock() {
            if (confirm("Unlock User?") == true)
                return true;
            else
                return false;
        }
        
        function confirmReset() {
            if (confirm("Reset Password?") == true)
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

    function ShowPopup() 
    {
        $("a#<%=uoHyperLinkUserAdd.ClientID %>").fancybox(
        {
            'width': '75%',
            'height': '112%',
            'autoScale': false,
            'transitionIn': 'fadeIn',
            'transitionOut': 'fadeOut',
            'type': 'iframe',
            'onClosed': function() {
                var a = $("#<%=uoHiddenFieldPopupUser.ClientID %>").val();
                if (a == '1')
                    $("#aspnetForm").submit();
            }
        });

        $(".UserLink").fancybox(
        {
            'width': '75%',
            'height': '112%',
            'autoScale': false,
            'transitionIn': 'fadeIn',
            'transitionOut': 'fadeOut',
            'type': 'iframe',
            'onClosed': function() {
                var a = $("#<%=uoHiddenFieldPopupUser.ClientID %>").val();
                if (a == '1')
                    $("#aspnetForm").submit();
            }
        });
        $("#<%=uoTextBoxSearchParam.ClientID %>").keypress(function(ev) {
            if (ev.which == "13") {
                $("#<%=uoHiddenFieldIsRefreshUserList.ClientID %>").val("1");
                $("#aspnetForm").submit();
            }

        });
    }                    
    </script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    
    <table width="100%" id="uotableAccount" runat="server">
        <tr>
            <td style="padding-left: 3px; padding-right: 3px;">
                <div>
                    <table style="width: 100%" cellspacing="0">
                        <tr>
                            <td style="width: 40%; text-align: left;" class="PageTitle">
                                <asp:Panel ID="uopaneluserhead" runat="server">
                                    User List
                                    <asp:Label ID="uolabeluser" runat="server" Text="" Font-Size="Small" />
                                </asp:Panel>
                            </td>
                            <td style="text-align: right; background-color: #82ABB8; padding-right: 3px;">                                
                                <a id="uoHyperLinkUserAdd" runat="server">
                                    <asp:Button ID="uoBtnAddAdd" runat="server" Text="Add" Font-Size="X-Small" />
                                </a>
                            </td>           
                        </tr>
                        <tr>
                            <td class="LeftClass">
                                <span>User Role:</span>
                                <asp:DropDownList ID="uoDropDownUserType" runat="server" Width="250px" AutoPostBack="True"
                                    AppendDataBoundItems="True" DataTextField="RoleToAccess"  DataValueField="RoleToAccess" 
                                    onselectedindexchanged="uoDropDownUserType_SelectedIndexChanged">
                                    <asp:ListItem Text="" Value=""/>
                                </asp:DropDownList>                               
                            </td>
                            <td class="LeftClass">
                                Name : <asp:TextBox ID="uoTextBoxSearchParam" runat="server" Font-Size="7.5pt" Width="180px" />
                                <asp:Button ID="uoButtonSearch" runat="server" Text="Search" 
                                    Font-Size="X-Small" onclick="uoButtonSearch_Click" />
                            </td>                           
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
        <tr>
            <td class="Module" align="right">
                <asp:ListView runat="server" ID="uoUserList" OnItemCommand="uoUserList_ItemCommand"
                    OnItemDeleting="uoUserList_ItemDeleting">
                    <LayoutTemplate>
                        <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                            <tr>
                                <th>Username</th>
                                <th>Name</th>
                                <th>Role</th>                                      
                                <th runat="server" style="width: 5%" id="EditTH" />  
                                <th runat="server" style="width: 5%" id="UnlockTH" />  
                                <th runat="server" style="width: 5%" id="ResetTH" />                                  
                                <th runat="server" id = "DeleteTH"></th>                                
                            </tr>
                            <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                        </table>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <tr>
                            <td class="leftAligned">
                             <%# Eval("uname")%>
                            </td>
                            <td class="leftAligned">
                             <%# Eval("name")%>
                            </td>
                            <td class="leftAligned">
                             <%# Eval("role")%>
                            </td>                                                                             
                            <td class='<%# (HideElemet()==""?"leftAligned":HideElemet()) %>'>
                                <a runat="server" class="UserLink" id="uoEditUser" href='<%# "~/Administration/CreateUserAccount.aspx?uId=" + Eval("uid") %>'>
                                    Edit</a>
                            </td>
                             <td class='<%# (HideDeleteElement()==""?"leftAligned":HideDeleteElement()) %>'>
                                <asp:LinkButton ID="LinkButton1" runat="server" OnClientClick="return confirmUnlock();"
                                    CommandArgument='<%#Eval("uname") %>' CommandName="Unlock" >Unlock</asp:LinkButton>
                            </td>
                             <td class='<%# (HideDeleteElement()==""?"leftAligned":HideDeleteElement()) %>'>
                                <asp:LinkButton ID="LinkButton3" runat="server" OnClientClick="return confirmReset();"
                                    CommandArgument='<%#Eval("uname") %>' CommandName="Reset" >Reset</asp:LinkButton>
                            </td>
                           
                            <td class='<%# (HideDeleteElement()==""?"leftAligned":HideDeleteElement()) %>'>
                                <asp:LinkButton ID="uoLinkButtonDelete" runat="server" OnClientClick="return confirmDelete();"
                                    CommandArgument='<%# Eval("uid") + "::" + Eval("uname") %>' CommandName="Delete" >Delete</asp:LinkButton>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <EmptyDataTemplate>
                        <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                            <tr>
                                <th>Username</th>
                                <th>Name</th>
                                <th>Role</th>
                                <th>Regions</th>
                                <%--<th>Password</th>--%>     
                            </tr>
                            <tr>
                                <td colspan="4" class="leftAligned">
                                    No Record
                                </td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                </asp:ListView>
                <asp:DataPager ID="uoUserListPager" runat="server" PagedControlID="uoUserList"
                    PageSize="20" OnPreRender="uoUserListPager_PreRender">
                    <Fields>
                        <asp:NumericPagerField ButtonType="link" NumericButtonCssClass="PagerClass" />
                    </Fields>
                </asp:DataPager>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="uoHiddenFieldPopupUser" runat="server" Value="0" />
    <asp:HiddenField ID="uoHiddenFieldRole" runat="server" Value="" />
    <asp:HiddenField ID="uoHiddenFieldUser" runat="server" Value="" />
    <asp:HiddenField ID="uoHiddenFieldIsRefreshUserList" runat="server" Value="0" />
    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

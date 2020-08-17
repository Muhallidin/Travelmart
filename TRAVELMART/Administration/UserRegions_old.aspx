<%@ Page Title="" Language="C#" MasterPageFile="~/TravelmartMaster.Master" AutoEventWireup="true" CodeBehind="UserRegions.aspx.cs" Inherits="TRAVELMART.UserRegions_old" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--<script type="text/javascript" src="../FBox/jquery-1.6.1.min.js"></script>
    <script type="text/javascript" src="../FBox/fancybox/jquery.mousewheel-3.0.4.pack.js"></script>
    <script type="text/javascript" src="../FBox/fancybox/jquery.fancybox-1.3.4.pack.js"></script>--%>
    
<script language="javascript" type="text/javascript">
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
            if ($("#<%=uoDropDownListRoles.ClientID %>").val() == "") {
                alert("Select Role");
                return false;
            }
            if ($("#<%=uoDropDownListUsers.ClientID %>").val() == "0") {
                alert("Select User");
                return false;
            }
        });
    }

    function confirmDelete() {
        if (confirm("Delete record?") == true)
            return true;
        else
            return false;
    }
</script>
    
<div class="ViewTitlePadding"  >        
    <table width="100%" cellpadding="0" cellspacing="0" class="PageTitle">
        <tr>
            <td>User Regions</td>            
            <td class="RightClass">
               
            </td>
        </tr>
    </table>    
</div>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    
    
<table width="100%">
    <tr>
        <td class="LeftClass">
        <span>Roles:</span>&nbsp;
         <asp:DropDownList ID="uoDropDownListRoles" runat="server" Width="250px" AutoPostBack="True"
            AppendDataBoundItems="True" DataTextField="RoleToAccess"  DataValueField="RoleToAccess" 
                onselectedindexchanged="uoDropDownListRoles_SelectedIndexChanged" >
            <asp:ListItem Text="" Value=""/>
        </asp:DropDownList>    
        </td>
        <td class="LeftClass">
            <span>Users:</span>&nbsp;
             <asp:DropDownList ID="uoDropDownListUsers" runat="server" Width="250px" AutoPostBack="True"
            AppendDataBoundItems="True" DataTextField="UNAME" DataValueField="UNAME" 
                onselectedindexchanged="uoDropDownListUsers_SelectedIndexChanged" >
            <asp:ListItem Text="" Value=""/>
        </asp:DropDownList>  
        </td>
        <td class="RightClass">
                <a id="uoHyperLinkAdd" runat="server" href ="#">
                    <asp:Button ID="uoButtonAdd" runat="server" Text="Add" 
                    CssClass="SmallButton"/>
                </a>
        </td>
    </tr>
    <tr>
        <td colspan="3">
            <asp:GridView ID="uoGridViewRegions" runat="server" CssClass="listViewTable" 
                AutoGenerateColumns="False" onrowcommand="uoGridViewRegions_RowCommand" 
                onrowdeleting="uoGridViewRegions_RowDeleting">
            <Columns>
                <asp:BoundField DataField="colRegionNameVarchar" HeaderText = "Region" ItemStyle-CssClass="leftAligned"/>
                <asp:TemplateField ItemStyle-Width="100px">
                    <ItemTemplate>
                        <asp:LinkButton Visible='<%# Convert.ToBoolean((Eval("IsExist"))) %>' CommandName="Delete" CommandArgument=<%#Eval("colUserRegionIDInt") %> runat="server" ID="uoLinkButtonDelete" Text="Delete" OnClientClick="return confirmDelete()"></asp:LinkButton>
                    </ItemTemplate>                                                
                </asp:TemplateField>
            </Columns>
            </asp:GridView>
        </td>
    </tr>
</table>

<%--<div style="display:none">
<div id ="uoDivAddRegions" style="width:450px; height:200px" >
    <table width="100%">
        <tr>
            <td colspan="3">                
                <div class="PageTitle">
                    Add User Region
                </div>
                <hr/>
            </td>
        </tr>
        <tr>
            <td class="leftAligned">
                Regions:
            </td>
            <td class="leftAligned">
                <asp:DropDownList ID="uoDropDownListRegion" runat="server" Width="300px" 
                    DataTextField="colMapNameVarchar"
                    DataValueField="colMapIDInt"
                    AppendDataBoundItems="True" >
                </asp:DropDownList>
            </td>
            <td class="leftAligned">
                <asp:Button ID="uoButtonSave" runat="server" Text="Save" 
                    onclick="uoButtonSave_Click" />
            </td>
        </tr>
    </table>
</div>
</div>--%>
<asp:HiddenField runat="server" ID="uoHiddenFieldAdd" Value="0"/>
<asp:HiddenField runat="server" ID="uoHiddenFieldRole" Value=""/>
<asp:HiddenField runat="server" ID="uoHiddenFieldUser" Value=""/>
</ContentTemplate>
</asp:UpdatePanel>
</asp:Content>

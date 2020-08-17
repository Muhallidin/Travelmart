<%@ Page Title="" Language="C#" MasterPageFile="~/TravelMartMaintenanceMaster.Master"
    AutoEventWireup="true" CodeBehind="UserRegions.aspx.cs" Inherits="TRAVELMART.UserRegions" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content runat="server" ID="Content3" ContentPlaceHolderID="HeaderContent">
    <div class="ViewTitlePadding">
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr class="PageTitle">
                <td colspan="2">
                    User Regions
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

    <script language="javascript" type="text/javascript">
        $(document).ready(function() {
            SetResolution();
            ShowPopup();
            controlSettings();
        });

        function pageLoad(sender, args) {
            var isAsyncPostback = Sys.WebForms.PageRequestManager.getInstance().get_isInAsyncPostBack();
            if (isAsyncPostback) {
                SetResolution();
                ShowPopup();
                controlSettings();
            }
        }

        function SetResolution() {
            var ht = $(window).height();
            var wd = $(window).width() * 0.90;
            if (screen.height <= 600) {
                ht = ht * 0.20;
            }
            else if (screen.height <= 720) {
                ht = ht * 0.39;
            }
            else {
                ht = ht * 0.53;
            }

            $("#Bv").height(ht);
            $("#Bv").width(wd);
            $("#Av").width(wd);

        }
        function ShowPopup() {

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
        function selectRecord(source) {

            $('table[id$=uoTableRegionToAdd' + '] input:checkbox[name *= uoCheckBoxSelect]').attr('checked', source.checked);
        }

        function controlSettings() {
            $('table[id$=uoTableRegionToAdd]').each(function() {
                $('input:checkbox[name *= uoCheckBoxSelect]').click(function(ev) {
               
                    if (this.value == 'off') {
                        $('input:checkbox[name *= uoCheckBoxCheck]').attr('checked', false);
                    }
                   
                });
            })
        }
    </script>

   <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">--%>
        <%--<ContentTemplate>--%>
            <div id="Av" style="overflow: auto; overflow-x: hidden; overflow-y: hidden;">
                <table width="100%">
                    <tr>
                        <td class="LeftClass">
                           <%-- <asp:UpdatePanel runat="server" ID="uoPanelRole" UpdateMode="Conditional">
                                <ContentTemplate>--%>
                                    <span>Roles:</span>&nbsp;
                                    <asp:DropDownList ID="uoDropDownListRoles" runat="server" Width="250px" AutoPostBack="True"
                                        AppendDataBoundItems="True" DataTextField="RoleToAccess" DataValueField="RoleToAccess"
                                        OnSelectedIndexChanged="uoDropDownListRoles_SelectedIndexChanged">
                                        <asp:ListItem Text="" Value="" />
                                    </asp:DropDownList>
                               <%-- </ContentTemplate>
                            </asp:UpdatePanel>--%>
                        </td>
                    </tr>
                    <tr>
                        <td class="LeftClass">
                            <%--<asp:UpdatePanel runat="server" ID="uoPanelUsers" UpdateMode="Conditional">
                                <ContentTemplate>--%>
                                    <span>Users:</span>&nbsp;
                                    <asp:DropDownList ID="uoDropDownListUsers" runat="server" Width="250px" AutoPostBack="True"
                                        AppendDataBoundItems="True" DataTextField="UNAME" DataValueField="UNAME" OnSelectedIndexChanged="uoDropDownListUsers_SelectedIndexChanged">
                                        <asp:ListItem Text="" Value="" />
                                    </asp:DropDownList>
                                <%--</ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="uoDropDownListRoles" EventName="SelectedIndexChanged" />
                                </Triggers>
                            </asp:UpdatePanel>--%>
                        </td>
                        <%--<td class="RightClass">
                            <a id="uoHyperLinkAdd" runat="server" href="#">
                                <asp:Button ID="uoButtonAdd" runat="server" Text="Add" CssClass="SmallButton" />
                            </a>
                        </td>--%>
                    </tr>
                </table>
            </div>
            <div id="Bv" style="overflow: auto; overflow-x: auto; overflow-y: auto;">
                <table width="100%" runat="server" id="uoTableRegion">
                    <tr>
                        <td style="vertical-align: top; width: 45%;text-align:left">
                            <asp:ListView runat="server" ID="uoListViewRegionToAdd" class="add">
                                <LayoutTemplate>
                                    <table border="0" cellpadding="0" cellspacing="0" class="listViewTable2" width="300px" id="uoTableRegionToAdd">
                                        <tr>
                                            <th>
                                                <asp:CheckBox ID="uoCheckBoxCheck" runat="server" onclick="selectRecord(this);"/>
                                            </th>
                                            <th>
                                                Region
                                            </th>
                                        </tr>
                                        <asp:PlaceHolder runat="server" ID="itemPlaceHolder"></asp:PlaceHolder>
                                    </table>
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td class="leftAligned">
                                            <asp:CheckBox ID="uoCheckBoxSelect" runat="server" />
                                        </td>
                                        <td class="leftAligned">
                                            <asp:HiddenField ID="uoHiddenFieldRegion" runat="server" Value='<%# Eval("RegionID") %>' />
                                            <asp:Label ID="uoLabelRegion" runat="server" Text='<%# Eval("RegionName")%>' />
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <EmptyDataTemplate>
                                    <table border="0" cellpadding="0" cellspacing="0" class="listViewTable" width="500px">
                                        <tr>
                                            <th>
                                                Region
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
                        <td style="vertical-align: top;text-align:center" align="center">
                              <table style="text-align:center">
                                <tr>
                                    <td>
                                        <asp:Button ID="uoButtonAdd" runat="server" CssClass="SmallButton" Text="Add >>"
                                            ValidationGroup="Region" Width="100px" OnClick="uoButtonAdd_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Button ID="uoButtonDelete" runat="server" CssClass="SmallButton" Text="<< Remove"
                                            ValidationGroup="Region" Width="100px" OnClick="uoButtonDelete_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                        <br />
                                        <br />
                                        
                                        <asp:Button ID="uoButtonSave" runat="server" CssClass="SmallButton" Text="Save"
                                            ValidationGroup="Region" Width="100px" OnClick="uoButtonSave_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="vertical-align: top; width: 45%; text-align:left">
                            <asp:ListView runat="server" ID="uoListViewRegion">
                                <LayoutTemplate>
                                    <table border="0" cellpadding="0" cellspacing="0" class="listViewTable2" width="400px">
                                        <tr>
                                            <th>
                                                
                                            </th>
                                            <th>
                                                Region
                                            </th>
                                        </tr>
                                        <asp:PlaceHolder runat="server" ID="itemPlaceHolder"></asp:PlaceHolder>
                                    </table>
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td class="leftAligned">
                                            <asp:CheckBox ID="uoCheckBoxSelect" runat="server" Enabled = '<%# Eval("IsExist") %>' />
                                        </td>
                                        <td class="leftAligned">
                                            <asp:HiddenField ID="uoHiddenFieldUserRegion" runat="server" Value='<%# Eval("UserRegionID") %>' />
                                            <asp:HiddenField ID="uoHiddenFieldRegion" runat="server" Value='<%# Eval("RegionID") %>' />
                                            <asp:Label ID="uoLabelRegion" runat="server" Text='<%# Eval("RegionName")%>' />
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <EmptyDataTemplate>
                                    <table border="0" cellpadding="0" cellspacing="0" class="listViewTable" width="300px">
                                        <tr>
                                            <th>
                                                Region
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
                </table>
               <%-- <table width="100%">
                    <tr>
                        <td colspan="3" class="Module">
                            <asp:GridView ID="uoGridViewRegions" runat="server" CssClass="listViewTable" AutoGenerateColumns="False"
                                OnRowCommand="uoGridViewRegions_RowCommand" OnRowDeleting="uoGridViewRegions_RowDeleting">
                                <Columns>
                                    <asp:BoundField DataField="colRegionNameVarchar" HeaderText="Region" ItemStyle-CssClass="leftAligned" />
                                    <asp:TemplateField ItemStyle-Width="100px">
                                        <ItemTemplate>
                                            <asp:LinkButton Visible='<%# Convert.ToBoolean((Eval("IsExist"))) %>' CommandName="Delete"
                                                CommandArgument='<%#Eval("colUserRegionIDInt") %>' runat="server" ID="uoLinkButtonDelete"
                                                Text="Delete" OnClientClick="return confirmDelete()"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>--%>
                <asp:HiddenField runat="server" ID="uoHiddenFieldRole" Value="" />
                <asp:HiddenField runat="server" ID="uoHiddenFieldUser" Value="" />
                <asp:HiddenField runat="server" ID="uoHiddenFieldAdd" Value="0" />
            </div>
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
        <%--</ContentTemplate>--%>
    <%--</asp:UpdatePanel>--%>
</asp:Content>

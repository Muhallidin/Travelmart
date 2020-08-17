<%@ Page Title="" Language="C#" MasterPageFile="~/TravelmartMaster.Master" AutoEventWireup="true" CodeBehind="RegionView_old.aspx.cs" Inherits="TRAVELMART.RegionView_old" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <script type="text/javascript" language="javascript">
        $(document).ready(function() {
            ShowPopup();            
        });

        function confirmDelete() {
            if (confirm("Delete record?") == true)
                return true;
            else
                return false;
        }
       
        function pageLoad(sender, args) {
            var isAsyncPostback = Sys.WebForms.PageRequestManager.getInstance().get_isInAsyncPostBack();
            if (isAsyncPostback) {
                ShowPopup();
            }
        }

        function ShowPopup() {
            $("a#<%=uoHyperLinkRegionAdd.ClientID %>").fancybox(
                {
                    'width': '40%',
                    'height': '50%',
                    'autoScale': false,
                    'transitionIn': 'fadeIn',
                    'transitionOut': 'fadeOut',
                    'type': 'iframe',
                    'onClosed': function() {
                        var a = $("#<%=uoHiddenFieldPopupRegion.ClientID %>").val();
                        if (a == '1')
                            $("#aspnetForm").submit();
                    }
                });

            $(".RegionLink").fancybox(
                {
                    'width': '40%',
                    'height': '50%',
                    'autoScale': false,
                    'transitionIn': 'fadeIn',
                    'transitionOut': 'fadeOut',
                    'type': 'iframe',
                    'onClosed': function() {
                        var a = $("#<%=uoHiddenFieldPopupRegion.ClientID %>").val();
                        if (a == '1')
                            $("#aspnetForm").submit();
                    }
                });              
        }
        function DeleteRegion(RegionID) {
            if (confirm("Delete record?") == true) {
                $("#<%=uoHiddenFieldRegionId.ClientID %>").val(RegionID);
                $("#<%=uoHiddenFieldIsDelete.ClientID %>").val(1);
                $("#aspnetForm").submit();
            }
            else {
                $("#<%=uoHiddenFieldIsDelete.ClientID %>").val(0);
                $("#<%=uoHiddenFieldRegionId.ClientID %>").val(0);
                return false;
            }
        }
    </script>
    
    <table width="100%" id="uotableRegionList" runat="server" >
        <tr>
            <td style="padding-left: 3px; padding-right: 3px;" colspan="2">
                <div>
                    <table style="width: 100%" cellspacing="0">
                        <tr>
                            <td style="width:40%; text-align:left;" class="PageTitle">
                                <asp:Panel ID="uopanelregionhead" runat="server">
                                    Region List
                                </asp:Panel>
                            </td>
                            <td style="text-align: right; background-color: #82ABB8; padding-right: 3px;">
                                <a id="uoHyperLinkRegionAdd" runat="server">
                                    <asp:Button ID="uoBtnRegionAdd" runat="server" Text="Add" Font-Size="X-Small" />
                                </a>                                
                            </td>
                        </tr>
                        <tr><td colspan="2">                           
                        </td></tr>
                        <tr>
                            <td colspan="2" align="right" class="RightClass">
                                Region: 
                                <asp:TextBox ID="uoTextSearchParam" runat="server" Font-Size="7.5pt" Width="200px">
                                </asp:TextBox>
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
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                         <asp:ListView runat="server" ID="uoRegionList" 
                            onitemcommand="uoRegionList_ItemCommand" 
                            onitemdeleting="uoRegionList_ItemDeleting"
                            >
                            <LayoutTemplate>
                                <table border="0" cellpadding="0" cellspacing="0" class="listViewTable2">
                                    <tr>
                                        <th>
                                            Seaport
                                        </th>
                                        <th runat="server" style="width: 5%" id="EditTh"></th>
                                        <th runat="server" style="width: 6%" id="DeleteTh"></th>                                        
                                    </tr>
                                    <asp:PlaceHolder runat="server" ID="itemPlaceHolder"></asp:PlaceHolder>
                                </table>
                            </LayoutTemplate>
                            <ItemTemplate>
                            <%# RegionAddGroup() %>
                                <tr>
                                    <td class="leftAligned" colspan=3>
                                        <%# Eval("colPortNameVarchar")%>
                                    </td>                                        
                                    <%--<td class="leftAligned">
                                        <a runat="server" class="RegionLink" id="uoAEditRegion" href='<%# "RegionAdd.aspx?vmId=" + Eval("colRegionIDInt") + "&vmName=" + Eval("colRegionNameVarchar") %>'>
                                            Edit
                                        </a>
                                    </td>--%>                                 
                                   <%--<td class="leftAligned">
                                       <asp:LinkButton ID="uoLinkButtonDelete" runat="server" CommandArgument='<%# Eval("colRegionIDInt") %>' Text="Delete" CommandName="Delete"
                                        OnClientClick="return confirmDelete();">
                                       </asp:LinkButton>
                                   </td>--%>
                                </tr>
                            </ItemTemplate>
                           
                            <EmptyDataTemplate>
                                <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                                    <tr>
                                        <th>
                                            Seaport
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
                          
                        <asp:DataPager ID="uoRegionListPager" runat="server" PagedControlID="uoRegionList"
                            PageSize="20" onprerender="uoRegionListPager_PreRender">
                            <Fields>
                                <asp:NumericPagerField ButtonType="Link" NumericButtonCssClass="PagerClass" />
                            </Fields>
                        </asp:DataPager>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="uoRegionListPager" EventName="PreRender" />
                        <asp:AsyncPostBackTrigger ControlID="uoButtonSearch" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" EnablePaging="True" 
                    OldValuesParameterFormatString="oldCount_{0}" 
                    SelectCountMethod="MasterfileRegionViewCount" 
                    SelectMethod="MasterfileRegionView" 
                    TypeName="TRAVELMART.BLL.MasterfileBLL" 
                    DeleteMethod="MasterfileRegionDelete">
                    <DeleteParameters>
                        <asp:ControlParameter ControlID="uoHiddenFieldRegionId" DbType="Int32" 
                            Name="regionId" PropertyName="Value" />
                        <asp:ControlParameter ControlID="uoHiddenFieldUser" Name="ModifiedBy" 
                            PropertyName="Value" />
                    </DeleteParameters>
                    <SelectParameters>
                        <asp:ControlParameter ControlID="uoTextSearchParam" Name="regionName" 
                            PropertyName="Text" />
                    </SelectParameters>
                </asp:ObjectDataSource>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="uoHiddenFieldRegionId" runat="server" Value="0" />
    <asp:HiddenField ID="uoHiddenFieldUser" runat="server" Value="0" />
    <asp:HiddenField ID="uoHiddenFieldPopupRegion" runat="server" Value="0" />
    <asp:HiddenField ID="uoHiddenFieldIsDelete" runat="server" Value="0" />
</asp:Content>

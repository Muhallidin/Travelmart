<%@ Page Title="" Language="C#" MasterPageFile="~/TravelMartMaintenanceMaster.Master"
    AutoEventWireup="true" CodeBehind="RegionView.aspx.cs" Inherits="TRAVELMART.RegionView" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content runat="server" ID="Content3" ContentPlaceHolderID="HeaderContent">
    <div style="padding-left: 3px; padding-right: 3px;">
        <table style="width: 100%" cellspacing="0">
            <tr>
                <td style="width: 40%; text-align: left;" class="PageTitle">
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
            <tr>
                <td colspan="2">
                </td>
            </tr>
            <tr>
                <td colspan="2" align="right" class="RightClass">
                    Region:
                    <asp:TextBox ID="uoTextSearchParam" runat="server" Font-Size="7.5pt" Width="200px">
                    </asp:TextBox>
                    <asp:Button ID="uoButtonSearch" runat="server" Text="Search" Font-Size="X-Small"
                        OnClick="uoButtonSearch_Click" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NaviPlaceHolder" runat="server">

    <script type="text/javascript" language="javascript">
        $(document).ready(function() {
            SetResolution();
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
                SetResolution();
                ShowPopup();
            }
        }

        function ShowPopup() {
            $("a#<%=uoHyperLinkRegionAdd.ClientID %>").fancybox(
                {
                    'width': '80%',
                    'height': '100%',
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
                    'width': '80%',
                    'height': '100%',
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

        function SetResolution() {
            var ht = $(window).height();

            var wd = $(window).width() * 0.90;

            if (screen.height <= 600) {
                ht = ht * 0.45;

            }
            else if (screen.height <= 720) {
                ht = ht * 0.60;
            }
            else {
                ht = ht * 0.65;
            }

            $("#Bv").height(ht);
            $("#Bv").width(wd);

        }                 
    </script>

    <div id="Bv" style="overflow: auto; width: 100%; overflow-x: auto; overflow-y: auto;">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <table width="98%">
                    <tr>
                        <td>
                            <asp:ListView runat="server" ID="uoRegionList" OnItemCommand="uoRegionList_ItemCommand"
                                OnItemDeleting="uoRegionList_ItemDeleting">
                                <LayoutTemplate>
                                    <table border="0" cellpadding="0" cellspacing="0" class="listViewTable2">
                                        <tr>
                                            <th>
                                                Seaport
                                            </th>
                                            <th runat="server" style="width: 5%" id="EditTh">
                                            </th>
                                            <th runat="server" style="width: 6%" id="DeleteTh">
                                            </th>
                                        </tr>
                                        <asp:PlaceHolder runat="server" ID="itemPlaceHolder"></asp:PlaceHolder>
                                    </table>
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <%# RegionAddGroup() %>
                                    <tr>
                                        <td class="leftAligned" colspan="3">
                                            <%# Eval("colPortNameVarchar")%>
                                        </td>
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
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <asp:DataPager ID="uoRegionListPager" runat="server" PagedControlID="uoRegionList"
                                PageSize="20" OnPreRender="uoRegionListPager_PreRender">
                                <Fields>
                                    <asp:NumericPagerField ButtonType="Link" NumericButtonCssClass="PagerClass" />
                                </Fields>
                            </asp:DataPager>
                            <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" EnablePaging="True" OldValuesParameterFormatString="oldCount_{0}"
                                SelectCountMethod="MasterfileRegionViewCount" SelectMethod="MasterfileRegionView"
                                TypeName="TRAVELMART.BLL.MasterfileBLL" DeleteMethod="MasterfileRegionDelete">
                                <DeleteParameters>
                                    <asp:ControlParameter ControlID="uoHiddenFieldRegionId" DbType="Int32" Name="regionId"
                                        PropertyName="Value" />
                                    <asp:ControlParameter ControlID="uoHiddenFieldUser" Name="ModifiedBy" PropertyName="Value" />
                                </DeleteParameters>
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="uoHiddenFieldRegion" Name="regionName" PropertyName="Value" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="uoRegionListPager" EventName="PreRender" />
                
            </Triggers>
        </asp:UpdatePanel>
        <asp:HiddenField ID="uoHiddenFieldRegionId" runat="server" Value="0" />
        <asp:HiddenField ID="uoHiddenFieldUser" runat="server" Value="0" />
        <asp:HiddenField ID="uoHiddenFieldPopupRegion" runat="server" Value="0" />
        <asp:HiddenField ID="uoHiddenFieldIsDelete" runat="server" Value="0" />
        <asp:HiddenField runat="server" ID="uoHiddenFieldRegion" Value="" />
    </div>
</asp:Content>

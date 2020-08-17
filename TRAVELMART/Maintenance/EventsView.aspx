<%@ Page Title="" Language="C#" MasterPageFile="~/TravelMartMaster2.Master" AutoEventWireup="true"
    CodeBehind="EventsView.aspx.cs" Inherits="TRAVELMART.EventsView" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script type="text/javascript" language="javascript">
    $(document).ready(function() {
        SetResolution();
        ShowPopup();
    });

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

    
                           
    </script>
</asp:Content>
<asp:Content runat="server" ID="Content3" ContentPlaceHolderID="HeaderContent">
    <script type="text/javascript">
        function pageLoad(sender, args) {
            var isAsyncPostback = Sys.WebForms.PageRequestManager.getInstance().get_isInAsyncPostBack();
            if (isAsyncPostback) {
                SetResolution();
                ShowScreen();
            }
        }

        $(document).ready(function() {
            SetResolution();
            ShowScreen();
        });
        function ShowScreen() {
            $("a#<%=uoHyperLinkEventsAdd.ClientID %>").fancybox(
                {
                    'width': '60%',
                    'height': '65%',
                    'autoScale': false,
                    'transitionIn': 'fadeIn',
                    'transitionOut': 'fadeOut',
                    'type': 'iframe',
                    'onClosed': function() {
                        var a = $("#<%=uoHiddenFieldPopupEvent.ClientID %>").val();
                        if (a == '1')
                            $("#aspnetForm").submit();
                    }
                });
        }
    </script>
    <table class="PageTitle" width="100%">
        <tr>
            <td class="LeftClass" style="width: 25%;">
                Events Maintenance
            </td>
            <td class="RightClass">
                <a id="uoHyperLinkEventsAdd" runat="server">
                    <asp:Button Font-Size="X-Small" runat="server" ID="uoButtonAdd" Text="Add" />
                </a>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NaviPlaceHolder" runat="server">

    
<script type="text/javascript">
    $(document).ready(function() {
        SetResolution();
        ShowPopup();
    });

    function pageLoad(sender, args) {
        var isAsyncPostback = Sys.WebForms.PageRequestManager.getInstance().get_isInAsyncPostBack();
        if (isAsyncPostback) {
            SetResolution();
            ShowPopup();
        }
    }

    function ShowPopup() {

        $(".EventsLink").fancybox(
        {
            'width': '60%',
            'height': '65%',
            'autoScale': false,
            'transitionIn': 'fadeIn',
            'transitionOut': 'fadeOut',
            'type': 'iframe',
            'onClosed': function() {
                var a = $("#<%=uoHiddenFieldPopupEvent.ClientID %>").val();
                if (a == '1')
                    $("#aspnetForm").submit();
            }
        });
    }
</script>
    <div id="Bv" style="overflow: auto; width: 100%; overflow-x: auto; overflow-y: auto;">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <table width="98%">
                    <tr>
                        <td class="LeftClass">
                            <asp:Button runat="server" ID="uoButtonViewAll" Text="View All" Font-Size="X-Small"
                                OnClick="uoButtonViewAll_Click" Visible="false" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:ListView runat="server" ID="uoEventList" OnItemCommand="uoEventList_ItemCommand"
                                OnItemDeleting="uoEventList_ItemDeleting" 
                                onitemupdated="uoEventList_ItemUpdated" 
                                onitemupdating="uoEventList_ItemUpdating">
                                <LayoutTemplate>
                                    <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                                        <tr>
                                            <th>
                                                Events Name
                                            </th>
                                            <th>
                                                Hotel
                                            </th>
                                            <th>
                                                Date From
                                            </th>
                                            <th>
                                                Date To
                                            </th>
                                            <th runat="server" style="width: 10%" id="DeleteTH">
                                            </th>
                                            <th>
                                                Tag as Done
                                            </th>
                                        </tr>
                                        <asp:PlaceHolder ID="itemPlaceholder" runat="server"></asp:PlaceHolder>
                                    </table>
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td class="leftAligned">
                                            <a runat="server" class="EventsLink" id="a1" href='<%# "EventsAdd.aspx?eId=" + Eval("colEventIdInt") + "&eName=" + Eval("colEventNameVarchar") %>'
                                                visible='<%# !(Convert.ToBoolean(Eval("colisDoneBit"))) %>'>
                                                <%#Eval("colEventNameVarchar") %></a>
                                            <asp:Label runat="server" ID="uoLabelEventName" Text='<%#Eval("colEventNameVarchar") %>'
                                                Visible='<%# (Convert.ToBoolean(Eval("colisDoneBit"))) %>'></asp:Label>
                                        </td>
                                        <td class="leftAligned">
                                            <%# Eval("colVendorBranchNameVarchar") %>
                                        </td>
                                        <td class="leftAligned">
                                            <%# String.Format("{0:dd-MMM-yyyy}", Eval("colEventDateFromDate"))%>
                                        </td>
                                        <td class="leftAligned">
                                            <%# String.Format("{0:dd-MMM-yyyy}", Eval("colEventDateToDate"))%>
                                            <%--<%# Eval("colEventDateToDate") %>--%>
                                        </td>
                                        <td class="leftAligned">
                                            <asp:LinkButton runat="server" ID="uoHyperLinkDelete" CommandArgument='<%# Eval("colEventIdInt") %>'
                                                CommandName="Delete" Text="Delete" Visible='<%# !(Convert.ToBoolean(Eval("colisDoneBit"))) %>'
                                                OnClientClick="return confirmDelete();"></asp:LinkButton>
                                        </td>
                                        <td>
                                            <asp:LinkButton runat="server" ID="LinkButton1" CommandArgument='<%# Eval("colEventIdInt") %>'
                                                CommandName="Update" Text="Tag" Visible='<%# !(Convert.ToBoolean(Eval("colisDoneBit"))) %>'></asp:LinkButton>
                                            <asp:Label runat="server" ID="uoLabelDone" Text="Done" Visible='<%# (Convert.ToBoolean(Eval("colisDoneBit"))) %>'></asp:Label>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <EmptyDataTemplate>
                                    <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                                        <tr>
                                            <th>
                                                Events Name
                                            </th>
                                            <th>
                                                Hotel
                                            </th>
                                            <th>
                                                Date From
                                            </th>
                                            <th>
                                                Date To
                                            </th>
                                            
                                        </tr>
                                        <tr>
                                            <td colspan="4" class="LeftClass">No Events</td>
                                        </tr>
                                    </table>
                                </EmptyDataTemplate>
                            </asp:ListView>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <asp:DataPager ID="uoEventListPager" runat="server" PagedControlID="uoEventList"
                                PageSize="50">
                                <Fields>
                                    <asp:NumericPagerField ButtonType="Link" NumericButtonCssClass="PagerClass" />
                                </Fields>
                            </asp:DataPager>
                            <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" EnablePaging="True" OldValuesParameterFormatString="oldcount_{0}"
                                SelectCountMethod="EventsViewLoadEventsCount" SelectMethod="EventsViewLoadEvents" StartRowIndexParameterName="startRowIndex"
                                TypeName="TRAVELMART.BLL.MasterfileBLL" DeleteMethod="EventsViewMaintenanceDelete" MaximumRowsParameterName ="maximumRows"
                                UpdateMethod="EventsViewMaintenanceTag">
                                <DeleteParameters>
                                    <asp:ControlParameter ControlID="uoHiddenFieldEventId" Name="EventId" PropertyName="Value" />
                                    <asp:ControlParameter ControlID="uoHiddenFieldUser" Name="ModifiedBy" PropertyName="Value" />
                                </DeleteParameters>
                                <UpdateParameters>
                                    <asp:ControlParameter ControlID="uoHiddenFieldEventId" Name="EventId" PropertyName="Value" />
                                    <asp:ControlParameter ControlID="uoHiddenFieldUser" Name="ModifiedBy" PropertyName="Value" />
                                </UpdateParameters>
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="uoHiddenFieldUser" DbType="String" Name="UserId"
                                        PropertyName="Value" />
                                    <asp:ControlParameter ControlID="uoHiddenFieldDateFrom" DbType="Date" Name="DateFrom"
                                        PropertyName="Value" />
                                    <asp:ControlParameter ControlID="uoHiddenFieldDateTo" DbType="Date" Name="DateTo"
                                        PropertyName="Value" />                                    
                                    <asp:ControlParameter ControlID="uoHiddenFieldRegionId" Name="RegionId" DbType="Int32"
                                        PropertyName="Value" />
                                    <asp:ControlParameter ControlID="uoHIddenFieldCountryId" Name="CountryId" DbType="Int32"
                                        PropertyName="Value" />
                                    <asp:ControlParameter ControlID="uoHiddenFieldBranchId" Name="BranchId" DbType="Int32"
                                        PropertyName="Value" />                                                                    
                                     <asp:ControlParameter ControlID="uoHiddenFieldLoadType" DbType="String" Name="LoadType"
                                        PropertyName="Value" />
                                   <%-- <asp:Parameter DefaultValue="1" Name="LoadType" DbType="Int16" />--%>
                                </SelectParameters>
                            </asp:ObjectDataSource>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="uoButtonViewAll" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
        <asp:HiddenField ID="uoHiddenFieldViewAll" runat="server" Value="true" EnableViewState="true" />
        <asp:HiddenField ID="uoHiddenFieldRegionId" runat="server" Value="0" EnableViewState="true" />
        <asp:HiddenField ID="uoHIddenFieldCountryId" runat="server" Value="0" EnableViewState="true" />
        <asp:HiddenField ID="uoHiddenFieldBranchId" runat="server" Value="0" EnableViewState="true" />
        <asp:HiddenField ID="uoHiddenFieldDateFrom" runat="server" Value="0" EnableViewState="true" />
        <asp:HiddenField ID="uoHiddenFieldDateTo" runat="server" Value="0" EnableViewState="true" />
        <asp:HiddenField ID="uoHiddenFieldEventId" runat="server" Value="0" EnableViewState="true" />
        <asp:HiddenField ID="uoHiddenFieldUser" runat="server" Value="0" EnableViewState="true" />
        <asp:HiddenField ID="uoHiddenFieldPopupEvent" runat="server" Value="0" EnableViewState="true" />
        <asp:HiddenField ID="uoHiddenFieldLoadType" runat="server" Value="0"/>
    </div>
</asp:Content>

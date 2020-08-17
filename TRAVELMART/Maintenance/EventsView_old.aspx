<%@ Page Title="" Language="C#" MasterPageFile="~/TravelmartMaster.Master" AutoEventWireup="true" CodeBehind="EventsView_old.aspx.cs" Inherits="TRAVELMART.EventsView_old" %>
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

<table class="PageTitle" width="100%">
    <tr >
        <td class="LeftClass" style="width:25%;">
            Events Maintenance
        </td>
        <td class="RightClass">
            <a id="uoHyperLinkEventsAdd" runat="server">
                <asp:Button Font-Size="X-Small" runat="server"
                    id="uoButtonAdd" Text="Add"  />
            </a>
        </td>
    </tr>
</table>
    
<table width="100%">   
    <tr><td colspan="2"></td></tr>
    <tr>
        <td class="LeftClass">
            <asp:Button runat="server" ID="uoButtonViewAll" 
                Text="View All" Font-Size="X-Small" onclick="uoButtonViewAll_Click"/>
        </td>
    </tr>
    <tr>
        <td colspan="2"></td>
    </tr>
    <tr>
        <td class="Module" colspan="2">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:ListView runat="server" ID="uoEventList" 
                        onitemcommand="uoEventList_ItemCommand" onitemdeleting="uoEventList_ItemDeleting" 
                        >
                        <LayoutTemplate>
                            <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                                <tr>
                                    <th>Events Name</th>
                                    <th>Hotel</th>
                                    <th>Date From</th>
                                    <th>Date To</th>
                                    <th runat="server" style="width: 10%" id="DeleteTH"></th>
                                    <th>Tag as Done</th>
                                </tr>
                                <asp:PlaceHolder ID="itemPlaceholder" runat="server"></asp:PlaceHolder>
                            </table>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <tr>
                                <td class="leftAligned">
                                    <a runat="server" class="EventsLink" id="a1" href='<%# "EventsAdd.aspx?eId=" + Eval("colEventIdInt") + "&eName=" + Eval("colEventNameVarchar") %>' 
                                        visible ='<%# !(Convert.ToBoolean(Eval("colisDoneBit"))) %>'>
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
                                    <%# String.Format("{0:dd-MMM-yyyy}", Eval("colEventDateToDate"))%>  <%--<%# Eval("colEventDateToDate") %>--%>                                  
                                </td>
                                <td class="leftAligned">
                                    <asp:LinkButton runat="server" ID="uoHyperLinkDelete" CommandArgument='<%# Eval("colEventIdInt") %>'
                                        CommandName = "Delete" Text="Delete" Visible='<%# !(Convert.ToBoolean(Eval("colisDoneBit"))) %>'
                                        OnClientClick="return confirmDelete();"></asp:LinkButton>
                                </td>
                                <td>
                                    <asp:LinkButton runat="server" ID="LinkButton1" CommandArgument='<%# Eval("colEventIdInt") %>'
                                        CommandName = "Update" Text="Tag" Visible='<%# !(Convert.ToBoolean(Eval("colisDoneBit"))) %>'></asp:LinkButton>
                                    <asp:Label runat="server" ID="uoLabelDone" Text="Done" Visible='<%# (Convert.ToBoolean(Eval("colisDoneBit"))) %>'></asp:Label>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:ListView>
                    <asp:DataPager ID="uoEventListPager" runat="server"
                        PagedControlID="uoEventList" PageSize="1">
                        <Fields>
                            <asp:NumericPagerField ButtonType="Link"
                                NumericButtonCssClass="PagerClass" />
                        </Fields>    
                    </asp:DataPager>  
                    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" EnablePaging="True" 
                        OldValuesParameterFormatString="oldcount_{0}" 
                        SelectCountMethod="EventsViewLoadEventsPageCount" 
                        SelectMethod="EventsViewLoadEventsPage" 
                        TypeName="TRAVELMART.BLL.MasterfileBLL" 
                        DeleteMethod="EventsViewMaintenanceDelete" 
                        UpdateMethod="EventsViewMaintenanceTag">
                        <DeleteParameters>
                            <asp:ControlParameter ControlID="uoHiddenFieldEventId" Name="EventId" 
                                PropertyName="Value" />
                            <asp:ControlParameter ControlID="uoHiddenFieldUser" Name="ModifiedBy" 
                                PropertyName="Value" />
                        </DeleteParameters>
                        <UpdateParameters>
                            <asp:ControlParameter ControlID="uoHiddenFieldEventId" Name="EventId" 
                                PropertyName="Value" />
                            <asp:ControlParameter ControlID="uoHiddenFieldUser" Name="ModifiedBy" 
                                PropertyName="Value" />
                        </UpdateParameters>
                        <SelectParameters>
                            <asp:ControlParameter ControlID="uoHiddenFieldRegionId" Name="RegionId"  DbType="Int32"
                                PropertyName="Value" />
                            <asp:ControlParameter ControlID="uoHIddenFieldCountryId" Name="CountryId" DbType="Int32"
                                PropertyName="Value" />
                            <asp:ControlParameter ControlID="uoHiddenFieldBranchId" Name="BranchId" DbType="Int32"
                                PropertyName="Value" />
                            <asp:ControlParameter ControlID="uoHiddenFieldDateFrom" DbType="Date" 
                                Name="DateFrom" PropertyName="Value" />
                            <asp:ControlParameter ControlID="uoHiddenFieldDateTo" DbType="Date" 
                                Name="DateTo" PropertyName="Value" />
                            <asp:ControlParameter ControlID="uoHiddenFieldUser" DbType="String" 
                                Name="UserId" PropertyName="Value" />
                            <asp:Parameter DefaultValue="1" Name="LoadType" DbType="Int16" />
                        </SelectParameters>
                    </asp:ObjectDataSource> 
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="uoButtonViewAll" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
        </td>
    </tr>
   
</table>
<asp:HiddenField ID="uoHiddenFieldViewAll" runat="server" Value="true" EnableViewState="true" />
<asp:HiddenField ID="uoHiddenFieldRegionId" runat="server" Value="0" EnableViewState="true"/>
<asp:HiddenField ID="uoHIddenFieldCountryId" runat="server" Value="0" EnableViewState="true"/>
<asp:HiddenField ID="uoHiddenFieldBranchId" runat="server" Value="0" EnableViewState="true"/>
<asp:HiddenField ID="uoHiddenFieldDateFrom" runat="server" Value="0" EnableViewState="true"/>
<asp:HiddenField ID="uoHiddenFieldDateTo" runat="server" Value="0" EnableViewState="true"/>
<asp:HiddenField ID="uoHiddenFieldEventId" runat="server" Value="0" EnableViewState="true"/>
<asp:HiddenField ID="uoHiddenFieldUser" runat="server" Value="0" EnableViewState="true"/>
<asp:HiddenField ID="uoHiddenFieldPopupEvent" runat="server" Value="0" EnableViewState="true"/>
</asp:Content>

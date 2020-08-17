<%@ Page Title="" Language="C#" MasterPageFile="~/TravelmartMaster.Master" AutoEventWireup="true" CodeBehind="SailMasterView.aspx.cs" Inherits="TRAVELMART.Maintenance.SailMaterAdd" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 <script type="text/javascript">
     function confirmDelete() {
         if (confirm("Delete record?") == true)
             return true;
         else
             return false;
     }
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
        $("a#<%=uoHyperLinkSailMasterAdd.ClientID %>").fancybox(
        {
            'width': '50%',
            'height': '85%',
            'autoScale': false,
            'transitionIn': 'fadeIn',
            'transitionOut': 'fadeOut',
            'type': 'iframe',
            'onClosed': function() {
            var a = $("#<%=uoHiddenFieldSailMaster.ClientID %>").val();
                if (a == '1')
                    $("#aspnetForm").submit();
            }
        });

        $(".SailMasterLink").fancybox(
        {
            'width': '50%',
            'height': '85%',
            'autoScale': false,
            'transitionIn': 'fadeIn',
            'transitionOut': 'fadeOut',
            'type': 'iframe',
            'onClosed': function() {
            var a = $("#<%=uoHiddenFieldSailMaster.ClientID %>").val();
                if (a == '1')
                    $("#aspnetForm").submit();
            }
        });
        }                 
</script>

    <table width="100%">
        <tr>
            <td colspan="2">
                <table class="PageTitle" width="100%">
                    <tr>
                        <td class="LeftClass">
                            Sail Master
                        </td>
                        <td class="RightClass">
                            <a id="uoHyperLinkSailMasterAdd" runat="server">
                                <asp:Button ID="uoBtnSailMasterAdd" runat="server"
                                    Text="Add" Font-Size ="X-Small" />
                            </a>
                        </td>
                    </tr>
                    
                </table>
            </td>
        </tr>
        <tr>
            <td class="LeftClass">
                Ship:
            </td>
            <td class="LeftClass" style="font-weight:bold;">
                <%=Request.QueryString["vName"]%>
            </td>
        </tr>
        <tr><%--<td colspan="2"></td>--%>
            <td class="LeftClass">
                Itinerary Code:
            </td>
            <td class="LeftClass">
                <asp:TextBox ID="uoTextBoxSearch" runat="server" CssClass="TextBoxInput" Width="250px" />
                &nbsp
                <asp:Button ID="uoButtonView" runat="server" CssClass="SmallButton" 
                onclick="uoButtonView_Click" Text="View" />
            </td>       
        </tr>
        <tr>
            <td colspan="2" class="Module" align="right">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server"
                    UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:ListView runat="server" ID="uoSailMasterList" 
                            onitemcommand="uoSailMasterList_ItemCommand" 
                            onitemdeleting="uoSailMasterList_ItemDeleting" >
                            <LayoutTemplate>
                                <table cellpadding="0" border="0" cellspacing="0" class="listViewTable">
                                    <tr>
                                        <th>Seaport</th>
                                        <th>Itinerary Code</th>
                                        <th>Voyage No.</th>
                                        <th>Schedule Date</th>
                                        <th>Day Seq.</th>
                                        <th>Schedule Date Start</th>
                                        <th>Schedule Date End</th>
                                        <th runat="server" style="width: 10%" id="EditTH" />
                                        <th runat="server" style="width: 10%" id="DeleteTH"></th>
                                    </tr>
                                    <asp:PlaceHolder ID="itemPlaceholder" runat="server"></asp:PlaceHolder>
                                </table>                                
                            </LayoutTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td class="leftAligned">
                                        <%# Eval("colPortNameVarchar")%>
                                    </td>
                                    <td class="leftAligned">
                                        <%# Eval("colItineraryCodeVarchar") %>
                                    </td>
                                    <td class="leftAligned">
                                        <%# Eval("colVoyageNoVarchar") %>
                                    </td>
                                    <td class="leftAligned">                                        
                                        <%# String.Format("{0:dd-MMM-yyyy}", Eval("colScheduleDate"))%>
                                    </td>
                                    <td class="leftAligned">
                                        <%# Eval("colDayNoTinyint")%>
                                    </td>
                                    <td class="leftAligned">
                                        <%# String.Format("{0:dd-MMM-yyyy}", Eval("colScheduleDatetimeFrom"))%>
                                    </td>
                                    <td class="leftAligned">
                                        <%# String.Format("{0:dd-MMM-yyyy}", Eval("colScheduleDatetimeTo"))%>
                                    </td>
                                    <td class="leftAligned">
                                        <a runat="server" class="SailMasterLink" id="uoAEditSailMaster"
                                            href='<%# "SailMasterAdd.aspx?sId=" + Eval("colSailMasterIDBigint") + "&vId=" + Eval("colVesselIdInt") + "&vName=" + Request.QueryString["vName"] %>'
                                            visible='<%# (Convert.ToBoolean(uoHyperLinkSailMasterAdd.Visible)) %>'>
                                            Edit</a>
                                    </td>
                                    <td class="leftAligned">
                                        <asp:LinkButton runat="server" ID="uoLinkButtonDelete"
                                            CommandArgument='<%# Eval("colSailMasterIDBigint") %>'
                                            OnClientClick="return confirmDelete();" CommandName="Delete"
                                            Text="Delete" visible='<%# (Convert.ToBoolean(uoHyperLinkSailMasterAdd.Visible)) %>'>
                                        </asp:LinkButton>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <EmptyDataTemplate>
                                <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                                    <tr>
                                        <th>Seaport</th>
                                        <th>Itinerary Code</th>
                                        <th>Voyage No.</th>
                                        <th>Schedule Date</th>
                                        <th>Day Seq.</th>
                                        <th>Schedule Date Start</th>
                                        <th>Schedule Date End</th>
                                    </tr>
                                    <tr>
                                        <td class="LeftClass" colspan="7">No Record</td>
                                    </tr>
                                </table>
                            </EmptyDataTemplate>
                        </asp:ListView>
                        <asp:DataPager ID="uoSailMasterListPager" runat="server"
                            PagedControlID="uoSailMasterList" PageSize="20" 
                            onprerender="uoSailMasterListPager_PreRender">
                            <Fields>
                                <asp:NumericPagerField ButtonType="Link"
                                    NumericButtonCssClass="PagerClass" />
                            </Fields>    
                        </asp:DataPager>    
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td class="RightClass" colspan="2">
                                 
            </td>
        </tr>
        <tr>
            <td class="RightClass" colspan="2">
                <asp:ObjectDataSource ID="ObjectDataSource1" 
                    runat="server" EnablePaging="True" 
                    OldValuesParameterFormatString="oldcount_{0}" 
                    SelectCountMethod="SailMasterAddMaintenanceSearchCount" 
                    SelectMethod="SailMasterAddMaintenanceSearch" 
                    TypeName="TRAVELMART.BLL.MasterfileBLL" 
                    DeleteMethod="SailMasterViewDelete">
                    <DeleteParameters>
                        <asp:ControlParameter ControlID="uoHiddenFieldSailMasterId" DefaultValue="" 
                            Name="pSailMasterId" PropertyName="Value" />
                        <asp:ControlParameter ControlID="uoHiddenFieldUser" DefaultValue="" 
                            Name="pModifiedBy" PropertyName="Value" />
                    </DeleteParameters>
                    <SelectParameters>
                        <asp:QueryStringParameter DefaultValue="0" Name="pVesselId" 
                            QueryStringField="vId" />
                    </SelectParameters>
                </asp:ObjectDataSource>
                <asp:HiddenField ID="uoHiddenFieldSailMaster" runat="server" Value="0" />
                <asp:HiddenField ID="uoHiddenFieldSailMasterId" runat="server" Value="0" />
                <asp:HiddenField ID="uoHiddenFieldUser" runat="server" Value="0" />                
                <asp:HiddenField ID="uoHiddenFieldVesselID" runat="server" Value="0" />
                <asp:HiddenField ID="uoHiddenFieldVesselName" runat="server" Value="0" />
            </td>
        </tr>
    </table>
</asp:Content>

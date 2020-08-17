<%@ Page Title="" Language="C#" MasterPageFile="~/TravelmartMaster.Master" AutoEventWireup="true" CodeBehind="PortAgentVehicleDashboard.aspx.cs" Inherits="TRAVELMART.PortAgentVehicleDashboard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<script type="text/javascript">
    $(document).ready(function() {
        HideSearch();
    });
    function HideSearch() {
        if ($("#<%=uoChkSearch.ClientID %>").attr('checked')) {
            $("#<%=uoPanelSearchDetails.ClientID %>").show();
        }
        else {
            $("#<%=uoPanelSearchDetails.ClientID %>").hide();
        }

        $("#<%=uoChkSearch.ClientID %>").click(function() {
            if ($(this).attr('checked')) {
                $("#<%=uoPanelSearchDetails.ClientID %>").fadeIn();
            }
            else {
                $("#<%=uoPanelSearchDetails.ClientID %>").fadeOut();
            }
        });
    }

    function pageLoad(sender, args) {
        var isAsyncPostback = Sys.WebForms.PageRequestManager.getInstance().get_isInAsyncPostBack();
        if (isAsyncPostback) {
            HideSearch();
        }
    }
</script>
<div class="PageTitle">Service Provider Vehicle Dashboard</div>
<br />
<asp:Panel runat="server" ID="uoPanelSearch">
    <div align="left">
        <asp:CheckBox runat="server" ID="uoChkSearch"
            Text="Advanced Search" />
    </div>
</asp:Panel>
<asp:Panel runat="server" ID="uoPanelSearchDetails">
    <table width="100%">
        <tr>
            <td class="LeftClass" style="width:150px;">&nbsp;Vehicle Brand :</td>
            <td class="LeftClass">
                <asp:UpdatePanel runat="server" ID="uoUpdatePanelBrand" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:DropDownList runat="server" ID="uoDropDownListBrand"
                            AppendDataBoundItems="true"
                            AutoPostBack="true"
                            Width="250px" 
                            onselectedindexchanged="uoDropDownListBrand_SelectedIndexChanged"></asp:DropDownList>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="uoButtonViewAll" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td class="LeftClass" style="width:150px">&nbsp;Vehicle Branch :</td>
            <td class="LeftClass">
                <asp:UpdatePanel runat="server" ID="uoUpdateBranch" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:DropDownList runat="server" ID="uoDropDownListBranch"
                            AppendDataBoundItems="true"
                            Width="250px">
                            <asp:ListItem Text="--Select Vehicle Branch--" Value="0"></asp:ListItem>
                        </asp:DropDownList>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="uoButtonViewAll" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="uoDropDownListBrand" 
                            EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
</asp:Panel>
<br />
<div align="left">
    &nbsp;
    <asp:Button runat="server" CssClass="SmallButton" Text="View"
        ID="uoButtonView" onclick="uoButtonView_Click" />
        &nbsp;
        <asp:Button runat="server" ID="uoButtonViewAll"
            CssClass="SmallButton" Text="View All" 
        onclick="uoButtonViewAll_Click" />
</div>
<br />
<div class="Module">
    <asp:UpdatePanel runat="server" ID="uoUpdatePanelDetails">
        <ContentTemplate>
            <asp:ListView runat="server" ID="uoVehicleDashboardList">
                <LayoutTemplate>
                    <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                        <tr>
                            <th>Date</th>
                            <th>Day</th>
                            <th>Arrived</th>
                            <th>In Transit</th>
                            <th>Cancelled</th>
                            <th>Unused</th>
                        </tr>
                        <asp:PlaceHolder runat="server" ID="itemPlaceHolder"></asp:PlaceHolder>
                    </table>
                </LayoutTemplate>
                <ItemTemplate>
                    <%# addDashBoardGroup()%>
                    <tr>
                        <td class="leftAligned">
                            <%# String.Format("{0:dd-MMM-yyyy}", Eval("CDate")) %>
                        </td>
                        <td class="leftAligned">
                            <%# Eval("CDay") %>
                        </td>
                        <td class="leftAligned">
                            <%# Eval("Arrived") %>
                        </td>
                        <td class="leftAligned">
                            <%# Eval("InTransit") %>
                        </td>
                        <td class="leftAligned">
                            <%# Eval("Cancelled") %>
                        </td>
                        <td class="leftAligned">
                            <%# Eval("Unused") %>
                        </td>
                    </tr>                   
                </ItemTemplate>
                <EmptyDataTemplate>
                    <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                        <tr>
                            <th>Date</th>
                            <th>Day</th>
                            <th>Arrived</th>
                            <th>In Transit</th>
                            <th>Cancelled</th>
                            <th>Unused</th>
                        </tr>
                        <tr>
                            <td class="leftAligned" colspan="8">No Records</td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
            </asp:ListView>
            <asp:DataPager runat="server" ID="uoVehicleDashboardListPager"
                PagedControlID="uoVehicleDashboardList" PageSize="20" 
                onprerender="uoVehicleDashboardListPager_PreRender">
                <Fields>
                    <asp:NumericPagerField ButtonType="Link" NumericButtonCssClass="PagerClass" />
                </Fields>    
            </asp:DataPager>
            <asp:HiddenField runat="server" ID="uoHiddenFieldPortAgent" Value="0" />
            <asp:HiddenField runat="server" ID="uoHiddenFieldStartDate" Value="0" />
            <asp:HiddenField runat="server" ID="uoHiddenFieldEndDate" Value="0" />
            <asp:HiddenField runat="server" ID="uoHiddenFieldBrand" Value="0" />
            <asp:HiddenField runat="server" ID="uoHiddenFieldbranch" Value="0" />
            <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" EnablePaging="True" 
                OldValuesParameterFormatString="oldCount_{0}" 
                SelectCountMethod="PortAgentVehicleDashboardDetailsCount" 
                SelectMethod="PortAgentVehicleDashboardDetails" 
                TypeName="TRAVELMART.BLL.DashboardBLL">
                <SelectParameters>
                    <asp:ControlParameter ControlID="uoHiddenFieldPortAgent" DbType="Int32" 
                        Name="portAgentId" PropertyName="Value" />
                    <asp:ControlParameter ControlID="uoHiddenFieldEndDate" DbType="Date" 
                        Name="endDate" PropertyName="Value" />
                    <asp:ControlParameter ControlID="uoHiddenFieldStartDate" DbType="Date" 
                        Name="startDate" PropertyName="Value" />
                    <asp:ControlParameter ControlID="uoHiddenFieldBrand" DbType="Int32" 
                        Name="BrandId" PropertyName="Value" />
                    <asp:ControlParameter ControlID="uoHiddenFieldbranch" DbType="Int32" 
                        Name="BranchId" PropertyName="Value" />
                </SelectParameters>
            </asp:ObjectDataSource>
        </ContentTemplate>
</asp:UpdatePanel>
</div>

</asp:Content>

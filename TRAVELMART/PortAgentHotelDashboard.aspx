<%@ Page Title="" Language="C#" MasterPageFile="~/TravelmartMaster.Master" AutoEventWireup="true" CodeBehind="PortAgentHotelDashboard.aspx.cs" Inherits="TRAVELMART.PortAgentHotelDashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

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
    function OpenDetails(Status, VendorId, BranchId, date) {
        var URLString = "HotelDashboardDetails.aspx?";
        URLString += "Status=" + Status + "&vId=" + VendorId + "&bId=" + BranchId + "&date=" + date;

        var screenWidth = screen.availwidth;
        var screenHeight = screen.availheight;

        screenWidth = 800;
        screenHeight = 500;

        window.open(URLString, 'Contract_Management', 'top=10, left=200, width=' + screenWidth + ', height=' + screenHeight + ', scrollbars=yes, menubar=no,toolbar=no,status=no,resizable=yes,addressbar=no');
        return false;

    }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<div class="PageTitle">Service Provider Hotel Dashboard</div>
<br />
<asp:Panel runat="server" ID="uoPanelSearch">
    <asp:Panel runat="server" ID="uoPanelSearchHeader">
    &nbsp;
        <div align="left">
            <asp:CheckBox runat="server" ID="uoChkSearch" Text="Advanced Search"/>
        </div>        
    </asp:Panel>
    <asp:Panel runat="server" ID="uoPanelSearchDetails">
        <table width="100%">
            <tr>
                <td class="LeftClass" style="width:150px;">&nbsp;Hotel Brand :</td>
                <td class="LeftClass">
                    <asp:UpdatePanel runat="server" ID="uoUpdatePanelBrand" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:DropDownList runat="server" ID="uoDropDownListBrand"
                                AppendDataBoundItems="true" AutoPostBack="true"
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
                <td class="LeftClass">&nbsp;Hotel Branch :</td>
                <td class="LeftClass">
                    <asp:UpdatePanel runat="server" ID="uoUpdatePanelBranch" 
                        UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:DropDownList runat="server" ID="uoDropDownListBranch"
                                AppendDataBoundItems="true" Width="250px">
                                <asp:ListItem Text="--Select Hotel Branch--" Value="0"></asp:ListItem>
                            </asp:DropDownList>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="uoDropDownListBrand" 
                                EventName="SelectedIndexChanged" />
                            <asp:AsyncPostBackTrigger ControlID="uoButtonViewAll" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Panel>
<br />
<div align="left">
    &nbsp;
    <asp:Button runat="server" ID="uoButtonView" CssClass="SmallButton" Text="View" 
        onclick="uoButtonView_Click" />
    &nbsp;
    <asp:Button runat="server" ID="uoButtonViewAll" CssClass="SmallButton" 
        Text="View All" onclick="uoButtonViewAll_Click" />
</div>
<br />

<asp:UpdatePanel runat="server" ID="uoUpdatePanelDetails" UpdateMode="Conditional">
    <ContentTemplate>
        <asp:ListView runat="server" ID="uoHotelDashboardDetails">
            <LayoutTemplate>
                <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                    <tr>
                        <th>Date</th>
                        <th>Day</th>
                        <th>Room Type</th>
                        <th>Checked In</th>
                        <th>Checked Out</th>
                        <th>Cancelled</th>
                        <th>No Show</th>
                        <th>Reserved</th>
                        <th>Used Room Blocks</th>
                        <th>Total Room Blocks</th>
                    </tr>
                    <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                </table>
            </LayoutTemplate>
            <ItemTemplate>
                <%# addDashBoardGroup()%>
                <tr>
                    <td>
                        <%# String.Format("{0:dd-MMM-yyyy}", Eval("CDate")) %>
                    </td>
                    <td >
                        <%# Eval("CDay") %>
                    </td>
                    <td >
                        <%# Eval("colRoomType") %>
                    </td>
                    <td >
                        
                        <a visible='<%# (Convert.ToBoolean(Eval("CheckedIn").Equals(0))) %>' runat="server"><%# Eval("CheckedIn")%></a>
                        <asp:LinkButton runat="server" ID="uoLinkCheckInDetails" Text='<%# Eval("CheckedIn")%>'
                                OnClientClick = '<%# "return OpenDetails(\"" + Eval("colStatus")  + "\", \"" + Eval("colVendorId") + "\", \"" + Eval("colBranchId") + "\", \""+ String.Format("{0:dd-MMM-yyyy}", Eval("CDate")) + "\")" %>'
                                Visible='<%# !(Convert.ToBoolean(Eval("CheckedIn").Equals(0))) %>'>       
                        </asp:LinkButton>                       
                    </td>
                    <td >
                        <a visible='<%# (Convert.ToBoolean(Eval("CheckedOut").Equals(0))) %>' runat="server"> <%# Eval("CheckedOut")%></a>
                        <asp:LinkButton runat="server" ID="uoLinkCheckOutDetails" Text='<%# Eval("CheckedOut")%>'
                                OnClientClick = '<%# "return OpenDetails(\"" + Eval("colStatus")  + "\", \"" + Eval("colVendorId") + "\", \"" + Eval("colBranchId") + "\", \""+ String.Format("{0:dd-MMM-yyyy}", Eval("CDate")) + "\")" %>'
                                Visible='<%# !(Convert.ToBoolean(Eval("CheckedOut").Equals(0))) %>' >       
                        </asp:LinkButton>
                    </td>
                    <td >
                        <a visible='<%# (Convert.ToBoolean(Eval("Cancelled").Equals(0))) %>' runat="server"> <%# Eval("Cancelled")%></a>
                        <asp:LinkButton runat="server" ID="uoLinkCancelledDetails" Text='<%# Eval("Cancelled")%>'
                                OnClientClick = '<%# "return OpenDetails(\"" + Eval("colStatus")  + "\", \"" + Eval("colVendorId") + "\", \"" + Eval("colBranchId") + "\", \""+ String.Format("{0:dd-MMM-yyyy}", Eval("CDate")) + "\")" %>'
                                Visible='<%# !(Convert.ToBoolean(Eval("Cancelled").Equals(0))) %>' >       
                        </asp:LinkButton>
                    </td>
                    <td>
                        <a visible='<%# (Convert.ToBoolean(Eval("NoShow").Equals(0))) %>' runat="server"><%# Eval("NoShow")%></a>
                        <asp:LinkButton runat="server" ID="LinkButton1" Text='<%# Eval("NoShow")%>'
                                OnClientClick = '<%# "return OpenDetails(\"" + Eval("colStatus")  + "\", \"" + Eval("colVendorId") + "\", \"" + Eval("colBranchId") + "\", \""+ String.Format("{0:dd-MMM-yyyy}", Eval("CDate")) + "\")" %>'
                                Visible='<%# !(Convert.ToBoolean(Eval("NoShow").Equals(0))) %>' >       
                        </asp:LinkButton>
                    </td>
                    <td >
                        <a visible='<%# (Convert.ToBoolean(Eval("Unused").Equals(0))) %>' runat="server"> <%# Eval("Unused")%></a>
                        <asp:LinkButton runat="server" ID="LinkButton2" Text='<%# Eval("Unused")%>'
                                OnClientClick = '<%# "return OpenDetails(\"" + Eval("colStatus")  + "\", \"" +  Eval("colVendorId") + "\", \"" + Eval("colBranchId") + "\", \""+ String.Format("{0:dd-MMM-yyyy}", Eval("CDate")) + "\")" %>'
                                Visible='<%# !(Convert.ToBoolean(Eval("Unused").Equals(0))) %>' >       
                        </asp:LinkButton>
                    </td>
                    <td>
                        <%# Eval("UsedRooms")%> 
                    </td>                    
                    <td>
                        <%# Eval("TotalDayCount")%>
                    </td>
                </tr>
            </ItemTemplate>
            <EmptyDataTemplate>
                <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                    <tr>
                        <th>Date</th>
                        <th>Day</th>
                        <th>Room Type</th>
                        <th>Checked In</th>
                        <th>Checked Out</th>
                        <th>Cancelled</th>
                        <th>No Show</th>
                        <th>Reserved</th>
                        <th>Used Room Blocks</th>
                        <th>Total Room Blocks</th>
                    </tr>
                    <tr>
                        <td colspan="10">No records</td>
                    </tr>
                </table>
            </EmptyDataTemplate>
        </asp:ListView>
        <asp:DataPager runat="server" ID="uoHotelDashboardDetailsPager"
            PagedControlID="uoHotelDashboardDetails"
            PageSize="20" onprerender="uoHotelDashboardDetailsPager_PreRender">
            <Fields>
                <asp:NumericPagerField ButtonType="Link" NumericButtonCssClass="PagerClass" />
            </Fields>        
        </asp:DataPager>
        <asp:HiddenField runat="server" ID="uoHiddenFieldPortAgent" Value="0" 
            />
        <asp:HiddenField runat="server" ID="uoHiddenFieldStartDate" Value="0" />
        <asp:HiddenField runat="server" ID="uoHiddenFieldEndDate" Value="0" />
        <asp:HiddenField runat="server" ID="uoHiddenFieldBrandId" Value="0" />
        <asp:HiddenField runat="server" ID="uoHiddenFieldBranchId" Value="0" />
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" EnablePaging="True" 
            OldValuesParameterFormatString="oldCount_{0}" 
            SelectCountMethod="PortAgentHotelDashboardDetailsCount" 
            SelectMethod="PortAgentHotelDashboardDetails" 
            TypeName="TRAVELMART.BLL.DashboardBLL">
            <SelectParameters>
                <asp:ControlParameter ControlID="uoHiddenFieldPortAgent" DbType="Int32" 
                    Name="portAgentId" PropertyName="Value" />
                <asp:ControlParameter ControlID="uoHiddenFieldStartDate" DbType="Date" 
                    Name="startDate" PropertyName="Value" />
                <asp:ControlParameter ControlID="uoHiddenFieldEndDate" DbType="Date" 
                    Name="EndDate" PropertyName="Value" />
                <asp:ControlParameter ControlID="uoHiddenFieldBranchId" DbType="Int32" 
                    Name="BranchId" PropertyName="Value" />
                <asp:ControlParameter ControlID="uoHiddenFieldBrandId" DbType="Int32" 
                    Name="BrandId" PropertyName="Value" />
            </SelectParameters>
        </asp:ObjectDataSource>
    </ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="uoButtonView" EventName="Click" />
        <asp:AsyncPostBackTrigger ControlID="uoButtonViewAll" EventName="Click" />
    </Triggers>
</asp:UpdatePanel>
</asp:Content>

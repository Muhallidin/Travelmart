<%@ Page Title="" Language="C#" MasterPageFile="~/TravelmartMaster.Master" AutoEventWireup="true" CodeBehind="HotelViewNoTransaction.aspx.cs" Inherits="TRAVELMART.Hotel.HotelViewNoTransaction" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
    function filterSettings() {
        var role = $("#<%=uoHiddenFieldRole.ClientID %>").val();
        $("#ctl00_ContentPlaceHolder1_uoHotelList_uoCheckBoxCheckAll").click(function(ev) {
            var status = $(this).attr('checked');
            $('input:checkbox[name *= uoSelectCheckBox]').attr('checked', status);
        });

        if (role != 'Hotel Vendor') {
            $("#<%=uoButtonApproved.ClientID %>").click(function(ev) {
                var listOfCheckbox = $('input:checkbox[name*=uoSelectCheckBox]:checked');

                var i = 0;
                $(listOfCheckbox).each(function() {
                    i++;
                });

                if (i == 0) {
                    alert("No selected request!");
                    return false;
                }
                else {
                    return true;
                }
            });
        }
    }

    $(document).ready(function() {
        ShowPopup();
        HideSearch();
        filterSettings();
    });

    function pageLoad(sender, args) {
        var isAsyncPostback = Sys.WebForms.PageRequestManager.getInstance().get_isInAsyncPostBack();
        if (isAsyncPostback) {
            ShowPopup();
            HideSearch();
            filterSettings();
        }
    }

    function HideSearch() {

    }

    function ShowPopup() {       
        $(".HotelLink").fancybox(
        {
            'width': '50%',
            'height': '90%',
            'autoScale': false,
            'transitionIn': 'fadeIn',
            'transitionOut': 'fadeOut',
            'type': 'iframe',
            'onClosed': function() {
                var a = $("#<%=uoHiddenFieldPopupHotel.ClientID %>").val();
                if (a == '1')
                    $("#aspnetForm").submit();
            }
        });
    }
    function OpenEventsList(branchID, cityID, OnOffDate) {
        var URLString = "../Maintenance/EventsList.aspx?bId=";
        URLString += branchID;
        URLString += "&cityId=" + cityID;
        URLString += "&Date=" + OnOffDate;

        var screenWidth = screen.availwidth;
        var screenHeight = screen.availheight;

        screenWidth = 600;
        screenHeight = 400;

        window.open(URLString, 'Events_List', 'top=10, left=200, width=' + screenWidth + ', height=' + screenHeight + ', scrollbars=yes, menubar=no,toolbar=no,status=no,resizable=yes,addressbar=no');
        return false;
    }             
</script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%" class ="PageTitle">
    <tr>
        <td align="left" >
            <asp:Label Font-Bold="true" Font-Size="11" ID="Label1" runat="server" Text="List of No Hotel Booking"></asp:Label>
            <asp:Label Font-Bold="true" Font-Size="11" ID="uoLabelBranchName" runat="server" Text=""></asp:Label>
             <asp:LinkButton ID="uoLinkButtonEvents" runat="server" Text="*" CssClass="EventNotification" ToolTip="With Event(s)" ></asp:LinkButton>
            </td>
        <td align="right">
            <asp:Button runat="server" ID="uoButtonApproved" Text="Approve" 
            CssClass="SmallButton" onclick="uoButtonApproved_Click"  />                            
        </td>
    </tr>
</table>


<div>
    <asp:UpdatePanel runat="server" ID="uoPanelPending" >
            <ContentTemplate>
                <asp:ListView runat="server" ID="uoHotelList" DataSourceID="ObjectDataSource1">
                    <LayoutTemplate>
                         <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                            <tr>  
                            <th class="hideElement">Req ID</th>        
                            <th>Is Manual</th>                 
                            <th>E1 ID</th>
                            <th>Name</th>                                                        
                            <th>On/Off Date</th>
                            <th>Checkin Date</th>
                            <%--<th>Hotel Name</th>--%>
                            <th>Port</th>
                            <th>Rank</th>
                            <th>Status</th>                            
                            <th>
                                <asp:CheckBox runat="server" ID="uoCheckBoxCheckAll" />
                            </th>
                            </tr>
                            <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                        </table>
                    </LayoutTemplate>
                    <ItemTemplate>
                     <%--   <%# HotelAddGroup()%>--%>
                        <tr>
                            <td class="hideElement">
                                <asp:HiddenField runat="server" ID="uoHiddenFieldColIdBigInt"
                                    Value='<%# Eval("HotelPendingID") %>' />
                                <asp:HiddenField runat="server" ID="uoHiddenFieldBranchId" Value='<%# Eval("colBranchIDInt") %>' />
                            </td>
                            <td class="leftAligned">
                                <%# Eval("IsManual") %>
                            </td>
                            <td class="leftAligned">
                                <asp:Label ID="uoLblE1ID" runat="server" Text='<%# Eval("SfID") %>'></asp:Label>
                            </td>
                            <td class="leftAligned">
                                <asp:LinkButton runat="server" ID="uoLinkSeafarer" PostBackUrl='<%# "~/SuperUserView.aspx?sfId=" + Eval("sfID") +  "&trID=" + Eval("TravelRequestID") + "&st=" + Eval("sfStatus") + "&ufn=" + Request.QueryString["ufn"] + "&manualReqID=" + Eval("RequestID")%>' Text='<%# Eval("Name")  + "&dt=" + Request.QueryString["dt"]%>'></asp:LinkButton>
                            </td>
                            <td class="leftAligned">
                                <%# String.Format("{0:dd-MMM-yyyy}", Eval("OnOffDate"))%>
                            </td>
                            <td class="leftAligned">
                                <asp:Label ID="uoLblCheckIn" runat="server"
                                    Text ='<%# String.Format("{0:dd-MMM-yyyy}", Eval("CheckInDate")) %>'></asp:Label>
                            </td>
                           <%-- <td class="leftAligned">
                                <asp:Label runat="server" ID="uoLblHotelName" Text='<%# Eval("HotelName") %>'></asp:Label>
                                <asp:Label ID="uoLblEvents" runat="server" Text="*" Font-Bold="true" ForeColor="Red"
                                    Visible='<%# IsEventExists(Eval("colBranchIDInt"), Eval("OnOffDate")) %>'></asp:Label>
                            </td>--%>
                            <td class="leftAligned">
                                <%# Eval("Port")%>
                            </td>
                            <td class="leftAligned">
                                <%# Eval("Rank")%>
                            </td>
                            <td class="leftAligned">
                                <%# Eval("sfStatus")%>
                            </td>
                            <td>
                                <asp:CheckBox CssClass="Checkbox" ID="uoSelectCheckBox" runat="server" />
                            </td>
                        </tr>
                    </ItemTemplate>
                    <EmptyDataTemplate>
                        <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                            <tr>  
                                <th class="hideElement">Req ID</th>        
                                <th>Is Manual</th>                 
                                <th>E1 ID</th>
                                <th>Name</th>                                                        
                                <th>Date</th>
                                <th>Checkin Date</th>
                               <%-- <th>Hotel Name</th>--%>
                                <th>Port</th>
                                <th>Rank</th>
                                <th>Status</th>     
                            </tr>
                            <tr>
                                <td colspan="9" class="leftAligned">No Hotel Bookings.</td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                </asp:ListView>           
                <asp:DataPager runat="server" ID="uoHotelListPager" PagedControlID="uoHotelList" 
                    PageSize="20" onprerender="uoHotelListPager_PreRender">
                    <Fields>
                        <asp:NumericPagerField ButtonType="Link" NumericButtonCssClass="PagerClass" />
                    </Fields>
                </asp:DataPager>     
                 <%-- 
                  SelectCountMethod="GetSFHotelTravelPendingCount" 
--%>                  
                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" EnablePaging="True" 
                    MaximumRowsParameterName="MaxRow" OldValuesParameterFormatString="oldCount_{0}" 
                    onselecting="ObjectDataSource2_Selecting" 
                    SelectCountMethod="GetSFHotelTravelNoTransactionCount"  SelectMethod="GetSFHotelTravelNoTransaction" 
                    StartRowIndexParameterName="StartRow" 
                    TypeName="TRAVELMART.BLL.SeafarerTravelBLL">
                    <SelectParameters>
                    
                        <asp:Parameter Name="DateFrom" Type="String"  />
                        <asp:Parameter Name="DateTo" Type="String" />
                        <asp:Parameter Name="UserID" Type="String" />
                        <asp:Parameter Name="FilterByDate" Type="String" />
                        <asp:Parameter Name="RegionID" Type="String" />
                        <asp:Parameter Name="CountryID" Type="String" />
                        <asp:Parameter Name="CityID" Type="String" />
                        <asp:Parameter Name="Status" Type="String" />
                        <asp:Parameter Name="FilterByNameID" Type="String" />
                        <asp:Parameter Name="FilterNameID" Type="String" />
                        <asp:Parameter Name="PortID" Type="String" />
                        <asp:Parameter Name="VesselID" Type="String" />
                        <asp:Parameter Name="Nationality" Type="String" />
                        <asp:Parameter Name="Gender" Type="String" />
                        <asp:Parameter Name="Rank" Type="String" />
                        <asp:Parameter Name="Role" Type="String" />
                        
                        <asp:Parameter Name="ByVessel" Type="Int16" />
                        <asp:Parameter Name="ByName" Type="Int16" />
                        <asp:Parameter Name="ByE1ID" Type="Int16" />
                        <asp:Parameter Name="ByDateOnOff" Type="Int16" />
                        <asp:Parameter Name="ByStatus" Type="Int16" />
                        <asp:Parameter Name="ByPort" Type="Int16" />
                        <asp:Parameter Name="ByRank" Type="Int16" />
                        <asp:Parameter Name="BranchId" Type="Int32" />
                        
                    </SelectParameters>
                </asp:ObjectDataSource>
            </ContentTemplate>
        </asp:UpdatePanel> 
   
</div>
<asp:HiddenField ID="uoHiddenFieldRole" Value="0" runat="server" />
<asp:HiddenField runat="server" ID="uoHiddenFieldPopupHotel" Value="0" />
<asp:HiddenField ID="uoHiddenFieldStartDate" Value="0" runat="server" />
<asp:HiddenField ID="uoHiddenFieldEndDate" Value="0" runat="server" />
<asp:HiddenField ID="uoHiddenFieldUserBranch" Value="0" runat="server" />

</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/TravelmartMaster.Master" AutoEventWireup="true" CodeBehind="HotelDashboardRoomType2.aspx.cs" Inherits="TRAVELMART.HotelDashboardRoomType2" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<%--<%@ OutputCache Duration="60" VaryByParam="none" VaryByControl="uoCalendarDashboard"%>--%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
   
    <style type="text/css">
        .style1
        {
            text-align: left;
            vertical-align: middle;
            width: 43px;
        }
        .style2
        {
            text-align: left;
            vertical-align: middle;
            white-space: nowrap;
            width: 272px;
        }
    </style>
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

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
        $(".HotelLink").fancybox(
        {
            'centerOnScroll': false,
            'width': '40%',
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
        var URLString = "Maintenance/EventsList.aspx?bId=";
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

    function OpenContract(vendorID, ContractID) {
        var URLString = "../ContractManagement/HotelContractView.aspx?vId=" + vendorID + "&cId=" + ContractID;

        var screenWidth = screen.availwidth;
        var screenHeight = screen.availheight;

        screenWidth = 800;
        screenHeight = 650;

        window.open(URLString, 'Contract_Management', 'top=10, left=200, width=' + screenWidth + ', height=' + screenHeight + ', scrollbars=yes, menubar=no,toolbar=no,status=no,resizable=yes,addressbar=no');
        return false;
    }    
</script>
           
   
<div class="PageTitle">
    <asp:Panel ID="uopanelroomhead" runat="server">
        Hotel Dashboard - Room Types
    </asp:Panel>
</div>

<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>  
<table width="100%" class="LeftClass" >   
   <tr  runat="server" id="TRSearch" >        
        <td class="style1">
            Hotel:
        </td>
        <td class="style2">
            <asp:TextBox ID="uoTextBoxSearch" runat="server" CssClass="TextBoxInput" Width="250px" />
            
        </td>
        <td class="LeftClass">            
            <asp:Button ID="uoButtonView" runat="server" CssClass="SmallButton" 
              onclick="uoButtonView_Click" Text="View" />    
        </td>
        <td class="RightClass" style="width:150px" >
            <asp:HyperLink ID="uoHyperLinkException" class="RedBgNotification" runat="server" NavigateUrl="#">Exception Bookings</asp:HyperLink>
        </td>
    </tr>    
   </table>

 <table width="100%" cellspacing="0" border="0">
 <tr>
    <td></td>
 </tr>
 <tr >
     <td >
                
        <asp:ListView runat="server" ID="uoListViewDashboard"                          
             onitemcommand="uoListViewDashboard_ItemCommand" 
             onitemdatabound="uoListViewDashboard_ItemDataBound">
            <LayoutTemplate>
                <table border="0" cellpadding="0" cellspacing="0" class="listViewTable" width="100%">
                    <tr>
                        <th width="90px">Date</th><th width="90px">Day of Week</th><th width="90px">Room Type</th><th width="90px">Reserved Crew</th><th width="90px">Overflow Crew</th><th width="90px">Total Crew</th><th width="90px">Reserved Room</th><th width="90px">Total Room Blocks</th><th width="90px">Available Room Blocks</th><th width="90px">Emergency Room Blocks</th><th width="90px">Available Emergency Room Blocks</th><th width="90px">Override</th><th width="90px">Emergency</th></tr><asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                </table>
            </LayoutTemplate>
            <ItemTemplate>
           <%# DashboardAddGroup()%>
                <%--<tr>--%>
                <%# DashboardChangeRowColor()%>
                    <%# DashboardAddDateRow()%>
                    <%--<td class="leftAligned" >                       
                        <asp:HiddenField ID="uoHiddenFieldBranchID" runat="server" Value ='<%# Eval("BranchID") + "_0_" + Eval("colDate") %>' /> 
                        <asp:HyperLink ID="uoHyperLinkDate" runat="server"  NavigateUrl='<%# "HotelDashboard2.aspx?ufn="+ Request.QueryString["ufn"].ToString() +"&dt=" + String.Format("{0:MM-dd-yyyy}", Eval("colDate")) + "&branchId=" + Eval("BranchID") + "&brandId=" + Eval("BrandId") + "&branchName=" + Eval("HotelBranchName") %>'><%# String.Format("{0:dd-MMM-yyyy}", Eval("colDate"))%></asp:HyperLink>
                        <asp:LinkButton ID="uoLinkButtonEvents" runat="server" Text="*" CssClass="EventNotification" Visible='<%# Eval("IsWithEvent") %>' ToolTip="With Event(s)"></asp:LinkButton>
                    </td>--%>
                    <%--<td class="leftAligned" >
                        <%# Eval("colDateName")%>
                    </td>--%>
                    <td class="leftAligned" >
                        <%# Eval("RoomType")%>
                    </td>
                    <td class="rightAligned" >
                        <%# Eval("ReservedCrew")%>
                    </td>
                    <td class="rightAligned" >
                        <%# Eval("OverflowCrew")%>
                    </td>
                    <td class="rightAligned" >
                        <%# Eval("TotalCrew")%>
                    </td>
                    <td class="rightAligned" >                       
                        <%# String.Format("{0:0.#}", Eval("ReservedRoom"))%>
                    </td>
                    <td class="rightAligned">
                        <%# Eval("TotalRoomBlocks")%>
                    </td>
                    <td class="rightAligned">                        
                        <span style="font-weight:bold"><%# String.Format("{0:0.#}", Eval("AvailableRoomBlocks"))%></span>
                    </td>
                    <td class="rightAligned">
                        <%# Eval("EmergencyRoomBlocks")%>
                    </td>
                    <td class="rightAligned">                        
                        <span style="font-weight:bold"><%# String.Format("{0:0.#}", Eval("AvailableEmergencyRoomBlocks"))%></span>
                    </td>
                    <td>
                        <asp:HyperLink CssClass="HotelLink" ID="uoHyperLinkEditOverride" runat="server" NavigateUrl='<%# "HotelRoomOverrideEdit.aspx?bID=" + Eval("BranchID") + "&rID=" + Eval("RoomTypeID") + "&dt=" + String.Format("{0:MM-dd-yyyy}", Eval("colDate")) + "&rc=" + String.Format("{0:#####}", Eval("ReservedRoom")) %>' >Edit</asp:HyperLink></td><td>
                        <asp:HyperLink CssClass="HotelLink" ID="uoHyperLinkEditEmergency" runat="server" NavigateUrl='<%# "HotelRoomEmergencyEdit.aspx?bID=" + Eval("BranchID") + "&rID=" + Eval("RoomTypeID") + "&dt=" + String.Format("{0:MM-dd-yyyy}", Eval("colDate")) %>' >Edit</asp:HyperLink></td>
                        <%--<td class="leftAligned" > 
                        <asp:HiddenField ID="uoHiddenFieldBranchID" runat="server" Value ='<%# Eval("BranchID") + "_" + Eval("colCityIDInt") + "_" + Eval("colDate") %>' />                                                
                        <asp:HyperLink ID="uoHyperLinkDate" runat="server"  NavigateUrl='<%# "HotelDashboard2.aspx?ufn="+ Request.QueryString["ufn"].ToString() +"&dt=" + String.Format("{0:MM-dd-yyyy}", Eval("colDate")) + "&branchId=" + Eval("BranchID") + "&brandId=" + Eval("BrandId") + "&branchName=" + Eval("BranchName") %>'><%# String.Format("{0:dd-MMM-yyyy}", Eval("colDate"))%></asp:HyperLink>
                        <asp:LinkButton ID="uoLinkButtonEvents" runat="server" Text="*" CssClass="EventNotification" Visible='<%# Eval("IsWithEvent") %>' ToolTip="With Event(s)"></asp:LinkButton>
                    </td>
                    <td class="leftAligned" >
                        <%# Eval("colDateName")%>
                    </td>
                    <td >                        
                        <asp:Label ID="uoLabelRoom1" runat="server" Text='<%# Eval("UseTotal1")%>' ></asp:Label>                        
                    </td>
                    <td>
                        <asp:Label ID="uoLabelRate1" runat="server" Text='<%# Eval("RateTotal1")%>'></asp:Label>
                    </td>
                    <td>                        
                        <asp:HyperLink CssClass="HotelLink" ID="uoHyperLinkEditOverride1" runat="server" NavigateUrl='<%# "HotelRoomOverrideEdit.aspx?bID=" + Eval("BranchID") + "&rID=1" + "&dt=" + String.Format("{0:MM-dd-yyyy}", Eval("colDate")) %>' >Edit</asp:HyperLink>
                    </td>
                    
                    <td >
                        <asp:Label ID="uoLabelRoom2" runat="server" Text='<%# Eval("UseTotal2")%>' ></asp:Label>                        
                    </td>
                    <td>
                         <asp:Label ID="uoLabelRate2" runat="server" Text='<%# Eval("RateTotal2")%>'></asp:Label>
                    </td>
                    <td>                        
                        <asp:HyperLink CssClass="HotelLink" ID="uoHyperLinkEditOverride2" runat="server" NavigateUrl='<%# "HotelRoomOverrideEdit.aspx?bID=" + Eval("BranchID") + "&rID=2" + "&dt=" + String.Format("{0:MM-dd-yyyy}", Eval("colDate")) %>' >Edit</asp:HyperLink>
                    </td> --%>                                              
                <%--</tr>--%>
            </ItemTemplate>
            <EmptyDataTemplate>
                <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                    <tr>
                        <th>
                            Date
                        </th>
                        <th>
                            DateName
                        </th>
                        <th>
                            Room Type
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
    <td class="LeftClass">
        <asp:DataPager ID="uoDataPagerDashboard" runat="server" PagedControlID="uoListViewDashboard"
            PageSize="20" OnPreRender="uoDataPagerDashboard_PreRender">
            <Fields>
                <asp:NumericPagerField ButtonType="link" NumericButtonCssClass="PagerClass" />
            </Fields>
        </asp:DataPager>
        
         <asp:ObjectDataSource ID="uoObjectDataSourceDashboard" runat="server" 
                onselecting="uoObjectDataSourceDashboard_Selecting" 
                MaximumRowsParameterName="MaxRow" SelectCountMethod="LoadHotelDashboardListCount" 
                SelectMethod="LoadHotelDashboardList" StartRowIndexParameterName="StartRow" 
                TypeName="TRAVELMART.BLL.HotelDashboardBLL" 
                OldValuesParameterFormatString="oldcount_{0}" EnablePaging="True">
                <SelectParameters>
                    <asp:Parameter Name="iRegionID" Type="Int32" />
                    <asp:Parameter Name="iCountryID" Type="Int32" />
                    <asp:Parameter Name="iCityID" Type="Int64" />
                    <asp:Parameter Name="iPortID" Type="Int32" />
                    <asp:Parameter Name="sUserName" Type="String" />
                    <asp:Parameter Name="sRole" Type="String" />
                    <asp:Parameter Name="iBranchID" Type="Int64" />
                    <asp:Parameter Name="dFrom" Type="DateTime"/>
                    <asp:Parameter Name="dTo" Type="DateTime" />
                    <asp:Parameter Name="sBranchName" Type="String" />
                    <asp:Parameter Name="iLoadType" Type="Int16" />
                </SelectParameters>
            </asp:ObjectDataSource>
             <asp:HiddenField ID="uoHiddenFieldPopupHotel" runat="server" Value="0"/>
             <asp:HiddenField ID="uoHiddenFieldRole" runat="server" />       
             <asp:HiddenField ID="uoHiddenFieldUser" runat="server" />       
             <asp:HiddenField ID="uoHiddenFieldDateRange" runat="server" />
             <asp:HiddenField ID="uoHiddenFieldDate" runat="server" />
             <asp:HiddenField ID="uoHiddenFieldLoadType" runat="server" Value="0" />
             <asp:HiddenField ID="uoHiddenFieldBranchID" runat="server" Value="0" />
    </td>
 </tr>
 </table>              
  </ContentTemplate> 
</asp:UpdatePanel>
</asp:Content>

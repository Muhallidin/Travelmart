<%@ Page Title="" Language="C#" MasterPageFile="~/TravelmartMaster.Master" AutoEventWireup="true" CodeBehind="HotelDashboardRoomType.aspx.cs" Inherits="TRAVELMART.HotelDashboardRoomType" %>
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
   <tr  runat="server" id="TRSearch">        
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
    </tr>    
   </table>

 <table width="100%" cellspacing="0" border="0">
 <tr>
    <td></td>
 </tr>
 <tr >
     <td >
                
        <asp:ListView runat="server" ID="uoListViewDashboard" 
             DataSourceID = "uoObjectDataSourceDashboard" 
             onitemcommand="uoListViewDashboard_ItemCommand" 
             onitemdatabound="uoListViewDashboard_ItemDataBound">
            <LayoutTemplate>
                <table border="0" cellpadding="0" cellspacing="0" class="listViewTable" width="100%">
                    <tr>
                        <th rowspan="2" style="width: 120px">
                            Date
                        </th>
                        <th rowspan="2" style="width: 180px">
                            Date Name
                        </th>
                        <th colspan="3">
                            <asp:Label ID="uoLabelRoomType1" runat="server" Text=""></asp:Label>
                        </th>
                        <th colspan="3">
                            <asp:Label ID="uoLabelRoomType2" runat="server" Text=""></asp:Label>
                        </th>
                       <%-- <th colspan="3">
                            <asp:Label ID="uoLabelRoomType3" runat="server" Text=""></asp:Label>
                        </th >
                        <th colspan="3">
                           <asp:Label ID="uoLabelRoomType4" runat="server" Text=""></asp:Label>
                        </th>
                        <th colspan="3">
                            <asp:Label ID="uoLabelRoomType5" runat="server" Text=""></asp:Label>
                        </th>       --%>                
                    </tr>
                    <tr>
                        <th>Reserved / Total Count</th>
                        <th>Total Amt / Amt per Room</th>
                        <th>Override</th>
                        
                        <th>Reserved / Total Count</th>
                        <th>Total Amt / Amt per Room</th>
                        <th>Override</th>
                        
                        <%--<th>Reserved/Total Count</th>
                        <th>Amount/Total Amt</th>
                        <th>Override</th>
                        
                        <th>Reserved/Total Count</th>
                        <th>Amount/Total Amt</th>
                        <th>Override</th>
                        
                        <th>Reserved/Total Count</th>                        
                        <th>Amount/Total Amt</th>
                        <th>Override</th>--%>
                    </tr>
                    <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                </table>
            </LayoutTemplate>
            <ItemTemplate>
           <%# DashboardAddGroup()%>
                <tr>
                    <td class="leftAligned" > 
                        <asp:HiddenField ID="uoHiddenFieldBranchID" runat="server" Value ='<%# Eval("BranchID") + "_" + Eval("colCityIDInt") + "_" + Eval("colDate") %>' />                                                
                        <asp:HyperLink ID="uoHyperLinkDate" runat="server"  NavigateUrl='<%# "HotelDashboard2.aspx?ufn="+ Request.QueryString["ufn"].ToString() +"&dt=" + String.Format("{0:MM-dd-yyyy}", Eval("colDate")) + "&branchId=" + Eval("BranchID") + "&brandId=" + Eval("BrandId") + "&branchName=" + Eval("BranchName") %>'><%# String.Format("{0:dd-MMM-yyyy}", Eval("colDate"))%></asp:HyperLink>
                        <asp:LinkButton ID="uoLinkButtonEvents" runat="server" Text="*" CssClass="EventNotification" Visible='<%# Eval("IsWithEvent") %>' ToolTip="With Event(s)"></asp:LinkButton>
                    </td>
                    <td class="leftAligned" >
                        <%# Eval("colDateName")%>
                    </td>
                    <td >                        
                        <asp:Label ID="uoLabelRoom1" runat="server" Text='<%# Eval("UseTotal1")%>' ></asp:Label>
                        <%--<asp:Label ID="uoLabelRoom1" runat="server" Text='<%# String.Format(FormatDouble(Eval("UseTotal1A"))) + "/" + <%# Eval("UseTotal1")%>%>' ></asp:Label>--%>
                        <%--<asp:LinkButton ID="uoLinkButtonRoom1" CommandName="ViewRoom" runat="server" Visible = '<%# Convert.ToBoolean(Eval("IsWithTransaction1")) %>'><%# Eval("UseTotal1")%></asp:LinkButton>                        --%>
                    </td>
                    <td>
                        <asp:Label ID="uoLabelRate1" runat="server" Text='<%# Eval("RateTotal1")%>'></asp:Label>
                    </td>
                    <td>
                        <%--<asp:HyperLink CssClass="HotelLink" ID="uoHyperLinkEditOverride1" runat="server" NavigateUrl='<%# "HotelRoomOverrideEdit.aspx?bID=" + Eval("BranchID") + "&rID=1" + "&dt=" + String.Format("{0:MM-dd-yyyy}", Eval("colDate")) %>' Visible = '<%# Convert.ToBoolean(Eval("IsWithThisRoomType1")) %>'>Edit</asp:HyperLink>--%>
                        <asp:HyperLink CssClass="HotelLink" ID="uoHyperLinkEditOverride1" runat="server" NavigateUrl='<%# "HotelRoomOverrideEdit.aspx?bID=" + Eval("BranchID") + "&rID=1" + "&dt=" + String.Format("{0:MM-dd-yyyy}", Eval("colDate")) %>' >Edit</asp:HyperLink>
                    </td>
                    
                    <td >
                        <asp:Label ID="uoLabelRoom2" runat="server" Text='<%# Eval("UseTotal2")%>' ></asp:Label>
                        <%--<asp:Label ID="uoLabelRoom2" runat="server" Text='<%# String.Format(FormatDouble(Eval("UseTotal2A"))) + "/" + <%# Eval("UseTotal2")%>%>' ></asp:Label>--%>
                        <%--<asp:LinkButton ID="uoLinkButtonRoom2" CommandName="ViewRoom" runat="server" Visible = '<%# Convert.ToBoolean(Eval("IsWithTransaction2")) %>'><%# Eval("UseTotal2")%></asp:LinkButton>--%>                        
                    </td>
                    <td>
                         <asp:Label ID="uoLabelRate2" runat="server" Text='<%# Eval("RateTotal2")%>'></asp:Label>
                    </td>
                    <td>
                        <%--<asp:HyperLink CssClass="HotelLink" ID="uoHyperLinkEditOverride2" runat="server" NavigateUrl='<%# "HotelRoomOverrideEdit.aspx?bID=" + Eval("BranchID") + "&rID=2" + "&dt=" + String.Format("{0:MM-dd-yyyy}", Eval("colDate")) %>' Visible = '<%# Convert.ToBoolean(Eval("IsWithThisRoomType2")) %>'>Edit</asp:HyperLink>--%>
                        <asp:HyperLink CssClass="HotelLink" ID="uoHyperLinkEditOverride2" runat="server" NavigateUrl='<%# "HotelRoomOverrideEdit.aspx?bID=" + Eval("BranchID") + "&rID=2" + "&dt=" + String.Format("{0:MM-dd-yyyy}", Eval("colDate")) %>' >Edit</asp:HyperLink>
                    </td>
                    
                  <%--  <td >
                        <asp:Label ID="uoLabelRoom3" runat="server" Text='<%# Eval("UseTotal3")%>' Visible = '<%# !Convert.ToBoolean(Eval("IsWithTransaction3")) %>'></asp:Label>
                        <asp:LinkButton ID="uoLinkButtonRoom3" CommandName="ViewRoom" runat="server" Visible = '<%# Convert.ToBoolean(Eval("IsWithTransaction3")) %>'><%# Eval("UseTotal3")%></asp:LinkButton>                        
                    </td>    
                    <td>
                        <asp:Label ID="uoLabelRate3" runat="server" Text='<%# Eval("RateTotal3")%>'></asp:Label>
                    </td>
                    <td>
                        <asp:HyperLink CssClass="HotelLink" ID="uoHyperLinkEditOverride3" runat="server" NavigateUrl='<%# "HotelRoomOverrideEdit.aspx?bID=" + Eval("BranchID") + "&rID=3" + "&dt=" + String.Format("{0:MM-dd-yyyy}", Eval("colDate")) %>' Visible = '<%# Convert.ToBoolean(Eval("IsWithThisRoomType3")) %>'>Edit</asp:HyperLink>
                    </td>   
                                         
                    <td >
                        <asp:Label ID="uoLabelRoom4" runat="server" Text='<%# Eval("UseTotal4")%>' Visible = '<%# !Convert.ToBoolean(Eval("IsWithTransaction4")) %>'></asp:Label>
                        <asp:LinkButton ID="uoLinkButtonRoom4" CommandName="ViewRoom" runat="server" Visible = '<%# Convert.ToBoolean(Eval("IsWithTransaction4")) %>'><%# Eval("UseTotal4")%></asp:LinkButton>                        
                    </td>
                    <td>
                        <asp:Label ID="uoLabelRate4" runat="server" Text='<%# Eval("RateTotal4")%>'></asp:Label>
                    </td>
                    <td>
                        <asp:HyperLink CssClass="HotelLink" ID="uoHyperLinkEditOverride4" runat="server" NavigateUrl='<%# "HotelRoomOverrideEdit.aspx?bID=" + Eval("BranchID") + "&rID=4" + "&dt=" + String.Format("{0:MM-dd-yyyy}", Eval("colDate")) %>' Visible = '<%# Convert.ToBoolean(Eval("IsWithThisRoomType4")) %>'>Edit</asp:HyperLink>
                    </td>
                    
                    <td >
                        <asp:Label ID="uoLabelRoom5" runat="server" Text='<%# Eval("UseTotal5")%>' Visible = '<%# !Convert.ToBoolean(Eval("IsWithTransaction5")) %>'></asp:Label>
                        <asp:LinkButton ID="uoLinkButtonRoom5" CommandName="ViewRoom" runat="server" Visible = '<%# Convert.ToBoolean(Eval("IsWithTransaction5")) %>'><%# Eval("UseTotal5")%></asp:LinkButton>                        
                    </td>    
                    <td>
                        <asp:Label ID="uoLabelRate5" runat="server" Text='<%# Eval("RateTotal5")%>'></asp:Label>
                    </td>
                    <td>
                        <asp:HyperLink CssClass="HotelLink" ID="uoHyperLinkEditOverride5" runat="server" NavigateUrl='<%# "HotelRoomOverrideEdit.aspx?bID=" + Eval("BranchID") + "&rID=5" + "&dt=" + String.Format("{0:MM-dd-yyyy}", Eval("colDate")) %>' Visible = '<%# Convert.ToBoolean(Eval("IsWithThisRoomType5")) %>'>Edit</asp:HyperLink>
                    </td> --%>              
                </tr>
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
                MaximumRowsParameterName="MaxRow" SelectCountMethod="getHotelDashboardRoomTypeCount" 
                SelectMethod="getHotelDashboardRoomType" StartRowIndexParameterName="StartRow" 
                TypeName="TRAVELMART.BLL.DashboardBLL" 
                OldValuesParameterFormatString="oldcount_{0}" EnablePaging="True">
                <SelectParameters>
                    <asp:Parameter Name="RegionID" Type="String" />
                    <asp:Parameter Name="CountryID" Type="String" />
                    <asp:Parameter Name="CityID" Type="String" />
                    <asp:Parameter Name="UserName" Type="String" />
                    <asp:Parameter Name="UserRole" Type="String" />
                    <asp:Parameter Name="BranchID" Type="String" />
                    <asp:Parameter Name="DateFrom" Type="String"/>
                    <asp:Parameter Name="DateTo" Type="String" />                                                                                                 
                </SelectParameters>
            </asp:ObjectDataSource>
             <asp:HiddenField ID="uoHiddenFieldPopupHotel" runat="server" Value="0"/>
             <asp:HiddenField ID="uoHiddenFieldRole" runat="server" />       
             <asp:HiddenField ID="uoHiddenFieldDateRange" runat="server" />
             <asp:HiddenField ID="uoHiddenFieldDate" runat="server" />
    </td>
 </tr>
 </table>              
  </ContentTemplate> 
</asp:UpdatePanel>
</asp:Content>

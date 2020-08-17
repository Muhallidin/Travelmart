<%@ Page Title="" Language="C#" MasterPageFile="~/TravelMartMaster2.Master" AutoEventWireup="true" CodeBehind="PortAgentDashboard.aspx.cs" 
Inherits="TRAVELMART.PortAgentDashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" runat="server">
    <table width="100%" cellpadding="0" cellspacing="0" style="height:25px">
        <tr class="PageTitle">
            <td align="left" style="vertical-align:middle; white-space:nowrap "  style="width: 150px;">
                <asp:Label ID="uoLabelTitle" runat="server" class="Title" Text="Vendor Dashboard"></asp:Label>
            </td>           
            <td align="right">
                 <asp:DropDownList runat="server" ID ="uoDropDownListSeaport" 
                    AppendDataBoundItems="true" Width="350px" AutoPostBack="true" 
                    onselectedindexchanged="uoDropDownListSeaport_SelectedIndexChanged">
                </asp:DropDownList>
                
            <%--</td>
            <td align="right" style="white-space: nowrap; width: 350px;">--%>               
                <asp:DropDownList runat="server" ID ="uoDropDownListPortAgent" 
                    AppendDataBoundItems="true" Width="350px" AutoPostBack="true" 
                    onselectedindexchanged="uoDropDownListPortAgent_SelectedIndexChanged">
                </asp:DropDownList>
                                
                
            <%--</td>             
            <td align="right" style="white-space: nowrap; width: 160px;">--%>
                <asp:DropDownList runat="server" ID ="uoDropDownListDays" 
                    AppendDataBoundItems="true" Width="160px" AutoPostBack="true" 
                    onselectedindexchanged="uoDropDownListDays_SelectedIndexChanged">
                </asp:DropDownList>       
            </td>
            <%--<td align="right" style="width: 600px">
                <asp:Label ID="ucLabelPort" runat="server" class="Title" Text="Vendor"></asp:Label>&nbsp;
                <asp:Label ID="lblPortAgentVendor" runat="server" Text="" class="Title" />
                <asp:DropDownList ID="uoDropDownListPort" runat="server" Width="280px" AutoPostBack="true"
                    OnSelectedIndexChanged="uoDropDownListPort_SelectedIndexChanged" Visible="false">
                </asp:DropDownList>
                &nbsp;&nbsp;
            </td>--%>            
        </tr>        
    </table>    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="NaviPlaceHolder" runat="server">
    <div style="overflow: auto; width: 100%; overflow-x: auto; overflow-y: auto; padding-top:5px ;display:none" >
        <asp:HyperLink ID="uoHyperLinkDashboard" runat="server">Non Turn Port Exception</asp:HyperLink>
    </div>
    <div id="uoDivDashboard" style="overflow: auto; width: 100%; overflow-x: auto; overflow-y: auto;">
        <br />
        <table width="500px" cellpadding="0" cellspacing="0" class="cssTblDashboard">
            <tr>
                <th colspan="3">
                    Hotel Service
                </th>                
            </tr>
            <tr>
                <td class="leftAligned" style="width:2px">&nbsp;</td>
                <td class="midAligned">
                    <asp:ListView runat="server" ID="uoListViewHotelCount" OnItemCommand="uoListViewDashboard_ItemCommand"
                        OnItemDataBound="uoListViewDashboard_ItemDataBound" >
                        <LayoutTemplate>
                            <table border="0" cellpadding="0" cellspacing="0" class="cssTblDashboard" width="100%">
                                <tr>
                                </tr>
                                <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                            </table>
                        </LayoutTemplate>
                        <ItemTemplate>
                          <tr>
                            <td>
                               <table width="100%" cellpadding="0" cellspacing="0" class="cssTblDashboardSub">
                                    <tr>
                                        <td colspan="6" class='<%# (GetRowDivision(Eval("iRow"))) %>'></td>
                                    </tr>
                                    <tr>
                                        <td class="DashboardVendorName" colspan="6">
                                            <asp:HyperLink runat="server" id="uoHyperLinkHotel"
                                                NavigateUrl='<%# "PortAgentHotelManifest.aspx?pid=" + Eval("PortAgentID") + 
                                                    "&ufn=" + Request.QueryString["ufn"] + "&dt=" + Session["DateFrom"] %>'  Text='<%# Eval("PortAgentName") %>'>  
                                                
                                             </asp:HyperLink>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="DashboardRowCaption">Booking Count:</td>
                                        <td class="DashboardRowValue">
                                            <%# Eval("TotalCount")%>
                                        </td>
                                        <td class="DashboardRowCaption">Approved:</td>
                                        <td class="DashboardRowValue">
                                           <span <%# GetPendingColor(Eval("ApprovedColor")) %> > <%# Eval("Approved")%> </span>
                                        </td>
                                        <td class="DashboardRowCaption">Cancelled:</td>
                                        <td class="DashboardRowValue">
                                            <span <%# GetPendingColor(Eval("CancelledColor")) %> ><%# Eval("Cancelled")%></span>
                                        </td>
                                    </tr>                                                                                                       
                                    <tr>                                       
                                        <td class="DashboardRowCaption">Pending With Vendor:</td>
                                        <td class="DashboardRowValue">
                                             <span <%# GetPendingColor(Eval("PendingColor")) %> ><%# Eval("PendingVendor")%></span>
                                        </td>
                                        <td class="DashboardRowCaption">Pending With RCCL:</td>
                                        <td class="DashboardRowValue" >
                                           <span <%# GetPendingColor(Eval("PendingRCCLColor")) %> > <%# Eval("PendingRCCL")%> </span>
                                        </td>
                                        <td class="DashboardRowCaption">Pending Cost Approval</td>
                                        <td class="DashboardRowValue">                                            
                                            <span <%# GetPendingColor(Eval("PendingRCCLCostColor")) %> > <%# Eval("PendingRCCLCost")%></span>
                                        </td>
                                    </tr>                          
                                </table>
                            </td>
                           </tr>                                                                                            
                        </ItemTemplate>
                        <EmptyDataTemplate> 
                            No Record
                        </EmptyDataTemplate>
                    </asp:ListView>
                </td>
                <td class="rightAligned" style="width:2px">&nbsp;</td>
            </tr>
            <tr>
                <td>
                    <br />
                </td>
            </tr>
            <tr>
                <th colspan="3">
                    Transportation Service
                </th>                
            </tr>
            <tr>
                <td class="leftAligned" style="width:2px">&nbsp;</td>
                <td class="midAligned">
                    <asp:ListView runat="server" ID="uoListViewVehicleCount" OnItemCommand="uoListViewDashboard_ItemCommand"
                        OnItemDataBound="uoListViewDashboard_ItemDataBound" >
                        <LayoutTemplate>
                            <table border="0" cellpadding="0" cellspacing="0" class="cssTblDashboard" width="100%">
                                <tr>
                                </tr>
                                <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                            </table>
                        </LayoutTemplate>
                        <ItemTemplate>                                                        
                          <tr>
                            <td>
                              <table width="100%" cellpadding="0" cellspacing="0" class="cssTblDashboardSub">
                                    <tr>
                                        <td colspan="6" class='<%# (GetRowDivision(Eval("iRow"))) %>'></td>
                                    </tr>
                                    <tr>
                                        <td class="DashboardVendorName" colspan="6">
                                            <asp:HyperLink runat="server" id="uoHyperLinkHotel"
                                                NavigateUrl='<%# "PortAgentVehicleManifest.aspx?pid=" + Eval("PortAgentID") + 
                                                "&ufn=" + Request.QueryString["ufn"] +
                                                "&dt=" + Session["DateFrom"] %>'  
                                                Text='<%# Eval("PortAgentName") %>'> </asp:HyperLink>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="DashboardRowCaption">Booking Count:</td>
                                        <td class="DashboardRowValue">
                                            <%# Eval("TotalCount")%>
                                        </td>
                                        <td class="DashboardRowCaption">Approved:</td>
                                        <td class="DashboardRowValue">
                                            <span <%# GetPendingColor(Eval("ApprovedColor")) %> > <%# Eval("Approved")%> </span>
                                        </td>
                                        <td class="DashboardRowCaption">Cancelled:</td>
                                        <td class="DashboardRowValue">
                                             <span <%# GetPendingColor(Eval("CancelledColor")) %> ><%# Eval("Cancelled")%></span>
                                        </td>
                                    </tr>                                                                                                       
                                    <tr>                                       
                                        <td class="DashboardRowCaption">Pending With Vendor:</td>
                                        <td class="DashboardRowValue">
                                             <span <%# GetPendingColor(Eval("PendingColor")) %> ><%# Eval("PendingVendor")%></span>
                                        </td>
                                        <td class="DashboardRowCaption">Pending With RCCL:</td>
                                        <td class="DashboardRowValue" >
                                           <span <%# GetPendingColor(Eval("PendingRCCLColor")) %> > <%# Eval("PendingRCCL")%> </span>
                                        </td>
                                        <td class="DashboardRowCaption">Pending Cost Approval</td>
                                        <td class="DashboardRowValue">                                            
                                            <span <%# GetPendingColor(Eval("PendingRCCLCostColor")) %> > <%# Eval("PendingRCCLCost")%></span>
                                        </td>
                                    </tr>                          
                                </table>
                              </td>
                           </tr>                                                                                            
                        </ItemTemplate>
                         <EmptyDataTemplate>
                            No Record
                        </EmptyDataTemplate>
                    </asp:ListView>
                </td>
                <td class="rightAligned" style="width:2px">&nbsp;</td>
            </tr>
           <tr>
                <td>
                    <br />
                </td>
            </tr>
            
            <%--=======Commented for now============================================================================================--%>
            <%--<tr>
                <th colspan="3">
                    Luggage
                </th>                
            </tr>
            <tr>
                <td class="leftAligned" style="width:2px">&nbsp;</td>
                <td class="midAligned">
                    <asp:ListView runat="server" ID="uoListViewLuggageCount" OnItemCommand="uoListViewDashboard_ItemCommand"
                        OnItemDataBound="uoListViewDashboard_ItemDataBound" >
                        <LayoutTemplate>
                            <table border="0" cellpadding="0" cellspacing="0" class="cssTblDashboard" width="100%">
                                <tr>
                                </tr>
                                <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                            </table>
                        </LayoutTemplate>
                         <ItemTemplate>                                                        
                          <tr>
                            <td>
                              <table width="100%" cellpadding="0" cellspacing="0" class="cssTblDashboardSub">
                                    <tr>
                                        <td colspan="6" class='<%# (GetRowDivision(Eval("iRow"))) %>'></td>
                                    </tr>
                                    <tr>
                                        <td class="DashboardVendorName" colspan="6">
                                            <asp:HyperLink runat="server" id="uoHyperLinkHotel"
                                                NavigateUrl='<%# "PortAgentRequest.aspx?pid=" + Eval("PortAgentID") + 
                                                "&ufn=" + Request.QueryString["ufn"] +
                                                "&dt=" + Session["DateFrom"] %>'  
                                                Text='<%# Eval("PortAgentName") %>'> </asp:HyperLink>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="DashboardRowCaption">Booking Count:</td>
                                        <td class="DashboardRowValue">
                                            <%# Eval("TotalCount")%>
                                        </td>
                                        <td class="DashboardRowCaption"></td>
                                        <td class="DashboardRowValue"> 
                                        </td>
                                        <td class="DashboardRowCaption"></td>
                                        <td class="DashboardRowValue">  
                                        </td>
                                    </tr>                                                                                                       
                                    <tr>                                       
                                        <td class="DashboardRowCaption"></td>
                                        <td class="DashboardRowValue">  
                                        </td>
                                        <td class="DashboardRowCaption"></td>
                                        <td class="DashboardRowValue" >  
                                        </td>
                                        <td class="DashboardRowCaption"></td>
                                        <td class="DashboardRowValue">                                              
                                        </td>
                                    </tr>                          
                                </table>
                              </td>
                           </tr>                                                                                            
                        </ItemTemplate>
                         <EmptyDataTemplate>
                            No Record
                        </EmptyDataTemplate>                                                                       
                    </asp:ListView>                
                </td>
                <td class="rightAligned" style="width:2px">&nbsp;</td>
            </tr>
           <tr>
                <td>
                    <br />
                </td>
            </tr>
            <tr>
                <th colspan="3">
                    Visa
                </th>                
            </tr>
            <tr>
            <td class="leftAligned" style="width:2px">&nbsp;</td>
            <td class="midAligned">
                     <asp:ListView runat="server" ID="uoListViewVisaCount" OnItemCommand="uoListViewDashboard_ItemCommand"
                        OnItemDataBound="uoListViewDashboard_ItemDataBound" >
                        <LayoutTemplate>
                            <table border="0" cellpadding="0" cellspacing="0" class="cssTblDashboard" width="100%">
                                <tr>
                                </tr>
                                <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                            </table>
                        </LayoutTemplate>
                         <ItemTemplate>                                                        
                          <tr>
                            <td>
                              <table width="100%" cellpadding="0" cellspacing="0" class="cssTblDashboardSub">
                                    <tr>
                                        <td colspan="6" class='<%# (GetRowDivision(Eval("iRow"))) %>'></td>
                                    </tr>
                                    <tr>
                                        <td class="DashboardVendorName" colspan="6">
                                            <asp:HyperLink runat="server" id="uoHyperLinkHotel"
                                                NavigateUrl='<%# "PortAgentRequest.aspx?pid=" + Eval("PortAgentID") + 
                                                "&ufn=" + Request.QueryString["ufn"] +
                                                "&dt=" + Session["DateFrom"] %>'  
                                                Text='<%# Eval("PortAgentName") %>'> </asp:HyperLink>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="DashboardRowCaption">Booking Count:</td>
                                        <td class="DashboardRowValue">
                                            <%# Eval("TotalCount")%>
                                        </td>
                                        <td class="DashboardRowCaption"></td>
                                        <td class="DashboardRowValue">  
                                        </td>
                                        <td class="DashboardRowCaption"></td>
                                        <td class="DashboardRowValue">  
                                        </td>
                                    </tr>                                                                                                       
                                    <tr>                                       
                                        <td class="DashboardRowCaption"></td>
                                        <td class="DashboardRowValue"> 
                                        </td>
                                        <td class="DashboardRowCaption"></td>
                                        <td class="DashboardRowValue" >
                                        </td>
                                        <td class="DashboardRowCaption"></td>
                                        <td class="DashboardRowValue">                                              
                                        </td>
                                    </tr>                          
                                </table>
                              </td>
                           </tr>                                                                                            
                        </ItemTemplate>
                         <EmptyDataTemplate>
                            No Record
                        </EmptyDataTemplate>                                                                       
                    </asp:ListView>           
            </td>
            <td class="rightAligned" style="width:2px">&nbsp;</td>
            </tr>
            <tr>
                <td>
                    <br />
                </td>            
            </tr>
            <tr>
                <th colspan="3">
                    SafeGuard
                </th>                
            </tr>
            <tr>
            <td class="leftAligned" style="width:2px">&nbsp;</td>
            <td class="midAligned">
                     <asp:ListView runat="server" ID="uoListViewSafeGuardCount" OnItemCommand="uoListViewDashboard_ItemCommand"
                        OnItemDataBound="uoListViewDashboard_ItemDataBound" >
                        <LayoutTemplate>
                            <table border="0" cellpadding="0" cellspacing="0" class="cssTblDashboard" width="100%">
                                <tr>
                                </tr>
                                <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                            </table>
                        </LayoutTemplate>
                         <ItemTemplate>                                                        
                          <tr>
                            <td>
                              <table width="100%" cellpadding="0" cellspacing="0" class="cssTblDashboardSub">
                                    <tr>
                                        <td colspan="6" class='<%# (GetRowDivision(Eval("iRow"))) %>'></td>
                                    </tr>
                                    <tr>
                                        <td class="DashboardVendorName" colspan="6">
                                            <asp:HyperLink runat="server" id="uoHyperLinkHotel"
                                                NavigateUrl='<%# "PortAgentRequest.aspx?pid=" + Eval("PortAgentID") + 
                                                "&ufn=" + Request.QueryString["ufn"] +
                                                "&dt=" + Session["DateFrom"] %>'  
                                                Text='<%# Eval("PortAgentName") %>'> </asp:HyperLink>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="DashboardRowCaption">Booking Count:</td>
                                        <td class="DashboardRowValue">
                                            <%# Eval("TotalCount")%>
                                        </td>
                                        <td class="DashboardRowCaption"></td>
                                        <td class="DashboardRowValue"> 
                                        </td>
                                        <td class="DashboardRowCaption"></td>
                                        <td class="DashboardRowValue">
                                            
                                        </td>
                                    </tr>                                                                                                       
                                    <tr>                                       
                                        <td class="DashboardRowCaption"></td>
                                        <td class="DashboardRowValue">  
                                        </td>
                                        <td class="DashboardRowCaption"></td>
                                        <td class="DashboardRowValue" >
                                        </td>
                                        <td class="DashboardRowCaption"></td>
                                        <td class="DashboardRowValue">                                            
                                        </td>
                                    </tr>                          
                                </table>
                              </td>
                           </tr>                                                                                            
                        </ItemTemplate>
                         <EmptyDataTemplate>
                            No Record
                        </EmptyDataTemplate>                                                                       
                    </asp:ListView>           
            </td>
            <td class="rightAligned" style="width:2px">&nbsp;</td>
            </tr>
           <tr>
                <td>
                    <br />
                </td>            
            </tr>
            <tr>
                <th colspan="3">
                    Meet and Greet
                </th>
            </tr>
            <tr>
                <td class="leftAligned" style="width:2px">&nbsp;</td>
                <td class="midAligned">
                     <asp:ListView runat="server" ID="uoListViewMAGCount" OnItemCommand="uoListViewDashboard_ItemCommand"
                        OnItemDataBound="uoListViewDashboard_ItemDataBound" >
                        <LayoutTemplate>
                            <table border="0" cellpadding="0" cellspacing="0" class="cssTblDashboard" width="100%">
                                <tr>
                                </tr>
                                <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                            </table>
                        </LayoutTemplate>
                         <ItemTemplate>                                                        
                          <tr>
                            <td>
                              <table width="100%" cellpadding="0" cellspacing="0" class="cssTblDashboardSub">
                                    <tr>
                                        <td colspan="6" class='<%# (GetRowDivision(Eval("iRow"))) %>'></td>
                                    </tr>
                                    <tr>
                                        <td class="DashboardVendorName" colspan="6">
                                            <asp:HyperLink runat="server" id="uoHyperLinkHotel"
                                                NavigateUrl='<%# "PortAgentRequest.aspx?pid=" + Eval("PortAgentID") + 
                                                "&ufn=" + Request.QueryString["ufn"] +
                                                "&dt=" + Session["DateFrom"] %>'  
                                                Text='<%# Eval("PortAgentName") %>'> </asp:HyperLink>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="DashboardRowCaption">Booking Count:</td>
                                        <td class="DashboardRowValue">
                                            <%# Eval("TotalCount")%>
                                        </td>
                                        <td class="DashboardRowCaption"></td>
                                        <td class="DashboardRowValue">
                                        </td>
                                        <td class="DashboardRowCaption"></td>
                                        <td class="DashboardRowValue">
                                        </td>
                                    </tr>                                                                                                       
                                    <tr>                                       
                                        <td class="DashboardRowCaption"></td>
                                        <td class="DashboardRowValue">
                                        </td>
                                        <td class="DashboardRowCaption"></td>
                                        <td class="DashboardRowValue" >
                                        </td>
                                        <td class="DashboardRowCaption"></td>
                                        <td class="DashboardRowValue">                                            
                                        </td>
                                    </tr>                          
                                </table>
                              </td>
                           </tr>                                                                                            
                        </ItemTemplate>
                         <EmptyDataTemplate>
                            No Record
                        </EmptyDataTemplate>                                                                       
                    </asp:ListView>                
                </td>
                <td class="rightAligned" style="width:2px">&nbsp;</td>
            </tr>                                                                                    
        </table>
    </div>--%>
    <%--=======Commented for now============================================================================================--%>
    
    <%--===================================================================================================--%>
    <%--Hotel Service Manifest--%>
    
    <%--<div style="display:none">
    <div id="uoDivHotelManifest" >       
        <table width="100%" cellpadding="0" cellspacing="0" class="LeftClass">
            <tr>
                <td>                 
                    <div id="Av" style="overflow: auto; overflow-x: hidden; width: 100%; overflow-y: hidden;">
                        <asp:ListView runat="server" ID="uoListViewHotelHeader" OnItemCommand="uoListViewHotelHeader_ItemCommand">
                        <LayoutTemplate>
                        </LayoutTemplate>
                        <ItemTemplate>
                        </ItemTemplate>
                        <EmptyDataTemplate>
                            <table class="listViewTable">
                                <tr>                        
                                    <th style="text-align: center; white-space: normal;">
                                        <asp:LinkButton ID="uoLinkButtonE1" runat="server" CommandName="EmployeeId"
                                            Text="E1 ID" Width="55px" />
                                    </th>
                                     <th style="text-align: center; white-space: normal;">
                                        <asp:LinkButton ID="uoLblLastNameHeader" runat="server" CommandName="LastName"
                                            Text="Last Name" Width="145px" />
                                    </th>
                                    <th style="text-align: center; white-space: normal;">
                                        <asp:LinkButton ID="uoLblFirstNameHeader" runat="server" CommandName="FirstName"
                                            Text="First Name" Width="144px" />
                                    </th>
                                    <th style="text-align: center; white-space: normal;">
                                        <asp:LinkButton ID="LinkButton3" runat="server" CommandName="HotelName"
                                            Text="Hotel Name" Width="100px" />
                                    </th>
                                    <th style="text-align: center; white-space: normal;">
                                        <asp:LinkButton ID="LinkButton4" runat="server" CommandName="ConfirmationNo"
                                            Text="Confirmation #" Width="100px" />
                                    </th>
                                    
                                    <th style="text-align: center; white-space: normal;">
                                        <asp:LinkButton ID="uoLblCheckInHeader" runat="server" CommandName="CheckIn"
                                            Text="Check-In" Width="64px" />
                                    </th>
                                    <th style="text-align: center; white-space: normal;">
                                        <asp:LinkButton ID="uoLblCheckOutHeader" runat="server" CommandName="CheckOut"
                                            Text="Check-Out" Width="64px" />
                                    </th>
                                    <th style="text-align: center; white-space: normal;">
                                        <asp:LinkButton ID="uoLblNitesHeader" runat="server" CommandName="HotelNites"
                                            Text="Hotel Nites" Width="34px" />
                                    </th>
                                    
                                  
                                    <th style="text-align: center; white-space: normal;">
                                        <asp:LinkButton ID="uoLblRoomHeader" runat="server" CommandName="RoomType"
                                            Text="Single/Double" Width="74px" />
                                    </th>
                                   
                                   
                                     <th style="text-align: center; white-space: normal;">
                                        <asp:LinkButton ID="uoLblShipHeader" runat="server" CommandName="VesselName" Text="Ship"
                                            Width="164px" />
                                    </th>
                                    
                                    <th style="text-align: center; white-space: normal;">
                                        <asp:LinkButton ID="uoLblTitleHeader" runat="server" CommandName="RankName" Text="Title"
                                            Width="215px" />
                                    </th>
                                  
                                    <th style="text-align: center; white-space: normal;">
                                        <asp:LinkButton ID="uoLblCCHeader" runat="server" CommandName="CostCenter" Text="Cost Center"
                                            Width="95px" />
                                    </th>
                                    
                                    <th style="text-align: center; white-space: normal;">
                                        <asp:LinkButton ID="uoLblRLHeader" runat="server" CommandName="RecordLocator"
                                            Text="Record Locator" Width="55px" />
                                    </th>
                                    <th style="text-align: center; white-space: normal;">
                                        <asp:LinkButton ID="uoLblDCityHeader" runat="server" CommandName="DeptCity"
                                            Text="Dept City" Width="45px" />
                                    </th>
                                    <th style="text-align: center; white-space: normal;">
                                        <asp:LinkButton ID="uoLblACityHeader" runat="server" CommandName="ArvlCity"
                                            Text="Arvl City" Width="43px" />
                                    </th>
                                    <th style="text-align: center; white-space: normal;">
                                        <asp:LinkButton ID="LinkButton1" runat="server" CommandName="DeptDate" Text="Dept Date"
                                            Width="60px" />
                                    </th>
                                    <th style="text-align: center; white-space: normal;">
                                        <asp:LinkButton ID="LinkButton2" runat="server" CommandName="ArvlDate" Text="Arvl Date"
                                            Width="60px" />
                                    </th>
                                    <th style="text-align: center; white-space: normal;">
                                        <asp:LinkButton ID="uoLblDTimeHeader" runat="server" CommandName="DeptTime"
                                            Text="Dept Time" Width="53px" />
                                    </th>
                                    <th style="text-align: center; white-space: normal;">
                                        <asp:LinkButton ID="uoLblATimeHeader" runat="server" CommandName="ArvlTime"
                                            Text="Arvl Time" Width="53px" />
                                    </th>
                                    <th style="text-align: center; white-space: normal;">
                                        <asp:LinkButton ID="uoLblCarrierHeader" runat="server" CommandName="Carrier"
                                            Text="Carrier" Width="35px" />
                                    </th>
                                    <th style="text-align: center; white-space: normal;">
                                        <asp:LinkButton ID="uoLblFlightNumHeader" runat="server" CommandName="FlightNo"
                                            Text="Flight No." Width="44px" />
                                    </th>
                                    <th style="text-align: center; white-space: normal;">
                                        <asp:LinkButton ID="uoLblVoucherHeader" runat="server" CommandName="Voucher"
                                            Text="Voucher" Width="44px" />
                                    </th>
                                    <th style="text-align: center; white-space: normal;">
                                        <asp:LinkButton ID="uoLblPNumHeader" runat="server" CommandName="PassportNo"
                                            Text="Passport No." Width="57px" />
                                    </th>
                                    <th style="text-align: center; white-space: normal;">
                                        <asp:LinkButton ID="uoLblIssuedHeader" runat="server" CommandName="PassportDateIssued"
                                            Text="Date Issued" Width="60px" />
                                    </th>                            
                                    <th style="text-align: center; white-space: normal;">
                                        <asp:LinkButton ID="uoLblExpHeader" runat="server" CommandName="PassportExpiration"
                                            Text="Passport Expiration" Width="59px" />
                                    </th>                                                                                         
                                    <th style="text-align: center; white-space: normal;">
                                        <asp:Label runat="server" ID="Label2" Text="" Width="25px"></asp:Label>
                                    </th>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
                    </asp:ListView>
                </div>
                <div id="Bv" style="overflow: auto; width: 100%; overflow-x: scroll; overflow-y: auto;"
                    onscroll="divScrollL();">
                    <asp:ListView runat="server" ID="uolistviewHotelInfo" >
                        <LayoutTemplate>
                            <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                                <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                            </table>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <%# HotelAddGroup()%>
                            <tr>   
                                 <td class="leftAligned" style="white-space: normal;">
                                    <asp:Label runat="server" ID="uoLblSfID" Text='<%# Eval("SeafarerIdInt")%>' Width="60px"></asp:Label>
                                </td>  
                                <td class="leftAligned" style="white-space: normal;">
                                    <asp:Label runat="server" ID="uoLabelLast" Text='<%# Eval("LastName")%>' Width="150px"></asp:Label>
                                </td>
                                <td class="leftAligned" style="white-space: normal;">
                                    <asp:LinkButton Width="150px" runat="server" ID="SeafarerLinkButton" PostBackUrl='<%# "../SuperUserView.aspx?sfId=" + Eval("SeafarerIdInt") + "&trID=" + Eval("TravelReqID") + "&st=" + Eval("Status") + "&recloc=" + Eval("RecordLocator") + "&manualReqID=0&p=" + Request.QueryString["p"]+ "&ufn=" + Request.QueryString["ufn"] + "&ID=" + Eval("IdBigint") + "&from=" + Request.QueryString["from"] + "&to=" + Request.QueryString["to"] + "&dt=" + Request.QueryString["dt"]%>'><%# Eval("FirstName") %></asp:LinkButton>
                                </td>
                                <td class="leftAligned" style="white-space: normal;">
                                    <asp:Label runat="server" ID="uoTextBoxHotelName" Text='<%# Eval("HotelName")%>' Width="100px"></asp:Label>
                                </td>     
                                <td class="leftAligned" style="white-space: normal;">
                                    <asp:Label runat="server" ID="uoTextBoxConfirmationNo" Text='<%# Eval("ConfirmationNo")%>' Width="100px"></asp:Label>
                                </td>                                                                                                         
                                <td class="leftAligned" style="white-space: normal;">
                                    <asp:Label runat="server" ID="uoLblCheckIn" Text='<%# String.Format("{0:dd-MMM-yyyy}",Eval("CheckIn"))%>'
                                        Width="70px"></asp:Label>
                                </td>
                                <td class="leftAligned" style="white-space: normal;">
                                    <asp:Label runat="server" ID="uoLblCheckOut" Text='<%# String.Format("{0:dd-MMM-yyyy}", Eval("CheckOut"))%>'
                                        Width="70px"></asp:Label>
                                </td>
                                <td class="leftAligned" style="white-space: normal;">
                                    <asp:Label runat="server" ID="uoLblNites" Text='<%# Eval("HotelNites")%>' Width="40px"></asp:Label>
                                </td>                           
                                                                                     
                                <td class="leftAligned" style="white-space: normal;">
                                    <asp:Label runat="server" ID="uoLblRoom" Text='<%# Eval("RoomType")%>' Width="70px"></asp:Label>
                                </td>
                                                            
                                <td class="leftAligned" style="white-space: normal;">
                                    <asp:Label runat="server" ID="uoLblVessel" Text='<%# Eval("VesselName")%>'
                                        Width="170px"></asp:Label>
                                </td>
                                <td class="leftAligned" style="white-space: normal;">
                                    <asp:Label runat="server" ID="uoLblRank" Text='<%# Eval("RankName")%>' Width="220px"></asp:Label>
                                </td>
                                <td class="leftAligned" style="white-space: normal;">
                                    <asp:Label runat="server" ID="Label1" Text='<%# Eval("CostCenter")%>' Width="100px"></asp:Label>
                                </td>
                                                            
                                <td class="leftAligned" style="white-space: normal;">
                                    <asp:Label runat="server" ID="uoLblRecLoc" Text='<%# Eval("RecordLocator")%>' Width="60px"></asp:Label>
                                </td>
                                <td class="leftAligned" style="white-space: normal;">
                                    <asp:Label runat="server" ID="uoLblDeptCity" Text='<%# Eval("DeptCity")%>' Width="50px"></asp:Label>
                                </td>
                                <td class="leftAligned" style="white-space: normal;">
                                    <asp:Label runat="server" ID="uoLblArvlCity" Text='<%# Eval("ArvlCity")%>' Width="50px"></asp:Label>
                                </td>
                                <td class="leftAligned" style="white-space: normal;">
                                    <asp:Label runat="server" ID="uoLblDepDate" Text='<%# String.Format("{0:dd-MMM-yyyy}", Eval("DeptDate"))%>'
                                        Width="67px"></asp:Label>
                                </td>
                                <td class="leftAligned" style="white-space: normal;">
                                    <asp:Label runat="server" ID="uoLblArDate" Text='<%# String.Format("{0:dd-MMM-yyyy}", Eval("ArvlDate"))%>'
                                        Width="67px"></asp:Label>
                                </td>
                                <td class="leftAligned" style="white-space: normal;">
                                    <asp:Label runat="server" ID="uoLblDepartureTime" Text='<%# Eval("DeptTime")%>' Width="58px"></asp:Label>
                                </td>
                                <td class="leftAligned" style="white-space: normal;">
                                    <asp:Label runat="server" ID="uoLblArrivalTime" Text='<%# Eval("ArvlTime")%>' Width="60px"></asp:Label>
                                </td>
                                <td class="leftAligned" style="white-space: normal;">
                                    <asp:Label runat="server" ID="uoLblCarrier" Text='<%# Eval("Carrier")%>' Width="40px"></asp:Label>
                                </td>
                                <td class="leftAligned" style="white-space: normal;">
                                    <asp:Label runat="server" ID="uoLblFN" Text='<%# Eval("FlightNo")%>' Width="50px"></asp:Label>
                                </td>
                                <td class="leftAligned" style="white-space: normal;">
                                    <asp:Label runat="server" ID="uoLblVoucher" Text='<%# String.Format("{0:0.00}", Eval("Voucher")) %>'
                                        Width="50px"></asp:Label>
                                </td>
                                <td class="leftAligned" style="white-space: normal;">
                                    <asp:Label runat="server" ID="uoLblPNum" Text='<%# Eval("PassportNo")%>' Width="65px"></asp:Label>
                                </td>
                                <td class="leftAligned" style="white-space: normal;">
                                    <asp:Label runat="server" ID="uoLblIssuedBy" Text='<%# Eval("PassportIssued")%>'
                                        Width="65px"></asp:Label>
                                </td>
                                <td class="leftAligned" style="white-space: normal;">
                                    <asp:Label runat="server" ID="uoLblExp" Text='<%# Eval("PassportExp")%>' Width="65px"></asp:Label>
                                </td>                                                                
                            </tr>
                        </ItemTemplate>
                        <EmptyDataTemplate>
                            <table class="listViewTable">
                                <tr>
                                    <td colspan="33" class="leftAligned">
                                        No Record
                                    </td>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
                    </asp:ListView>
                    <div align="left">
                        <asp:DataPager ID="uolistviewHotelInfoPager" runat="server" PagedControlID="uolistviewHotelInfo"
                            PageSize="20">
                            <Fields>
                                <asp:NumericPagerField ButtonType="link" NumericButtonCssClass="PagerClass" />
                            </Fields>
                        </asp:DataPager>
                    </div>
                </div>                
            </td>
            </tr>
        </table>
    </div>--%>
    </div>
    
    <asp:HiddenField ID="uoHiddenFieldUser" runat="server" />    
    <asp:HiddenField ID="uoHiddenFieldRole" runat="server" />    
    
    <asp:HiddenField ID="uoHiddenFieldDate" runat="server" />
    <asp:HiddenField ID="uoHiddenFieldOrder" runat="server" />
    <asp:HiddenField ID="uoHiddenFieldSortBy" runat="server" />
    <asp:HiddenField ID="uoHiddenFieldPortAgentID" runat="server" />
    <asp:HiddenField ID="uoHiddenFieldManifestStatus" runat="server" />
    <script type="text/javascript" language="javascript">
    
    
    
        Sys.Application.add_load(function() {
            SetDashboardResolution();
            HideDashboardLink();
        });

        $(document).ready(function() {            
            HideDashboardLink();
        });
        
        function pageLoad(sender, args) {
            var isAsyncPostback = Sys.WebForms.PageRequestManager.getInstance().get_isInAsyncPostBack();
            if (isAsyncPostback) {
                            
                HideDashboardLink();
                
            }
        } 
        
        function SetDashboardResolution() {
            var ht = $(window).height(); //550;
            var wd = $(window).width() * 0.90;
            var screenH = screen.height;
            var percent = 0.55;

            if (screenH <= 600) {
                //alert('less 600');
                ht = ht * 0.35;
            }
            else if (screenH <= 720) {
                //alert('less 720')
                ht = ht * 0.49;
            }
            else {
                if (screenH = 768) {
                    percent = (540 - (screenH - ht)) / ht;
                }
                ht = ht * percent;
            }
            $("#uoDivDashboard").height(ht);
            $("#uoDivDashboard").width(wd);
        }

        function HideDashboardLink() {
            if (document.getElementById("ctl00_NaviPlaceHolder_uoHiddenFieldRole").value != 'Hotel Specialist') {
                document.getElementById("ctl00_NaviPlaceHolder_uoHyperLinkDashboard").style.visibility = 'hidden';
            }
        }
        
//        function SetTMResolution() {
//            var ht = $(window).height();
//            var ht2 = $(window).height();

//            var wd = $(window).width() * 0.90;

//            if (screen.height <= 600) {
//                ht = ht * 0.22;
//                ht2 = ht2 * 0.46;
//                wd = $(window).width();
//            }
//            else if (screen.height <= 720) {
//                ht = ht * 0.28;
//                ht2 = ht2 * 0.59;
//            }
//            else {
//                ht = ht * 0.65;
//                ht2 = ht2 * 0.62;
//            }

//            $("#Bv").height(ht);
//            $("#Av").width(wd);
//            $("#Bv").width(wd);
//        }

//        function controlSettings() {
//            $(".clsSelectHotelManifest").fancybox(
//            {
//                'centerOnScroll': false,
//                'width': '35%',
//                'height': '100%',
//                'autoScale': false,
//                'transitionIn': 'fadeIn',
//                'transitionOut': 'fadeOut'
//            });
//        }
//        function divScrollL() {
//            var Right = document.getElementById('Av');
//            var Left = document.getElementById('Bv');
//            Right.scrollLeft = Left.scrollLeft;
//        }
    </script>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/TravelMartMaster2.Master" AutoEventWireup="true" ValidateRequest="false"
    CodeBehind="HotelExceptionBookingsReport.aspx.cs" Inherits="TRAVELMART.Hotel.HotelExceptionBookingsReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">        
</asp:Content>
<asp:Content ContentPlaceHolderID="HeaderContent" runat="server" ID="header">
    <%--<div class="PageTitle">
        Hotel Booking Exception List</div>--%>
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr class="PageTitle">
                <td align="left">
                   Hotel Booking Exception Dashboard
                </td>                        
                <td align="right">                
                Region: &nbsp;&nbsp
			     <asp:DropDownList ID="uoDropDownListRegion" runat="server" Width="200px" AutoPostBack="true" 
			         OnSelectedIndexChanged="uoDropDownListRegion_SelectedIndexChanged" >
                 </asp:DropDownList>
                 &nbsp;&nbsp;Port: &nbsp;&nbsp;
                <asp:DropDownList ID="uoDropDownListPortPerRegion" runat="server" Width="200px" AutoPostBack="true"
                    AppendDataBoundItems="true" OnSelectedIndexChanged ="uoDropDownListPortPerRegion_SelectedIndexChanged">
                </asp:DropDownList>
                </td>
            </tr>
        </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NaviPlaceHolder" runat="server">
    <div>    
      <table width="100%" cellpadding="0" cellspacing="0" style="width:100%; text-align:left">
            <tr>
                <td style="width: 5%">
                    Year:
                </td>
                <td style="width: 20%">
                    <asp:DropDownList ID="uoDropDownListYear" runat="server" Width="200px" >
                    </asp:DropDownList>
                </td>
                <td  style="width: 5%">
                    Month:
                </td>
                <td style="width: 20%">
                    <asp:DropDownList ID="uoDropDownListMonth" runat="server" Width="200px">
                    </asp:DropDownList>
                </td>
                <td>
                     <asp:Button runat="server" ID="uoButtonView" CssClass="SmallButton" Text="View"
                     Visible="true" onclick="uoButtonView_Click" /> &nbsp;&nbsp;
                    <asp:Button runat="server" ID="uoBtnExportList" CssClass="SmallButton" Text="Export Exception List"
                     Visible="true" onclick="uoBtnExportList_Click" />
                </td>
            </tr>    
            <tr>
                <td colspan="5">
                    <asp:Label runat="server" ID="uoLabelRed" CssClass="RedNotification"
                        Text="Exception list is based from selected month and future signon or signoff date of the year" >
                    </asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="5">
                <br/>
                <br/>
                     <div id="Av" style="overflow: auto; overflow-x: hidden; width: 100%; overflow-y: hidden;
                        position: relative;">
                        <asp:ListView runat="server" ID="ListView1" >
                            <LayoutTemplate>
                            </LayoutTemplate>
                            <ItemTemplate>
                            </ItemTemplate>
                            <EmptyDataTemplate>
                                <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                                    <tr>                        
                                        <th class="hideElement">
                                        </th>             
                                        <th>
                                             <asp:Label runat="server" ID="Label16" Text="Exception Remarks" Width="280px"> </asp:Label>                                            
                                        </th>
                                        <th>
                                           <asp:Label runat="server" ID="Label2" Text="January" Width="53px"> </asp:Label>  
                                        </th>
                                        <th>
                                            <asp:Label runat="server" ID="Label3" Text="February" Width="53px"> </asp:Label>  
                                        </th>
                                        <th>
                                            <asp:Label runat="server" ID="Label4" Text="March" Width="53px"> </asp:Label>  
                                        </th>
                                        <th>
                                            <asp:Label runat="server" ID="Label5" Text="April" Width="53px"> </asp:Label>  
                                        </th>
                                        <th>
                                            <asp:Label runat="server" ID="Label6" Text="May" Width="53px"> </asp:Label>  
                                        </th>
                                        <th>
                                            <asp:Label runat="server" ID="Label7" Text="June" Width="53px"> </asp:Label>  
                                        </th>
                                        <th>
                                            <asp:Label runat="server" ID="Label8" Text="July" Width="53px"> </asp:Label>  
                                        </th>
                                        <th>
                                            <asp:Label runat="server" ID="Label9" Text="August" Width="53px"> </asp:Label>  
                                        </th>
                                        <th>
                                            <asp:Label runat="server" ID="Label11" Text="September" Width="53px"> </asp:Label>  
                                        </th>
                                        <th>
                                            <asp:Label runat="server" ID="Label12" Text="October" Width="53px"> </asp:Label>  
                                        </th>
                                        <th>
                                            <asp:Label runat="server" ID="Label13" Text="November" Width="53px"> </asp:Label>  
                                        </th>
                                        <th>
                                            <asp:Label runat="server" ID="Label14" Text="December" Width="53px"> </asp:Label>  
                                        </th>
                                        <th>
                                            <asp:Label runat="server" ID="Label27" Text="Total" Width="53px"> </asp:Label>  
                                        </th>
                                    </tr>
                                </table>
                            </EmptyDataTemplate>
                        </asp:ListView>
                    </div>                                        
                    <div id="Bv" style="overflow: auto; width: 100%; overflow-x: auto; overflow-y: auto; 
                     position: relative;"  onscroll="divScrollL();">
                    <asp:ListView runat="server" ID="uoListViewDashboard" >
                        <LayoutTemplate>
                            <table border="0" cellpadding="0" cellspacing="0" class="listViewTable2" id="uoTblExceptionList">
                                <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                            </table>
                        </LayoutTemplate>
                        <ItemTemplate>     
                            <tr>
                                 <td class="hideElement">                               
                                    <asp:HiddenField ID="uoHiddenFieldExceptionID" runat="server" Value='<%# Eval("ExceptiopnID") %>'/>
                                 </td>
                                 <td class="leftAligned" style="white-space: normal;">                                    
                                    <asp:Label runat="server" ID="Label16" Text='<%# Eval("ExceptionRemarks")%>' Width="250px"> </asp:Label> 
                                 </td>
                                  <td class="rightAligned" style="white-space: normal;">
                                   <asp:Label runat="server" ID="Label1" Text='<%# Eval("January")%>' Width="48px"> </asp:Label> 
                                 </td>
                                 <td class="rightAligned" style="white-space: normal;">
                                    <asp:Label runat="server" ID="Label15" Text='<%# Eval("February")%>' Width="48px"> </asp:Label> 
                                 </td>
                                 <td class="rightAligned" style="white-space: normal;">
                                    <asp:Label runat="server" ID="Label17" Text='<%# Eval("March")%>' Width="48px"> </asp:Label> 
                                 </td>
                                 <td class="rightAligned" style="white-space: normal;">
                                    <asp:Label runat="server" ID="Label18" Text='<%# Eval("April")%>' Width="48px"> </asp:Label> 
                                 </td>
                                 <td class="rightAligned" style="white-space: normal;">
                                    <asp:Label runat="server" ID="Label19" Text='<%# Eval("May")%>' Width="48px"> </asp:Label> 
                                 </td>
                                 <td class="rightAligned" style="white-space: normal;">
                                    <asp:Label runat="server" ID="Label20" Text='<%# Eval("June")%>' Width="48px"> </asp:Label> 
                                 </td>
                                 <td class="rightAligned" style="white-space: normal;">
                                    <asp:Label runat="server" ID="Label21" Text='<%# Eval("July")%>' Width="48px"> </asp:Label> 
                                 </td>
                                 <td class="rightAligned" style="white-space: normal;">
                                    <asp:Label runat="server" ID="Label22" Text='<%# Eval("August")%>' Width="48px"> </asp:Label> 
                                 </td>
                                 <td class="rightAligned" style="white-space: normal;">
                                    <asp:Label runat="server" ID="Label23" Text='<%# Eval("September")%>' Width="48px"> </asp:Label> 
                                 </td>
                                 <td class="rightAligned" style="white-space: normal;">
                                    <asp:Label runat="server" ID="Label24" Text='<%# Eval("October")%>' Width="48px"> </asp:Label> 
                                 </td>
                                 <td class="rightAligned" style="white-space: normal;">
                                    <asp:Label runat="server" ID="Label25" Text='<%# Eval("November")%>' Width="48px"> </asp:Label> 
                                 </td>
                                 <td class="rightAligned" style="white-space: normal;">
                                    <asp:Label runat="server" ID="Label26" Text='<%# Eval("December")%>' Width="48px"> </asp:Label> 
                                 </td>
                                  <td class="rightAligned" style="white-space: normal;">
                                    <asp:Label runat="server" ID="Label28" Text='<%# Eval("Total")%>' Width="50px" Font-Bold="true"> </asp:Label> 
                                 </td>
                             </tr>
                        </ItemTemplate>
                        <EmptyDataTemplate>
                            <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                                <tr>
                                    <td colspan="20" class="leftAligned">
                                        <asp:Label runat="server" ID="Label10" Text="No Record" Width="1080px"> </asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
                    </asp:ListView>
                </div>                      
                </td>
            </tr>
        </table>        
    </div>
    
    <asp:HiddenField runat="server" ID="uoHiddenFieldUser" Value="" />
    <asp:HiddenField runat="server" ID="uoHiddenFieldStartDate" Value="" />   
</asp:Content>

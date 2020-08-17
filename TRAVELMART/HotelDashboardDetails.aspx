<%@ Page Title="" Language="C#" MasterPageFile="~/Popup.Master" AutoEventWireup="true" CodeBehind="HotelDashboardDetails.aspx.cs" Inherits="TRAVELMART.HotelDashboardDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="PageTitle">Hotel Dashboard Details</div>
    <br />
    <table width="100%">
        <tr>
            <td class="LeftClass" style="width:150px">
                &nbsp;Hotel Brand :
            </td>
            <td align="left">
                <%= VendorName %>
            </td>
        </tr>
        <tr>
            <td class="LeftClass" style="width:150px">
                &nbsp;Hotel Branch :
            </td>
            <td align="left">
                <%= BranchName %>
            </td>
        </tr>
        
        <tr><td colspan="2"></td></tr>
        <tr>
            <td colspan ="2" class="Module">
                <asp:ListView runat="server" ID="uoDashBoardListDetails">
                    <LayoutTemplate>                    
                        <table border="0" cellspacing="0" cellpadding="0" class="listViewTable">
                            <tr>
                                <th>Rec Loc</th>
                                <th>E1 ID</th>
                                <th>Status</th>
                                <th>Room</th>
                                <th>Name</th>
                                <th>Date From</th>
                                <th>Date To</th>                                
                                <th>Rank</th>
                                <th>Gender</th>
                                <th>Nationality</th>
                                <th>Cost Center</th>
                                <th>Duration</th>
                                <th>Hotel City</th>
                            </tr>
                            <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                        </table>
                     </LayoutTemplate>
                     <ItemTemplate>
                        <tr>
                            <td class="leftAligned">
                                <%# Eval("recloc") %>
                            </td>
                            <td class="leftAligned">
                                <%# Eval("e1Id") %>
                            </td>
                            <td class="leftAligned">
                                <%# Eval("colHotelStatusVarchar") %>
                            </td>
                            <td class="leftAligned">
                                <%# Eval("colRoomNameVarchar") %>
                            </td>
                            <td class="leftAligned">
                                <%# Eval("Name") %>
                            </td>
                            <td class="leftAligned">
                                <%# String.Format("{0:dd-MMM-yyyy}", Eval("cDate")) %>
                            </td>
                            <td class="leftAligned">
                                <%# String.Format("{0:dd-MMM-yyyy}", Eval("cEndDate")) %>
                            </td>
                            
                            <td class="leftAligned">
                                <%# Eval("colRankNameVarchar") %>
                            </td>
                            <td class="leftAligned">
                                <%# Eval("Gender") %>
                            </td>
                            <td class="leftAligned">
                                <%# Eval("Nationality") %>
                            </td>
                            <td class="leftAligned">
                                <%# Eval("colCostCenterNameVarchar") %>
                            </td>
                            <td class="leftAligned">
                                <%# Eval("colTimeSpanDurationInt") %>
                            </td>
                            <td class="leftAligned">
                                <%# Eval("colCityNameVarchar") %>
                            </td>
                        </tr>
                     </ItemTemplate>
                     <EmptyDataTemplate>
                        <table border="0" cellspacing="0" cellpadding="0" class="listViewTable">
                            <tr>
                                <th>Rec Loc</th>
                                <th>E1 ID</th>
                                <th>Status</th>
                                <th>Room</th>
                                <th>Name</th>
                                <th>Date From</th>
                                <th>Date To</th>
                                
                                <th>Rank</th>
                                <th>Gender</th>
                                <th>Nationality</th>
                                <th>Cost Center</th>
                                <th>Duration</th>
                                <th>Hotel City</th>
                            </tr>
                            <tr>
                                <td colspan ="12">No Records</td>
                            </tr>
                         </table>
                     </EmptyDataTemplate>
                </asp:ListView>
            </td>
        </tr>
    </table>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/Popup.Master" AutoEventWireup="true" CodeBehind="PortAgentContractHotelView.aspx.cs" Inherits="TRAVELMART.ContractManagement.PortAgentContractHotelView" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<table width="100%">
    <tr>
        <td class="PageTitle" colspan="2">Hotel Service</td>
    </tr>
    <tr><td colspan="2"><hr /></td></tr>
    <tr>
        <td class="LeftClass" style="width:150px;">&nbsp;Hotel Brand Name :</td>
        <td class="LeftClass">
            <asp:Label runat="server" ID="uoLabelBrand"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="LeftClass" style="width:150px;">&nbsp;Hotel Branch Name :</td>
        <td class="LeftClass">
           <asp:Label runat="server" ID="uoLabelBranch"></asp:Label>
        </td>
    </tr>
    <tr><td colspan="2"></td></tr>
    <tr>
        <td colspan="2" class="PageTitle">Hotel Service Details</td>
    </tr>
    <tr><td colspan="2"><hr /></td></tr>
    <tr>
        <td class="leftAligned" style="width:150px;">&nbsp;With Shuttle :</td>
        <td class="leftAligned">
            <asp:CheckBox runat="server" ID="uoCheckBoxWithShuttle" Enabled="false"  />
        </td>
    </tr>   
    <tr>
        <td class="leftAligned" style="width:150px;">&nbsp;Meal Rate :</td>
        <td class="leftAligned">
            <asp:Label runat="server" ID="uoLabelMealRate"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="leftAligned" style="width:150px;">&nbsp;Meal Rate Tax (%) :</td>
        <td class="leftAligned">
            <asp:Label runat="server" ID="uoLabelMealTax"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="leftAligned">&nbsp;Meal Rate Tax inclusive :</td>
        <td class="leftAligned">
            <asp:CheckBox runat="server" ID="uoCheckBoxTaxInclusive" Enabled="false" />
        </td>
    </tr>
    <tr>
        <td class="leftAligned">&nbsp;Meals included:</td>
        <td class="leftAligned">
            <asp:CheckBox runat="server" ID="uoCheckBoxBreakFast" Text="Breakfast" Enabled="false"
                 Font-Bold="true"/>
        </td>
    </tr>
    <tr>
        <td></td>
        <td class="leftAligned">
            <asp:CheckBox runat="server" ID="uoCheckBoxLunch" Text="Lunch"
                Enabled="false" Font-Bold="true"/>
        </td>
    </tr>
     <tr>
        <td></td>
        <td class="leftAligned">
            <asp:CheckBox runat="server" ID="uoCHeckBoxDinner" Text="Dinner"
                Enabled="false" Font-Bold="true"/>
        </td>
    </tr>
     <tr>
        <td></td>
        <td class="leftAligned">
            <asp:CheckBox runat="server" ID="uoCheckBoxLunchOrDinner" Text="Lunch or Dinner"
                Enabled="false"  Font-Bold="true"/>
        </td>
    </tr>
    <tr><td colspan="2"></td></tr>
    
    <tr><td colspan="2"></td></tr>
    <tr runat="server" id="uoTRRooms"><td colspan="2" class="PageTitle">Room List</td></tr>
    <tr>
        <td colspan="2" class="Module">
            <asp:ListView runat="server" ID="uoRoomsList">
                <LayoutTemplate>
                    <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                        <tr>
                            <th>Room Type</th>
                            <th>Date From</th>
                            <th>Date To</th>
                            <th>Room Rate</th>
                            <th>Room Tax Rate (%)</th>
                            <th>Currency</th>
                            <th>Room Rate Tax Inclusive</th>
                            <th>Mon</th>
                            <th>Tue</th>
                            <th>Wed</th>
                            <th>Thu</th>
                            <th>Fri</th>
                            <th>Sat</th>
                            <th>Sun</th>
                        </tr>
                        <asp:panel runat="server" ID="itemPlaceHolder"></asp:panel>
                    </table>
                </LayoutTemplate>
                <ItemTemplate>
                    <tr>
                        <td class="leftAligned"><%# Eval("colRoomNameVarchar") %></td>
                        <td class="leftAligned"><%# String.Format("{0:dd-MMM-yyyy}", Eval("colDateFromDatetime")) %></td>
                        <td class="leftAligned"><%# String.Format("{0:dd-MMM-yyyy}", Eval("colDateToDatetime")) %></td>
                        <td class="leftAligned"><%# Eval("colServiceRateMoney") %></td>
                        <td class="leftAligned"><%# Eval("colServiceRateTaxDecimal") %></td>
                        <td class="leftAligned"><%# Eval("colCurrencyNameVarchar") %></td>
                        <td class="leftAligned"><%# Eval("colServiceRateTaxInclusiveBit") %></td>
                        <td class="leftAligned"><%# Eval("colMondayCountInt") %></td>
                        <td class="leftAligned"><%# Eval("colTuesdayCountInt")%></td>
                        <td class="leftAligned"><%# Eval("colWednesdayCountInt")%></td>
                        <td class="leftAligned"><%# Eval("colThursdayCountInt")%></td>
                        <td class="leftAligned"><%# Eval("colFridayCountInt")%></td>
                        <td class="leftAligned"><%# Eval("colSaturdayCountInt")%></td>
                        <td class="leftAligned"><%# Eval("colSundayCountInt")%></td>
                        
                    </tr>
                </ItemTemplate>
            </asp:ListView>
        </td>
    </tr>
   
</table>

</asp:Content>

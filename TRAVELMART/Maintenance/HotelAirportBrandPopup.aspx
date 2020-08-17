<%@ Page Title="" Language="C#" MasterPageFile="~/Popup.Master" AutoEventWireup="true" CodeBehind="HotelAirportBrandPopup.aspx.cs" 
Inherits="TRAVELMART.Maintenance.HotelAirportBrandPopup" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script type="text/javascript" src="../FBox/jquery-1.6.1.min.js"></script> 
    <style type="text/css">
        .style1
        {
            width: 399px;
            vertical-align: top;
        }
        .style3
        {
            width: 143px;
            vertical-align: top;
        }
        .style4
        {
            width: 107px;
            vertical-align: middle;
        }
        .style5
        {
            width: 100px;
            vertical-align: top;
            text-align:center;
        }
        .style6
        {
            width: 107px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="PageTitle">
                    Hotel Branch: Airport-Brand Assignment
                </div>
    <hr/>     
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td class="style3"> Hotel:</td>        
            <td colspan="3">
                <%--<asp:DropDownList runat="server" ID="uoDropDownListPortAgent" AppendDataBoundItems="true" Width="300px"></asp:DropDownList>--%>
                <asp:TextBox runat="server" ID="uoTextBoxHotelName" Text="Hotel Name" Font-Bold="true" CssClass="ReadOnly" 
                ReadOnly="true" Width="300px"></asp:TextBox>
            </td>
            <%--<td class="style5"></td>
            <td class="style4"> &nbsp;</td>
            <td class="style1">
                &nbsp;</td>--%>
        </tr>
        <tr>
            <td class="style3">Brand:</td>
            <td  colspan="3">
                <asp:CheckBoxList runat="server" ID="uoCheckBoxListBrand" 
                    RepeatDirection="Horizontal"></asp:CheckBoxList>
            <%--</td>
            <td class="style5"></td>
            <td class="style6">
                &nbsp;</td>
            <td>              
            </td>--%> 
        </tr>
        <tr>
            <td colspan="4">
                <br />
            </td>
        </tr>
        <tr>
            <td>Search Airport: </td>
            <td>
                <asp:TextBox ID="uoTextBoxAirport" runat="server" Width="200px" CssClass="SmallText"></asp:TextBox>
                <asp:Button ID="uoButtonSearchAirport" runat="server" Text="Search Airport" CssClass="SmallButton" 
                    onclick="uoButtonSearchAirport_Click" />
            </td>
            <td>                
            </td>
            <td></td>            
        </tr>
        <tr>
            <td class="style3">Airport To be Added:</td>
            <td class="style1">
                <asp:ListView ID="uoListViewAirportNotAssigned" runat="server">
                     <LayoutTemplate>
                        <table border="0" cellpadding="0" cellspacing="0" class="listViewTable2" width="500px">
                            <tr>
                                <th>
                                    
                                </th>
                                <th>
                                    Airport
                                </th>
                            </tr>
                            <asp:PlaceHolder runat="server" ID="itemPlaceHolder"></asp:PlaceHolder>
                        </table>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <tr>
                            <td class="leftAligned">
                                <asp:CheckBox ID="uoCheckBoxSelect" runat="server" />
                            </td>
                            <td class="leftAligned">
                                <asp:HiddenField ID="uoHiddenFieldAirport" runat="server" Value='<%# Eval("AirportIDString") %>' />
                                <asp:Label ID="uoLabelAirport" runat="server" Text='<%# Eval("AirportNameString")%>' />
                            </td>
                        </tr>
                    </ItemTemplate>
                    <EmptyDataTemplate>
                        <table border="0" cellpadding="0" cellspacing="0" class="listViewTable" width="500px">
                            <tr>
                                <th>
                                    Airport
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
            <td class="style5">
                 <br />                                  
                 <br />
                 <br />
                 <asp:Button ID="uoButtonAdd" runat="server" CssClass="SmallButton" Text="Add >>"
                              ValidationGroup="Airport" Width="65px" OnClick="uoButtonAdd_Click" />                
                <br />
                    <asp:Button ID="uoButtonRemove" runat="server" CssClass="SmallButton" Text="<< Remove"
                    ValidationGroup="Airport" Width="65px" OnClick="uoButtonRemove_Click" />                        
            </td>
            <td class="style3">Assigned Airport:</td>
            <td class="style1">
                <asp:ListView ID="uoListViewAirportAssigned" runat="server">
                     <LayoutTemplate>
                        <table border="0" cellpadding="0" cellspacing="0" class="listViewTable2" width="500px">
                            <tr>
                                <th>
                                    
                                </th>
                                <th>
                                    Airport
                                </th>
                            </tr>
                            <asp:PlaceHolder runat="server" ID="itemPlaceHolder"></asp:PlaceHolder>
                        </table>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <tr>
                            <td class="leftAligned">
                                <asp:CheckBox ID="uoCheckBoxSelect" runat="server" />
                            </td>
                            <td class="leftAligned">
                                <asp:HiddenField ID="uoHiddenFieldAirport" runat="server" Value='<%# Eval("AirportIDString") %>' />
                                <asp:Label ID="uoLabelAirport" runat="server" Text='<%# Eval("AirportNameString")%>' />
                            </td>
                        </tr>
                    </ItemTemplate>
                    <EmptyDataTemplate>
                        <table border="0" cellpadding="0" cellspacing="0" class="listViewTable" width="500px">
                            <tr>
                                <th>
                                    Airport
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
                <br />
                 <asp:Button ID="uoButtonSave" runat="server" Text="Save" CssClass="SmallButton" 
                    Width="100px" onclick="uoButtonSave_Click" />
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="uoHiddenFieldHotelID" Value="0" runat="server" />
    <asp:HiddenField ID="uoHiddenFieldUser" runat="server" Value="" />
</asp:Content>

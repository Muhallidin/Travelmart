<%@ Page Title="" Language="C#" MasterPageFile="~/Popup.Master" AutoEventWireup="true" CodeBehind="UserVehicle.aspx.cs" Inherits="TRAVELMART.UserVehicle" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="PageTitle">
             User Vehicle Vendor
    </div>
    <hr/>  
    
    <table class="LeftClass" >
        <tr>
            <td class="contentCaption">Search Vehicle Vendor:</td>
            <td class="contentValue" style="white-space:nowrap">
                 <asp:TextBox ID="uoTextBoxSearch" runat="server" Width="300px" CssClass="TextBoxInput"></asp:TextBox>
            </td>
        </tr>       
        <tr>
            <td></td>
            <td>
                <asp:Button ID="uoButtonSearch" runat="server" Text="Search" CssClass="SmallButton" onclick="uoButtonSearch_Click"/>
            </td>
        </tr>
        <tr>
            <td style="vertical-align: top" colspan="2" style="width: 400px">
                <asp:ListView runat="server" ID="uoListViewVehicle">
                    <LayoutTemplate>
                        <table border="0" cellpadding="0" cellspacing="0" class="listViewTable2" width="500px">
                            <tr>
                                <th>
                                    
                                </th>
                                <th>
                                    Vehicle Vendor
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
                                <asp:HiddenField ID="uoHiddenFieldVehicleVendor" runat="server" Value='<%# Eval("VehicleID")%>' />
                                <asp:Label ID="uoLabelVehicle" runat="server" Text='<%# Eval("VehicleName")%>' />
                            </td>
                        </tr>
                    </ItemTemplate>
                    <EmptyDataTemplate>
                        <table border="0" cellpadding="0" cellspacing="0" class="listViewTable" width="500px">
                            <tr>
                                <th>
                                    Vehicle Vendor
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
           <td style="vertical-align: top"  align="center">
                <table>
                    <tr>
                        <td>
                            <asp:Button ID="uoButtonAdd" runat="server" CssClass="SmallButton" Text="Add >>"
                                ValidationGroup="Seaport" Width="70px" OnClick="uoButtonAdd_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="uoButtonDelete" runat="server" CssClass="SmallButton" Text="<< Remove"
                                ValidationGroup="Seaport" Width="70px" OnClick="uoButtonDelete_Click" />
                        </td>
                    </tr>
                </table>
            </td>
            <td style="width: 400px; vertical-align:top">
                   <asp:ListView runat="server" ID="uoListViewVehicleSaved">
                    <LayoutTemplate>
                        <table border="0" cellpadding="0" cellspacing="0" class="listViewTable2" width="500px">
                            <tr>
                                <th>
                                    
                                </th>
                                <th>
                                    Vehicle Vendor
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
                                <asp:HiddenField ID="uoHiddenFieldVehicleVendor" runat="server" Value='<%# Eval("VehicleID") %>' />
                                <asp:Label ID="uoLabelVehicle" runat="server" Text='<%# Eval("VehicleName")%>' />
                            </td>
                        </tr>
                    </ItemTemplate>
                    <EmptyDataTemplate>
                        <table border="0" cellpadding="0" cellspacing="0" class="listViewTable" width="500px">
                            <tr>
                                <th>
                                    Vehicle Vendor
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
                <br/>
                <asp:Button ID="uoButtonSave" runat="server" Text="Save" 
                       onclick="uoButtonSave_Click" />
            </td>
        </tr>
    </table>
     <asp:HiddenField ID="uoHiddenFieldUser" runat="server" Value=""/> 
     <asp:HiddenField ID="uoHiddenFieldUserName" runat="server" Value=""/>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>

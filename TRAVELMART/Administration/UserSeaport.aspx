<%@ Page Title="" Language="C#" MasterPageFile="~/Popup.Master" AutoEventWireup="true" CodeBehind="UserSeaport.aspx.cs" Inherits="TRAVELMART.UserSeaport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="PageTitle">
             User Seaport       
    </div>
    <hr/>  
    
    <table class="LeftClass" >
        <tr>
            <td class="contentCaption">Search Seaport By:</td>
            <td class="contentValue" style="white-space:nowrap">
                <asp:DropDownList ID="uoDropDownListSearchType" runat="server" Width="302px" CssClass="TextBoxInput">
                    <asp:ListItem>Name</asp:ListItem>
                    <asp:ListItem>Code</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td></td>
            <td>
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
                <asp:ListView runat="server" ID="uoListViewSeaport">
                    <LayoutTemplate>
                        <table border="0" cellpadding="0" cellspacing="0" class="listViewTable2" width="500px">
                            <tr>
                                <th>
                                    
                                </th>
                                <th>
                                    Seaport
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
                                <asp:HiddenField ID="uoHiddenFieldSeaport" runat="server" Value='<%# Eval("SeaportID") %>' />
                                <asp:Label ID="uoLabelSeaport" runat="server" Text='<%# Eval("SeaportName")%>' />
                            </td>
                        </tr>
                    </ItemTemplate>
                    <EmptyDataTemplate>
                        <table border="0" cellpadding="0" cellspacing="0" class="listViewTable" width="500px">
                            <tr>
                                <th>
                                    Seaport
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
                                ValidationGroup="Seaport" Width="65px" OnClick="uoButtonAdd_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="uoButtonDelete" runat="server" CssClass="SmallButton" Text="<< Remove"
                                ValidationGroup="Seaport" Width="65px" OnClick="uoButtonDelete_Click" />
                        </td>
                    </tr>
                </table>
            </td>
            <td style="width: 400px; vertical-align:top">
                   <asp:ListView runat="server" ID="uoListViewSeaportSaved">
                    <LayoutTemplate>
                        <table border="0" cellpadding="0" cellspacing="0" class="listViewTable2" width="500px">
                            <tr>
                                <th>
                                    
                                </th>
                                <th>
                                    Seaport
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
                                <asp:HiddenField ID="uoHiddenFieldSeaport" runat="server" Value='<%# Eval("SeaportID") %>' />
                                <asp:Label ID="uoLabelSeaport" runat="server" Text='<%# Eval("SeaportName")%>' />
                            </td>
                        </tr>
                    </ItemTemplate>
                    <EmptyDataTemplate>
                        <table border="0" cellpadding="0" cellspacing="0" class="listViewTable" width="500px">
                            <tr>
                                <th>
                                    Seaport
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

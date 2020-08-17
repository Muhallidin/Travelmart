<%@ Page Title="" Language="C#" MasterPageFile="~/Popup.Master" AutoEventWireup="true" CodeBehind="CrewAdminTranspo.aspx.cs" 
Inherits="TRAVELMART.CrewAdminTranspo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script type="text/javascript" src="../FBox/jquery-1.6.1.min.js"></script> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="PageTitle">
             Add/Cancel Transportation
    </div>
    <hr/> 
    <div>
        <asp:Button ID="uoButtonReturn" runat="server" Text="Return to Exception List" Visible="false"
            CssClass="SmallButton" onclick="uoButtonReturn_Click" />
    </div>
    <div>
        <table width="100%">
            <tr>
                <td>
                    Vehicle Vendor:
                </td>
                <td>
                    <asp:TextBox runat="server" ID="uoTextBoxVehicleVendor" Width="300px" CssClass="ReadOnly" ReadOnly="true" ></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Email To:
                </td>
                <td>
                    <asp:TextBox runat="server" ID="uoTextBoxEmailAdd" Width="300px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Email Cc:
                </td>
                <td>
                    <asp:TextBox runat="server" ID="uoTextBoxCopy" Width="300px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                   
                </td>
                <td>
                    <asp:Button runat="server" ID="uoButtonEmail" Text="Email" Width="50px" 
                        CssClass="SmallButton" onclick="uoButtonEmail_Click"/>
                </td>
            </tr>
        </table>
    </div>
    <div id="Div2" class="PageSubTitle" style="text-decoration: underline;">
        New Added Transportation Request
    </div>
    <div id="Av" style="overflow: auto; overflow-x: hidden; width: 100%; overflow-y: hidden;
        position: relative;">
        <asp:ListView runat="server" ID="ListView1">
            <LayoutTemplate>
            </LayoutTemplate>
            <ItemTemplate>
            </ItemTemplate>
            <EmptyDataTemplate>
                <table border="0" cellpadding="0" cellspacing="0" class="listViewTable" width="100%">
                    <tr>
                        <th class="hideElement">
                            requestInfo
                        </th>
                        <th>
                            <asp:CheckBox runat="server" ID="CheckBox1" Width="20px" CssClass="Checkbox" onclick="SetSettings(this);" />
                        </th> 
                        <th style="text-align: center;">
                            <asp:Label runat="server" ID="Label13" Text="E1 ID" Width="55px"> </asp:Label>
                        </th>
                        <th style="text-align: center;">
                            <asp:Label runat="server" ID="uoLablE1Hdr" Text="Last Name" Width="112px"> </asp:Label>
                        </th>
                        <th style="text-align: center;">
                            <asp:Label runat="server" ID="uoLablRecLocHdr" Text="First Name" Width="112px"> </asp:Label>
                        </th>
                        <th style="text-align: center;">
                            <asp:Label runat="server" ID="uoLablEmpIdHdr" Text="Route From" Width="70px"> </asp:Label>
                        </th>
                        <th style="text-align: center;">
                            <asp:Label runat="server" ID="uoLablNameHdr" Text="Route To" Width="70px"> </asp:Label>
                        </th>
                        <th style="text-align: center;">
                            <asp:Label runat="server" ID="Label1" Text="Rec Loc" Width="60px"> </asp:Label>
                        </th>
                        <th style="text-align: center;">
                            <asp:Label runat="server" ID="uoLablStatHdr" Text="Flight No." Width="55px"> </asp:Label>
                        </th>
                        <th style="text-align: center;">
                            <asp:Label runat="server" ID="uoLablOnOffHdr" Text="Dep" Width="55px"> </asp:Label>
                        </th>
                        <th style="text-align: center;">
                            <asp:Label runat="server" ID="uoLablGenderHdr" Text="Arr" Width="55px"> </asp:Label>
                        </th>
                        <th style="text-align: center;">
                            <asp:Label runat="server" ID="uoLablRankHdr" Text="Dep Date" Width="60px"> </asp:Label>
                        </th>
                        <th style="text-align: center;">
                            <asp:Label runat="server" ID="uoLablStripeHdr" Text="Arr Date" Width="55px"> </asp:Label>
                        </th>                        
                        <th>
                            <asp:Label runat="server" ID="Label11" Text="" Width="10px"> </asp:Label>
                        </th>
                    </tr>
                </table>
            </EmptyDataTemplate>
        </asp:ListView>
    </div>
    
    <div id="Bv" style="overflow: auto; width: 100%; overflow-x: auto; overflow-y: auto;
        position: relative;" onscroll="divScrollL();">
        <asp:ListView runat="server" ID="uoListViewAdded">
            <LayoutTemplate>
                <table border="0" cellpadding="0" cellspacing="0" class="listViewTable2" id="uoTblExceptionList" width="100%">
                    <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                </table>
            </LayoutTemplate>
            <ItemTemplate>
                <%# OverflowChangeRowColor()%>
                <td class="hideElement">                                
                    <asp:HiddenField runat="server" ID="hfIdBigint" Value='<%# Eval("IDBigint") %>' />
                    <asp:HiddenField runat="server" ID="hfTransID" Value='<%# Eval("TransID") %>' />                                        
                </td>
                <td style="white-space: normal;">
                    <asp:CheckBox CssClass="Checkbox" ID="uoSelectCheckBox" Width="24px" runat="server" />
                </td>
                 <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="Label14" Text='<%# Eval("E1ID") %>' Width="50px"> </asp:Label>
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="Label16" Text='<%# Eval("LastName") %>' Width="100px"> </asp:Label>
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="uoLblE1TrId" Text='<%# Eval("FirstName") %>' Width="100px"> </asp:Label>
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="uoLblRouteFr" Text='<%# Eval("RouteFrom") %>' Width="60px"></asp:Label>
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="uoLblRouteTo" Width="60px" Text='<%# Eval("RouteTo")%>'></asp:Label>
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="Label2" Width="50px" Text='<%# Eval("RecLoc") %>'></asp:Label>                  
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="uoLblSeafarerName" Width="50px" Text='<%# Eval("FlightNo") %>'></asp:Label>                  
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="uoLblStatus" Width="50px" Text='<%# Eval("Departure") %>'></asp:Label>
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="Label8" Width="50px" Text='<%# Eval("Arrival") %>'></asp:Label>
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="Label12" Width="50px" Text='<%# Eval("DepartureDate") %>'></asp:Label>
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="Label18" Width="50px" Text='<%# Eval("ArrivalDate") %>'></asp:Label>
                </td>
                </tr>
            </ItemTemplate>
            <EmptyDataTemplate>
                <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                    <tr>
                        <td colspan="20" class="leftAligned">
                            <asp:Label runat="server" ID="Label10" Text="No Record" > </asp:Label>
                        </td>
                    </tr>
                </table>
            </EmptyDataTemplate>
        </asp:ListView>
        <asp:HiddenField ID="uoHiddenFieldUser" runat="server" />
    </div>
    
    <br />
   <div id="Div1" class="PageSubTitle" style="text-decoration: underline;">
        Cancelled Transportation
    </div>
    
    <%--Cancelled Start--%>
    <div id="AvCancel" style="overflow: auto; overflow-x: hidden; width: 100%; overflow-y: hidden;
        position: relative;">
        <asp:ListView runat="server" ID="ListView2">
            <LayoutTemplate>
            </LayoutTemplate>
            <ItemTemplate>
            </ItemTemplate>
            <EmptyDataTemplate>
                <table border="0" cellpadding="0" cellspacing="0" class="listViewTable" width="100%">
                    <tr>
                        <th class="hideElement">
                            requestInfo
                        </th>
                        <th>
                            <asp:CheckBox runat="server" ID="CheckBox1" Width="20px" CssClass="Checkbox" onclick="SetSettings(this);" />
                        </th> 
                        <th style="text-align: center;">
                            <asp:Label runat="server" ID="Label13" Text="E1 ID" Width="55px"> </asp:Label>
                        </th>
                        <th style="text-align: center;">
                            <asp:Label runat="server" ID="uoLablE1Hdr" Text="Last Name" Width="112px"> </asp:Label>
                        </th>
                        <th style="text-align: center;">
                            <asp:Label runat="server" ID="uoLablRecLocHdr" Text="First Name" Width="112px"> </asp:Label>
                        </th>
                        <th style="text-align: center;">
                            <asp:Label runat="server" ID="uoLablEmpIdHdr" Text="Route From" Width="70px"> </asp:Label>
                        </th>
                        <th style="text-align: center;">
                            <asp:Label runat="server" ID="uoLablNameHdr" Text="Route To" Width="70px"> </asp:Label>
                        </th>
                        <th style="text-align: center;">
                            <asp:Label runat="server" ID="Label1" Text="Rec Loc" Width="60px"> </asp:Label>
                        </th>
                        <th style="text-align: center;">
                            <asp:Label runat="server" ID="uoLablStatHdr" Text="Flight No." Width="55px"> </asp:Label>
                        </th>
                        <th style="text-align: center;">
                            <asp:Label runat="server" ID="uoLablOnOffHdr" Text="Dep" Width="55px"> </asp:Label>
                        </th>
                        <th style="text-align: center;">
                            <asp:Label runat="server" ID="uoLablGenderHdr" Text="Arr" Width="55px"> </asp:Label>
                        </th>
                        <th style="text-align: center;">
                            <asp:Label runat="server" ID="uoLablRankHdr" Text="Dep Date" Width="60px"> </asp:Label>
                        </th>
                        <th style="text-align: center;">
                            <asp:Label runat="server" ID="uoLablStripeHdr" Text="Arr Date" Width="55px"> </asp:Label>
                        </th>                        
                        <th>
                            <asp:Label runat="server" ID="Label11" Text="" Width="10px"> </asp:Label>
                        </th>
                    </tr>
                </table>
            </EmptyDataTemplate>
        </asp:ListView>
    </div>
    <div id="BvCancel" style="overflow: auto; width: 100%; overflow-x: auto; overflow-y: auto;
        position: relative;" onscroll="divScrollL();">
        <asp:ListView runat="server" ID="uoListViewCancel">
            <LayoutTemplate>
                <table border="0" cellpadding="0" cellspacing="0" class="listViewTable2" id="uoTblExceptionList" width="100%">
                    <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                </table>
            </LayoutTemplate>
            <ItemTemplate>
                <%# OverflowChangeRowColor()%>
                <td class="hideElement">                                
                    <asp:HiddenField runat="server" ID="hfIdBigint" Value='<%# Eval("IDBigint") %>' />
                    <asp:HiddenField runat="server" ID="hfTransID" Value='<%# Eval("TransID") %>' />                                        
                </td>
                <td style="white-space: normal;">
                    <asp:CheckBox CssClass="Checkbox" ID="uoSelectCheckBox" Width="24px" runat="server" />
                </td>
                 <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="Label14" Text='<%# Eval("E1ID") %>' Width="50px"> </asp:Label>
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="Label16" Text='<%# Eval("LastName") %>' Width="100px"> </asp:Label>
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="uoLblE1TrId" Text='<%# Eval("FirstName") %>' Width="100px"> </asp:Label>
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="uoLblRouteFr" Text='<%# Eval("RouteFrom") %>' Width="60px"></asp:Label>
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="uoLblRouteTo" Width="60px" Text='<%# Eval("RouteTo")%>'></asp:Label>
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="Label2" Width="50px" Text='<%# Eval("RecLoc") %>'></asp:Label>                  
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="uoLblSeafarerName" Width="50px" Text='<%# Eval("FlightNo") %>'></asp:Label>                  
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="uoLblStatus" Width="50px" Text='<%# Eval("Departure") %>'></asp:Label>
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="Label8" Width="50px" Text='<%# Eval("Arrival") %>'></asp:Label>
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="Label12" Width="50px" Text='<%# Eval("DepartureDate") %>'></asp:Label>
                </td>
                <td class="leftAligned" style="white-space: normal;">
                    <asp:Label runat="server" ID="Label18" Width="50px" Text='<%# Eval("ArrivalDate") %>'></asp:Label>
                </td>
                </tr>
            </ItemTemplate>
            <EmptyDataTemplate>
                <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                    <tr>
                        <td colspan="20" class="leftAligned">
                            <asp:Label runat="server" ID="Label10" Text="No Record" > </asp:Label>
                        </td>
                    </tr>
                </table>
            </EmptyDataTemplate>
        </asp:ListView>
        <asp:HiddenField ID="HiddenField1" runat="server" />
    </div>
    <%--Cancelled End--%>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>

<%@ Page Language="C#" MasterPageFile="~/Popup.Master" AutoEventWireup="true"
        CodeBehind="VehicleContractView.aspx.cs" Inherits="TRAVELMART.ContractManagement.VehicleContractView" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src="../FBox/jquery-1.6.1.min.js"></script>
    <script type="text/javascript" src="../FBox/fancybox/jquery.mousewheel-3.0.4.pack.js"></script>
    <script type="text/javascript" src="../FBox/fancybox/jquery.fancybox-1.3.4.pack.js"></script>
    <script src="../Menu/menu.js" type="text/javascript"></script>  
    
    <style type="text/css">
        .style2
        {
            width: 33px;
        }
        .style4
        {
            width: 250px;
            white-space:nowrap;
        }
        .style6
        {
            width: 147px;
            height: 30px;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <div class="leftAligned">
        <div class="PageTitle">
            Vehicle Contract
        </div>
        <hr />
        <table width="100%" style="text-align: left">
           
            <tr>
                <td colspan="4">
                    &nbsp;Contract Status : &nbsp;<asp:Label ID="ucLabelContractStatus" runat="server" 
                        Font-Size="14pt" ForeColor="Red" Font-Bold="true"></asp:Label>
                </td>
            </tr>
            <tr>
                <td >
                    &nbsp;Contract Attachment: &nbsp;
                    <%--<asp:LinkButton ID="LinkButton1" runat="server" onclick="LinkButton1_Click">View Attachment</asp:LinkButton>--%>
                </td>
                <td colspan="3">
                    <asp:ListView runat="server" ID="uoListViewAttachment">
                            <LayoutTemplate>
                                <table border="0" cellpadding="0" cellspacing="0" class="listViewTable" >
                                    <tr>
                                        <th style="text-align: center; white-space: normal;">
                                            Filename
                                        </th>
                                    </tr>
                                    <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                                </table>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td class="leftAligned">                                       
                                        <asp:LinkButton runat="server" ID="uoLinkButtonFileName" Text='<%# Eval("FileName") %>'
                                            OnClick="ucLabelAttached_Click" CommandArgument='<%# Eval("FileName") %>'></asp:LinkButton>
                                    </td>                                    
                                </tr>
                            </ItemTemplate>
                            <EmptyDataTemplate>
                                <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                                    <tr>
                                        <th style="text-align: center; white-space: normal;">
                                            Filename
                                        </th>
                                    </tr>
                                    <tr>
                                        <td class="leftAligned">
                                            No Record
                                        </td>
                                    </tr>
                                </table>
                            </EmptyDataTemplate>
                        </asp:ListView>
                </td>                
            </tr>
            <tr>
                <td class="style4">
                   &nbsp;Vehicle Branch Name :
                </td>
                <td colspan="2">
                    <asp:TextBox ID="uoTextBoxVehicleName" runat="server" Width="300px" ReadOnly="true" CssClass="ReadOnly"></asp:TextBox>
                </td>
            </tr> 
            <tr>
                <td>
                    &nbsp;Country:
                </td>
                <td>
                    <asp:TextBox ID="uoTextBoxCountry" runat="server" Width="300px" ReadOnly="true" CssClass="ReadOnly"></asp:TextBox>
                </td>
                <td>
                    &nbsp;City:
                </td>
                <td>
                    <asp:TextBox ID="uoTextBoxCity" runat="server" Width="300px" ReadOnly="true" CssClass="ReadOnly"></asp:TextBox>
                </td>
            </tr>           
            <tr>
                <td colspan="4">
                </td>
            </tr>
            <tr>
                <td class="style4">
                    &nbsp;Contract Title :
                </td>
                <td colspan="2">
                    <asp:TextBox ID="uoTextBoxContractTitle" runat="server" Width="300px" ReadOnly="true" CssClass="ReadOnly" />                            
                </td>
            </tr>
            <tr>
                <td class="style4">
                    &nbsp;Effective Date :
                </td>
                <td class="style2">
                    <asp:TextBox ID="uoTextBoxContractStartDate" runat="server" ReadOnly="true" CssClass="ReadOnly" width="300px"></asp:TextBox>                                                                                      
                </td>
                <td class="style4">
                    &nbsp;Expiration Date :
                </td>
                <td>
                    <asp:TextBox ID="uoTextBoxContractEndDate" runat="server" ReadOnly="true" CssClass="ReadOnly" Width="300px"></asp:TextBox>                                                      
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;Currency:
                </td>
                <td>
                     <asp:TextBox ID="uoTextBoxCurrency" runat="server" ReadOnly="true" CssClass="ReadOnly" Width="300px"></asp:TextBox>
                </td>
                <td></td>
                <td class="style4" >
               <asp:CheckBox runat="server" Enabled="False" ID="uoCheckBoxAirportToHotel" Text="Airport To Hotel"/>
               &nbsp;
               &nbsp;
                <asp:CheckBox runat="server" Enabled="False" ID="uoCheckBoxHotelToShip" Text="Hotel To Ship"/>
                </td>                
            </tr>
            <tr>
                <td colspan="4">
                </td>
            </tr>
            <tr>
                <td class="style4">
                    &nbsp;Remarks :
                </td>
                <td >
                    <asp:TextBox ID="uoTextBoxRemarks" runat="server" Width="300px" TextMode="MultiLine" ReadOnly="true" CssClass="ReadOnly" />
                </td>
                <td >                    
                    Brand:
                </td>
                <td >
                    <asp:CheckBoxList ID="uoCheckBoxListBrand" runat="server" Enabled="false"
                        RepeatDirection="Horizontal">
                        <asp:ListItem Value="1">RCI</asp:ListItem>
                        <asp:ListItem Value="2">AZA</asp:ListItem>
                        <asp:ListItem Value="3">CEL</asp:ListItem>
                        <asp:ListItem Value="4">PUL</asp:ListItem>
                        <asp:ListItem Value="5">SKS</asp:ListItem>                        
                    </asp:CheckBoxList>
                </td>
            </tr>            
            <tr>
                <td class="style6">
                    <asp:HiddenField ID="uoHiddenFieldContractId" runat="server" />
                </td>   
                <td class="style6">
                    <asp:HiddenField ID="uoHiddenFieldBranchId" runat="server" />
                </td>              
            </tr>            
        </table>
        <div class="PageTitle">            
               Assigned Airport & Seaport
        </div>
        <table style="width:100%">
            <tr>
                <td valign="top" align="left" style="width: 50%">
                    <asp:ListView runat="server" ID="uoListViewAirport" >
                        <LayoutTemplate>
                            <table border="0" cellpadding="0" cellspacing="0" class="listViewTable" width="90%">
                                <tr>
                                    <th style="text-align: center; white-space: normal;">
                                        Airport
                                    </th>                                    
                                </tr>
                                <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                            </table>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <tr>
                                <td class="leftAligned">
                                    <asp:Label ID="uoLabelAirport" runat="server" Text='<%# Eval("AirportName")%>'></asp:Label>
                                </td>                                
                            </tr>
                        </ItemTemplate>
                        <EmptyDataTemplate>
                            <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                                <tr>
                                    <th>
                                        Airport
                                    </th>
                                </tr>
                                <tr>
                                    <td class="leftAligned">
                                        No Record
                                    </td>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
                    </asp:ListView>
                </td>
                
                <td valign="top" align="left" style="width: 50%">
                    <asp:ListView runat="server" ID="uoListViewSeaport">
                        <LayoutTemplate>
                            <table border="0" cellpadding="0" cellspacing="0" class="listViewTable" width="90%">
                                <tr>
                                    <th style="text-align: center; white-space: normal;">
                                        Seaport
                                    </th>                                    
                                </tr>
                                <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                            </table>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <tr>
                                <td class="leftAligned">                                    
                                    <asp:Label ID="uoLabelSeaport" runat="server" Text='<%# Eval("SeaportName")%>'></asp:Label>
                                </td>                               
                            </tr>
                        </ItemTemplate>
                        <EmptyDataTemplate>
                            <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                                <tr>
                                    <th>
                                        Seaport
                                    </th>
                                </tr>
                                <tr>
                                    <td class="leftAligned">
                                        No Record
                                    </td>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
                    </asp:ListView>
                </td>
            </tr>
        </table>
        <br />
        <%--</div>--%>
        
        <div class="PageTitle">
            <asp:Panel ID="uoPanelVehicleTypeAndCapacity" runat="server">
                Vehicle Type and Capacity
                <asp:Label ID="uoLabelCapacity" runat="server" Text="" Font-Size="Small" />
            </asp:Panel>
        </div>
        <asp:Panel ID="uoPanelVehicleTypeAndCapacityForm" runat="server" CssClass="CollapsiblePanel">
            <table width="100%">
                <tr>
                    <td valign="top" align="left">
                       <asp:ListView runat="server" ID="uoListViewVehicleTypeCapacity" >
                        <LayoutTemplate>
                            <table border="0" cellpadding="0" cellspacing="0" class="listViewTable" width="50%">
                                <tr>
                                    <th style="text-align: center; white-space: normal;">
                                        Vehicle Type
                                    </th>
                                    <th style="text-align: center; white-space: normal;">
                                        Min Capacity
                                    </th>
                                    <th style="text-align: center; white-space: normal;">
                                        Max Capacity
                                    </th>                                    
                                </tr>
                                <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                            </table>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <tr>
                                <td class="leftAligned">                                   
                                    <asp:Label ID="uoLabelVehicleTypeName" runat="server" Text='<%# Eval("VehicleType")%>'></asp:Label>
                                </td>
                                <td class="leftAligned">
                                    <asp:Label ID="uoLabelMinCapacity" runat="server" Text='<%# Eval("MinCapacity")%>'></asp:Label>
                                </td>
                                <td class="leftAligned">
                                    <asp:Label ID="uoLabelMaxCapacity" runat="server" Text='<%# Eval("MaxCapacity")%>'></asp:Label>
                                </td>                               
                            </tr>
                        </ItemTemplate>
                        <EmptyDataTemplate>
                            <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                                <tr>
                                    <th style="text-align: center; white-space: normal;">
                                        Vehicle Type
                                    </th>
                                    <th style="text-align: center; white-space: normal;">
                                        Min Capacity
                                    </th>
                                    <th style="text-align: center; white-space: normal;">
                                        Max Capacity
                                    </th>
                                </tr>
                                <tr>
                                    <td class="leftAligned" colspan="3">
                                        No Record
                                    </td>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
                    </asp:ListView>
                    </td>
                </tr>                
            </table>
        </asp:Panel>
        <cc1:collapsiblepanelextender ID="uocollapsibleLuggageVan" runat="server" TargetControlID="uoPanelVehicleTypeAndCapacityForm"
            ExpandControlID="uoPanelVehicleTypeAndCapacity" CollapseControlID="uoPanelVehicleTypeAndCapacity" TextLabelID="uoLabelCapacity"
            CollapsedText="(Show List)" ExpandedText="(Hide List)" Collapsed="false" 
            SuppressPostBack="true">
        </cc1:collapsiblepanelextender>
        <br />
        
        <div class="PageTitle">           
            Contract Details           
        </div>
        
       <table style ="width:100%">
        <tr>
            <td>
                <asp:GridView ID="uoGridViewVehicle" runat="server" Width="100%" AutoGenerateColumns="False" CssClass="listViewTable" >
                    <Columns>
                        <asp:BoundField DataField="ContractDetailID" HeaderText="Contract Detail ID" Visible="false" />
                        <asp:BoundField DataField="VehicleTypeID" HeaderText="Vehicle ID" Visible="false" />
                        <asp:BoundField DataField="RouteFrom" HeaderText="Route ID From" Visible="false" />
                        <asp:BoundField DataField="RouteTo" HeaderText="Route ID To" Visible="false" />
                        <asp:BoundField DataField="VehicleType" HeaderText="Type" />
                        <asp:BoundField DataField="RouteFrom" HeaderText="Route From" />
                        <asp:BoundField DataField="RouteTo" HeaderText="Route To" />
                        <asp:BoundField DataField="Origin" HeaderText="Origin" />
                        <asp:BoundField DataField="Destination" HeaderText="Destination" />
                        <%--<asp:BoundField DataField="StartDate" HeaderText="Start Date"/>--%>
                        <%--<asp:BoundField DataField="EndDate" HeaderText="End Date"/>--%>
                        <%--<asp:BoundField DataField="CurrencyID" HeaderText="Currency ID" Visible="false"/>--%>
                        <%--<asp:BoundField DataField="Currency" HeaderText="Currency"/>--%>
                        <asp:BoundField DataField="RateAmount" HeaderText="Rate" DataFormatString="{0:#,##0.00}" />
                        <asp:BoundField DataField="Tax" HeaderText="Tax" DataFormatString="{0:#,##0.00}" />                        
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
       </table>
        <br />
    </div>       
</asp:Content>
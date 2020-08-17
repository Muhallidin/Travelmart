<%@ Page Title="" Language="C#" MasterPageFile="~/TravelMart.Master" AutoEventWireup="true" CodeBehind="Seafarer.aspx.cs" Inherits="TRAVELMART.Seafarer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="PageTitle">
        Seafarer List
    </div>
    <table width="80%">    
        <tr>
        <td>
            <asp:ListView runat="server" ID = "uoListViewTravel" >
                <LayoutTemplate>
                    <table border="0" cellpadding="0" cellspacing="0" class="listViewTable" width="100%" >
                        <tr>
                            <th style="text-align:left;width:100px">E1 ID</th>
                            <th style="text-align:left;width:300px">Seafarer Name</th>
                            <th style="text-align:left;width:200px">Rank</th>
                            <th style="text-align:left;width:100px">Cost Center</th>                        
                            <th style="text-align:left;width:100px">Status</th>                                                
                            <th style="text-align:left;width:150px">Embarkation Date</th>
                            <th style="text-align:left;width:150px">Disembarkation Date</th>
                        </tr>                                                            
                        <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                    </table>
                </LayoutTemplate>
                <ItemTemplate>
                    <tr>
                        <td class="leftAligned" >                        
                            <%# Eval("colEmployeeIdChar")%>
                        </td>
                        <td class="leftAligned" >                        
                            <asp:LinkButton runat="server" ID="uoLinkButtonName" Text= '<%# Eval("colSeafarerNameVarhar")%>'></asp:LinkButton> 
                        </td>
                        <td class="leftAligned">
                            <%# Eval("colPositionVarchar")%>
                        </td>                                   
                        <td class="leftAligned">
                            <%# Eval("colCostCenterVarchar")%>
                        </td> 
                        <td class="leftAligned">
                            <%# Eval("colStatusVarchar")%>
                        </td>                                     
                         <td class="leftAligned">
                            <%# String.Format("{0:dd-MMM-yyyy}", Eval("colEmbarkationDate"))%>                        
                        </td> 
                        <td class="leftAligned">
                            <%# String.Format("{0:dd-MMM-yyyy}", Eval("colDisembarkationDate"))%>                           
                        </td> 
                    </tr>
                </ItemTemplate>
            </asp:ListView>
        </td>
        </tr>
    </table>
</asp:Content>

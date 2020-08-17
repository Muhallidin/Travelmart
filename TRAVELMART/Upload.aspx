<%@ Page Title="" Language="C#" MasterPageFile="~/TravelmartMaster.Master" AutoEventWireup="true" CodeBehind="Upload.aspx.cs" Inherits="TRAVELMART.Upload" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style1
        {
            width: 96px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="ViewTitlePadding"  >        
        <table width="100%" cellpadding="0" cellspacing="0" class="PageTitle">
            <tr>
                <td>Upload </td>                           
            </tr>
        </table>    
    </div>
    <table width="100%">
    <tr>
    <td align="Left" class="style1">
        &nbsp;&nbsp; File:</td>
    <td align="Left">
        <asp:DropDownList ID="uoDropDownListFile" runat="server" Width="200px">
            <asp:ListItem Value="0">Active Seafarer</asp:ListItem>
            <asp:ListItem Value="1">Ship Master</asp:ListItem>
            <asp:ListItem Value="2">Job Code</asp:ListItem>
        </asp:DropDownList>
        </td>
    </tr>
    <tr>
    <td align="Left" class="style1">
        &nbsp;&nbsp; Browse File:
    </td>
    <td align="Left">
        <asp:FileUpload ID="uoFileUpload" runat="server" Height="24px" Width="600px" />
    </td>
    </tr>
    <tr>
    <td align="Left" class="style1">
        &nbsp;</td>
    <td align="Left">
        <asp:Button ID="uoButtonExtract" runat="server" Text="Extract" 
            CssClass="SmallButton" onclick="uoButtonExtract_Click" />
    </td>
    </tr>
    </table>
    <table width="100%">
    <tr >
    <asp:ListView runat="server" ID="uolistviewExtract" 
            >
                    <LayoutTemplate>
                        <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                            <tr>
                            <th>Rec Loc</th>
                            <th>E1 ID</th>
                            <th>Name</th>
                            <th>CheckIn</th>
                            <th>Room</th>
                            <th>Rank</th>
                            <th>Gender</th>
                            <th>Nationality</th>

                            </tr>
                            <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                        </table>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <tr>                                                
<%--                             <td class="leftAligned">
                                <%# Eval("RecLoc")%>
                            </td>
                            <td class="leftAligned">
                                <%# Eval("SfID")%>
                            </td>
  
                          <td class="leftAligned">
                                <%# String.Format("{0:dd-MMM-yyyy}", Eval("colTimeSpanStartDate"))%>
                            </td>

                            <td class="leftAligned">
                                <%# Eval("colRoomNameVarchar")%>
                            </td>       
                            <td class="leftAligned">
                                <%# Eval("Rank")%>
                            </td>      
                            <td class="leftAligned">
                                <%# Eval("Gender")%>
                            </td>  
                            <td class="leftAligned">
                                <%# Eval("Nationality")%>
                            </td>  
                             <td class="leftAligned">
                                <%# Eval("CostCenter")%>
                            </td>             
                            <td class="leftAligned">
                                <%# Eval("colTimeSpanDurationInt")%>
                            </td>    
                            <td class="leftAligned">
                                <%# Eval("HotelCity")%>
                            </td>  
                            <td class="leftAligned">
                                <%# Eval("colMarketingAirlineCodeVarchar")%>
                            </td>
                            <td class="leftAligned">
                                <%# Eval("FromCity")%>
                            </td>  
                            <td class="leftAligned">
                                <%# Eval("ToCity")%>
                            </td>  --%>

                        </tr>
                    </ItemTemplate>
                    <EmptyDataTemplate>  
                    <table class="listViewTable">
                        <tr>
                            <th>Rec Loc</th> 
                            <th>E1 ID</th>
                            <th>CheckOut</th>
                            <th>Room</th>
                            <th>Rank</th>
                            <th>Gender</th>
                            <th>Nationality</th>
                            <th>CostCenter</th>
                            <th>Duration</th>
                            <th>Hotel City</th>
                            <th>Airline</th>
                            <th>From City</th>
                            <th>To City</th>
                           <%-- <th>Tag as Scanned</th>--%>
                        </tr>
                        <tr>
                            <td colspan="15" class="leftAligned">
                                No Record
                            </td>
                        </tr>                        
                        </table>
                    </EmptyDataTemplate>
                </asp:ListView>
                <asp:DataPager ID="uolistviewExtractPager" runat="server" PagedControlID="uolistviewExtract"
                    PageSize="20">
                    <Fields>
                        <asp:NumericPagerField ButtonType="link" NumericButtonCssClass="PagerClass" />
                    </Fields>
                </asp:DataPager>  
    </tr>
    </table>
</asp:Content>

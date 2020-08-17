<%@ Page Title="" Language="C#" MasterPageFile="~/TravelmartMaster.Master" AutoEventWireup="true" CodeBehind="HotelEmailView.aspx.cs" Inherits="TRAVELMART.Hotel.HotelEmailView" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <div class="ViewTitlePadding"  >        
        <table width="100%" cellpadding="0" cellspacing="0" class="PageTitle">
            <tr>
                <td>Hotel Manifest Email Sent</td>                           
            </tr>
        </table>    
    </div>
    <table width="100%">
    <tr align="left">
    <td>
        <asp:ListView ID="uoListViewEmail" runat="server">
        <LayoutTemplate>
        <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
        <tr>
        <th >From</th>    
        <th >To</th>     
        <th >Subject</th>     
        <th >Filename</th>   
        <th >Datetime Sent</th>   
        <th >User</th>       
        </tr>
        <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
        </table>
        </LayoutTemplate>
        <ItemTemplate>
         <tr>
         <td class="leftAligned">
               <%# Eval("colFromVarchar")%>            
         </td>
                  <td class="leftAligned">
               <%# Eval("colToVarchar")%>            
         </td>
                  <td class="leftAligned">
               <%# Eval("colSubjectVarchar")%>            
         </td>
                  <td class="leftAligned">
               <%# Eval("colFilenameVarchar")%>            
         </td>
                  <td class="leftAligned">
               <%# Eval("colSentDatetime")%>            
         </td>
         <td class="leftAligned">
               <%# Eval("colCreatedByVarchar")%>            
         </td>
         </tr>
        </ItemTemplate>
        </asp:ListView>
        <asp:DataPager ID="uolistviewHotelInfoPager" runat="server" PagedControlID="uoListViewEmail"
            PageSize="20" OnPreRender="uolistviewHotelInfoPager_PreRender">
            <Fields>
                <asp:NumericPagerField ButtonType="link" NumericButtonCssClass="PagerClass" />
            </Fields>
        </asp:DataPager>  
    </td>
    </tr>
    </table>
</asp:Content>

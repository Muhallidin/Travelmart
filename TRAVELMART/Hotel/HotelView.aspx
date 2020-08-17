<%@ Page Title="" Language="C#" MasterPageFile="~/TravelMart.Master" AutoEventWireup="true"
    CodeBehind="HotelView.aspx.cs" Inherits="TRAVELMART.HotelView" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<%--    <script type="text/javascript" src="../FBox/jquery-1.6.1.min.js"></script>        
    <script type="text/javascript" src="../FBox/fancybox/jquery.mousewheel-3.0.4.pack.js"></script>
    <script type="text/javascript" src="../FBox/fancybox/jquery.fancybox-1.3.4.pack.js"></script>
    
    <script src="../Menu/menu.js" type="text/javascript"></script>        
--%>    
    <script type="text/javascript" >
        $(document).ready(function() {
            $("a#<%=uoHyperLinkSeafarer.ClientID %>").fancybox(
                {
                    'width': '45%',
                    'height': '120%',
                    'autoScale': false,
                    'transitionIn': 'fadeIn',
                    'transitionOut': 'fadeOut',
                    'type': 'iframe',
                    'onClosed': function() {
                        var a = $("#<%=uoHiddenFieldSeafarer.ClientID %>").val();
                        if (a == '1')
                            $("#aspnetForm").submit();
                    }
                });
            });           
    </script>  
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>    
        
        <div class="ViewTitlePadding"  >        
        <table width="100%" cellpadding="0" cellspacing="0" class="PageTitle">
            <tr>
                <td>Hotel Travel List</td>                    
                <td class="RightClass">
                    <span> Seafarer Name:</span>
               <asp:TextBox ID="uoTextBoxName" runat="server" Width="350px" CssClass="TextBoxInput"></asp:TextBox>
                    <asp:Button ID="uoButtonSearch" runat="server" Text="Search" CssClass="SmallButton"
                        onclick="uoButtonSearch_Click" />
                </td>        
            </tr>
        </table>    
    </div> 
                
    <table width="100%">          
        <tr>
         <td class="LeftClass">
             <span>Region:</span>
            <asp:DropDownList ID="uoDropDownListMapRef" runat="server" Width="250px" 
                AutoPostBack="True"                                 
                AppendDataBoundItems="True" 
                onselectedindexchanged="uoDropDownListMapRef_SelectedIndexChanged" >
            </asp:DropDownList>
            <cc1:listsearchextender ID="ListSearchExtender_uoDropDownListMapRef" runat="server"
                TargetControlID ="uoDropDownListMapRef"
                PromptText="Type to search"
                PromptPosition="Top"
                IsSorted="true"
                PromptCssClass="dropdownSearch"/>                                                        
            </td>
        </tr>
        <tr>
            <td valign="top">  
            <asp:ListView runat="server" ID="uolistviewHotelInfo">
                    <LayoutTemplate>
                        <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                            <tr>
                            <th>Name</th>
                            <th><asp:Label runat="server" ID="ucLabelONOFF" Text="Onsigning"></asp:Label></th>
                            <th>Nationality</th>
                            <th>Rank</th>
                            <th>Hotel</th>
                            <th>Check-in Date/Time</th>                       
                            </tr>
                            <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                        </table>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <tr>
                            <td style ="width:180px" class="leftAligned">
                                <asp:LinkButton runat="server" ID="SeafarerLinkButton" PostBackUrl='<%# "~/SuperUserView.aspx?sfId=" + Eval("sfID") + "&trID=" + Eval("colTravelReqIdInt") + "&st=" + Request.QueryString["st"] + "&p=" + Request.QueryString["p"]+ "&ufn=" + Request.QueryString["ufn"] + "&ID=" + Eval("colIdBigint") + "&from=" + Request.QueryString["from"] + "&to=" + Request.QueryString["to"] + "&dt=" + Request.QueryString["dt"]%>'><%# Eval("Name") %></asp:LinkButton>
                            </td>                           
                            <td class="leftAligned">
                                <%# String.Format("{0:dd-MMM-yyyy HHmm}", Eval("date"))%> <%--String.Format(DateFormat(Eval("IsEmbarkDateTime")), Eval("DATE"))--%>
                            </td>                            
                             <td class="leftAligned">
                                <%# Eval("nationality")%>
                            </td>
                            <td class="leftAligned">
                                <%# Eval("rank")%>
                            </td>
                            <td class="leftAligned">
                                <%# Eval("hotel")%>
                            </td>                            
                            <td class="leftAligned">
                                <%--<%# String.Format("{0:dd-MMM-yyyy HHmm}", Eval("CheckInDate"))%>--%>
                                <%# String.Format(DateFormat(Eval("IsDateTime")), Eval("CheckInDate"))%>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <EmptyDataTemplate>  
                    <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                        <tr>
                            <th style ="width:180px" >Name</th>   
                            <th>Onsigning</th>
                            <th>Nationality</th>
                            <th>Rank</th>
                            <th>Hotel</th>
                            <th>Check-in Date/Time</th>
                        </tr>
                        <tr>
                            <td colspan="7" class="leftAligned">
                                No Record
                            </td>
                        </tr>                        
                        </table>
                    </EmptyDataTemplate>
                </asp:ListView>
                <asp:DataPager ID="uolistviewHotelInfoPager" runat="server" PagedControlID="uolistviewHotelInfo"
                    PageSize="20" OnPreRender="uolistviewHotelInfoPager_PreRender">
                    <Fields>
                        <asp:NumericPagerField ButtonType="link" NumericButtonCssClass="PagerClass" />
                    </Fields>
                </asp:DataPager>                  
                <%--<%# Eval("nationality")%>--%>
            </td>
        </tr>
    </table>    
    <asp:HiddenField ID="uoHiddenFieldDateFrom" runat="server" />
    <asp:HiddenField ID="uoHiddenFieldDateTo" runat="server" />    
    <asp:HiddenField ID="uoHiddenFieldFilterBy" runat="server" />
    <div class="">
         <asp:HiddenField ID="uoHiddenFieldSeafarer" runat="server" Value="0" />
             <a id="uoHyperLinkSeafarer" runat="server" href="~/Hotel/AddSeafarer.aspx">
             <asp:Button ID="uoButtonSeafarerAdd" runat="server" 
             Text="Add Seafarer" Font-Size="X-Small" visible="false" />
             </a>
    </div>
    </ContentTemplate>
    </asp:UpdatePanel> 
</asp:Content>

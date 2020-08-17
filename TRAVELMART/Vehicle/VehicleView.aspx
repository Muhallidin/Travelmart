<%@ Page Title="" Language="C#" MasterPageFile="~/TravelMartMaster.Master" AutoEventWireup="true"
    CodeBehind="VehicleView.aspx.cs" Inherits="TRAVELMART.VehicleView" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
   <%-- <script type="text/javascript" src="../FBox/jquery-1.6.1.min.js"></script>
    <script type="text/javascript" src="../FBox/fancybox/jquery.mousewheel-3.0.4.pack.js"></script>
    <script type="text/javascript" src="../FBox/fancybox/jquery.fancybox-1.3.4.pack.js"></script>
    
    <script src="../Menu/menu.js" type="text/javascript"></script> --%>       
    
    <script type="text/javascript">
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
                <td>Vehicle Travel List</td>                    
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
            <td valign="top" colspan="2">
                <asp:ListView runat="server" ID="uolistviewVehicleTravelInfo">
                    <LayoutTemplate>
                        <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                            <tr>
                                <th>
                                    Name
                                </th>
                                <th>
                                   <asp:Label runat="server" ID="ucLabelONOFF" Text="Onsigning"></asp:Label>
                                </th>
                                <th>
                                    Rank
                                </th>
                                <th>
                                    Pick-up Date/Time
                                </th>
                                <th>
                                    Vehicle
                                </th>
                                <th>
                                    Vehicle Type
                                </th>
                            </tr>
                            <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                        </table>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <tr>
                            <td style="width: 180px" class="leftAligned">
                                <asp:LinkButton runat="server" ID="SeafarerLinkButton" PostBackUrl='<%# "~/SuperUserView.aspx?trID=" + Eval("colTravelReqIdInt") + "&sfId=" + Eval("sfID") + "&st=" + Request.QueryString["st"]  + "&p=" + Request.QueryString["p"]+ "&ufn=" + Request.QueryString["ufn"] + "&ID=" + Eval("colIdBigint") + "&from=" + Request.QueryString["from"] + "&to=" + Request.QueryString["to"]  + "&dt=" + Request.QueryString["dt"]%>'><%# Eval("Name") %></asp:LinkButton>
                            </td>
                            <td class="leftAligned">
                                <%# String.Format("{0:dd-MMM-yyyy HHmm}", Eval("date"))%>
                                <%--DateFormat(Eval("IsEmbarkDateTime")), Eval("DATE"))--%>
                            </td>
                            <td class="leftAligned">
                                <%# Eval("rank")%>
                            </td>
                            <td class="leftAligned">
                                <%# String.Format(DateFormat(Eval("IsDateTime")), Eval("PICKUPTIME"))%>
                            </td>
                            <td class="leftAligned">
                                <%# Eval("vehicle")%>
                            </td>
                            <td class="leftAligned">
                                <%# Eval("vehicletype")%>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <EmptyDataTemplate>
                        <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                            <tr>
                                <th style="width: 180px">
                                    Name
                                </th>
                                <th>
                                    Onsigning
                                </th>
                                <th>
                                    Rank
                                </th>
                                <th>
                                    Pick-up Date/Time
                                </th>
                                <th>
                                    Vehicle
                                </th>
                                <th>
                                    Vehicle Type
                                </th>
                            </tr>
                            <tr>
                                <td colspan="7" class="leftAligned">
                                    No Record
                                </td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                </asp:ListView>     
                <asp:DataPager ID="uolistviewVehicleTravelInfoPager" runat="server" PagedControlID="uolistviewVehicleTravelInfo"
                    PageSize="20" OnPreRender="uolistviewVehicleTravelInfoPager_PreRender">
                    <Fields>
                        <asp:NumericPagerField ButtonType="link" NumericButtonCssClass="PagerClass" />
                    </Fields>
                </asp:DataPager>           
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="uoHiddenFieldDateFrom" runat="server" />
    <asp:HiddenField ID="uoHiddenFieldDateTo" runat="server" />
    <asp:HiddenField ID="uoHiddenFieldFilterBy" runat="server" />
     <tr>
        <td colspan="2">
            <div class="">
                <asp:HiddenField ID="uoHiddenFieldSeafarer" runat="server" Value="0" />
                <a id="uoHyperLinkSeafarer" runat="server" href="~/Vehicle/AddSeafarer.aspx">
                    <asp:Button ID="uoButtonSeafarerAdd" runat="server" Text="Add Seafarer" Font-Size="X-Small" Visible="false" />
                </a>
            </div>
        </td>
        </tr>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

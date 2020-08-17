<%@ Page Title="" Language="C#" MasterPageFile="~/TravelMart.Master" AutoEventWireup="true"
    CodeBehind="GeneralListView.aspx.cs" Inherits="TRAVELMART.GeneralListView" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript" language="javascript">
        $("#<%=uoListViewGeneralList.ClientID %>").click(function(ev) {
            evt.preventDefault();
        });
    </script>
    <%--<div class="ViewTitlePadding"  >        
        <table width="100%" cellpadding="0" cellspacing="0" class="PageTitle">
            <tr>
                <td>Travel Package List</td>                    
                <td class="RightClass">
                    <span> Last Name, First Name:</span>
                    <asp:TextBox ID="uoTextBoxName" runat="server" Width="350px" CssClass="TextBoxInput"></asp:TextBox>
                    <asp:Button ID="uoButtonSearch" runat="server" Text="Search" CssClass="SmallButton"
                        onclick="uoButtonSearch_Click" />
                </td>        
            </tr>
        </table>    
    </div>
            
    <div runat="server" id="uoDivDateRange">
        <table width="100%">
            <tr>
                <td>
                    Date From:
                    <asp:TextBox ID="uoTextBoxDateFrom" runat="server"></asp:TextBox>
                </td>
                <td>
                    Date To:
                    <asp:TextBox ID="uoTextBoxDateTo" runat="server"></asp:TextBox>
                </td>
                <td>
                     <asp:Button ID="uoButtonSearchDateRange" runat="server" Text="Search" CssClass="SmallButton"/>
                </td> 
            </tr>
        </table>
    </div>    --%>
    <div class="ViewTitlePadding"  >        
        <table width="100%" cellpadding="0" cellspacing="0" >
        <tr>
            <td class="PageTitle">
                <table width="100%">
                    <tr>
                        <td style="width:200px">Travel Package List</td>                    
                        <td class="RightClass">
                            <span> Seafarer Name:</span>
                            <asp:TextBox ID="uoTextBoxName" runat="server" Width="350px" CssClass="TextBoxInput"></asp:TextBox>
                            <asp:Button ID="uoButtonSearch" runat="server" Text="Search" CssClass="SmallButton" Width="50px"
                                onclick="uoButtonSearch_Click" />
                        </td>        
                    </tr>
                </table>
            </td>           
        </tr>        
        <tr>
            <td class="PageTitleNoColor">                                        
                <table width="100%">
                    <tr>
                        <td class="LeftClass">                                                    
                            <span>Region:</span>
                            <asp:DropDownList ID="uoDropDownListRegion" runat="server" Width="250px" 
                                AutoPostBack="True"                                 
                                AppendDataBoundItems="True" 
                                onselectedindexchanged="uoDropDownListRegion_SelectedIndexChanged" 
                               >
                            </asp:DropDownList>
                            <cc1:ListSearchExtender ID="ListSearchExtender_uoDropDownListRegion" runat="server"
                                TargetControlID ="uoDropDownListRegion"
                                PromptText="Type to search"
                                PromptPosition="Top"
                                IsSorted="true"
                                PromptCssClass="dropdownSearch"/>                                                        
                        </td>
                        <td class="RightClass">
                            <span> Group By:</span>
                            <asp:DropDownList ID="uoDropDownListGroupBy" runat="server" Width="200px" 
                                AutoPostBack="True" 
                                onselectedindexchanged="uoDropDownListGroupBy_SelectedIndexChanged" 
                                >
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>                                
            </td>
        </tr>
        <%--<tr runat="server" id= "uoTRDateRange" visible = "false">
             <td class="PageTitleNoColor">
                 <table width="100%">
                    <tr>
                        <td class="style1"></td>
                        <td class="RightClass">
                             <span>Date From:</span>
                            <asp:TextBox ID="uoTextBoxDateFrom" runat="server"></asp:TextBox>
                        </td>
                        <td class="RightClass">
                             <span>Date To:</span>
                            <asp:TextBox ID="uoTextBoxDateTo" runat="server"></asp:TextBox>                        
                            <asp:Button ID="uoButtonSearchDateRange" runat="server" Text="View" CssClass="SmallButton" Width="50px"/>
                        </td> 
                    </tr>
                </table>
            </td>
        </tr>--%>
        </table>    
    </div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">               
    <ContentTemplate>
    <table width="100%">  
        <tr>
            <td valign="top" >
                
                <asp:ListView runat="server" ID="uoListViewGeneralList">
                    <LayoutTemplate>
                        <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                            <tr>
                                <th style ="width:200px">Name</th>
                                <th runat="server" id="ucTHOnOffDate"><asp:Label runat="server" ID="ucLabelDate"></asp:Label></th>
                                <th runat="server" id="ucTHFlightDate"><asp:Label runat="server" ID="ucLabelFlightDate"></asp:Label></th>
                                <th runat="server" id="ucTHPort">Port</th>
                                <th>Brand</th>
                                <th runat="server" id="ucTHVessel">Ship</th>
                                <th>Rank</th>
                                <th>Air</th>
                                <th>Car</th>
                                <th>Hotel</th>
                            </tr>
                            <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                        </table>
                    </LayoutTemplate>
                    <ItemTemplate>
                          <%# TravelListAddGroup() %>
                        <tr>
                            <td style ="width:200px" class="leftAligned">
                                <asp:LinkButton runat="server" ID="uoLinkButtonSeafarer" PostBackUrl='<%# "~/SuperUserView.aspx?trID=" + Eval("colTravelReqIdInt") + "&sfId=" + Eval("SFID") + "&st=" + Request.QueryString["st"] + "&p=" + Request.QueryString["p"] + "&ufn=" + Request.QueryString["ufn"] + "&af=" + Request.QueryString["af"] + "&from=" + Request.QueryString["from"] + "&to=" + Request.QueryString["to"]%>'><%# Eval("Name") %></asp:LinkButton>
                            </td>
                            <td class='<%# (HideOnOffDate()==""?"leftAligned":HideOnOffDate())%>' >                              
                                <%# String.Format("{0:dd-MMM-yyyy HHmm}", Eval("date"))%>    
                            </td>
                            <td class='<%# (HideFlightDate()==""?"leftAligned":HideFlightDate())%>' >                              
                                <%# String.Format("{0:dd-MMM-yyyy HHmm}", Eval("FlightDate"))%>    
                            </td>
                             <td class='<%# (HidePort()==""?"leftAligned":HidePort())%>' >                              
                                <%# Eval("colPortNameVarchar")%>    
                            </td>
                            <td class="leftAligned">
                                <%# Eval("Brand") %>
                            </td>
                            <td class='<%# (HideVessel()==""?"leftAligned":HideVessel()) %>' >
                                <%# Eval("Vessel") %>
                            </td >
                            <td class="leftAligned">
                                <%# Eval("Rank") %>
                            </td>
                            <%--<td style="background: transparent url('<%#GetStatusImage(Eval("AIR")) %>');">--%>
                            <td>
                                <asp:Image ID="uoImageAir" runat="server" ImageUrl='<%#GetStatusImage(Eval("AIR")) %>' ToolTip=<%# Eval("colAirStatusVarchar")%> />
                            </td>
                            <td >
                                <asp:Image ID="uoImageCar" runat="server" ImageUrl='<%#GetStatusImage(Eval("VEHICLE")) %>' ToolTip=<%# Eval("colVehicleStatusVarchar")%> />
                            </td>
                            <td >
                                <asp:Image ID="uoImageHotel" runat="server" ImageUrl='<%#GetStatusImage(Eval("HOTEL")) %>' ToolTip=<%# Eval("colHotelStatusVarchar")%> />
                            </td>
                        </tr>
                    </ItemTemplate>
                    <EmptyDataTemplate>
                        <table class="listViewTable">
                            <tr>
                                <th>Name</th>
                                <th>Onsigning</th>
                                <th>Brand</th>
                                <th>Ship</th>
                                <th>Rank</th>
                                <th>Air</th>
                                <th>Car</th>
                                <th>Hotel</th>
                            </tr>
                            <tr>
                                <td colspan="8" class="leftAligned">No Record</td>                                
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                </asp:ListView>
                <asp:DataPager ID="uoListViewGeneralListPager" runat="server" PagedControlID="uoListViewGeneralList"
                    PageSize="20" OnPreRender="uoListViewGeneralListPager_PreRender">
                    <Fields>
                        <asp:NumericPagerField ButtonType="link" NumericButtonCssClass="PagerClass" />
                    </Fields>
                </asp:DataPager>   
            </td>
        </tr>
    </table>
    </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="uoButtonSearch" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="uoDropDownListGroupBy" 
                EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="uoDropDownListRegion" 
                EventName="SelectedIndexChanged" />
        </Triggers>
    </asp:UpdatePanel>   
    <asp:HiddenField ID="ucHiddenFieldOnOff" runat="server" />
    <asp:HiddenField ID="uoHiddenFieldDateFrom" runat="server" />
    <asp:HiddenField ID="uoHiddenFieldDateTo" runat="server" />
    <asp:HiddenField ID="uoHiddenFieldFilterBy" runat="server" />
</asp:Content>

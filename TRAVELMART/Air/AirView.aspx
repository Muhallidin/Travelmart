<%@ Page Title="" Language="C#" MasterPageFile="~/TravelmartMaster.Master" AutoEventWireup="true"
    CodeBehind="AirView.aspx.cs" Inherits="TRAVELMART.AirView" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">      
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<script type="text/javascript" language="javascript">
    $(document).ready(function() {
        filterSettings();
    });

    function pageLoad(sender, args) {
        var isAsyncPostback = Sys.WebForms.PageRequestManager.getInstance().get_isInAsyncPostBack();
        if (isAsyncPostback) {
            filterSettings();
        }
    }

    function filterSettings() {
        if ($("#<%=uoCheckBoxAdvanceSearch.ClientID %>").attr('checked')) {
            $("#<%=uoTableAdvanceSearch.ClientID %>").show();
        }
        else {
            $("#<%=uoTableAdvanceSearch.ClientID %>").hide();
        }
            
        $("#<%=uoCheckBoxAdvanceSearch.ClientID %>").click(function() {
            if ($(this).attr('checked')) {
                $("#<%=uoTableAdvanceSearch.ClientID %>").fadeIn();
            }
            else {
                $("#<%=uoTableAdvanceSearch.ClientID %>").fadeOut();
            }
        });

        $("#<%=uoDropDownListFilterBy.ClientID %>").change(function(ev) {
            if ($(this).val() != "1") {
                $("#<%=uoTextBoxFilter.ClientID %>").val("");
            }
        });
    }
</script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    <div class="ViewTitlePadding"  >        
        <table width="100%" cellpadding="0" cellspacing="0" class="PageTitle">
            <tr>
                <td>Air Travel List</td>                      
            </tr>
        </table>    
    </div>        
             

<table width="100%" class="LeftClass">
    <tr>
        <td >
            <asp:CheckBox ID="uoCheckBoxAdvanceSearch" runat="server" 
                Text="View Advance Search" />
        </td>
    </tr>       
</table>
<table width="100%" class="LeftClass" id="uoTableAdvanceSearch" runat="server">   
 <tr>
        <td class="contentCaption">Vessel:</td>
        <td class="contentValue" >
            <asp:DropDownList ID="uoDropDownListVessel" runat="server" Width="300px" 
                AppendDataBoundItems="True">
                <asp:ListItem>--SELECT VESSEL--</asp:ListItem>
            </asp:DropDownList>
        </td> 
        <td></td>      
    </tr>
    <tr>
         <td class="contentCaption">Filter By:</td>
        <td class="contentValue" >
            <asp:DropDownList ID="uoDropDownListFilterBy" runat="server" Width="300px">
                <asp:ListItem Value="1">SEAFARER NAME</asp:ListItem>
                <asp:ListItem Value="2">EMPLOYEE ID</asp:ListItem>
            </asp:DropDownList>
        </td>        
        <td>
            <asp:TextBox ID="uoTextBoxFilter" runat="server" CssClass="TextBoxInput"  Width="300px"></asp:TextBox>
        </td>
    </tr> 
    <tr>
        <td class="contentCaption">Nationality:</td>
        <td class="contentValue"> <asp:DropDownList ID="uoDropDownListNationality" runat="server" Width="300px" 
                AppendDataBoundItems="True">
            <asp:ListItem>--SELECT NATIONALITY--</asp:ListItem>
        </asp:DropDownList></td>
        <td></td>
    </tr>
    <tr>
        <td class="contentCaption">Gender:</td>
        <td class="contentValue"> <asp:DropDownList ID="uoDropDownListGender" runat="server" Width="300px" 
                AppendDataBoundItems="True">
            <asp:ListItem>--SELECT GENDER--</asp:ListItem>
        </asp:DropDownList></td>
        <td></td>
    </tr>
    <tr>
        <td class="contentCaption">Rank:</td>
        <td> <asp:DropDownList ID="uoDropDownListRank" runat="server" Width="300px" 
                AppendDataBoundItems="True">
            <asp:ListItem>--SELECT RANK--</asp:ListItem>
        </asp:DropDownList></td>
        <td></td>
    </tr>
    <tr>
        <td class="contentCaption">Status:</td>
        <td> <asp:DropDownList ID="uoDropDownListStatus" runat="server" Width="300px">
            <asp:ListItem Value="0">--SELECT STATUS--</asp:ListItem>
            <asp:ListItem>ON</asp:ListItem>
            <asp:ListItem>OFF</asp:ListItem>
        </asp:DropDownList></td>
        <td></td>
    </tr>
    <tr>
        <td class="contentCaption">
            &nbsp;</td>
        <td>
            <asp:Button ID="uoButtonView" runat="server" CssClass="SmallButton" 
                onclick="uoButtonView_Click" Text="View" />
        </td>
        <td>
            &nbsp;</td>
    </tr>
</table>
                                 
    <table width="100%">        
        <%--<tr>
            <td class="LeftClass" colspan="2">                                                    
                <span>Region:</span>
                <asp:DropDownList ID="uoDropDownListMapRef" runat="server" Width="250px" 
                    AutoPostBack="True"                                 
                    AppendDataBoundItems="True" 
                    onselectedindexchanged="uoDropDownListMapRef_SelectedIndexChanged"  >
                </asp:DropDownList>
                <cc1:listsearchextender ID="ListSearchExtender_uoDropDownListMapRef" runat="server"
                    TargetControlID ="uoDropDownListMapRef"
                    PromptText="Type to search"
                    PromptPosition="Top"
                    IsSorted="true"
                    PromptCssClass="dropdownSearch"/>                                                        
            </td>
        </tr>--%>

        <tr>
            <td valign="top"  align="right">
                 <asp:ListView runat="server" ID="uoListViewAirTravelInfo">
                    <LayoutTemplate>
                        <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                            <tr>
                                <th>Record Loc</th>
                                <th>E1 ID</th>
                                <th>Name</th>
                                <th>City Pair</th>
                                <th>Departure Date/Time</th>
                                <th>Arrival Date/Time</th>
                                <th>Airline</th>
                                <th>Flight No.</th>
                                <th>Status</th>
                            </tr>
                            <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                        </table>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <tr>
                            <td class="leftAligned">
                                <%# Eval("RecLoc")%>
                            </td>
                            <td class="leftAligned">
                                <%# Eval("sfID")%>
                            </td>
                            <td style ="width:180px" class="leftAligned">
                                <%--<asp:LinkButton runat="server" ID="SeafarerLinkButton" PostBackUrl='<%# "~/SuperUserView.aspx?trID=" + Eval("colTravelReqIdInt") + "&sfId=" + Eval("sfID") + "&st=" + Request.QueryString["st"] + "&dt=" + Request.QueryString["dt"] + "&p=" + Request.QueryString["p"]+ "&ufn=" + Request.QueryString["ufn"] + "&af=" + Request.QueryString["af"] + "&recloc=" + Eval("RecLoc") + "&ID=" + Eval("colIdBigint") + "&from=" + Request.QueryString["from"] + "&to=" + Request.QueryString["to"]%>'><%# Eval("Name") %></asp:LinkButton>--%>
                                <asp:HyperLink ID="uoHyperLinkName" NavigateUrl='<%# "~/SuperUser.aspx?ufn=" + Request.QueryString["ufn"] + "&sfId=" + Eval("SfID") + "&recloc=" + Eval("RecLoc") + "&st=" + Eval("SFSTATUS") + "&ID=" + Eval("IDBigInt") + "&trID=" + Eval("TravelRequestID") + "&manualReqID=0" + "&dt=" + Request.QueryString["dt"] %>' runat="server"><%# Eval("Name")%></asp:HyperLink>
                            </td>
                            <td class="leftAligned">
                                <%# Eval("citypair")%>
                            </td>
                            <td class="leftAligned">
                                <%# String.Format("{0:dd-MMM-yyyy HHmm}", Eval("departure"))%>
                            </td>
                            
                            <td class="leftAligned">
                                 <%# String.Format("{0:dd-MMM-yyyy HHmm}", Eval("arrival"))%>
                            </td >
                            <td class="leftAligned">
                                <%# Eval("airline")%>
                            </td>
                            <td class="leftAligned">
                                <%# Eval("flightno")%>
                            </td>
                             <td class="leftAligned">
                                <%# Eval("airstatus")%>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <EmptyDataTemplate>  
                    <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                        <tr>
                            <th style ="width:180px" >Name</th>
                            <th>City Pair</th>
                            <th>Departure Date/Time</th>
                            <th>Arrival Date/Time</th>
                            <th>Airline</th>
                            <th>Flight No.</th>
                            <th>Status</th>
                        </tr>
                        <tr>
                            <td colspan="7" class="leftAligned">
                                No Record
                            </td>
                        </tr>                        
                        </table>
                    </EmptyDataTemplate>
                </asp:ListView>  
                 <asp:DataPager ID="uoListViewAirTravelInfoPager" runat="server" PagedControlID="uoListViewAirTravelInfo"
                    PageSize="20" OnPreRender="uoListViewAirTravelInfo_PreRender">
                    <Fields>
                        <asp:NumericPagerField ButtonType="link" NumericButtonCssClass="PagerClass" />
                    </Fields>
                </asp:DataPager>     
                <%--<asp:GridView ID="uogridviewAirTravelInfo" runat="server" AutoGenerateColumns="False"
                    CssClass="listViewTable" Width="100%">
                    <Columns>
                        <asp:TemplateField ItemStyle-Width="20%" HeaderText="Name" ItemStyle-CssClass="leftAligned">
                            <ItemTemplate>
                                <asp:LinkButton runat="server" ID="SeafarerLinkButton" PostBackUrl='<%# "~/SuperUserView.aspx?sfId=" + Eval("sfID") + "&st=" + Request.QueryString["st"] + "&dt=" + Request.QueryString["dt"] + "&p=" + Request.QueryString["p"]+ "&recloc=" + Eval("colItineraryreclocTinyint") %>'><%# Eval("Name") %></asp:LinkButton></ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="citypair" HeaderText="City Pair" ItemStyle-CssClass="leftAligned">
                            <ItemStyle Width="10%" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="departure" HeaderText="Departure Date/Time" DataFormatString="{0:dd-MMM-yyyy HHmm}"
                            HtmlEncode="false" ItemStyle-CssClass="leftAligned">
                            <ItemStyle Width="15%" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="arrival" HeaderText="Arrival Date/Time" DataFormatString="{0:dd-MMM-yyyy HHmm}"
                            HtmlEncode="false" ItemStyle-CssClass="leftAligned">
                            <ItemStyle Width="15%" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="airline" HeaderText="Airline" ItemStyle-CssClass="leftAligned">
                            <ItemStyle Width="15%" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="flightno" HeaderText="Flight No." ItemStyle-CssClass="leftAligned">
                            <ItemStyle Width="10%" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="airstatus" HeaderText="Status" ItemStyle-CssClass="leftAligned">
                            <ItemStyle Width="8%" HorizontalAlign="Center" />
                        </asp:BoundField>
                    </Columns>
                    <EmptyDataTemplate>                                                                
                        <tr>
                            <th>Name</th>
                            <th>City Pair</th>
                            <th>Departure Date/Time</th>
                            <th>Arrival Date/Time</th>
                            <th>Airline</th>
                            <th>Flight No.</th>
                            <th>Status</th>
                        </tr>
                        <tr>
                            <td colspan="7" class="leftAligned">
                                No Record
                            </td>
                        </tr>
                        
                    </EmptyDataTemplate>
                </asp:GridView>--%>
            </td>
        </tr>
    </table>
    
    <asp:HiddenField ID="uoHiddenFieldDate" runat="server" />
    <asp:HiddenField ID="uoHiddenFieldDateTo" runat="server" />
    <asp:HiddenField ID="uoHiddenFieldDateRange" runat="server" />
    <asp:HiddenField ID="uoHiddenFieldUser" runat="server" />
    <asp:HiddenField ID="uoHiddenFieldFilterBy" runat="server" />
    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

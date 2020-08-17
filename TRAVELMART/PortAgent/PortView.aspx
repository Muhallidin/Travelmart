<%@ Page Title="" Language="C#" MasterPageFile="~/TravelmartMaster.Master" AutoEventWireup="true"
    CodeBehind="PortView.aspx.cs" Inherits="TRAVELMART.PortView" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript" language="javascript">
    function confirmEmail() {
        if (confirm("Email record?") == true)
            return true;
        else
            return false;
    }
    function validate(key) {
        if ($("#<%=uoDropDownListFilterBy.ClientID %>").val() != "1") {

            //getting key code of pressed key
            var keycode = (key.which) ? key.which : key.keyCode;
            if ((keycode < 48 || keycode > 57) && (keycode < 45 || keycode > 46)) {
                return false;
            }
            else {
                return true;
            }
        }
        return true;
    }   
</script>

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
                      
    <div class="ViewTitlePadding"  >        
        <table width="100%" cellpadding="0" cellspacing="0" class="PageTitle">
            <tr>
                <td>Port Travel List</td>                    
                <%--<td class="RightClass">
                    <span> Seafarer Name:</span>
                    <asp:TextBox ID="uoTextBoxName" runat="server" Width="350px" CssClass="TextBoxInput"></asp:TextBox>
                    <asp:Button ID="uoButtonSearch" runat="server" Text="Search" CssClass="SmallButton"
                        onclick="uoButtonSearch_Click" />
                </td>    --%>    
            </tr>
        </table>    
    </div>  
    
    
<table width="100%" class="LeftClass">
    <tr>
        <td class="contentCaption">Ship:</td>
        <td class="contentValue" >
            <asp:DropDownList ID="uoDropDownListVessel" runat="server" Width="300px" 
                AppendDataBoundItems="True">
                <asp:ListItem>--SELECT SHIP--</asp:ListItem>
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
            <asp:TextBox ID="uoTextBoxFilter" runat="server" CssClass="TextBoxInput"  Width="300px" onkeypress="return validate(event);"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="contentCaption"></td>
        <td class="contentValue" >
            <asp:CheckBox ID="uoCheckBoxAdvanceSearch" Text="View Advance Search" runat="server" />
        </td>        
        <td>        
        </td>
    </tr>       
</table>
<table width="100%" class="LeftClass" id="uoTableAdvanceSearch" runat="server">    
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
        <td class="contentValue"> <asp:DropDownList ID="uoDropDownListRank" runat="server" Width="300px" 
                AppendDataBoundItems="True">
            <asp:ListItem>--SELECT RANK--</asp:ListItem>
        </asp:DropDownList></td>
        <td></td>
    </tr>
    <tr>
        <td class="contentCaption">Status:</td>
        <td class="contentValue"> <asp:DropDownList ID="uoDropDownListStatus" runat="server" Width="300px">
            <asp:ListItem Value="0">--SELECT STATUS--</asp:ListItem>
            <asp:ListItem>ON</asp:ListItem>
            <asp:ListItem>OFF</asp:ListItem>
        </asp:DropDownList></td>
        <td></td>
    </tr>   
</table>
            
       
    <table width="100%">
    <tr>                
        <td align="left"> 
            <asp:Button ID="uoButtonView" runat="server" Text="View"  
                CssClass="SmallButton" onclick="uoButtonView_Click"/></td>            
        <td class="RightClass">
            <asp:Button ID="uoButtonSendEmail" runat="server" Text="Send Email"  OnClientClick="javascript: return confirmEmail();"
            CssClass="SmallButton" Width="100px" onclick="uoButtonSendEmail_Click" 
                Visible="False"/>
        </td>    
    </tr>
    <tr>
        <%--<tr>
            <td colspan="2" class="ViewTitlePadding">
                <div class="PageTitle">
                    Port Travel List</div>
            </td>
        </tr>--%>
            <%--<tr>
                 <td class="LeftClass" colspan="2">
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
            </tr>--%>
            <td valign="top" colspan="2">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>    
            <asp:ListView runat="server" ID="uolistviewPortTravelInfo" 
                        DataSourceID="uoObjectDataSourcePortView" 
                        onitemcommand="uolistviewPortTravelInfo_ItemCommand">
                    <LayoutTemplate>
                        <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                            <tr>          
                            <th><asp:LinkButton ID="uoLinkButtonHeaderRecLoc" runat="server" CommandName="SortByRLoc">Rec Loc</asp:LinkButton></th>                  
                            <th><asp:LinkButton ID="uoLinkButtonHeaderE1ID" runat="server" CommandName="SortByE1">E1 ID</asp:LinkButton></th>
                            <th><asp:LinkButton ID="uoLinkButtonHeaderName" runat="server" CommandName="SortByName">Name</asp:LinkButton></th>
                            <%--<th><asp:Label runat="server" ID="ucLabelONOFF" Text="Onsigning"></asp:Label></th>--%>
                            <th><asp:LinkButton ID="uoLinkButtonHeaderOnOff" runat="server" CommandName="SortByOnOff">On/Off Date</asp:LinkButton></th>
                           <%-- <th>Nationality</th>--%>
                            <th><asp:LinkButton ID="uoLinkButtonHeaderRank" runat="server" CommandName="SortByRank">Rank</asp:LinkButton></th>
                            <th class="hideElement">Ship</th>       
                            <th><asp:LinkButton ID="uoLinkButtonHeaderPort" runat="server" CommandName="SortByPort">Port</asp:LinkButton></th>                      
                            <th>Cost Center</th>
                            <th><asp:LinkButton ID="uoLinkButtonHeaderStatus" runat="server" CommandName="SortByStatus">Status</asp:LinkButton></th> 
                            <th><asp:LinkButton ID="uoLinkButtonHeaderPortStatus" runat="server" CommandName="SortByPortStatus">Port Status</asp:LinkButton></th> 
                            </tr>
                            <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                        </table>
                    </LayoutTemplate>
                    <ItemTemplate>
                         <%# PortAddGroup() %>
                        <tr>      
                            <td class="leftAligned">
                                <%# Eval("recLoc")%>
                            </td>                        
                            <td class="leftAligned">
                                <%# Eval("sfid")%>
                            </td>  
                            <td style ="width:180px" class="leftAligned">
                                <%--<asp:LinkButton runat="server" ID="SeafarerLinkButton" PostBackUrl='<%# "~/SuperUserView.aspx?sfId=" + Eval("sfID") +  "&trID=" + Eval("TravelRequestID") + "&st=" + Request.QueryString["st"] + "&dt=" + Request.QueryString["dt"] + "&p=" + Request.QueryString["p"] + "&ufn=" + Request.QueryString["ufn"] +  "&from=" + Request.QueryString["from"] + "&to=" + Request.QueryString["to"]%>'><%# Eval("Name") %></asp:LinkButton>--%>
                                <asp:LinkButton runat="server" ID="SeafarerLinkButton" PostBackUrl='<%# "~/SuperUserView.aspx?sfId=" + Eval("sfID") +  "&trID=" + Eval("TravelRequestID") + "&st=" + Eval("sfStatus") + "&ufn=" + Request.QueryString["ufn"] + "&manualReqID=" + Eval("manualReqID") + "&dt=" + Request.QueryString["dt"]%>'><%# Eval("Name") %></asp:LinkButton>                                
                            </td>                           
                            <td class="leftAligned">
                             <%# String.Format("{0:dd-MMM-yyyy}", Eval("date"))%>                       
                           <%-- </td>   
                               <td class="leftAligned">
                                <%# Eval("nationality")%>
                            </td>   --%>                      
                             <td class="leftAligned">
                                <%# Eval("rankcode")%> - <%# Eval("rank")%>
                            </td>                           
                            <td class="hideElement">
                                <%# Eval("vessel")%>
                            </td>
                            <td class="leftAligned">
                                <%# Eval("portcode")%> - <%# Eval("port")%>
                            </td>   
                            <td class="leftAligned">
                                <%# Eval("costcentercode")%> - <%# Eval("costcenter")%>
                            </td> 
                             <td class="leftAligned">
                                <%# Eval("sfStatus")%>
                            </td>  
                            <td class="leftAligned">
                                <%# Eval("status")%>
                            </td>  
                        </tr>
                    </ItemTemplate>
                    <EmptyDataTemplate>  
                    <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                        <tr>
                            <th style ="width:180px" >Name</th>   
                            <th>Onsigning</th>
                          <%--  <th>Nationality</th>--%>
                            <th>Rank</th>
                            <th>Ship</th>
                            <th>Port</th>
                            <th>Cost Center</th>
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
                <asp:DataPager ID="uolistviewPortTravelInfoPager" runat="server" 
                    PagedControlID="uolistviewPortTravelInfo" 
                    OnPreRender="uolistviewPortTravelInfoPager_PreRender">
                    <Fields>
                        <asp:NumericPagerField ButtonType="link" NumericButtonCssClass="PagerClass" />
                    </Fields>
                </asp:DataPager>   
                <asp:ObjectDataSource  ID="uoObjectDataSourcePortView" runat="server" 
                onselecting="uoObjectDataSourcePortView_Selecting" 
                MaximumRowsParameterName="MaxRow" SelectCountMethod="GetSFPortTravelDetailsCount" 
                SelectMethod="GetSFPortTravelDetailsWithCount" StartRowIndexParameterName="StartRow" 
                TypeName="TRAVELMART.BLL.SeafarerTravelBLL" 
                OldValuesParameterFormatString="oldcount_{0}" EnablePaging="True" >     
                    <SelectParameters>
                        <asp:Parameter Name="DateFrom" Type="String"/>
                        <asp:Parameter Name="DateTo" Type="String"/>
                        <asp:Parameter Name="UserID" Type="String"/>
                        <asp:Parameter Name="FilterByDate" Type="String"/>
                        <asp:Parameter Name="RegionID" Type="String"/>
                        <asp:Parameter Name="CountryID" Type="String"/>
                        <asp:Parameter Name="CityID" Type="String"/>
                        <asp:Parameter Name="Status" Type="String"/>
                        <asp:Parameter Name="FilterByNameID" Type="String"/>
                        <asp:Parameter Name="FilterNameID" Type="String"/>                        
                        <asp:Parameter Name="PortID" Type="String"/>
                        <asp:Parameter Name="VesselID" Type="String"/>
                        <asp:Parameter Name="Nationality" Type="String"/>
                        <asp:Parameter Name="Gender" Type="String"/>
                        <asp:Parameter Name="Rank" Type="String"/>
                        <asp:Parameter Name="Role" Type="String"/>
                        
                        <asp:Parameter Name="ByVessel" Type="Int16"/>
                        <asp:Parameter Name="ByName" Type="Int16"/>
                        <asp:Parameter Name="ByRecLoc" Type="Int16"/>
                        <asp:Parameter Name="ByE1ID" Type="Int16"/>
                        <asp:Parameter Name="ByDateOnOff" Type="Int16"/>
                        <asp:Parameter Name="ByStatus" Type="Int16"/>
                        <asp:Parameter Name="ByPort" Type="Int16"/>
                        <asp:Parameter Name="ByRank" Type="Int16"/>
                        <asp:Parameter Name="ByPortStatus" Type="Int16"/>
                        <asp:Parameter Name="ByNationality" Type="Int16"/>                        
                    </SelectParameters>               
                </asp:ObjectDataSource>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="uoButtonView" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:GridView ID="uoGridViewPortManifest"  runat="server" 
                    AutoGenerateColumns="false" Visible="false" 
                    ondatabound="uoGridViewPortManifest_DataBound" >
                    <Columns>
                        <asp:BoundField DataField= "RecLoc" HeaderText = "Rec Loc"/>
                        <asp:BoundField DataField= "SfID" HeaderText = "E1 ID"/>
                        <asp:BoundField DataField= "Name" HeaderText = "Name"/>
                        <asp:BoundField DataField= "date" DataFormatString="{0:dd-MMM-yyyy}" HtmlEncode="false" HeaderText = "On/Off Date"/>                        
                        <asp:BoundField DataField= "Vessel" HeaderText = "Ship"/>
                        <asp:BoundField DataField= "Rank" HeaderText = "Rank"/>
                        <asp:BoundField DataField= "port" HeaderText = "Port"/>
                        <asp:BoundField DataField= "nationality" HeaderText = "Nationality"/>
                        <asp:BoundField DataField= "costCenterName" HeaderText = "Cost Center"/>                        
                        <asp:BoundField DataField= "sfStatus" HeaderText = "Status"/>
                        <asp:BoundField DataField= "status" HeaderText = "Port Status"/>                        
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="uoHiddenFieldDateFrom" runat="server" />
    <asp:HiddenField ID="uoHiddenFieldDateTo" runat="server" />
    <asp:HiddenField ID="uoHiddenFieldFilterBy" runat="server" />
    <asp:HiddenField ID="uoHiddenFieldUser" runat="server" Value=""/>
    <asp:HiddenField ID="uoHiddenFieldRole" runat="server" Value=""/>
    
    <asp:HiddenField ID="uoHiddenFieldByVessel" runat="server" Value="1"/>
    <asp:HiddenField ID="uoHiddenFieldByName" runat="server" Value="2"/>
    <asp:HiddenField ID="uoHiddenFieldByRecLoc" runat="server" Value="0"/>
    <asp:HiddenField ID="uoHiddenFieldByE1ID" runat="server" Value="0"/>
    <asp:HiddenField ID="uoHiddenFieldByDateOnOff" runat="server" Value="0"/>
    <asp:HiddenField ID="uoHiddenFieldByStatus" runat="server" Value="0"/>
    <asp:HiddenField ID="uoHiddenFieldByPort" runat="server" Value="0"/>
    <asp:HiddenField ID="uoHiddenFieldByRank" runat="server" Value="0"/>
    <asp:HiddenField ID="uoHiddenFieldByPortStatus" runat="server" Value="0"/>
    <asp:HiddenField ID="uoHiddenFieldByNationality" runat="server" Value="0"/>
             
</asp:Content>

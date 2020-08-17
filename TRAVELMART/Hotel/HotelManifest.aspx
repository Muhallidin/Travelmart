<%@ Page Title="" Language="C#" MasterPageFile="~/TravelmartHotel.Master" AutoEventWireup="true" CodeBehind="HotelManifest.aspx.cs" Inherits="TRAVELMART.Hotel.HotelManifest" %>
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

    function SetVisibility() {
        if ($("#<%=uoCheckBoxAdvanceSearch.ClientID %>").attr('checked')) {
            $("#<%=uoTableAdvanceSearch.ClientID %>").show();
        }
        else {
            $("#<%=uoTableAdvanceSearch.ClientID %>").hide();
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

        $("#<%=uoButtonView.ClientID %>").click(function(ev) {
            if ($("#<%=uoDropDownListBranch.ClientID %>").val() == "0")
            {
                alert("Select Hotel Branch");
                return false;
            }
            if ($("#<%=uoDropDownListHours.ClientID %>").val() == "0") {
                alert("Select Manifest Type");
                return false;
            }
        });

        $("#<%=uoDropDownListBranch.ClientID %>").change(function(ev) {
            showHideBtnEmail();
        });

        $("#<%=uoDropDownListHours.ClientID %>").change(function(ev) {
            showHideBtnEmail();
        });
    }

    function showHideBtnEmail() {
        if ($("#<%=uoDropDownListBranch.ClientID %>").val() == "0" || $("#<%=uoDropDownListHours.ClientID %>").val() == "0") {
            $("#<%=uoButtonRequestSendEmail.ClientID %>").hide();
        }
        else {
            $("#<%=uoButtonRequestSendEmail.ClientID %>").show();
        }
    }
</script>

<div class="ViewTitlePadding"  >        
<table width="100%" cellpadding="0" cellspacing="0" class="PageTitle">
    <tr>
        <td>Hotel Locked Manifest </td>                           
    </tr>
</table>    
</div>

<table width="80%" class="LeftClass" >   
    <tr>
        <td class="contentCaption">Hotel Branch:</td>
        <td class="contentValue" >
            <asp:DropDownList ID="uoDropDownListBranch" runat="server" Width="300px" 
                AppendDataBoundItems="True">
                <asp:ListItem>--SELECT HOTEL--</asp:ListItem>
            </asp:DropDownList>
             <cc1:listsearchextender ID="ListSearchExtender_uoDropDownListBranch" runat="server"
                TargetControlID="uoDropDownListBranch" PromptText="Type to search" PromptPosition="Top" 
                IsSorted="true" PromptCssClass="dropdownSearch" />
        </td>        
        <td>
            &nbsp;</td>
    </tr>
    <tr >
        <td class="contentCaption">Manifest Type:</td>
        <td class="contentValue" >
            <asp:UpdatePanel runat="server" ID="uoPanelManifestType">
                <ContentTemplate>
                    <asp:DropDownList ID="uoDropDownListHours" runat="server"  AppendDataBoundItems="true" 
                        AutoPostBack="true"
                        onselectedindexchanged="uoDropDownListHours_SelectedIndexChanged" Width="300px">               
                     </asp:DropDownList>
                </ContentTemplate>
            </asp:UpdatePanel>
             
        </td>        
        <td>          
        </td>
    </tr>
    <tr>
        <td class="contentCaption">
            <asp:UpdatePanel runat="server" ID="uopanelLabelCompare" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Label runat="server" ID="uoLabelCompare" Text="Compare To:"></asp:Label>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="uoButtonView" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
        </td>
        <td class="contentValue">
            <asp:UpdatePanel runat="server" ID="uoPanelCompare" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:DropDownList ID="uoDropDownListCompare" runat="server" AppendDataBoundItems ="true"
                        Width="300px"></asp:DropDownList>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="uoButtonView" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="uoDropDownListHours" 
                        EventName="SelectedIndexChanged" />
                </Triggers>
            </asp:UpdatePanel>
            
        </td>
        <td>
            <asp:UpdatePanel runat="server" ID="uoPanelBUttonCompare">
                <ContentTemplate>
                    <asp:Button runat="server" ID="uoButtonCompare" Text="Compare"
                        CssClass="SmallButton" onclick="uoButtonCompare_Click" />
                </ContentTemplate>
            </asp:UpdatePanel>
            
        </td>
    </tr>
</table>
<table width="100%" class="LeftClass">
<tr>
    <td class="contentValue" >
        <asp:CheckBox ID="uoCheckBoxAdvanceSearch" Text="View Advance Search"
             runat="server" />
    </td>        

</tr>  
</table>
<table width="100%" class="LeftClass" id="uoTableAdvanceSearch" runat="server">   
    <tr id="uoTRVessel" runat="server">
        <td class="contentCaption">Vessel:</td>
        <td class="contentValue" >
            <asp:DropDownList ID="uoDropDownListVessel" runat="server" Width="300px" 
                AppendDataBoundItems="True">
                <asp:ListItem>--SELECT VESSEL--</asp:ListItem>
            </asp:DropDownList>
        </td>        
        <td>
            &nbsp;</td>
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
    <tr>
        <td class="contentCaption">&nbsp;</td>
        <td class="contentValue"> &nbsp;</td>
        <td >           
        </td>
    </tr>          
</table>
    
    
<table width="100%" class="LeftClass">  
    <tr>
        <td class="LeftClass" width="45%">                
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
            
                <asp:Button ID="uoButtonView" runat="server" CssClass="SmallButton" 
                    onclick="uoButtonView_Click" Text="View" />
                <asp:Button ID="uoButtonRequestSendEmail" runat="server" Text="Send Email"  OnClientClick="javascript: return confirmEmail();"
                CssClass="SmallButton" Width="100px" onclick="uoButtonRequestSendEmail_Click"/>
                
                <asp:CheckBox ID="uoCheckBoxPDF" runat="server" 
                    Text="Send as .pdf" ToolTip="When unchecked, the default is .xls" Enabled=false /> 
                                        
                &nbsp;&nbsp;
                    
                <asp:CheckBox ID="uoCheckBoxDiff" runat="server" 
                    Text="Send with the difference" Enabled="False" />
                    
            </ContentTemplate>
            </asp:UpdatePanel>
        </td>
        <td></td>
    </tr> 
    <%--<tr>   
        <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;            
            <asp:CheckBox ID="uoCheckBoxPDF" runat="server" 
                Text="Send as .pdf (default is .xls)"/>        
        </td>     
        <td></td>
    </tr>--%>
</table>

    <%# String.Format("{0:dd-MMM-yyyy}", Eval("colTimeSpanStartDate"))%> <%--<td class="leftAligned">
                                <%# String.Format("{0:dd-MMM-yyyy}", Eval("colTimeSpanEndDate"))%>
                            </td>    --%>
    <table width="100%"> 
    <tr>
        <td class="PageSubTitle">
        <asp:UpdatePanel ID="UpdatePanel" runat=server>
        <ContentTemplate>
            <asp:Label runat="server" ID="uoLabelHeader"></asp:Label>
        </ContentTemplate>
        </asp:UpdatePanel>        
        </td>
    </tr>
    <tr>
        <td valign="top">  
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>            
            <asp:ListView runat="server" ID="uolistviewHotelInfo" Enabled="true" EnableViewState="true">
                    <LayoutTemplate>
                        <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                            <tr>
                            <th>Couple</th>
                            <th>Gender</th>
                            <th>Nationality</th>
                            <th>Cost Center</th>
                            <th>Check-In</th>
                            <th>Check-Out</th>
                            <th>Name</th>
                            <th>Employee ID</th>
                            <th>Ship</th>
                            <th>Hotel Request</th>
                            <th>Single/Double</th>
                            <th>Title</th>
                            <%--<th>Hotel</th>--%>
                            <th>Hotel City</th>
                            <th>Hotel Nites</th>
                            <th>From City</th>
                            <th>To City</th>
                            <th>A/L</th>
                            <th>Record Locator</th>
                            <th>Passport No.</th>
                            <th>Issued By</th>
                            <th>Passport Expiration</th>
                            <th>Dept Date</th>
                            <th>Arvl Date</th>
                            <th>Dept City</th>
                            <th>Arvl City</th>
                            <th>Dept Time</th>
                            <th>Arvl Time</th>
                            <th>Carrier</th>
                            <th>Flight No.</th>
                            <th>Sign On</th>
                            <th>Voucher</th>
                            <th>Travel Date</th>
                            <th>Reason</th>
                            <%--<th>Rec Loc</th>
                            <th>E1 ID</th>
                            <th>Name</th>
                            <th>CheckIn</th>                           
                            <th>Room</th>
                            <th>Rank</th>
                            <th>Gender</th>
                            <th>Nationality</th>
                            <th>Cost Center</th>
                            <th>Duration</th>
                            <th>Hotel</th>
                            <th>Hotel City</th>
                            <th>Airline</th>
                            <th>From City</th>
                            <th>To City</th>--%>
                            <%--<th>Tag as Scanned</th>--%>
                            </tr>
                            <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                        </table>
                    </LayoutTemplate>
                    <ItemTemplate>
                    <%# HotelAddGroup()%> 
                        <tr>
                            <td class="leftAligned">
                                <%# Eval("Couple")%>
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
                                <%--<%# Eval("CheckIn")%>--%>
                                <%# String.Format("{0:dd-MMM-yyyy}", Eval("CheckIn"))%>
                            </td>
                            <td class="leftAligned">
                                <%--<%# Eval("CheckOut")%>--%>
                                <%# String.Format("{0:dd-MMM-yyyy}", Eval("CheckOut"))%>
                            </td>
                            <td class="leftAligned">
                                <asp:HyperLink ID="uoHyperLinkName" NavigateUrl='<%# "~/SuperUserView.aspx?ufn=" + Request.QueryString["ufn"] + "&sfId=" + Eval("EmployeeID") + "&recloc=" + Eval("RecordLocator") + "&st=" + Eval("Status") + "&ID=" + Eval("IDBigInt") + "&trID=" + Eval("TravelRequestID") + "&manualReqID=" + Eval("RequestID") + "&dt=" + Request.QueryString["dt"]%>' runat="server"><%# Eval("Name")%></asp:HyperLink>
                            </td>
                            <td class="leftAligned">
                                <%# Eval("EmployeeID")%>
                            </td> 
                            <td class="leftAligned">
                                <%# Eval("Ship")%>
                            </td>
                            <td class="leftAligned">
                                <%# Eval("HotelRequest")%>
                            </td>
                            <td class="leftAligned">
                                <%# Eval("SingleDouble")%>
                            </td>
                            <td class="leftAligned">
                                <%# Eval("Title")%>
                            </td>
                            <%--<td class="leftAligned">
                                <asp:Label ID="uoLabelHotelBranch" runat="server" Text='<%# Eval("HotelBranch") %>'></asp:Label>
                               <asp:Label ID="uoLabelWithEvent" runat="server" Text="*" Visible='<%# Eval("WithEvent") %>' ForeColor="red" Font-Bold="true" Font-Size=Large></asp:Label>
                            </td>--%>
                            <td class="leftAligned">
                                <%--<%# Eval("HotelCity")%>--%>
                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("HotelCity") %>'></asp:Label>
                                <asp:Label ID="uoLabelWithEvent" runat="server" Text="*" Visible='<%# Eval("WithEvent") %>' ForeColor="red" Font-Bold="true" Font-Size=Large></asp:Label>
                            </td>
                            <td class="leftAligned">
                                <%# Eval("HotelNites")%>
                            </td>
                            <td class="leftAligned">
                                <%# Eval("FromCity")%>
                            </td>
                            <td class="leftAligned">
                                <%# Eval("ToCity")%>
                            </td>
                            <td class="leftAligned">
                                <%# Eval("AL")%>
                            </td>
                            <td class="leftAligned">
                                <%# Eval("RecordLocator")%>
                            </td>
                            <td class="leftAligned">
                                <%# Eval("PassportNo")%>
                            </td>
                            <td class="leftAligned">
                                <%# Eval("IssuedBy")%>
                            </td>
                            <td class="leftAligned">
                                <%# Eval("PassportExpiration")%>
                            </td>
                            <td class="leftAligned">
                                <%# Eval("DeptDate")%>
                            </td>
                            <td class="leftAligned">
                                <%# Eval("ArvlDate")%>
                            </td>
                            <td class="leftAligned">
                                <%# Eval("DeptCity")%>
                            </td>
                            <td class="leftAligned">
                                <%# Eval("ArvlCity")%>
                            </td>
                            <td class="leftAligned">
                                <%# Eval("DeptTime")%>
                            </td>
                            <td class="leftAligned">
                                <%# Eval("ArvlTime")%>
                            </td>
                            <td class="leftAligned">
                                <%# Eval("Carrier")%>
                            </td>
                            <td class="leftAligned">
                                <%# Eval("FlightNo")%>
                            </td>
                            <td class="leftAligned">
                                <%--<%# Eval("SignOn")%>--%>
                                <%# String.Format("{0:dd-MMM-yyyy}", Eval("SignOn"))%>
                            </td>
                            <td class="leftAligned">
                                <%--<%# Eval("Voucher")%>--%>
                                <%# String.Format("{0:0.00}", Eval("Voucher")) %>
                            </td>
                            <td class="leftAligned">
                                <%--<%# Eval("TravelDate")%>--%>
                                <%# String.Format("{0:dd-MMM-yyyy}", Eval("TravelDate"))%>
                            </td>
                            <td class="leftAligned">
                                <%# Eval("Reason")%>
                            </td>
<%--                        <td style ="width:180px" class="leftAligned">
                                <asp:LinkButton runat="server" ID="SeafarerLinkButton" PostBackUrl='<%# "~/SuperUserView.aspx?sfId=" + Eval("sfID") + "&trID=" + Eval("colTravelReqIdInt") + "&st=" + Request.QueryString["st"] + "&dt=" + Request.QueryString["dt"] + "&p=" + Request.QueryString["p"]+ "&ufn=" + Request.QueryString["ufn"] + "&ID=" + Eval("colIdBigint") + "&from=" + Request.QueryString["from"] + "&to=" + Request.QueryString["to"]%>'><%# Eval("Name") %></asp:LinkButton>
                            </td>   --%>                                                                              
                             <%--<td class="leftAligned">
                                <%# Eval("RecLoc")%>
                            </td>
                            <td class="leftAligned">
                                <%# Eval("SfID")%>
                            </td>
                            <td class="leftAligned">                                       
                                <asp:HyperLink ID="uoHyperLinkName" NavigateUrl='<%# "~/SuperUser.aspx?ufn=" + Request.QueryString["ufn"] + "&sfId=" + Eval("SfID") + "&recloc=" + Eval("RecLoc") + "&st=" + Eval("Status") + "&ID=" + Eval("IDBigInt") + "&trID=" + Eval("TravelRequestID") + "&manualReqID=" + Eval("RequestID")%>' runat="server"><%# Eval("Name")%></asp:HyperLink>
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
                               <asp:Label ID="uoLabelHotelBranch" runat="server" Text='<%# Eval("HotelBranch") %>'></asp:Label>
                               <asp:Label ID="uoLabelWithEvent" runat="server" Text="*" Visible='<%# Eval("WithEvent") %>' ForeColor="red" Font-Bold="true" Font-Size=Large></asp:Label>
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
                            </td>--%>  
                            <%--<td></td>     --%> 
                        </tr>
                    </ItemTemplate>
                    <EmptyDataTemplate>  
                    <table class="listViewTable">
                        <tr>
                            <th>Couple</th>
                            <th>Gender</th>
                            <th>Nationality</th>
                            <th>Cost Center</th>
                            <th>Check-In</th>
                            <th>Check-Out</th>
                            <th>Name</th>
                            <th>Employee ID</th>
                            <th>Ship</th>
                            <th>Hotel Request</th>
                            <th>Single/Double</th>
                            <th>Title</th>
                            <%--<th>Hotel</th>--%>
                            <th>Hotel City</th>
                            <th>Hotel Nites</th>
                            <th>From City</th>
                            <th>To City</th>                            
                            <th>A/L</th>
                            <th>Record Locator</th>
                            <th>Passport No.</th>
                            <th>Issued By</th>
                            <th>Passport Expiration</th>
                            <th>Dept Date</th>
                            <th>Arvl Date</th>
                            <th>Dept City</th>
                            <th>Arvl City</th>
                            <th>Dept Time</th>
                            <th>Arvl Time</th>
                            <th>Carrier</th>
                            <th>Flight No.</th>
                            <th>Sign On</th>
                            <th>Voucher</th>
                            <th>Travel Date</th>
                            <th>Reason</th>
                            <%--<th>Rec Loc</th> 
                            <th>E1 ID</th>
                            <th>Name</th>                           
                            <th>CheckOut</th>
                            <th>Room</th>
                            <th>Rank</th>
                            <th>Gender</th>
                            <th>Nationality</th>
                            <th>CostCenter</th>
                            <th>Duration</th>
                            <th>Hotel</th>
                            <th>Hotel City</th>
                            <th>Airline</th>
                            <th>From City</th>
                            <th>To City</th>--%>
                           <%-- <th>Tag as Scanned</th>--%>
                        </tr>
                        <tr>
                            <td colspan="33" class="leftAligned">
                                No Record
                            </td>
                        </tr>                        
                        </table>
                    </EmptyDataTemplate>
                </asp:ListView>
               <%-- <asp:DataPager ID="uolistviewHotelInfoPager" runat="server" PagedControlID="uolistviewHotelInfo"
                     OnPreRender="uolistviewHotelInfoPager_PreRender">
                    <Fields>
                        <asp:NumericPagerField ButtonType="link" NumericButtonCssClass="PagerClass" />
                    </Fields>
                </asp:DataPager>  --%>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="uoButtonView" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>                    
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
        </tr>
        <tr runat="server">
            <td class="PageSubTitle">
                <asp:UpdatePanel runat="server" ID="uoUpdatePanelDiffHeader" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:Label runat="server" ID="uoLabelCompareHeader"><%=compareHeader %></asp:Label>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td class="Module">
                <asp:UpdatePanel runat="server" ID="uoUpdatePanelDiff" UpdateMode ="Conditional">
                    <ContentTemplate>
                        <asp:ListView runat="server" ID="uoListDiff">
                            <LayoutTemplate>
                                <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                                    <tr>
                                        <th>Couple</th>
                                        <th>Gender</th>
                                        <th>Nationality</th>
                                        <th>Cost Center</th>
                                        <th>Check-In</th>
                                        <th>Check-Out</th>
                                        <th>Name</th>
                                        <th>Employee ID</th>
                                        <th>Ship</th>
                                        <th>Hotel Request</th>
                                        <th>Single/Double</th>
                                        <th>Title</th>
                                        <%--<th>Hotel</th>--%>
                                        <th>Hotel City</th>
                                        <th>Hotel Nites</th>
                                        <th>From City</th>
                                        <th>To City</th>                            
                                        <th>A/L</th>
                                        <th>Record Locator</th>
                                        <th>Passport No.</th>
                                        <th>Issued By</th>
                                        <th>Passport Expiration</th>
                                        <th>Dept Date</th>
                                        <th>Arvl Date</th>
                                        <th>Dept City</th>
                                        <th>Arvl City</th>
                                        <th>Dept Time</th>
                                        <th>Arvl Time</th>
                                        <th>Carrier</th>
                                        <th>Flight No.</th>
                                        <th>Sign On</th>
                                        <th>Voucher</th>
                                        <th>Travel Date</th>
                                        <th>Reason</th>
                                    </tr>
                                    <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                                </table>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <tr style="text-decoration:line-through; color:Red" visible='<%# !(Convert.ToBoolean(Eval("isNew")))%>' runat="server">                                    
                                    <td class="leftAligned"><%# Eval("Couple") %></td>
                                    <td class="leftAligned"><%# Eval("Gender") %></td>
                                    <td class="leftAligned"><%# Eval("Nationality") %></td>
                                    <td class="leftAligned"><%# Eval("CostCenter") %></td>
                                    <td class="leftAligned"><%# String.Format("{0:dd-MMM-yyyy}", Eval("colCheckIn")) %></td>
                                    <td class="leftAligned"><%#  String.Format("{0:dd-MMM-yyyy}",Eval("colCheckOut")) %></td>
                                     <td class="leftAligned">
                                        <asp:HyperLink ID="HyperLink2" NavigateUrl='<%# "~/SuperUser.aspx?ufn=" + Request.QueryString["ufn"] + "&sfId=" + Eval("e1Id") + "&recloc=" + Eval("RecLoc") + "&st=" + Eval("colSFStatus") + "&ID=" + Eval("colId") + "&trID=" + Eval("colTravelReqId") + "&manualReqID=" + Eval("colReqId")%>' runat="server"><%# Eval("Name")%></asp:HyperLink>
                                    </td>
                                    <td class="leftAligned"><%# Eval("e1Id") %></td>
                                    <td class="leftAligned"><%# Eval("Ship") %></td>
                                    <td class="leftAligned"><%# Eval("HotelRequest") %></td>
                                    <td class="leftAligned"><%# Eval("colRoomNameVarchar")%></td>
                                    <td class="leftAligned"><%# Eval("colRankNameVarchar") %></td>
                                    <td class="leftAligned"><%# Eval("colCityNameVarchar") %></td>
                                    <td class="leftAligned"><%# Eval("duration") %></td>
                                    <td class="leftAligned"><%# Eval("FromCity")%></td>
                                    <td class="leftAligned"><%# Eval("ToCity")%></td>
                                    <td class="leftAligned"><%# Eval("colMarketingAirlineCodeVarchar") %></td>
                                    <td class="leftAligned"><%# Eval("RecLoc") %></td>
                                    <td class="leftAligned"><%# Eval("colPassportNoVarchar") %></td>
                                    <td class="leftAligned"><%# Eval("colPassportIssuedByVarchar") %></td>
                                    <td class="leftAligned"><%# String.Format("{0:dd-MMM-yyyy}", Eval("colPassportExpirationDate"))%></td>
                                    <td class="leftAligned"><%# String.Format("{0:dd-MMM-yyyy}", Eval("depDate"))%></td>
                                    <td class="leftAligned"><%# String.Format("{0:dd-MMM-yyyy}", Eval("arrDate"))%></td>
                                    <td class="leftAligned"><%# Eval("DeptCity")%></td>
                                    <td class="leftAligned"><%# Eval("ArvlCity")%></td>
                                    <td class="leftAligned"><%# Eval("depTime") %></td>
                                    <td class="leftAligned"><%# Eval("arrTime") %></td>
                                    <td class="leftAligned"><%# Eval("Carrier") %></td>
                                    <td class="leftAligned"><%# Eval("colFlightNoVarchar") %></td>
                                    <td class="leftAligned"><%# String.Format("{0:dd-MMM-yyyy}", Eval("colSignOnDate")) %></td>
                                    <td class="leftAligned"><%# String.Format("{0:0.00}", Eval("Voucher")) %></td>
                                    <td class="leftAligned"><%# String.Format("{0:dd-MMM-yyyy}", Eval("colTravelDate")) %></td>
                                    <td class="leftAligned"><%# Eval("colReasonCodeVarchar")%></td>
                                </tr>
                                <tr visible='<%# (Convert.ToBoolean(Eval("isNew")))%>' runat="server">                                    
                                    <td class="leftAligned"><%# Eval("Couple") %></td>
                                    <td class="leftAligned"><%# Eval("Gender") %></td>
                                    <td class="leftAligned"><%# Eval("Nationality") %></td>
                                    <td class="leftAligned"><%# Eval("CostCenter") %></td>
                                    <td class="leftAligned"><%# String.Format("{0:dd-MMM-yyyy}", Eval("colCheckIn")) %></td>
                                    <td class="leftAligned"><%#  String.Format("{0:dd-MMM-yyyy}",Eval("colCheckOut")) %></td>
                                     <td class="leftAligned">
                                        <asp:HyperLink ID="HyperLink1" NavigateUrl='<%# "~/SuperUser.aspx?ufn=" + Request.QueryString["ufn"] + "&sfId=" + Eval("e1Id") + "&recloc=" + Eval("RecLoc") + "&st=" + Eval("colSFStatus") + "&ID=" + Eval("colId") + "&trID=" + Eval("colTravelReqId") + "&manualReqID=" + Eval("colReqId")%>' runat="server"><%# Eval("Name")%></asp:HyperLink>
                                    </td>
                                    <td class="leftAligned"><%# Eval("e1Id") %></td>
                                    <td class="leftAligned"><%# Eval("Ship") %></td>
                                    <td class="leftAligned"><%# Eval("HotelRequest") %></td>
                                    <td class="leftAligned"><%# Eval("colRoomNameVarchar")%></td>
                                    <td class="leftAligned"><%# Eval("colRankNameVarchar") %></td>
                                    <td class="leftAligned"><%# Eval("colCityNameVarchar") %></td>
                                    <td class="leftAligned"><%# Eval("duration") %></td>
                                    <td class="leftAligned"><%# Eval("FromCity")%></td>
                                    <td class="leftAligned"><%# Eval("ToCity")%></td>
                                    <td class="leftAligned"><%# Eval("colMarketingAirlineCodeVarchar") %></td>
                                    <td class="leftAligned"><%# Eval("RecLoc") %></td>
                                    <td class="leftAligned"><%# Eval("colPassportNoVarchar") %></td>
                                    <td class="leftAligned"><%# Eval("colPassportIssuedByVarchar") %></td>
                                    <td class="leftAligned"><%# String.Format("{0:dd-MMM-yyyy}", Eval("colPassportExpirationDate"))%></td>
                                    <td class="leftAligned"><%# String.Format("{0:dd-MMM-yyyy}", Eval("depDate"))%></td>
                                    <td class="leftAligned"><%# String.Format("{0:dd-MMM-yyyy}", Eval("arrDate"))%></td>
                                    <td class="leftAligned"><%# Eval("DeptCity")%></td>
                                    <td class="leftAligned"><%# Eval("ArvlCity")%></td>
                                    <td class="leftAligned"><%# Eval("depTime") %></td>
                                    <td class="leftAligned"><%# Eval("arrTime") %></td>
                                    <td class="leftAligned"><%# Eval("Carrier") %></td>
                                    <td class="leftAligned"><%# Eval("colFlightNoVarchar") %></td>
                                    <td class="leftAligned"><%# String.Format("{0:dd-MMM-yyyy}", Eval("colSignOnDate")) %></td>
                                    <td class="leftAligned"><%# String.Format("{0:0.00}", Eval("Voucher")) %></td>
                                    <td class="leftAligned"><%# String.Format("{0:dd-MMM-yyyy}", Eval("colTravelDate")) %></td>
                                    <td class="leftAligned"><%# Eval("colReasonCodeVarchar")%></td>
                                </tr>
                            </ItemTemplate>
                            <EmptyDataTemplate>
                                <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                                    <tr>
                                        <th>Couple</th>
                                        <th>Gender</th>
                                        <th>Nationality</th>
                                        <th>Cost Center</th>
                                        <th>Check-In</th>
                                        <th>Check-Out</th>
                                        <th>Name</th>
                                        <th>Employee ID</th>
                                        <th>Ship</th>
                                        <th>Hotel Request</th>
                                        <th>Single/Double</th>
                                        <th>Title</th>
                                        <%--<th>Hotel</th>--%>
                                        <th>Hotel City</th>
                                        <th>Hotel Nites</th>
                                        <th>From City</th>
                                        <th>To City</th>                            
                                        <th>A/L</th>
                                        <th>Record Locator</th>
                                        <th>Passport No.</th>
                                        <th>Issued By</th>
                                        <th>Passport Expiration</th>
                                        <th>Dept Date</th>
                                        <th>Arvl Date</th>
                                        <th>Dept City</th>
                                        <th>Arvl City</th>
                                        <th>Dept Time</th>
                                        <th>Arvl Time</th>
                                        <th>Carrier</th>
                                        <th>Flight No.</th>
                                        <th>Sign On</th>
                                        <th>Voucher</th>
                                        <th>Travel Date</th>
                                        <th>Reason</th>
                                    </tr>
                                    <tr>
                                        <td colspan="33" class="leftAligned">There are no new/cancelled bookings.</td>
                                    </tr>
                                </table>
                            </EmptyDataTemplate>
                        </asp:ListView>
                         <asp:DataPager runat="server" ID="uoListDiffPager"
                            PagedControlID="uoListDiff"
                            PageSize="20">
                            <Fields>
                                <asp:NumericPagerField ButtonType ="Link" NumericButtonCssClass="PagerClass" />
                            </Fields>        
                        </asp:DataPager>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="uoButtonCompare" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table> 
    <asp:GridView ID="uoGridViewToExport" runat="server" Visible = "false">
    </asp:GridView>
    <asp:HiddenField ID="uoHiddenFieldDate" runat="server" />   
    <asp:HiddenField ID="uoHiddenFieldManifestDate" runat="server" />        
    <asp:HiddenField ID="uoHiddenFieldRole" runat="server" />    
    <asp:HiddenField ID="uoHiddenFieldEmail" runat="server" />
    <asp:HiddenField ID="uoHiddenFieldUser" runat="server" />
    
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" EnablePaging="True" 
        OldValuesParameterFormatString="oldCount_{0}" 
        SelectCountMethod="GetHotelManifestDiffCount" 
        SelectMethod="GetHotelManifestDiff" TypeName="TRAVELMART.BLL.HotelManifestBLL">
        <SelectParameters>
            <asp:ControlParameter ControlID="uoDropDownListHours" DbType="Int32" 
                DefaultValue="" Name="ManifestType" PropertyName="SelectedValue" />
            <asp:ControlParameter ControlID="uoDropDownListCompare" DbType="Int32" 
                Name="CompareManifestType" PropertyName="SelectedValue" />
            <asp:ControlParameter ControlID="uoDropDownListBranch" DbType="Int32" 
                Name="BranchId" PropertyName="SelectedValue" />
            <asp:ControlParameter ControlID="uoHiddenFieldManifestDate" DbType="Date" 
                Name="ManifesDate" PropertyName="Value" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>

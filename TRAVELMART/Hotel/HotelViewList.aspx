<%@ Page Title="" Language="C#" MasterPageFile="~/TravelmartHotel.Master" AutoEventWireup="true" CodeBehind="HotelViewList.aspx.cs" Inherits="TRAVELMART.Hotel.HotelViewList" %>
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
        function confirmLock() {
//            var e = $("#<%=uoDropDownListHotel.ClientID %>").val();
//            var e = document.getElementById("uoDropDownListHotel");
//            var strVal = e.options[e.selectedIndex].text;
            if ($("#<%=uoDropDownListHotel.ClientID %>").val() == "0") {
                alert("Select Hotel Branch");
                return false;
            }
            if (confirm("Lock record?") == true)            
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
            <td>Hotel Tentative Manifest </td>                           
        </tr>
    </table>    
</div>
<asp:UpdatePanel ID="UpdatePanel3" runat="server">
<ContentTemplate>    
<table width="100%" class="LeftClass" >   
    <tr>
        <td class="contentCaption">Hotel Branch:</td>
        <td class="contentValue" >
            <asp:DropDownList ID="uoDropDownListHotel" runat="server" Width="300px" 
                AppendDataBoundItems="True" AutoPostBack="True" 
                onselectedindexchanged="uoDropDownListHotel_SelectedIndexChanged">
                <asp:ListItem Value="0">--SELECT HOTEL--</asp:ListItem>
            </asp:DropDownList>
             <cc1:listsearchextender ID="ListSearchExtender_uoDropDownListBranch" runat="server"
                TargetControlID="uoDropDownListHotel" PromptText="Type to search" PromptPosition="Top" 
                IsSorted="true" PromptCssClass="dropdownSearch" />
             <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
            ControlToValidate="uoDropDownListHotel" Enabled="False" 
            ErrorMessage="Required Hotel" InitialValue="0">*</asp:RequiredFieldValidator>
        </td>        
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td class="contentCaption">Manifest Type:</td>
        <td class="contentValue" >
            <asp:DropDownList ID="uoDropDownListHours" runat="server" Width="300px" >
                <asp:ListItem Value="24">24 Hrs</asp:ListItem>
                <asp:ListItem Value="48">48 Hrs</asp:ListItem>
                <asp:ListItem Value="72">72 Hrs</asp:ListItem>
            </asp:DropDownList>          
        </td>        
        <td>
            &nbsp;</td>
    </tr>
</table>
<table width="100%" class="LeftClass">
    <tr>
        <td class="contentValue" >
            <asp:CheckBox ID="uoCheckBoxAdvanceSearch" Text="View Advance Search" runat="server" />
        </td>        

    </tr>  
</table>

<table width="100%" class="LeftClass" id="uoTableAdvanceSearch" runat="server">   

    <tr id="uoTRVessel" runat="server">
        <td class="contentCaption">Ship:</td>
        <td class="contentValue" >
            <asp:DropDownList ID="uoDropDownListVessel" runat="server" Width="300px" 
                AppendDataBoundItems="True">
                <asp:ListItem>--SELECT SHIP--</asp:ListItem>
            </asp:DropDownList>
        </td>        
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td class="contentCaption">Filter By:</td>
        <td class="contentValue" >
            <asp:DropDownList ID="uoDropDownListFilterBy" runat="server" Width="300px">
                <asp:ListItem Value="2">EMPLOYEE ID</asp:ListItem>
                <asp:ListItem Value="1">SEAFARER NAME</asp:ListItem>                
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
        <td class="contentValue"> <asp:Button ID="uoButtonView" runat="server" Text="View" 
                CssClass="SmallButton" onclick="uoButtonView_Click"/></td>
        <td >           
        </td>
    </tr>          
</table>
        
<table width="100%" class="LeftClass">  
    <tr>  
    <td  class="contentCaption">            
            <asp:Button ID="uoButtonView2" runat="server" CssClass="SmallButton" Visible="false"
                 Text="View" onclick="uoButtonView2_Click" />
    </td>
        <td class="LeftClass">
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <%--<asp:CheckBox ID="uoCheckBoxEnabled" runat="server" AutoPostBack="True" 
                Checked="True" oncheckedchanged="uoCheckBoxEnabled_CheckedChanged" 
                Visible="False" />
            --%>
            <asp:Button ID="uoButtonLock" runat="server"  Text="Lock" CssClass="SmallButton"  OnClientClick="javascript: return confirmLock(); "
                Width="100px" onclick="uoButtonLock_Click"/>
           <%-- <asp:Button ID="uoButtonRequestSendEmail" runat="server" Text="Send Email"  OnClientClick="javascript: return confirmEmail();"
            CssClass="SmallButton" Width="100px" onclick="uoButtonRequestSendEmail_Click" 
                Visible="False"/>--%>
           <%-- <asp:Button ID="uoButtonEmailView" runat="server" Text="Email Sent View" 
                CssClass="SmallButton" Width="100px" onclick="uoButtonEmailView_Click" 
                Visible="False"/>--%>
        </ContentTemplate>
        </asp:UpdatePanel>
        </td>
    </tr> 
    <tr>
    <td align="Left"> 
            
            &nbsp;</td>
        <td class="RightClass">
            &nbsp;</td>
    </tr> 
</table>
    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>        
    <table width="100%"> 
    <tr>
        <td valign="top">  
               
            <asp:ListView runat="server" ID="uolistviewHotelInfo" 
                DataSourceID="uoObjectDataSourceManifest" 
                ondatabound="uolistviewHotelInfo_DataBound">
                    <LayoutTemplate>
                        <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                            <tr>
                            <th>Travel Request No.</th>
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
                                <%# Eval("colTravelReqIdInt")%>
                            </td>
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
                                <%# String.Format("{0:dd-MMM-yyyy}", Eval("colTimeSpanStartDate"))%>
                            </td>
                            <td class="leftAligned">
                                <%--<%# Eval("CheckOut")%>--%>
                                <%# String.Format("{0:dd-MMM-yyyy}", Eval("colTimeSpanEndDate"))%>
                            </td>
                            <td class="leftAligned">
                                <asp:LinkButton runat="server" ID="SeafarerLinkButton" PostBackUrl='<%# "../SuperUserView.aspx?sfId=" + Eval("sfID") + "&trID=" + Eval("colTravelReqIdInt") + "&st=" + Eval("Status") + "&recloc=" + Eval("RecLoc") + "&manualReqID=" + Eval("RequestID") + "&p=" + Request.QueryString["p"]+ "&ufn=" + Request.QueryString["ufn"] + "&ID=" + Eval("IDBigInt") + "&from=" + Request.QueryString["from"] + "&to=" + Request.QueryString["to"] + "&dt=" + Request.QueryString["dt"]%>'><%# Eval("Name") %></asp:LinkButton>
                                <%--<asp:LinkButton runat="server" ID="SeafarerLinkButton" Text='<%# Eval("Name") %>' OnClick="SeafarerLinkButton_Click"></asp:LinkButton>--%>
                            </td>
                            <td class="leftAligned">
                                <%# Eval("SfID")%>
                            </td> 
                            <td class="leftAligned">
                                <%# Eval("colVesselCodeVarchar")%>
                            </td>
                            <td class="leftAligned">
                                <%# Eval("HotelRequest")%>
                            </td>
                            <td class="leftAligned">
                                <%# Eval("colRoomNameVarchar")%>
                            </td>
                            <td class="leftAligned">
                                <%# Eval("Rank")%>
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
                                <%# Eval("colTimeSpanDurationInt")%>
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
                                <%# Eval("RecLoc")%>
                            </td>
                            <td class="leftAligned">
                                <%# Eval("colPassportNoVarchar")%>
                            </td>
                            <td class="leftAligned">
                                <%# Eval("colPassportIssuedByVarchar")%>
                            </td>
                            <td class="leftAligned">                                
                                <%# String.Format("{0:dd-MMM-yyyy}", Eval("colPassportExpirationDate"))%>
                            </td>
                            <td class="leftAligned">                                
                                <%# String.Format("{0:dd-MMM-yyyy}", Eval("colDepartureDateTime"))%>
                            </td>
                            <td class="leftAligned">
                                <%# String.Format("{0:dd-MMM-yyyy}", Eval("colArrivalDateTime"))%>
                            </td>
                            <td class="leftAligned">
                                <%# Eval("DeptCity")%>
                            </td>
                            <td class="leftAligned">
                                <%# Eval("ArvlCity")%>
                            </td>
                            <td class="leftAligned">
                                <%# Eval("DepartureTime")%>
                            </td>
                            <td class="leftAligned">
                                <%# Eval("ArrivalTime")%>
                            </td>
                            <td class="leftAligned">
                                <%# Eval("Carrier")%>
                            </td>
                            <td class="leftAligned">
                                <%# Eval("colFlightNoVarchar")%>
                            </td>
                            <td class="leftAligned">                                
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
                                <asp:HiddenField ID="uoHiddenFieldsfID" runat="server" Value=""/>
                                <asp:HiddenField ID="uoHiddenFieldcolTravelReqIdInt" runat="server" Value=""/>
                                <asp:HiddenField ID="uoHiddenFieldStatus" runat="server" Value=""/>
                                <asp:HiddenField ID="uoHiddenFieldRecLoc" runat="server" Value=""/>
                                <asp:HiddenField ID="uoHiddenFieldRequestID" runat="server" Value=""/>
                                <asp:HiddenField ID="uoHiddenFieldIDBigInt" runat="server" Value=""/>
                            </td>                                                                                        
                            <%--<td style ="width:180px" class="leftAligned">
                                <asp:LinkButton runat="server" ID="SeafarerLinkButton" PostBackUrl='<%# "~/SuperUserView.aspx?sfId=" + Eval("sfID") + "&trID=" + Eval("colTravelReqIdInt") + "&st=" + Request.QueryString["st"] + "&dt=" + Request.QueryString["dt"] + "&p=" + Request.QueryString["p"]+ "&ufn=" + Request.QueryString["ufn"] + "&ID=" + Eval("colIdBigint") + "&from=" + Request.QueryString["from"] + "&to=" + Request.QueryString["to"]%>'><%# Eval("Name") %></asp:LinkButton>
                            </td>--%>                                                  
                            <%--<td class="leftAligned">
                                <%# Eval("RecLoc")%>
                            </td>
                            <td class="leftAligned">
                                <%# Eval("SfID")%>
                            </td>
                            <td class="leftAligned">                               
                                <asp:LinkButton runat="server" ID="SeafarerLinkButton" PostBackUrl='<%# "~/SuperUser.aspx?sfId=" + Eval("sfID") + "&trID=" + Eval("colTravelReqIdInt") + "&st=" + Eval("Status") + "&recloc=" + Eval("RecLoc") + "&manualReqID=" + Eval("RequestID") + "&p=" + Request.QueryString["p"]+ "&ufn=" + Request.QueryString["ufn"] + "&ID=" + Eval("IDBigInt") + "&from=" + Request.QueryString["from"] + "&to=" + Request.QueryString["to"]%>'><%# Eval("Name") %></asp:LinkButton>
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
            </td>            
        </tr>    
        <tr>
            <td class="LeftClass">
                  <asp:DataPager ID="uolistviewHotelInfoPager" runat="server" PagedControlID="uolistviewHotelInfo"
                    PageSize="20" OnPreRender="uolistviewHotelInfoPager_PreRender">
                    <Fields>
                        <asp:NumericPagerField ButtonType="link" NumericButtonCssClass="PagerClass" />
                    </Fields>
                </asp:DataPager>      
            </td>
        </tr>    
    </table> 
    </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="uoButtonView" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>   
    <%# Eval("colRoomNameVarchar")%>
    <asp:HiddenField ID="uoHiddenFieldDate" runat="server" />    
    <asp:HiddenField ID="uoHiddenFieldRole" runat="server" />
    <asp:HiddenField ID="uoHiddenFieldUser" runat="server" />
    <asp:ObjectDataSource ID="uoObjectDataSourceManifest" runat="server"                 
                MaximumRowsParameterName="MaxRow" SelectCountMethod="GetTentativeManifestCount" 
                SelectMethod="GetTentativeManifestList" StartRowIndexParameterName="StartRow" 
                TypeName="TRAVELMART.BLL.ManifestBLL" onselecting="uoObjectDataSourceManifest_Selecting"
                OldValuesParameterFormatString="oldcount_{0}" EnablePaging="True"   >
                
                <SelectParameters>
                    <asp:Parameter Name="DateFromString" Type="String"/>                    
                    <asp:Parameter Name="strUser" Type="String"/>
                    <asp:Parameter Name="DateFilter" Type="String" />                    
                    <asp:Parameter Name="ByNameOrID" Type="String" />
                    <asp:Parameter Name="filterNameOrID" Type="String" />
                    
                    <asp:Parameter Name="Nationality" Type="String" />
                    <asp:Parameter Name="Gender" Type="String" />
                    <asp:Parameter Name="Rank" Type="String" />                    
                    <asp:Parameter Name="Status" Type="String" />
                    
                    <asp:Parameter Name="Region" Type="String" />
                    <asp:Parameter Name="Country" Type="String" />
                    <asp:Parameter Name="City" Type="String" />
                    
                    <asp:Parameter Name="Port" Type="String" />
                    <asp:Parameter Name="Hotel" Type="String" />
                    <asp:Parameter Name="Vessel" Type="String" />
                    <asp:Parameter Name="UserRole" Type="String" />
                    <asp:Parameter Name="LoadType" Type="String" />                                        
                    
                </SelectParameters>
            </asp:ObjectDataSource>
</ContentTemplate>
</asp:UpdatePanel> 
</asp:Content>

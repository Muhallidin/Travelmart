<%@ Page Title="" Language="C#" MasterPageFile="~/TravelmartMaster.Master" AutoEventWireup="true" CodeBehind="HotelViewPending.aspx.cs" Inherits="TRAVELMART.Hotel.HotelViewPending" %>
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

        $("#ctl00_ContentPlaceHolder1_uolistviewHotelInfo_uoCheckBoxAll").click(function(ev) {
            var status = $(this).attr('checked');
            $('input:checkbox[name *= uoCheckBoxSelect]').attr('checked', status);
        });

        $("#<%=uoButtonApproved.ClientID %>").click(function(ev) {
            var listOfCheckbox = $('input:checkbox[name*=uoCheckBoxSelect]:checked');

            var i = 0;
            $(listOfCheckbox).each(function() {
                i++;
            });

            if (i == 0) {
                alert("No selected request!");
                return false;
            }
            else {
                return true;
            }
        });
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

<%--<script type="text/javascript" language="javascript">
    function confirmApprove() {
        if (confirm("Approve record?") == true)
            return true;
        else
            return false;
    }
</script>--%>

        <div class="ViewTitlePadding"  >        
        <table width="100%" cellpadding="0" cellspacing="0" class="PageTitle">
            <tr>
                <td>Hotel Pending List</td>                           
            </tr>
        </table>    
    </div>
    <table width="100%" class="LeftClass">
    <tr>
        <td class="contentValue" >
            <asp:CheckBox ID="uoCheckBoxAdvanceSearch" Text="View Advance Search" runat="server" />
        </td>        

    </tr>  
</table>
<table width="100%" class="LeftClass" id="uoTableAdvanceSearch" runat="server">   

<tr>
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
        <td class="contentValue"> <asp:Button ID="uoButtonView" runat="server" Text="View"  
                CssClass="SmallButton" onclick="uoButtonView_Click"/></td>
        <td>&nbsp;</td>
    </tr>    
</table>
<%--<table width="100%">
    <tr>
        <td align=left> &nbsp;</td>
    </tr> 
</table>--%>
<%--<div style="overflow:scroll; height:350px;">--%>
    <table width="100%"> 
    <tr>
        <td class="RightClass">             
            <asp:Button ID="uoButtonApproved" runat="server" Text="Approve"  
            CssClass="SmallButton" Width="70px" onclick="uoButtonApproved_Click"/>            
        </td>
    </tr>
    <tr>
        <td valign="top"> 
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>                
            <asp:ListView runat="server" ID="uolistviewHotelInfo" DataSourceID="uoObjectDataSourceHotel">
                    <LayoutTemplate>
                        <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                            <tr>  
                            <th class="hideElement">Req ID</th>        
                            <th>Is Manual</th>                 
                            <th>E1 ID</th>
                            <th>Name</th>                                                        
                            <th>On/Off Date</th>
                            <th>Checkin Date</th>
                            <th>Duration</th>
                            <th>Room</th>
                            <th>Hotel Name</th>
                            <th>Port</th>
                            <th>Rank</th>
                            <th>Status</th>                            
                            <th>
                                <asp:CheckBox ID="uoCheckBoxAll" runat="server" />
                            </th>
                            </tr>
                            <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                        </table>
                    </LayoutTemplate>
                    <ItemTemplate>
                    <%# HotelAddGroup()%> 
                        <tr> 
                            <td class="hideElement">
                                <asp:HiddenField runat="server" id="uoHiddenFieldIdBigInt" value=<%# Eval("HOTELPENDINGID") %> />
                                <asp:HiddenField runat="server" id="uoHiddenFieldBranchIDInt" value=<%# Eval("BRANCH") %> />                               
                            </td>  
                            <td>
                                <%# Eval("IsManual")%>
                            </td>
                            <td class="leftAligned">                                
                                <asp:Label ID="uoLabelE1ID" runat="server" Text='<%# Eval("E1ID")%>'></asp:Label>
                            </td>
                            <td class="leftAligned">                               
                                <%--<asp:LinkButton runat="server" ID="SeafarerLinkButton" PostBackUrl='<%# "~/SuperUser.aspx?sfId=" + Eval("sfID") + "&trID=" + Eval("TravelReqIdInt") + "&st=" + Eval("SfStatus") + "&recloc=" + Eval("RecLoc") + "&manualReqID=" + Eval("RequestID") + "&dt=" + Request.QueryString["dt"] + "&p=" + Request.QueryString["p"]+ "&ufn=" + Request.QueryString["ufn"] + "&ID=" + Eval("IDBigInt") + "&from=" + Request.QueryString["from"] + "&to=" + Request.QueryString["to"]%>'><%# Eval("Name") %></asp:LinkButton>--%>
                                <asp:LinkButton runat="server" ID="SeafarerLinkButton" PostBackUrl='<%# "~/SuperUserView.aspx?sfId=" + Eval("E1ID") +  "&trID=" + Eval("TRAVELREQUESTID") + "&st=" + Eval("sfStatus") + "&ufn=" + Request.QueryString["ufn"] + "&manualReqID=" + Eval("MANUALREQUESTID")  + "&dt=" + Request.QueryString["dt"]%>' Text='<%# Eval("NAME") %>'></asp:LinkButton>                                
                            </td>
                            <td class="leftAligned">
                                <%# String.Format("{0:dd-MMM-yyyy}", Eval("ONOFFDATE"))%>
                            </td>
                             <td class="leftAligned">
                                <asp:Label ID="uoLabelCheckinDate" runat="server" Text='<%# String.Format("{0:dd-MMM-yyyy}", Eval("CHECKINDATE"))%>'></asp:Label>                                
                            </td>
                            <td>
                                <%# Eval("HOTELNITES")%>
                            </td>
                             <td>
                                <%# Eval("colRoomNameVarchar")%>
                            </td>
                            <td class="leftAligned">
                                <asp:Label ID="uoLabelHotelName" runat="server" Text='<%# Eval("HotelName")%>'></asp:Label>
                                <asp:Label ID="uoLabelEvents" runat="server" Text="*" Font-Bold="true" Font-Size="Large" ForeColor="Red" Visible='<%# IsEventExists(Eval("BRANCH"), Eval("ONOFFDATE")) %>'></asp:Label>
                            </td>
                            <td class="leftAligned">
                                <%# Eval("Port")%>
                            </td>
                            <td class="leftAligned">
                                <%# Eval("Rank")%>
                            </td>
                            <td class="leftAligned">
                                <%# Eval("sfStatus")%>
                            </td>
                            <td>
                                <asp:CheckBox CssClass="Checkbox" ID="uoCheckBoxSelect" runat="server" />
                            </td>
                            <%--<td class='<%# (HideHotelPending()==""?"leftAligned":HideHotelPending()) %>'>
                               <asp:LinkButton ID="uoLinkButtonApprove" runat="server" OnClientClick='return confirmApprove();' 
                                    CommandArgument='<%#Eval("HotelPendingID") %>' CommandName="Approve">Approve</asp:LinkButton>
                           </td>--%>
                        </tr>
                    </ItemTemplate>
                    <EmptyDataTemplate>  
                    <table class="listViewTable">
                        <tr>   
                            <th class="hideElement">Req ID</th>   
                            <th>Is Manual</th>                     
                            <th>E1 ID</th>
                            <th>Name</th>                                                        
                            <th>Date</th>
                            <th>Checkin Date</th>
                            <th>Hotel Name</th>
                            <th>Port</th>
                            <th>Rank</th>
                            <th>Status</th>     
                            <th></th>                       
                        </tr>
                        <tr>
                            <td colspan="10" class="leftAligned">
                                No Record
                            </td>
                        </tr>                        
                        </table>
                    </EmptyDataTemplate>
                </asp:ListView>
                <asp:DataPager ID="uolistviewHotelInfoPager" runat="server" PagedControlID="uolistviewHotelInfo"
                    PageSize="10" OnPreRender="uolistviewHotelInfoPager_PreRender">
                    <Fields>
                        <asp:NumericPagerField ButtonType="link" NumericButtonCssClass="PagerClass" />
                    </Fields>
                </asp:DataPager>    
                <asp:ObjectDataSource ID="uoObjectDataSourceHotel" runat="server"
                    onselecting="uoObjectDataSourceHotel_Selecting" 
                    MaximumRowsParameterName="MaxRow" SelectCountMethod="GetSFHotelTravelPendingCount" 
                    SelectMethod="GetSFHotelTravelPending" StartRowIndexParameterName="StartRow" 
                    TypeName="TRAVELMART.BLL.SeafarerTravelBLL" 
                    OldValuesParameterFormatString="oldcount_{0}" EnablePaging="True">
                    <SelectParameters>
                        <asp:Parameter Name="DateFrom" Type="String" />
                        <asp:Parameter Name="DateTo" Type="String" />
                        <asp:Parameter Name="UserID" Type="String" />
                        <asp:Parameter Name="FilterByDate" Type="String" />
                        <asp:Parameter Name="RegionID" Type="String" />
                        <asp:Parameter Name="CountryID" Type="String" />
                        <asp:Parameter Name="CityID" Type="String" />
                        <asp:Parameter Name="Status" Type="String" />
                        <asp:Parameter Name="FilterByNameID" Type="String" />
                        <asp:Parameter Name="FilterNameID" Type="String" />
                        <asp:Parameter Name="PortID" Type="String" />
                        <asp:Parameter Name="VesselID" Type="String" />
                        <asp:Parameter Name="Nationality" Type="String" />
                        <asp:Parameter Name="Gender" Type="String" />
                        <asp:Parameter Name="Rank" Type="String" />
                        <asp:Parameter Name="Role" Type="String" />
                        
                        <asp:Parameter Name="ByVessel" Type="Int16" />
                        <asp:Parameter Name="ByName" Type="Int16" />
                        <asp:Parameter Name="ByE1ID" Type="Int16" />
                        <asp:Parameter Name="ByDateOnOff" Type="Int16" />
                        <asp:Parameter Name="ByStatus" Type="Int16" />
                        <asp:Parameter Name="ByPort" Type="Int16" />
                        <asp:Parameter Name="ByRank" Type="Int16" />
                        <asp:Parameter Name="BranchId" Type="Int32" />
                    </SelectParameters>
                </asp:ObjectDataSource>  
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="uoButtonView" EventName="Click" />
                </Triggers>
                </asp:UpdatePanel>             
            </td>
        </tr>
    </table> 
  <%--  </div>--%>
    <asp:HiddenField ID="uoHiddenFieldDate" runat="server" />        
    <asp:HiddenField ID="uoHiddenFieldDateTo" runat="server" />    
    <asp:HiddenField ID="uoHiddenFieldDateRange" runat="server" />
    
    <asp:HiddenField ID="uoHiddenFieldRole" runat="server" />
    <asp:HiddenField ID="uoHiddenFieldUser" runat="server" />
    
    <asp:HiddenField ID="uoHiddenFieldByVessel" runat="server" Value="1"/>
    <asp:HiddenField ID="uoHiddenFieldByName" runat="server" Value="2"/>
    <asp:HiddenField ID="uoHiddenFieldByE1ID" runat="server" Value="0"/>
    <asp:HiddenField ID="uoHiddenFieldByDateOnOff" runat="server" Value="0"/>
    <asp:HiddenField ID="uoHiddenFieldByStatus" runat="server" Value="0"/>
    <asp:HiddenField ID="uoHiddenFieldByPort" runat="server" Value="0"/>
    <asp:HiddenField ID="uoHiddenFieldByRank" runat="server" Value="0"/>
</asp:Content>

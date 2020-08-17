<%@ Page Title="" Language="C#" MasterPageFile="~/TravelMartMaster.Master" AutoEventWireup="true" CodeBehind="VehicleViewList.aspx.cs" Inherits="TRAVELMART.Vehicle.VehicleViewList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">      
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="ViewTitlePadding">
    <table width="100%" cellpadding="0" cellspacing="0" >
        <tr>
            <td class="PageTitle">
                Vehicle Manifest</td>
        </tr>
    </table>
</div>
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

<table width="100%" class="LeftClass">
    <tr>
        <td class="contentValue" >
            <asp:CheckBox ID="uoCheckBoxAdvanceSearch" Text="View Advance Search" runat="server" />
        </td>        

    </tr>  
</table>
<table width="100%" class="LeftClass" id="uoTableAdvanceSearch" runat="server">   

<tr  id="uoTRVessel" runat="server">
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
            <asp:TextBox ID="uoTextBoxFilter" runat="server" CssClass="TextBoxInput"  Width="300px" onkeypress="return validate(event);" ></asp:TextBox>
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
<table width="100%" class="">   
    <tr>                        
        <td class="RightClass">
            <asp:Button ID="uoButtonSendEmail" runat="server" Text="Send Email"  OnClientClick="javascript: return confirmEmail();"
            CssClass="SmallButton" Width="100px" onclick="uoButtonSendEmail_Click" 
                Visible="False"/>
        </td>        
    </tr>
</table>
<table width="100%" class="LeftClass">   
    <%--<tr>                
        <td> <asp:Button ID="uoButtonView" runat="server" Text="View"  
                CssClass="SmallButton" onclick="uoButtonView_Click"/></td>
    </tr>--%>
    <tr>
        <td align="right">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>                        
            <asp:ListView runat="server" ID="uoListViewVehicle">
                    <LayoutTemplate>
                        <table border="0" cellpadding="0" cellspacing="0" class="listViewTable" width="100%">
                            <tr>
                                <th class="hideElement">IDBigInt</th>                                
                                <th>Record Loc</th>
                                <th>E1 ID</th>
                                <th>Name</th>
                                <th>Pickup Date</th>
                                <th>Dropoff Date</th>
                                <th>Vehicle Type</th>                                
                                <%--<th>Vessel</th>--%>
                                <th>Rank</th>                                
                                <th>Gender</th>
                                <th>Nationality</th>
                                <th>Cost Center</th>
                                <th>Origin</th>
                                <th>Destination</th>                               
                            </tr>
                            <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                        </table>
                    </LayoutTemplate>
                    <ItemTemplate>  
                    <%# VehicleAddGroup()%>                        
                        <tr>
                            <td class="hideElement">
                                <asp:HiddenField runat="server" id="uoHiddenFieldIdBigInt" value="IDBigInt"/>
                            </td>
                            <td class="leftAligned">
                                 <%# Eval("RecLoc")%>
                            </td>
                            <td class="leftAligned"><%# Eval("SfID")%></td>
                            <td class="leftAligned"><asp:LinkButton runat="server" ID="SeafarerLinkButton" PostBackUrl='<%# "~/SuperUser.aspx?sfId=" + Eval("sfID") + "&trID=" + Eval("colTravelReqIdInt") + "&st=" + Eval("Status") + "&recloc=" + Eval("RecLoc") + "&manualReqID=" + Eval("RequestID")  + "&p=" + Request.QueryString["p"]+ "&ufn=" + Request.QueryString["ufn"] + "&ID=" + Eval("IDBigInt") + "&from=" + Request.QueryString["from"] + "&to=" + Request.QueryString["to"]%>'><%# Eval("Name") %></asp:LinkButton></td><%--<%# Eval("NAME")%>--%> <%--<%# "~/SuperUser.aspx?sfId=" + Eval("sfID") + "&trID=" + Eval("colTravelReqIdInt") + "&st=" + Request.QueryString["st"] + "&dt=" + Request.QueryString["dt"] + "&p=" + Request.QueryString["p"]+ "&ufn=" + Request.QueryString["ufn"] + "&ID=" + Eval("IDBigInt") + "&from=" + Request.QueryString["from"] + "&to=" + Request.QueryString["to"]%>--%>
                            <td class="leftAligned"><%# String.Format(DateFormat(Eval("IsPickUpDateTime")), Eval("PICKUPDATETIME"))%>
                            <td class="leftAligned"><%# String.Format(DateFormat(Eval("IsDropOffDateTime")), Eval("DROPOFFDATETIME"))%>
                            <td class="leftAligned"><%# Eval("VEHICLETYPE")%></td>
                            <%--<td class="leftAligned"><%# Eval("VESSEL")%></td>--%>
                            <td class="leftAligned"><%# Eval("RANK")%></td>
                            <td class="leftAligned"><%# Eval("GENDER")%></td>
                            <td class="leftAligned"><%# Eval("NATIONALITY")%></td>
                            <td class="leftAligned"><%# Eval("COSTCENTER")%></td>
                            <td class="leftAligned"><%# Eval("ORIGIN")%></td>
                            <td class="leftAligned"><%# Eval("DESTINATION")%></td>                            
                        </tr>
                    </ItemTemplate>
                    <EmptyDataTemplate>
                        <table class="listViewTable">
                            <tr>                               
                                <th>Record Loc</th>
                                <th>E1 ID</th>
                                <th>Name</th>
                                <th>Pickup Date</th>
                                <th>Dropoff Date</th>
                                <th>Vehicle Type</th>                                
                                <%--<th>Vessel</th>--%>
                                <th>Rank</th>                                
                                <th>Gender</th>
                                <th>Nationality</th>
                                <th>Cost Center</th>
                                <th>Origin</th>
                                <th>Destination</th>                               
                            </tr>
                            <tr>
                                <td colspan="12" class="leftAligned">No Record</td>                                
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                </asp:ListView>
                <asp:DataPager ID="uoListViewVehiclePager" runat="server" PagedControlID="uoListViewVehicle"
                    PageSize="10" OnPreRender="uoListViewVehicle_PreRender">
                    <Fields>
                        <asp:NumericPagerField ButtonType="link" NumericButtonCssClass="PagerClass" />
                    </Fields>
                </asp:DataPager>
            </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="uoButtonView" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
        </td>
    </tr>
    <tr>
        <td>
            <asp:GridView ID="uoGridViewVehicleManifest" runat="server" 
                AutoGenerateColumns="false" Visible="false" 
                ondatabound="uoGridViewVehicleManifest_DataBound" >
                <Columns>                                       
                        <asp:BoundField  DataField="RecLoc" HeaderText="Rec Loc"/>
                        <asp:BoundField  DataField="SfID" HeaderText="E1 ID"/>                            
                        <asp:BoundField  DataField="Name" HeaderText="Name"/>
                        <asp:BoundField  DataField="PICKUPDATETIME" HeaderText="Pickup Date" DataFormatString="{0:dd-MMM-yyyy}" HtmlEncode="false"/>
                        <asp:BoundField  DataField="DROPOFFDATETIME" HeaderText="Dropoff Date" DataFormatString="{0:dd-MMM-yyyy}" HtmlEncode="false"/>
                        <asp:BoundField  DataField="VEHICLETYPE" HeaderText="Vehicle Type"/>
                        <asp:BoundField  DataField="RANK" HeaderText="Rank"/>
                        <asp:BoundField  DataField="GENDER" HeaderText="Gender"/>
                        <asp:BoundField  DataField="NATIONALITY" HeaderText="Nationality"/>
                        <asp:BoundField  DataField="COSTCENTER" HeaderText="Cost Center"/>
                        <asp:BoundField  DataField="ORIGIN" HeaderText="Origin"/>
                        <asp:BoundField  DataField="DESTINATION" HeaderText="Destination"/>                                                                                                                                                                                
                </Columns>
            </asp:GridView>
        </td>
    </tr>
</table>
</asp:Content>


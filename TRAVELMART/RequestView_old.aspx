<%@ Page Title="" Language="C#" MasterPageFile="~/TravelmartMaster.Master" AutoEventWireup="true" CodeBehind="RequestView_old.aspx.cs" Inherits="TRAVELMART.RequestView_old" %>
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
      
      //Events
        $("#<%=uoCheckBoxAdvanceSearch.ClientID %>").click(function() {
            if ($(this).attr('checked')) {
                $("#<%=uoTableAdvanceSearch.ClientID %>").fadeIn();
            }
            else {
                $("#<%=uoTableAdvanceSearch.ClientID %>").fadeOut();
            }
        });

        $("#ctl00_ContentPlaceHolder1_uoListViewRequest_uoCheckBoxAll").click(function(ev) {
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

        $("#<%=uoDropDownListFilterBy.ClientID %>").change(function(ev) {
            if ($(this).val() != "1") {
                $("#<%=uoTextBoxFilter.ClientID %>").val("");
            }
        });
    }

    function OpenRequestEditor(id) {       
        var screenWidth = screen.availwidth;
        var screenHeight = screen.availheight;

        screenWidth = 1060;
        screenHeight = 600;

        window.open('RequestEditor.aspx?id=' + id, 'Hotel_Vehicle_Request', 'top=10, left=200, width=' + screenWidth + ', height=' + screenHeight + ', scrollbars=yes, menubar=no,toolbar=no,status=no,resizable=yes,addressbar=no');
        return false;
    }

    function RefreshPageFromPopup() {
        $("#<%=uoHiddenFieldPopEditor.ClientID %>").val(1);        
        $("#aspnetForm").submit();
    }

    function OpenRequestEditorNew() {
        var screenWidth = screen.availwidth;
        var screenHeight = screen.availheight;

        screenWidth = 1060;
        screenHeight = 600;

        window.open('RequestEditor.aspx?id=0&r=1', 'Hotel_Vehicle_Request', 'top=10, left=200, width=' + screenWidth + ', height=' + screenHeight + ', scrollbars=yes, menubar=no,toolbar=no,status=no,resizable=yes,addressbar=no');
        return false;
    }
</script>


<div class="ViewTitlePadding">
    <table width="100%" cellpadding="0" cellspacing="0" >
        <tr>
            <td class="PageTitle">
                Hotel/Vehicle Manual Request </td>
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
        <td class="contentCaption">Group By:</td>
        <td class="contentValue">
            <asp:DropDownList ID="uoDropDownListGroupBy" runat="server" Width="305px" 
                AppendDataBoundItems="True">
                <asp:ListItem Value="VesselName">SHIP</asp:ListItem>
                <asp:ListItem Value="HotelName">HOTEL</asp:ListItem>
            </asp:DropDownList>
        </td>      
    </tr>
    <tr>
        <td class="contentCaption">Filter By:</td>
        <td class="contentValue" >
            <asp:DropDownList ID="uoDropDownListFilterBy" runat="server" Width="300px">
                <asp:ListItem Value="1">SEAFARER NAME</asp:ListItem>
                <asp:ListItem Value="2">EMPLOYEE ID</asp:ListItem>
            </asp:DropDownList>
        </td>   
        <td class="contentCaption"></td>     
        <td class="contentValue">
            <asp:TextBox ID="uoTextBoxFilter" runat="server" CssClass="TextBoxInput"  Width="300px"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="contentCaption"></td>
        <td class="contentValue" >
            <asp:CheckBox ID="uoCheckBoxAdvanceSearch" Text="View Advance Search" runat="server" />
        </td>        
        <td class="contentCaption"></td>
        <td class="contentValue"></td>
    </tr>       
</table>
<table width="100%" class="LeftClass" id="uoTableAdvanceSearch" runat="server">    
    <tr>
        <td class="contentCaption">Nationality:</td>
        <td class="contentValue"> <asp:DropDownList ID="uoDropDownListNationality" runat="server" Width="300px" 
                AppendDataBoundItems="True">
            <asp:ListItem>--SELECT NATIONALITY--</asp:ListItem>
        </asp:DropDownList></td>
        <td class="contentCaption"></td>
        <td class="contentValue" ></td>
    </tr>
    <tr>
        <td class="contentCaption">Gender:</td>
        <td class="contentValue"> <asp:DropDownList ID="uoDropDownListGender" runat="server" Width="300px" 
                AppendDataBoundItems="True">
            <asp:ListItem>--SELECT GENDER--</asp:ListItem>
        </asp:DropDownList></td>
        <td class="contentCaption"></td>
        <td class="contentValue" ></td>
    </tr>
    <tr>
        <td class="contentCaption">Rank:</td>
        <td class="contentValue"> <asp:DropDownList ID="uoDropDownListRank" runat="server" Width="300px" 
                AppendDataBoundItems="True">
            <asp:ListItem>--SELECT RANK--</asp:ListItem>
        </asp:DropDownList></td>
        <td class="contentCaption"></td>
        <td class="contentValue" ></td>
    </tr>
    <tr>
        <td class="contentCaption">Status:</td>
        <td class="contentValue"> <asp:DropDownList ID="uoDropDownListStatus" runat="server" Width="300px">
            <asp:ListItem Value="">--SELECT STATUS--</asp:ListItem>
            <asp:ListItem>ON</asp:ListItem>
            <asp:ListItem>OFF</asp:ListItem>
        </asp:DropDownList></td>
        <td class="contentCaption"></td>
        <td class="contentCaption" ></td>
    </tr>      
</table>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate> 
<table width="100%" class="LeftClass">   
    <tr>                
        <td>  
            <asp:Button ID="uoButtonViewAll" runat="server" Text="View All"  
                CssClass="SmallButton" onclick="uoButtonViewAll_Click" /> 
            <asp:Button ID="uoButtonView" runat="server" Text="View"  
                CssClass="SmallButton" onclick="uoButtonView_Click"/> 
            </td>
        <td class="RightClass">             
            <asp:Button ID="uoButtonRequest" runat="server" CssClass="SmallButton" 
                        OnClientClick="javascript: return OpenRequestEditorNew();" 
                        Text="Add Hotel/Vehicle Request" Width="140px" />
            <asp:Button ID="uoButtonApproved" runat="server" Text="Approve"  
            CssClass="SmallButton" Width="70px" onclick="uoButtonApproved_Click"/>            
        </td>
    </tr>
    <tr>
        <td colspan="2" align="right">                      
            <asp:ListView runat="server" ID="uoListViewRequest">
                    <LayoutTemplate>
                        <table border="0" cellpadding="0" cellspacing="0" class="listViewTable" width="100%">
                            <tr>
                                <th class="hideElement">Req ID</th>                                
                                <th>E1 ID</th>
                                <th>Name</th>
                                <th>On/Off Date</th>                                
                                <th>Port</th>
                                <th>Brand</th>                                
                                <th runat="server" id="ucTHVessel">Ship</th>
                                <th>Rank</th>                                
                                <th runat="server" id="ucTHHotel">Hotel</th>                                
                                <th>Crew Status</th>
                                <th>
                                    <asp:CheckBox ID="uoCheckBoxAll" runat="server" />
                                </th>
                                <th></th>
                            </tr>
                            <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                        </table>
                    </LayoutTemplate>                   
                    <ItemTemplate>   
                         <%# RequestViewAddGroup() %>                       
                        <tr>
                            <td class="hideElement">
                                <asp:HiddenField runat="server" id="uoHiddenFieldIdBigInt" value=<%# Eval("RequestID") %> />
                                <asp:HiddenField runat="server" id="uoHiddenFieldCountry" value=<%# Eval("colCountryIDInt") %> />
                                <asp:HiddenField runat="server" id="uoHiddenFieldBranch" value=<%# Eval("colBranchIDInt") %> />
                            </td>                          
                            <td class="leftAligned">
                                <asp:Label ID="uoLabelE1ID" runat="server" Text='<%# Eval("SfID")%>'></asp:Label>
                            </td>
                            <td class="leftAligned">
                                <asp:Label ID="uoLabelName" runat="server" Text='<%# Eval("Name")%>'></asp:Label></td>
                            <td class="leftAligned"> 
                                <asp:Label ID="uoLabelOnOffDate" runat="server" Text='<%# String.Format("{0:dd-MMM-yyyy}", Eval("OnOffDate"))%>'></asp:Label>  
                            </td>                            
                            <td class="leftAligned"><%# Eval("PortName")%></td>
                            <td class="leftAligned"><%# Eval("BrandName")%></td>                            
                            <td class='<%# (HideVessel()==""?"leftAligned":HideVessel()) %>' ><%# Eval("VesselName")%></td>
                            <td class="leftAligned"><%# Eval("RankName")%></td>
                            <td class='<%# (HideHotel()==""?"leftAligned":HideHotel()) %>' ><%# Eval("HotelName")%></td>
                            <td><%# Eval("Status")%></td>  
                            <td>
                                <asp:CheckBox CssClass="Checkbox" ID="uoCheckBoxSelect" runat="server" />
                            </td>
                            <td>
                                <asp:LinkButton ID="uoLinkButtonView" runat="server" Text = "View" OnClientClick='<%# "javascript: return OpenRequestEditor(" + Eval("RequestID")  + ");" %>'></asp:LinkButton>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <EmptyDataTemplate>
                        <table class="listViewTable">
                            <tr>                               
                                <th class="hideElement">Req ID</th>                                
                                <th>E1 ID</th>
                                <th>Name</th>
                                <th>On/Off Date</th>                                
                                <th>Port</th>
                                <th>Brand</th>                                
                                <th>Ship</th>
                                <th>Rank</th>                                
                                <th>Hotel</th>                                
                                <th>Crew Status</th>
                            </tr>
                            <tr>
                                <td colspan="10" class="leftAligned">No Record</td>                                
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                </asp:ListView>                            
        </td>
    </tr>
    <tr>
        <td class="LeftClass">
            <asp:DataPager ID="uoListViewRequestPager" runat="server" PagedControlID="uoListViewRequest"
                PageSize="20" OnPreRender="uoListViewRequest_PreRender">
                <Fields>
                    <asp:NumericPagerField ButtonType="link" NumericButtonCssClass="PagerClass" />
                </Fields>
            </asp:DataPager>
        </td>
    </tr>
</table>
 </ContentTemplate>
   <%-- <Triggers>
        <asp:AsyncPostBackTrigger ControlID="uoButtonView" EventName="Click" />
        <asp:AsyncPostBackTrigger ControlID="uoButtonViewAll" EventName="Click" />
        <asp:AsyncPostBackTrigger ControlID="uoButtonApproved" EventName="Click" />
    </Triggers>--%>
</asp:UpdatePanel>
    <asp:HiddenField ID="uoHiddenFieldPopEditor" runat="server" Value ="0" />
    <asp:HiddenField ID="uoHiddenFieldRole" runat="server" Value ="" />
    <asp:HiddenField ID="uoHiddenFieldDate" runat="server" Value ="" />    
    <asp:HiddenField ID="uoHiddenFieldDateRange" runat="server" Value ="" />
    <asp:HiddenField ID="uoHiddenFieldUser" runat="server" Value ="" />    
</asp:Content>

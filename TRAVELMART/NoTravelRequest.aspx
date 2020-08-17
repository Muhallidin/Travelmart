<%@ Page Title="" Language="C#" MasterPageFile="~/TravelmartMaster.Master" AutoEventWireup="True" CodeBehind="NoTravelRequest.aspx.cs" Inherits="TRAVELMART.NoTravelRequest" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
      <style type="text/css">
        .style1
        {
            width: 143px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type ="text/javascript" language ="javascript">
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


    <div class="PageTitle" style="width:100%;">
            No Travel Request List
    </div>
    <table width="100%" class="LeftClass" onload="opener.location.reload();">
    <tr>
        <td class="style1" >
            <asp:CheckBox ID="uoCheckBoxAdvanceSearch" Text="View Advance Search" runat="server" />
        </td>
        <td >
            
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
        <td></td>      
    </tr>
    <tr>
        <td class="contentCaption">Filter By:</td>
        <td class="contentValue" >
            <asp:DropDownList ID="uoDropDownListFilterBy" runat="server" Width="300px">
                <asp:ListItem Value="1">SEAFARER NAME</asp:ListItem>
                <asp:ListItem Value="2" Selected="True">EMPLOYEE ID</asp:ListItem>
            </asp:DropDownList>
        </td>        
        <td>
            <asp:TextBox ID="uoTextBoxFilter" runat="server" CssClass="TextBoxInput"  Width="300px" onkeypress="return validate(event);"  ></asp:TextBox>
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
        <td class="contentCaption">
            &nbsp;</td>
        <td class="contentValue">
            <asp:Button ID="uoButtonView" runat="server" CssClass="SmallButton" 
                 onclick="uoButtonView_Click" Text="View" />
        </td>
        <td>
            
        </td>
    </tr>
</table>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate> 
<table width="100%" class="LeftClass">   
    <tr>
        <td>
        <asp:ListView runat="server" ID="uoListViewTR" DataSourceID="uoObjectDataSourceTR" 
                onitemcommand="uoListViewTR_ItemCommand">
                <LayoutTemplate>
                    <table border="0" cellpadding="0" cellspacing="0" class="listViewTable" width="100%">
                        <tr>                            
                            <th><asp:LinkButton ID="uoLinkButtonHeaderE1ID" runat="server" CommandName="SortByE1">E1 ID</asp:LinkButton></th>
                            <th><asp:LinkButton ID="uoLinkButtonHeaderName" runat="server" CommandName="SortByName">Name</asp:LinkButton></th>                                
                            <th><asp:LinkButton ID="uoLinkButtonHeaderOnOff" runat="server" CommandName="SortByOnOff">On/Off Date</asp:LinkButton></th>
                            <th><asp:LinkButton ID="uoLinkButtonHeaderCrew" runat="server" CommandName="SortByStatus">Crew Status</asp:LinkButton></th>
                            <th><asp:LinkButton ID="uoLinkButtonHeaderReasonCode" runat="server" CommandName="SortByReason">Reason Code</asp:LinkButton></th>
                            
                            <th><asp:LinkButton ID="uoLinkButtonHeaderPort" runat="server" CommandName="SortByPort">Port</asp:LinkButton></th>
                            <th><asp:LinkButton ID="uoLinkButtonHeaderBrand" runat="server" CommandName="SortByBrand">Brand</asp:LinkButton></th>                            
                            <th><asp:LinkButton ID="uoLinkButtonHeaderRank" runat="server" CommandName="SortByRank">Rank</asp:LinkButton></th> 
                            <th>Nationality</th>                                                           
                        </tr>
                        <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                    </table>
                </LayoutTemplate>
                <ItemTemplate>     
                    <%# TRAddGroup() %>                     
                    <tr>                        
                         <td class="leftAligned">
                             <%# Eval("SfID")%>
                        </td>                        
                        <td class="leftAligned">
                            <asp:HyperLink ID="uoHyperLinkName" NavigateUrl='<%# "SuperUserView.aspx?ufn=" + Request.QueryString["ufn"] + "&sfId=" + Eval("SfID") + "&recloc=&st=" + Eval("Status") + "&ID=0&trID=0&manualReqID=0" + "&dt=" + Request.QueryString["dt"] + "&e1TR=0"%>' runat="server"><%# Eval("Name")%></asp:HyperLink>                            
                        </td>
                        <td class="leftAligned"><%# String.Format("{0:dd-MMM-yyyy}", Eval("DateOnOff"))%>  </td>
                        <td><%# Eval("Status")%></td>  
                        <td><%# Eval("ReasonCode")%></td>                          
                        
                        <td class="leftAligned"><%# Eval("PortCode")%> - <%# Eval("Port")%></td>
                        <td class="leftAligned"><%# Eval("Brand")%></td>                                                   
                        <td class="leftAligned"><%# Eval("RankCode")%> - <%# Eval("Rank")%></td>   
                        <td class="leftAligned"><%# Eval("NationalityName")%></td>                       
                    </tr>
                </ItemTemplate>
                <EmptyDataTemplate>
                    <table class="listViewTable">
                        <tr>                                              
                            <th>E1 ID</th>
                            <th>Name</th>
                            <th>On/Off Date</th>     
                            <th>Crew Status</th>    
                            <th>Reason Code</th>                 
                            
                            <th>Port</th>
                            <th>Brand</th>
                            <th>Ship</th>
                            <th>Rank</th>                                
                            <th>Air Status</th> 
                            <th>Nationality</th>                          
                        </tr>
                        <tr>
                            <td colspan="11" class="leftAligned">No Record</td>                                
                        </tr>
                    </table>
                </EmptyDataTemplate>
            </asp:ListView>                    
        </td>      
      </tr>  
       <tr>
        <td class="LeftClass">
            <asp:DataPager ID="uoListViewTRPager" runat="server" PagedControlID="uoListViewTR" PageSize="20" 
                onprerender="uoListViewTRPager_PreRender">
                        <Fields>
                            <asp:NumericPagerField ButtonType="link" NumericButtonCssClass="PagerClass" />
                        </Fields>
            </asp:DataPager>
            <asp:ObjectDataSource ID="uoObjectDataSourceTR" runat="server"                 
                MaximumRowsParameterName="MaxRow" SelectCountMethod="GetNoTravelRequestListCount" 
                SelectMethod="GetNoTravelRequestList" StartRowIndexParameterName="StartRow" 
                TypeName="TRAVELMART.BLL.TravelRequestBLL" 
                OldValuesParameterFormatString="oldcount_{0}" EnablePaging="True" 
                onselecting="uoObjectDataSourceTR_Selecting">                
                <SelectParameters>
                    <asp:Parameter Name="LoadType" Type="String"/>
                    <asp:Parameter Name="FromDate" Type="String"/>
                    <asp:Parameter Name="ToDate" Type="String"/>
                    <asp:Parameter Name="UserID" Type="String" />                    
                    <asp:Parameter Name="Role" Type="String" />
                    <asp:Parameter Name="OrderBy" Type="String" />
                    
                    <asp:Parameter Name="VesselID" Type="String" />
                    <asp:Parameter Name="FilterByName" Type="String" />
                    <asp:Parameter Name="SeafarerID" Type="String" />
                    
                    <asp:Parameter Name="NationalityID" Type="String" />
                    <asp:Parameter Name="Gender" Type="String" />
                    <asp:Parameter Name="RankID" Type="String" />
                    <asp:Parameter Name="Status" Type="String" />
                    
                    <asp:Parameter Name="RegionID" Type="String" />
                    <asp:Parameter Name="CountryID" Type="String" />
                    <asp:Parameter Name="CityID" Type="String" />
                    <asp:Parameter Name="PortID" Type="String" />                                      
                </SelectParameters>
            </asp:ObjectDataSource>                 
            <asp:HiddenField runat="server" ID="uoHiddenFieldOrderBy" Value = "SortByStatus" />
            <asp:HiddenField runat="server" ID="uoHiddenFieldLoadType" Value = "0" />      
            <asp:HiddenField ID="uoHiddenFieldByVessel" runat="server" Value="1"/>
           
            <asp:HiddenField ID="uoHiddenFieldByName" runat="server" Value="2"/>
            
            <asp:HiddenField ID="uoHiddenFieldByDateOnOff" runat="server" Value="0"/>            
            <asp:HiddenField ID="uoHiddenFieldByStatus" runat="server" Value="0"/>
            <asp:HiddenField ID="uoHiddenFieldByBrand" runat="server" Value="0"/>
            <asp:HiddenField ID="uoHiddenFieldByPort" runat="server" Value="0"/>
            <asp:HiddenField ID="uoHiddenFieldByRank" runat="server" Value="0"/>
           
            <asp:HiddenField ID="uoHiddenFieldUserRole" runat="server" Value=""/>
            <asp:HiddenField ID="uoHiddenFieldUser" runat="server" Value=""/>
            
            <asp:HiddenField runat="server" ID="uoHiddenFieldDate" Value = "0"/>      
        </td>
      </tr>          
    </table>
</ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="uoButtonView" EventName="Click" />        
    </Triggers>
</asp:UpdatePanel>
</asp:Content>

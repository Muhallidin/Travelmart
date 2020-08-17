<%@ Page Title="" Language="C#" MasterPageFile="~/TravelmartMaster.Master" AutoEventWireup="true" CodeBehind="CityView_old.aspx.cs" Inherits="TRAVELMART.CityView_old" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  
    <script type="text/javascript" language="javascript">
        $(document).ready(function() {
            ShowPopup();
        });

        function confirmDelete() {
            if (confirm("Delete record?") == true)
                return true;
            else
                return false;
        }
        
        function pageLoad(sender, args) {
            var isAsnycPostback = Sys.WebForms.PageRequestManager.getInstance().get_isInAsyncPostBack();
            if (isAsnycPostback) {
                ShowPopup();
                }
        }

        function ShowPopup() {
            $("a#<%=uoHyperLinkCityAdd.ClientID %>").fancybox(
                {
                    'width': '40%',
                    'height': '35%',
                    'autoScale': false,
                    'transitionIn': 'fadeIn',
                    'transitionOut': 'fadeOut',
                    'type': 'iframe',
                    'onClosed': function() {
                        var a = $("#<%=uoHiddenFieldPopupCity.ClientID %>").val();
                        if (a == '1')
                            $("#aspnetForm").submit();
                    }
                });

            $(".CityLink").fancybox(
                {
                    'width': '40%',
                    'height': '35%',
                    'autoScale': false,
                    'transitionIn': 'fadeIn',
                    'transitionOut': 'fadeOut',
                    'type': 'iframe',
                    'onClosed': function() {
                        var a = $("#<%=uoHiddenFieldPopupCity.ClientID %>").val();
                        if (a == '1')
                            $("#aspnetForm").submit();
                    }
                });
        }
                           
    </script>
    <table width="100%" id = "uotableCityList" runat="server">
        <tr>
            <td style="padding-left: 3px; padding-right: 3px;" colspan ="2">
                <div>
                    <table style="width:100%;" cellspacing="0">
                        <tr>
                            <td style="width:40%; text-align:left;" class="PageTitle">
                                <asp:Panel ID="uopanelcityhead" runat="server">
                                    City List
                                </asp:Panel>
                            </td>
                            <td style="text-align:right; background-color: #82ABB8; padding-right:3px;">
                                <a id="uoHyperLinkCityAdd" runat="server">
                                    <asp:Button ID="uoBtnCityAdd" runat="server" Text="Add" Font-Size="X-Small" />
                                </a>
                            </td>
                        </tr>
                        <tr><td colspan="2"></td></tr>
                        <tr>
                            <%--<td class="LeftClass">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                         <span class="leftAligned">Region: </span> &nbsp;
                                         <asp:DropDownList ID="uoDropDownListRegion" runat="server" 
                                            Width="250px" AutoPostBack="true" AppendDataBoundItems="true" 
                                            onselectedindexchanged="uoDropDownlListRegion_SelectedIndexChanged">
                                </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                               
                            </td>--%>
                            <td class="RightClass" colspan="2">
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                         City: 
                                        <asp:TextBox ID ="uoTextBoxSearchParam" runat="server"
                                            Font-Size="7.5pt" width="180px"></asp:TextBox>
                                        <asp:Button ID="uoButtonSearch" runat="server"
                                           Text="Search" Font-Size="X-Small" onclick="uoButtonSearch_Click" />
                                    </ContentTemplate>
                                    <%--<Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="uoCityListPager" EventName="PreRender" />
                                    </Triggers>--%>
                                </asp:UpdatePanel>
                               
                            </td>
                        </tr>
                        <%--<tr>
                            <td class="LeftClass">
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                         <span>Country: </span>
                                        <asp:DropDownList ID="uoDropDownListCountry" runat="server"
                                            Width="250px" AutoPostBack="true" AppendDataBoundItems="true" 
                                            onselectedindexchanged="uoDropDownListCountry_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="uoDropDownListRegion" 
                                            EventName="SelectedIndexChanged" />
                                    </Triggers>
                                </asp:UpdatePanel>
                               
                            </td>
                        </tr>--%>
                    </table>
                </div>
            </td>
        </tr>
        <tr>
            <td class="Module" align="right">
                <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                         <asp:ListView ID="uoCityList" runat="server" 
                            onitemcommand="uoCityList_ItemCommand" 
                            onitemdeleting="uoCityList_ItemDeleting">
                            <LayoutTemplate>
                                <table border="0" cellpadding="0" cellspacing="0"
                                    class="listViewTable">
                                    <tr>
                                        
                                        <th>
                                            City
                                        </th>
                                        <th>
                                            City Code
                                        </th>
                                        <th>
                                            Country
                                        </th>
                                        <%--<th>
                                            Region
                                        </th>--%>
                                        <th runat="server" style="width:10%" id="EditTH"></th>
                                        <th runat="server" style="width:10%" id="DeleteTH"></th>
                                    </tr>
                                    <asp:PlaceHolder runat="server" ID="itemPlaceHolder"></asp:PlaceHolder>
                                </table>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <tr>
                                    
                                    <td class="leftAligned">
                                        <%# Eval("colCityNameVarchar") %>
                                    </td>
                                    <td class="leftAligned">
                                        <%# Eval("colCityCodeVarchar") %>
                                    </td>
                                     <td class="leftAligned">
                                        <%# Eval("colCountryNameVarchar")%>
                                    </td>
                                    <%--<td class="leftAligned">
                                        <%# Eval("colMapNameVarchar")%>
                                    </td>--%>
                                    <td class="leftAligned">
                                        <a runat="server" class="CityLink" id="uoAEditCity" href='<%# "CityAdd.aspx?vcId=" + Eval("colCountryIDInt") + "&vcName=" + Eval("colCountryNameVarchar") + "&vcTId=" + Eval("colCityIDInt") + "&vcTName=" + Eval("colCityNameVarchar") + "&vcTCode=" + Eval("colCityCodeVarchar") %>'> <%--"&vrName=" + Eval("colMapNameVarchar") +--%>
                                            Edit
                                        </a>
                                    </td>
                                    <td class="leftAligned">
                                        <asp:LinkButton ID = "uoLonkButtonDelete" runat="server" OnClientClick="return confirmDelete();"
                                            Text="Delete" CommandName="Delete" CommandArgument='<%# Eval("colCityIDInt") %>'></asp:LinkButton>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <EmptyDataTemplate>
                              <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                                    <tr>
                                       
                                        <th>City</th>
                                         <th>City Code</th>
                                        <th>Country</th>
                                        <%--<th>Region</th>--%>
                                    </tr>
                                    <tr>
                                        <td colspan="6" class="leftAligned">No record</td>
                                    </tr>
                              </table>
                            </EmptyDataTemplate>
                            <SelectedItemTemplate>
                                <table>
                                    <tr>
                                        <th><%# Eval("colCityCodeVarchar") %></th>
                                    </tr>
                                </table>
                            </SelectedItemTemplate>
                        </asp:ListView>
                        <asp:DataPager ID="uoCityListPager" runat="server"
                            PagedControlID="uoCityList" PageSize="20" 
                            onprerender="uoCityListPager_PreRender">
                            <Fields>
                                <asp:NumericPagerField ButtonType="Link" 
                                    NumericButtonCssClass="PagerClass" />
                            </Fields>    
                        </asp:DataPager>
                        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" EnablePaging="True" 
                             OldValuesParameterFormatString="oldcount_{0}" 
                             SelectCountMethod="MasterfileCityViewCount" 
                             SelectMethod="MasterfileCityView" 
                             TypeName="TRAVELMART.BLL.MasterfileBLL" 
                             DeleteMethod="MasterfileCityDelete">
                            <DeleteParameters>
                                <asp:ControlParameter ControlID="uoHiddenFieldCityId" Name="pCityId" 
                                    PropertyName="Value" />
                                <asp:ControlParameter ControlID="uoHiddenFieldUser" Name="pModifiedBy" 
                                    PropertyName="Value" />
                            </DeleteParameters>
                            <SelectParameters>
                                <asp:ControlParameter ControlID="uoHiddenFieldCountryString" Name="pCountryId" 
                                    PropertyName="Value" />
                                <asp:ControlParameter ControlID="uoHiddenFieldRole" Name="RoleID" 
                                    PropertyName="Value" />
                                <asp:ControlParameter ControlID="uoHiddenFieldCity" Name="pCityName" 
                                    PropertyName="Value" />                               
                            </SelectParameters>
                         </asp:ObjectDataSource>
                    </ContentTemplate>
                    <%--<Triggers>--%>
                        <%--<asp:AsyncPostBackTrigger ControlID="uoCityListPager" EventName="PreRender" />--%>
                        <%--<asp:AsyncPostBackTrigger ControlID="uoDropDownListCountry" 
                            EventName="SelectedIndexChanged" />--%>
                        <%--<asp:AsyncPostBackTrigger ControlID="uoButtonSearch" EventName="Click" />--%>
                    <%--</Triggers>--%>
                </asp:UpdatePanel>               
            </td>
        </tr>
    </table>
     <asp:HiddenField ID="uoHiddenFieldPopupCity" runat="server" Value="0" />
     <asp:HiddenField ID="uoHiddenFieldCountryString" runat="server" Value="0" />
     <asp:HiddenField ID="uoHiddenFieldCityId" runat="server" Value="0" />
     <asp:HiddenField ID="uoHiddenFieldUser" runat="server" Value="0" />
     <asp:HiddenField ID="uoHiddenFieldRole" runat="server" Value="" />
     <asp:HiddenField runat="server" ID="uoHiddenFieldCity" Value="" />
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/TravelmartMaster.Master" AutoEventWireup="true" CodeBehind="CountryView_old.aspx.cs" Inherits="TRAVELMART.CountryView_old" %>
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
            var isAsyncPostback = Sys.WebForms.PageRequestManager.getInstance().get_isInAsyncPostBack();
            if (isAsyncPostback) {
                ShowPopup();
            }
        }

        function ShowPopup() {
            $("a#<%=uoHyperLinkCountryAdd.ClientID %>").fancybox(
                {
                    'width': '40%',
                    'height': '30%',
                    'autoScale': false,
                    'transitionIn': 'fadeIn',
                    'transitionOut': 'fadeOut',
                    'type': 'iframe',
                    'onClosed': function() {
                        var a = $("#<%=uoHiddenFieldPopupCountry.ClientID %>").val();
                        if (a == '1')
                            $("#aspnetForm").submit();
                    }
                });

            $(".CountryLink").fancybox(
                {
                    'width': '40%',
                    'height': '30%',
                    'autoScale': false,
                    'transitionIn': 'fadeIn',
                    'transitionOut': 'fadeOut',
                    'type': 'iframe',
                    'onClosed': function() {
                        var a = $("#<%=uoHiddenFieldPopupCountry.ClientID %>").val();
                        if (a == '1')
                            $("#aspnetForm").submit();
                    }
                });
        }
                           
    </script>
    
    <table width="100%" id="uotableCountryList" runat="server">
        <tr>
            <td style="padding-left: 3px; padding-right:3px" colspan="2">
                <div>
                    <table style="width: 100%" cellspacing="0">
                        <tr>
                            <td style="width: 40%; text-align:left;" class="PageTitle">
                                <asp:Panel ID="uopanelcountryhead" runat="server">
                                    Country List
                                </asp:Panel>
                            </td>
                            <td style="text-align: right; background-color: #82ABB8; padding-right: 3px;">
                                <a id="uoHyperLinkCountryAdd" runat="server">
                                    <asp:Button ID="uoBtnCountryAdd" runat="server" Text="Add" Font-Size="X-Small" />
                                </a>
                            </td>
                        </tr>
                        <tr><td colspan="2"></td></tr>
                        <tr>
                            <%--<td class="LeftClass">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <span>Region:</span>
                                        <asp:DropDownList ID="uoDropDownListRegion" runat="server" 
                                            Width="250px" AutoPostBack="true" AppendDataBoundItems="true" 
                                            onselectedindexchanged="uoDropDownListRegion_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                               
                            </td>--%>
                            <td class="RightClass" colspan="2">
                                
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                         Country:
                                         <asp:TextBox ID="uoTextBoxSearchParam" runat="server"
                                            Font-Size="7.5pt" Width="180px"></asp:TextBox>
                                          <asp:Button ID="uoButtonSearch" runat="server" Text="Search"
                                            Font-Size="X-Small" onclick="uoButtonSearch_Click" />
                                    </ContentTemplate>
                                    <%--<Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="uoCountryListPager" 
                                            EventName="PreRender" />
                                    </Triggers>--%>
                                </asp:UpdatePanel>
                               
                               
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
        <tr>
            <td class="Module" align="right">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:ListView ID="uoCountryList" 
                            onitemcommand="uoCountryList_ItemCommand" 
                            onitemdeleting="uoCountryList_ItemDeleting" runat="server" Visible="true"
                            >
                            <LayoutTemplate>
                                <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                                        <tr>
                                            <th>
                                                Country Code
                                            </th>
                                            <th>
                                                Country
                                            </th>
                                            <%--<th>
                                                Region
                                            </th>--%>
                                           
                                            <th runat="server" style="width: 10%" id="EditTh"></th>
                                            <th runat="server" style="width: 10%" id="DeleteTh"></th>
                                        </tr>
                                        <asp:PlaceHolder runat="server" ID="itemPlaceHolder"></asp:PlaceHolder>
                                </table>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td class="leftAligned">
                                        <%# Eval("colCountryCodeVarchar") %>
                                    </td>
                                    <td class="leftAligned">
                                        <%# Eval("colCountryNameVarchar") %>
                                    </td>
                                    <%--<td class="leftAligned">
                                        <%# Eval("colRegionNameVarchar")%>
                                    </td>--%>
                                     <td class="leftAligned">
                                        <%--<a runat="server" class="CountryLink" id="uoAEditCountry" href='<%# "CountryAdd.aspx?vrId=" + Eval("colRegionIDInt") + "&vrName=" + Eval("colRegionNameVarchar") + "&vcId=" + Eval("colCountryIDInt") + "&vcName=" + Eval("colCountryNameVarchar") + "&vcCode=" + Eval("colCountryCodeVarchar") %>'>
                                            Edit
                                        </a>--%>
                                        <a runat="server" class="CountryLink" id="uoAEditCountry" href='<%# "CountryAdd.aspx?vrId=" + "&vcId=" + Eval("colCountryIDInt") + "&vcName=" + Eval("colCountryNameVarchar") + "&vcCode=" + Eval("colCountryCodeVarchar") %>'>
                                            Edit
                                        </a>
                                    </td>
                                    <td class="leftAligned">
                                       <asp:LinkButton ID="uoLinkButtonDelete" runat="server" CommandArgument='<%# Eval("colCountryIDInt") %>' 
                                        Text="Delete" CommandName="Delete" OnClientClick="return confirmDelete();">
                                       </asp:LinkButton>
                                   </td>
                                </tr>
                            </ItemTemplate>
                            <EmptyDataTemplate>
                                <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                                     <tr>
                                        <th>
                                            Country Code
                                        </th>
                                        <th>
                                            Country
                                        </th>
                                       <%--<th>
                                            Region
                                       </th--%>>
                                    </tr>
                                    <tr>
                                        <td colspan="5" class="leftAligned">
                                                No Record
                                            </td>
                                    </tr>
                                </table>
                                
                            </EmptyDataTemplate>
                        </asp:ListView>
                        
                         <asp:DataPager ID="uoCountryListPager" runat="server" PagedControlID="uoCountryList"
                            PageSize="20" onprerender="uoCountryListPager_PreRender">
                            <Fields>
                                <asp:NumericPagerField ButtonType="Link" NumericButtonCssClass="PagerClass" />
                            </Fields>
                        </asp:DataPager>
                        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" EnablePaging="true"
                            OldValuesParameterFormatString="oldcount_{0}" 
                            SelectCountMethod="MasterfileCountryViewByRoleCount" 
                            SelectMethod="MasterfileCountryViewByRole" 
                            TypeName="TRAVELMART.BLL.MasterfileBLL">
                            <SelectParameters>
                               <%-- <asp:ControlParameter ControlID="uoHiddenFieldRegionId" Name="RegionID" 
                                    PropertyName="Value" />--%>
                                <asp:ControlParameter ControlID="uoHiddenFieldRole" Name="RoleID" 
                                    PropertyName="Value" />
                                <asp:ControlParameter ControlID="uoTextBoxSearchParam" Name="CountryName" 
                                    PropertyName="Text" DefaultValue="" />                                
                            </SelectParameters>
                        </asp:ObjectDataSource>
                    </ContentTemplate>
                   
                    <%--<Triggers>                        
                        <asp:AsyncPostBackTrigger ControlID="uoCountryListPager" 
                            EventName="PreRender" />
                        <asp:AsyncPostBackTrigger ControlID="uoButtonSearch" EventName="Click" />
                    </Triggers>--%>
                   
                </asp:UpdatePanel>
                
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="uoHiddenFieldPopupCountry" runat="server" Value="0" />
    <asp:HiddenField ID="uoHiddenFieldRegionId" runat="server" Value="0" />
    <asp:HiddenField ID="uoHiddenFieldRole" runat="server" Value="" />
</asp:Content>

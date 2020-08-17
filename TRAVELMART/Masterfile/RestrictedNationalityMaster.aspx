<%@ Page Title="" Language="C#" MasterPageFile="~/TravelMartMaintenanceMaster.Master"
    AutoEventWireup="true" CodeBehind="RestrictedNationalityMaster.aspx.cs" Inherits="TRAVELMART.RestrictedNationalityMaster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content runat="server" ID="Content3" ContentPlaceHolderID="HeaderContent">
    <div style="padding-left: 3px; padding-right: 3px">
        <table style="width: 100%" cellspacing="0">
            <tr class="PageTitle">
                <td style="text-align: left;" colspan="2">
                    Restricted Nationality Maintenance
                </td>
            </tr>
            <tr>
                <td colspan="2">
                </td>
            </tr>
            <tr>
                <td class="LeftClass">
                    <table>
                        <tr>
                            <td class="contentCaption">Country:</td>
                            <td class="contentValue">
                                <asp:DropDownList ID="uoDropDownListCountry" runat="server" 
                                    CssClass="TextBoxInput" Width="180px" AutoPostBack="true" 
                                    onselectedindexchanged="uoDropDownListCountry_SelectedIndexChanged" >                                
                                </asp:DropDownList> 
                                <cc1:ListSearchExtender ID="ListSearchExtender_uoDropDownListCountry" runat="server" 
                                    TargetControlID="uoDropDownListCountry" PromptText="Type to search" PromptPosition="Top"
                                    IsSorted="true" PromptCssClass="dropdownSearch" />    
                            </td>
                            <td class="contentCaption">Nationality:</td>
                            <td class="contentValue">
                                <asp:DropDownList ID="uoDropDownListNationality" runat="server" CssClass="TextBoxInput" Width="300px" >                                
                                </asp:DropDownList> 
                                <cc1:ListSearchExtender ID="ListSearchExtender1" runat="server"
                                    TargetControlID="uoDropDownListNationality" PromptText="Type to search" PromptPosition="Top"
                                    IsSorted="true" PromptCssClass="dropdownSearch" />    
                            </td>
                            <td>
                                <asp:Button ID="uoButtonAdd" runat="server" Text="Add"  Visible ="false" 
                                    onclick="uoButtonAdd_Click" CssClass="SmallButton"/>
                            </td>
                        </tr>
                    </table>
                    <%--<asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            Country:
                            <asp:TextBox ID="uoTextBoxSearchParam" runat="server" Font-Size="7.5pt" Width="180px"></asp:TextBox>
                            <asp:Button ID="uoButtonSearch" runat="server" Text="Search" Font-Size="X-Small"
                                OnClick="uoButtonSearch_Click" />
                        </ContentTemplate>
                    </asp:UpdatePanel>--%>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NaviPlaceHolder" runat="server">

    <script type="text/javascript" language="javascript">
        $(document).ready(function() {
            SetResolution();
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
                SetResolution();
                ShowPopup();
            }
        }

        function ShowPopup() {
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

                $("#<%=uoButtonAdd.ClientID %>").click(function() {
                    if ($("#<%=uoDropDownListNationality.ClientID %>").val() == "0") {
                        alert("No selected nationality!");
                        return false;
                    }
                });
        }

        function SetResolution() {
            var ht = $(window).height();

            var wd = $(window).width() * 0.90;

            if (screen.height <= 600) {
                ht = ht * 0.45;

            }
            else if (screen.height <= 720) {
                ht = ht * 0.60;
            }
            else {
                ht = ht * 0.65;
            }

            $("#Bv").height(ht);
            $("#Bv").width(wd);

        }                 
    </script>

    <div id="Bv" style="overflow: auto; width: 100%; overflow-x: auto; overflow-y: auto;">
        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <table width="98%">
                    <tr>
                        <td>
                            <asp:ListView ID="uoNationalityList" OnItemCommand="uoNationalityList_ItemCommand" OnItemDeleting="uoNationalityList_ItemDeleting"
                                runat="server" Visible="true">
                                <LayoutTemplate>
                                    <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                                        <tr>
                                            <th>
                                                Nationality Code
                                            </th>
                                            <th>
                                                Nationality
                                            </th>
                                            <th runat="server" style="width: 10%" id="DeleteTh">
                                            </th>
                                        </tr>
                                        <asp:PlaceHolder runat="server" ID="itemPlaceHolder"></asp:PlaceHolder>
                                    </table>
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td class="leftAligned">
                                            <%# Eval("NationalityCode") %>
                                        </td>
                                        <td class="leftAligned">
                                            <%# Eval("NationalityName")%>
                                        </td>
                                        <td class="leftAligned">
                                            <asp:LinkButton ID="uoLinkButtonDelete" runat="server" CommandArgument='<%# Eval("RestrictedID") %>'
                                                Text="Delete" CommandName="Delete" OnClientClick="return confirmDelete();">
                                            </asp:LinkButton>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <EmptyDataTemplate>
                                    <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                                        <tr>
                                            <th>
                                                 Nationality Code
                                            </th>
                                            <th>
                                                Nationality
                                            </th>
                                        </tr>
                                        <tr>
                                            <td colspan="5" class="leftAligned">
                                                No Record
                                            </td>
                                        </tr>
                                    </table>
                                </EmptyDataTemplate>
                            </asp:ListView>
                        </td>
                    </tr>
                  <%--  <tr>
                        <td align="left">
                            <asp:DataPager ID="uoNationalityListPager" runat="server" PagedControlID="uoNationalityList"
                                PageSize="20" OnPreRender="uoNationalityListPager_PreRender">
                                <Fields>
                                    <asp:NumericPagerField ButtonType="Link" NumericButtonCssClass="PagerClass" />
                                </Fields>
                            </asp:DataPager>
                            <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" EnablePaging="true" OldValuesParameterFormatString="oldcount_{0}"
                                SelectCountMethod="MasterfileCountryViewByRoleCount" SelectMethod="MasterfileCountryViewByRole"
                                TypeName="TRAVELMART.BLL.MasterfileBLL">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="uoHiddenFieldRole" Name="RoleID" PropertyName="Value" />
                                    <asp:ControlParameter ControlID="uoHiddenFieldSearch" Name="CountryName" PropertyName="Value"
                                        DefaultValue="" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                        </td>
                    </tr>--%>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:HiddenField ID="uoHiddenFieldPopupCountry" runat="server" Value="0" />
        <asp:HiddenField ID="uoHiddenFieldRegionId" runat="server" Value="0" />
        <asp:HiddenField ID="uoHiddenFieldRole" runat="server" Value="" />
        <asp:HiddenField runat="server" ID="uoHiddenFieldSearch" Value="" />
        <asp:HiddenField ID="uoHiddenFieldUser" runat="server" Value="" />
        
    </div>
</asp:Content>

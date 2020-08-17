<%@ Page Title="" Language="C#" MasterPageFile="~/TravelMartMaintenanceMaster.Master"
    AutoEventWireup="true" CodeBehind="Nationality.aspx.cs" Inherits="TRAVELMART.Nationality" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content runat="server" ID="Content3" ContentPlaceHolderID="HeaderContent">
    <div style="padding-left: 3px; padding-right: 3px">
        <table style="width: 100%" cellspacing="0">
            <tr class="PageTitle">
                <td style="text-align: left;" colspan="2">
                    Nationality
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
                            <td class="contentCaption">Nationality:</td>
                            <td class="contentValue">
                                <asp:TextBox ID="uoTextBoxNationality" runat="server" Width="250px" CssClass="SmallText"></asp:TextBox>
                            </td>
                            <td class="contentCaption" colspan="3">
                                <asp:Button ID="uoButtonSearch" runat="server" Text="Search"  Width="50px"
                                    onclick="uoButtonSearch_Click" CssClass="SmallButton"/>
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
            }
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
    <table width="98%">
        <tr>
            <td>
                <asp:ListView ID="uoNationalityList" OnItemCommand="uoNationalityList_ItemCommand" OnItemDeleting="uoNationalityList_ItemDeleting"
                    runat="server" Visible="true">
                    <LayoutTemplate>
                        <table border="0" cellpadding="0" cellspacing="0" class="listViewTable" width="100%" id="tblNationality">
                            <tr>
                                <th style="text-align: left; white-space: normal;">
                                    <asp:LinkButton ID="LinkButton1" runat="server" CommandName="SortByNationalityCode">Nationality Code</asp:LinkButton>                                    
                                </th>
                                <th style="text-align: left; white-space: normal;">
                                     <asp:LinkButton ID="LinkButton2" runat="server" CommandName="SortByNationalityName">Nationality Name</asp:LinkButton>
                                </th>
                                 <th style="text-align: left; white-space: normal;">
                                     <asp:LinkButton ID="LinkButton3" runat="server" CommandName="SortByOKTB">Ok to Brazil</asp:LinkButton>
                                </th>
                               <%-- <th runat="server" style="width: 10%" id="DeleteTh">
                                </th>--%>
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
                                <%# Eval("Nationality")%>
                            </td>
                             <td class="leftAligned">
                                 <%--<asp:HiddenField ID="uoHiddenFieldID" runat="server" Value='<%# Eval("NationalityID")%>'/>--%>
                                 <asp:CheckBox ID="uoCheckBoxOKTB" Checked='<%# Eval("IsOKTB")%>' runat="server" OnClick='<%# "CheckUncheckOKTB(this, " + Eval("NationalityID") + ");" %>' />
                            </td>
                           <%-- <td class="leftAligned">
                                <asp:LinkButton ID="uoLinkButtonDelete" runat="server" CommandArgument='<%# Eval("RestrictedID") %>'
                                    Text="Delete" CommandName="Delete" OnClientClick="return confirmDelete();">
                                </asp:LinkButton>
                            </td>--%>
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
                                 <th>
                                    OK to Brazil
                                </th>
                            </tr>
                            <tr>
                                <td colspan="3" class="leftAligned">
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
                <div>                    
                    <asp:DataPager ID="uoNationalityListPager" runat="server" PagedControlID="uoNationalityList"
                        PageSize="20">
                        <Fields>
                            <asp:NumericPagerField ButtonType="link" NumericButtonCssClass="PagerClass" />                                            
                        </Fields>
                    </asp:DataPager>
                </div>
             </td>
        </tr>   
    </table> 
      <asp:ObjectDataSource ID="uoObjectDataSourceNationality" runat="server" MaximumRowsParameterName="iMaxRow"
            SelectCountMethod="GetNationalityCount" SelectMethod="GetNationalityList"
            StartRowIndexParameterName="iStartRow" TypeName="TRAVELMART.BLL.MasterfileBLL"
            OldValuesParameterFormatString="oldcount_{0}" EnablePaging="True" OnSelecting="uoObjectDataSourceNationality_Selecting">
            <SelectParameters>
                <asp:Parameter Name="sFilter" Type="String" />
                <asp:Parameter Name="sSortedBy" Type="String" />                
            </SelectParameters>
        </asp:ObjectDataSource>           
        <asp:HiddenField ID="uoHiddenFieldOrderBy" runat="server" Value="" />
        <asp:HiddenField ID="uoHiddenFieldUser" runat="server" Value="" />        
    </div>
    
    <script language="javascript" type="text/javascript">
        function CheckUncheckOKTB(IsOKTB, NationalityID)
        {            
            var sUserName = $("#<%= uoHiddenFieldUser.ClientID %>").val();                       
            var sData = "{'UserId': '" + sUserName + "', 'iNationalityID': " + NationalityID +
                    ", 'IsOKTB': " + IsOKTB.checked + ", 'strPageName': '" + "Nationality.aspx" + "'}";

            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "/PageMethods.aspx/UpdateNationalityOkTB",
                data: sData,
                dataType: "json",
                success: function(data) {
                    alert("Ok To Brazil column updated!");
                }
                        ,
                error: function(objXMLHttpRequest, textStatus, errorThrown) {
                    alert(errorThrown);
                }
            });
        }
    </script>
</asp:Content>

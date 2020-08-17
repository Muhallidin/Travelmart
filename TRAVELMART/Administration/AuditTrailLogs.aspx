<%@ Page Title="" Language="C#" MasterPageFile="~/TravelMartMaster2.Master" AutoEventWireup="true"
    CodeBehind="AuditTrailLogs.aspx.cs" Inherits="TRAVELMART.Administration.AuditTrailLogs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script language="javascript" type="text/javascript">
        $(document).ready(function() {
            SetResolution();
            ShowPopup();
        });

        function pageLoad(sender, args) {
            var isAsyncPostback = Sys.WebForms.PageRequestManager.getInstance().get_isInAsyncPostBack();
            if (isAsyncPostback) {
                SetResolution();
                ShowPopup();
            }
        }

        function ShowPopup() {

        }

        function SetResolution() {
            var ht = $(window).height();
            var wd = $(window).width() * 0.90;
            if (screen.height <= 600) {
                ht = ht * 0.40;
            }
            else if (screen.height <= 720) {
                ht = ht * 0.55;
            }
            else {
                ht = ht * 0.61;
            }

            $("#Bv").height(ht);
            $("#Bv").width(wd);

        }
    </script>
</asp:Content>
<asp:Content runat="server" ID="Content3" ContentPlaceHolderID="HeaderContent">
    <div class="ViewTitlePadding">
        <table width="100%" cellpadding="0" cellspacing="0" class="PageTitle">
            <tr>
                <td>
                    Audit Trail Logs
                </td>
                <td class="RightClass">
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NaviPlaceHolder" runat="server">

 <div id="Bv" style="overflow: auto; overflow-x: auto; overflow-y: auto;">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table width="98%">
                <tr>
                    <td>
                        <asp:ListView runat="server" ID="uoListViewAuditTrail" DataSourceID="uoObjectDataSourceAuditTrail">
                            <LayoutTemplate>
                                <table border="0" cellpadding="0" cellspacing="0" class="listViewTable" width="100%">
                                    <tr>
                                        <th>
                                            Description
                                        </th>
                                        <th>
                                            Function
                                        </th>
                                        <th>
                                            Page Name
                                        </th>
                                        <th>
                                            Date
                                        </th>
                                        <th>
                                            User
                                        </th>
                                    </tr>
                                    <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                                </table>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <%-- <%# ExceptionAddGroup() %>     --%>
                                <tr>
                                    <td class="leftAligned">
                                        <%# Eval("AuditTrailDescription")%>
                                    </td>
                                    <td class="leftAligned">
                                        <%# Eval("AuditTrailFunction")%>
                                    </td>
                                    <td class="leftAligned">
                                        <%# Eval("PageName")%>
                                    </td>
                                    <td class="leftAligned">
                                        <%# String.Format("{0:dd-MMM-yyyy HHmm}", Eval("DateCreatedGMT"))%>
                                    </td>
                                    <td class="leftAligned">
                                        <%# Eval("CreatedBy")%>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <EmptyDataTemplate>
                                <table class="listViewTable">
                                    <tr>
                                        <th>
                                            Description
                                        </th>
                                        <th>
                                            Function
                                        </th>
                                        <th>
                                            Page Name
                                        </th>
                                        <th>
                                            Date
                                        </th>
                                        <th>
                                            User
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
                <tr>
                    <td align="left">
                        <asp:DataPager ID="uoListViewAuditTrailPager" runat="server" PagedControlID="uoListViewAuditTrail"
                            OnPreRender="uoListViewAuditTrail_PreRender" PageSize="20">
                            <Fields>
                                <asp:NumericPagerField ButtonType="link" NumericButtonCssClass="PagerClass" />
                            </Fields>
                        </asp:DataPager>
                        <asp:ObjectDataSource ID="uoObjectDataSourceAuditTrail" runat="server" MaximumRowsParameterName="MaxRow"
                            SelectCountMethod="GetAuditTrailCount" SelectMethod="GetAuditTrail" StartRowIndexParameterName="StartRow"
                            TypeName="TRAVELMART.BLL.AuditTrailBLL" OldValuesParameterFormatString="oldcount_{0}"
                            EnablePaging="True" OnSelecting="uoObjectDataSourceAuditTrail_Selecting">
                            <SelectParameters>
                                <asp:Parameter Name="DateFrom" Type="DateTime" />
                                <asp:Parameter Name="DateTo" Type="DateTime" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:HiddenField ID="uoHiddenFieldDate" runat="server" />
    </div>
</asp:Content>

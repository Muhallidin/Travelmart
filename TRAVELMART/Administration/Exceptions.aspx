<%@ Page Title="" Language="C#" MasterPageFile="~/TravelMartMaster2.Master" AutoEventWireup="true"
    CodeBehind="Exceptions.aspx.cs" Inherits="TRAVELMART.Exceptions" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">
    <div class="ViewTitlePadding">
        <table width="100%" cellpadding="0" cellspacing="0" class="PageTitle">
            <tr>
                <td>
                    Exception Logs
                </td>
                <td class="RightClass">
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NaviPlaceHolder" runat="server">

    <script type="text/javascript">
        $(document).ready(function() {
            SetTMResolution();

        });

        function pageLoad(sender, args) {
            var isAsyncPostback = Sys.WebForms.PageRequestManager.getInstance().get_isInAsyncPostBack();
            if (isAsyncPostback) {
                SetTMResolution();

            }
        }

        function SetTMResolution() {
            var ht = $(window).height();
            var ht2 = $(window).height();

            var wd = $(window).width() * 0.90;

            if (screen.height <= 600) {
                ht = ht * 0.22;
                ht2 = ht2 * 0.35;
                
            }
            else if (screen.height <= 720) {
                ht = ht * 0.28;
                ht2 = ht2 * 0.55;
            }
            else {
                ht = ht * 0.61;
                ht2 = ht2 * 0.61;
            }

            $("#Bv").height(ht2);
            $("#Bv").width(wd);

        }
        
        
    </script>

    <div id="Bv" style="overflow: auto; width: 100%; overflow-x: auto; overflow-y: auto;">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table width="98%">
                    <tr>
                        <td>
                            <asp:ListView runat="server" ID="uoListViewException" DataSourceID="uoObjectDataSourceException">
                                <LayoutTemplate>
                                    <table border="0" cellpadding="0" cellspacing="0" class="listViewTable" width="100%">
                                        <tr>
                                            <th>
                                                Exception
                                            </th>
                                            <th>
                                                From
                                            </th>
                                            <th>
                                                Filename
                                            </th>
                                            <th>
                                                Date
                                            </th>
                                        </tr>
                                        <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                                    </table>
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <%-- <%# ExceptionAddGroup() %>     --%>
                                    <tr>
                                        <td class="leftAligned">
                                            <%# Eval("Exception")%>
                                        </td>
                                        <td class="leftAligned">
                                            <%# Eval("ExceptionFrom")%>
                                        </td>
                                        <td class="leftAligned">
                                            <%# Eval("ExceptionFilename")%>
                                        </td>
                                        <td class="leftAligned">
                                            <%# String.Format("{0:dd-MMM-yyyy HHmm}", Eval("DateCreatedGMT"))%>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <EmptyDataTemplate>
                                    <table class="listViewTable">
                                        <tr>
                                            <th>
                                                Exception
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
                                            <td colspan="4" class="leftAligned">
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
                            <asp:DataPager ID="uoListViewExceptionPager" runat="server" PagedControlID="uoListViewException"
                                OnPreRender="uoListViewException_PreRender" PageSize="20">
                                <Fields>
                                    <asp:NumericPagerField ButtonType="link" NumericButtonCssClass="PagerClass" />
                                </Fields>
                            </asp:DataPager>
                            <asp:ObjectDataSource ID="uoObjectDataSourceException" runat="server" MaximumRowsParameterName="MaxRow"
                                SelectCountMethod="GetExceptionCount" SelectMethod="GetException" StartRowIndexParameterName="StartRow"
                                TypeName="TRAVELMART.BLL.ExceptionBLL" OldValuesParameterFormatString="oldcount_{0}"
                                EnablePaging="True" OnSelecting="uoObjectDataSourceException_Selecting">
                                <SelectParameters>
                                    <asp:Parameter Name="DateFrom" Type="DateTime" />
                                    <asp:Parameter Name="DateTo" Type="DateTime" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                            <asp:HiddenField ID="uoHiddenFieldDate" runat="server" />
                            <asp:HiddenField ID="uoHiddenFieldDateTo" runat="server" />
                            <asp:HiddenField ID="uoHiddenFieldDateRange" runat="server" />
                            <asp:HiddenField ID="uoHiddenFieldUser" runat="server" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>

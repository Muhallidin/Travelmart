<%@ Page Title="" Language="C#" MasterPageFile="~/TravelmartMaster.Master" AutoEventWireup="true" CodeBehind="Errors_old.aspx.cs" Inherits="TRAVELMART.Errors_old" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--<script type="text/javascript" src="../FBox/jquery-1.6.1.min.js"></script>
    <script type="text/javascript" src="../FBox/fancybox/jquery.mousewheel-3.0.4.pack.js"></script>
    <script type="text/javascript" src="../FBox/fancybox/jquery.fancybox-1.3.4.pack.js"></script>--%>
    
<script language="javascript" type="text/javascript">
    $(document).ready(function() {
        ShowPopup();
    });

    function pageLoad(sender, args) {
        var isAsyncPostback = Sys.WebForms.PageRequestManager.getInstance().get_isInAsyncPostBack();
        if (isAsyncPostback) {
            ShowPopup();
        }
    }

    function ShowPopup() {
       
    }
   </script>
    
<div class="ViewTitlePadding"  >        
    <table width="100%" cellpadding="0" cellspacing="0" class="PageTitle">
        <tr>
            <td>Error Logs</td>            
            <td class="RightClass">
               
            </td>
        </tr>
    </table>    
</div>
<table width="100%">
    <tr>
        <td>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>            
            <asp:ListView runat="server" ID="uoListViewError" DataSourceID="uoObjectDataSourceError">
                    <LayoutTemplate>
                        <table border="0" cellpadding="0" cellspacing="0" class="listViewTable" width="100%">
                            <tr>                                
                                <th>Error</th>
                                <th>Page Name</th>                                
                                <th>Date</th>
                                <th>User</th>                                
                            </tr>
                            <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                        </table>
                    </LayoutTemplate>
                    <ItemTemplate>     
                       <%-- <%# ErrorAddGroup() %>     --%>                
                        <tr>
                            <td class="leftAligned">
                                 <%# Eval("Error")%>
                            </td>
                             <td class="leftAligned">
                                <%# Eval("PageName")%>
                            </td>
                            <td class="leftAligned"><%# String.Format("{0:dd-MMM-yyyy HHmm}", Eval("DateCreatedGMT"))%>  </td>
                            <td class="leftAligned">
                                <%# Eval("CreatedBy")%>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <EmptyDataTemplate>
                        <table class="listViewTable">
                            <tr>                               
                                <th>Error</th>
                                <th>Page Name</th>                                
                                <th>Date</th>
                                <th>User</th> 
                            </tr>
                            <tr>
                                <td colspan="4" class="leftAligned">No Record</td>                                
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                </asp:ListView>
                <asp:DataPager ID="uoListViewErrorPager" runat="server" 
                PagedControlID="uoListViewError" 
                OnPreRender="uoListViewError_PreRender" PageSize="20">
                    <Fields>
                        <asp:NumericPagerField ButtonType="link" NumericButtonCssClass="PagerClass" />
                    </Fields>
                </asp:DataPager> 
            <asp:ObjectDataSource ID="uoObjectDataSourceError" runat="server"                 
                MaximumRowsParameterName="MaxRow" SelectCountMethod="GetErrorCount" 
                SelectMethod="GetError" StartRowIndexParameterName="StartRow" 
                TypeName="TRAVELMART.BLL.ErrorBLL" 
                OldValuesParameterFormatString="oldcount_{0}" EnablePaging="True" 
                    onselecting="uoObjectDataSourceError_Selecting">                
                
                <SelectParameters>
                    <asp:Parameter Name="DateFrom" Type="DateTime"/>
                    <asp:Parameter Name="DateTo" Type="DateTime"/>
                </SelectParameters>
                
            </asp:ObjectDataSource>
            </ContentTemplate>
            </asp:UpdatePanel>
        </td>
    </tr>
</table>
<asp:HiddenField ID="uoHiddenFieldDate" runat="server" />
</asp:Content>

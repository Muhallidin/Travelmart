<%@ Page Title="" Language="C#" MasterPageFile="~/TravelmartMaster.Master" AutoEventWireup="true" CodeBehind="AuditTrailLogs_old.aspx.cs" Inherits="TRAVELMART.Administration.AuditTrailLogs_old" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        
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
            <td>Audit Trail Logs</td>            
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
            <asp:ListView runat="server" ID="uoListViewAuditTrail" DataSourceID="uoObjectDataSourceAuditTrail">
                    <LayoutTemplate>
                        <table border="0" cellpadding="0" cellspacing="0" class="listViewTable" width="100%">
                            <tr>                                
                                <th>Description</th>
                                <th>Function</th>
                                <th>Page Name</th>
                                <th>Date</th>
                                <th>User</th>                                
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
                            <td class="leftAligned"><%# String.Format("{0:dd-MMM-yyyy HHmm}", Eval("DateCreatedGMT"))%>  </td>
                            <td class="leftAligned">
                                <%# Eval("CreatedBy")%>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <EmptyDataTemplate>
                        <table class="listViewTable">
                            <tr>                               
                                <th>Description</th>
                                <th>Function</th>
                                <th>Page Name</th>
                                <th>Date</th>
                                <th>User</th> 
                            </tr>
                            <tr>
                                <td colspan="5" class="leftAligned">No Record</td>                                
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                </asp:ListView>
                <asp:DataPager ID="uoListViewAuditTrailPager" runat="server" 
                PagedControlID="uoListViewAuditTrail" 
                OnPreRender="uoListViewAuditTrail_PreRender" PageSize="20">
                    <Fields>
                        <asp:NumericPagerField ButtonType="link" NumericButtonCssClass="PagerClass" />
                    </Fields>
                </asp:DataPager> 
            <asp:ObjectDataSource ID="uoObjectDataSourceAuditTrail" runat="server"                 
                MaximumRowsParameterName="MaxRow" SelectCountMethod="GetAuditTrailCount" 
                SelectMethod="GetAuditTrail" StartRowIndexParameterName="StartRow" 
                TypeName="TRAVELMART.BLL.AuditTrailBLL" 
                OldValuesParameterFormatString="oldcount_{0}" EnablePaging="True" 
                    onselecting="uoObjectDataSourceAuditTrail_Selecting">                
                
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

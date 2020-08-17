<%@ Page Title="" Language="C#" MasterPageFile="~/TravelmartMaster.Master" AutoEventWireup="true" CodeBehind="VendorServiceMaintenance.aspx.cs" Inherits="TRAVELMART.Maintenance.VendorServiceMaintenance" %>
<%--<%@ Register TagPrefix="cc1" Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" %>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" language="javascript">

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td class="PageTitle">Services</td>
        </tr>
        <tr><td></td></tr>
        <tr>
            <td>
                <asp:UpdatePanel runat="server" ID="uoUpdatePanelServices">
                    <ContentTemplate>
                        <asp:ListView runat="server" ID="uoServiceList">
                            <LayoutTemplate>
                                <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                                    <tr>
                                        <th>Service</th>
                                        <th>Service Code</th>
                                        <th>Remarks</th>
                                        <th runat="server" id="EditTH" style="width:10%"></th>
                                        <th runat="server" id="DeleteTH" style="width:10%"></th>
                                    </tr>
                                </table>
                            </LayoutTemplate>
                            <EmptyDataTemplate>
                                <tr>
                                    <td class="leftAligned">
                                        <%# Eval("colVendorTypeNameVarchar") %>
                                    </td>
                                    <td class="leftAligned">
                                        <%# Eval("colVendorTypeCodeVarhcar") %>
                                    </td>
                                    <td class="leftAligned">
                                        <%# Eval("colRemarksVarchar") %>
                                    </td>
                                </tr>
                            </EmptyDataTemplate>
                        </asp:ListView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
</asp:Content>

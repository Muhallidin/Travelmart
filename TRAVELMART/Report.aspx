<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Report.aspx.cs" Inherits="TRAVELMART.Report" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form2" runat="server">
    <asp:Panel ID="PanelViewPrint" runat="server">
        <div>
            &nbsp;&nbsp;&nbsp;
            <asp:Button ID="ButtonRefresh" runat="server" Font-Bold="True" 
                OnClick="ButtonRefresh_Click" Text="Refresh" Visible="False" />
            <asp:Button ID="ButtonRefreshPrint" runat="server" Font-Bold="True" 
                onclick="ButtonRefreshPrint_Click" Text="Refresh" Visible="false" />
            <asp:Button ID="ButtonPrint" runat="server" Font-Bold="True" 
                OnClick="ButtonPrint_Click" Text="Print" Visible="False" />
        </div>
    </asp:Panel>
    <asp:Panel ID="PanelReport" runat="server" Height="837px" Visible="false">
        <div>
            <rsweb:reportviewer id="ReportViewer1" runat="server" height="770px" 
                onreporterror="ReportViewer1_ReportError" width="397px">
            </rsweb:reportviewer>
        </div>
    </asp:Panel>
   </form>
<%--    <form id="form1" runat="server">
    <div>
    
    </div>
    </form>--%>
</body>
</html>

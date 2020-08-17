<%@ Page Title="" Language="C#" MasterPageFile="~/TravelmartMaster.Master" AutoEventWireup="true" CodeBehind="DashboardPortAgent.aspx.cs" Inherits="TRAVELMART.DashboardPortAgent" %>
<%@ Register TagPrefix="cc1" Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td class="PageTitle" colspan="2">Dashboard</td>
        </tr>
        <tr><td colspan="2"></td></tr>
      
        <tr>
            <td colspan="2"></td>
        </tr>
        <tr>
            <td colspan="2" class="Module">
                <asp:UpdatePanel runat="server" ID="UpdatePanelPort" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:ListView runat="server" ID="uoDashboardList" >
                            <LayoutTemplate>
                                <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                                    <tr>
                                        <th>Date</th>
                                        <th>Day</th>
                                        <th>Signing On</th>
                                        <th>Signing Off</th>
                                    </tr>
                                    <asp:PlaceHolder runat="server" ID="itemPlaceHolder"></asp:PlaceHolder>
                                </table>
                            </LayoutTemplate>
                            <ItemTemplate>
                            <%# DashboardAddGroup()%>
                                <tr>
                                     <td class="leftAligned">
                                        <%# String.Format("{0:dd-MMM-yyyy}", Eval("cDate"))%>
                                    </td>
                                    <td class="leftAligned">
                                        <%# Eval("cDay")%>
                                    </td>
                                    <td class="leftAligned">
                                        <%# Eval("SignOn")%>
                                    </td>
                                    
                                    <td class="leftAligned">
                                        <%# Eval("SignOff")%>
                                    </td>
                                </tr>
                            </ItemTemplate>
                             <EmptyDataTemplate>
                                <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                                    <tr>
                                        <th>
                                            Date
                                        </th>
                                        <th>
                                            Day
                                        </th>
                                        <th>
                                            SignOn
                                        </th>
                                        <th>
                                            SignOff
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
                    </ContentTemplate>
                </asp:UpdatePanel>                
            </td>
        </tr>
    </table>
</asp:Content>

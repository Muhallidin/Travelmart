<%@ Page Title="" Language="C#" MasterPageFile="~/Popup.Master" AutoEventWireup="true" CodeBehind="PortAgentContractMedicalView.aspx.cs" Inherits="TRAVELMART.ContractManagement.PortAgentContractMedicalView" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<table width="100%">
    <tr>
        <td colspan="2" class="PageTitle">
           Medical Service
        </td>
    </tr>
    <tr>
        <td colspan="2">
           <hr />
        </td>
    </tr>
    <tr>
        <td class="LeftClass">&nbsp;No of Days :</td>
        <td class="LeftClass">
            <asp:Label runat="server" ID="uoLabelDays"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="LeftClass">&nbsp;Rate Per Day :</td>
        <td class="LeftClass">
            <asp:Label runat="server" ID="uoLabelRate"></asp:Label>
        </td>
    </tr>
    <tr><td colspan="2"></td></tr>
    <tr><td colspan="2" class="PageTitle">Medical Transfers</td></tr> 
    <tr>
        <td colspan="2">
           <hr />
        </td>
    </tr>  
    <tr><td colspan="2"></td></tr>
    <tr>
        <td colspan="2">
            <asp:ListView runat="server" ID="uoTransferList" >
                <LayoutTemplate>
                    <table border="0" cellpadding="0" cellspacing="0" class="listViewTable">
                        <tr>
                            <th>Rate</th>
                            <th>Currency</th>
                            <th>Origin</th>
                            <th>Destination</th>
                            <th>Remarks</th>
                        </tr>
                        <asp:PlaceHolder runat="server" ID="itemPlaceHolder"></asp:PlaceHolder>
                    </table>
                </LayoutTemplate>
                <ItemTemplate>
                    <tr>
                        <td class="leftAligned">
                            <%# Eval("colServiceRateMoney") %>
                        </td>
                        <td class="leftAligned">
                            <%# Eval("colCurrencyNameVarchar")%>
                        </td>
                        <td class="leftAligned">
                            <%# Eval("colOriginVarchar") %>
                        </td>
                        <td class="leftAligned">
                            <%# Eval("colDestinationVarchar") %>
                        </td>
                        <td class="leftAligned">
                            <%# Eval("colRemarksVarchar") %>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:ListView>
        </td>
    </tr>
  
</table>
</asp:Content>

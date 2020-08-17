<%@ Page Title="" Language="C#" MasterPageFile="~/Popup.Master" AutoEventWireup="true" CodeBehind="UserRegionAdd.aspx.cs" Inherits="TRAVELMART.UserRegionAdd" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src="../FBox/jquery-1.6.1.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript" language="javascript">
        $(document).ready(function() {
            $("#<%=uoButtonSave.ClientID %>").click(function() {
                if ($("#<%=uoDropDownListRegion.ClientID %>").val() == "0") {
                    alert("Select Region");
                    return false;
                }
            });
        });
    </script>
        
    <div class="PageTitle">
        <asp:Label ID="ucLabelTitle" runat="server" Font-Bold="True" Text="Add User Region"></asp:Label>
    </div>
    <hr/>
    <table>
        <tr>
            <td>
                Region:
            </td>
            <td>
                <asp:DropDownList ID="uoDropDownListRegion" runat="server" Width="300px" 
                    DataTextField="colRegionNameVarchar"
                    DataValueField="colRegionIDInt"
                    AppendDataBoundItems="True" >
                </asp:DropDownList>
            </td>
            <td>
                <asp:Button ID="uoButtonSave" runat="server" Text="Save" 
                    onclick="uoButtonSave_Click" />
            </td>
        </tr>
    </table>
    </asp:Content>

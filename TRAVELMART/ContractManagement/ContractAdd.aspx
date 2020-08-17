<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ContractAdd.aspx.cs" Inherits="TRAVELMART.ContractManagement.ContractAdd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table>
            <tr>
                <td colspan="2">
                </td>
            </tr>
            <tr>
                <td colspan="2">
                </td>
            </tr>
            <tr>
                <td>
                    Contract Title :
                </td>
                <td>
                    <asp:TextBox ID="uotextboxContractTitle" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Category :
                </td>
                <td>
                    <asp:DropDownList ID="uodropdownCategory" runat="server">
                        <asp:ListItem Value="0" Text="[Select Category]" />
                        <asp:ListItem Value="Vehicle" Text="Vehicle" />
                        <asp:ListItem Value="Hotel" Text="Hotel" />
                        <asp:ListItem Value="Port" Text="Port" />
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    Vendor :
                </td>
                <td>
                    <asp:DropDownList ID="uodropdownVendor" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    Cost :
                </td>
                <td>
                    <asp:TextBox ID="uotextboxCost" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Contract Start Date :
                </td>
                <td>
                    <asp:TextBox ID="uotextboxStartDate" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Contract End Date :
                </td>
                <td>
                    <asp:TextBox ID="uotextboxEndDate" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Remarks :
                </td>
                <td>
                    <asp:TextBox ID="uotextboxRemarks" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Button ID="uobuttonSave" runat="server" Text="Save" 
                        onclick="uobuttonSave_Click" />
                    <asp:Button ID="uobuttonCancel" runat="server" Text="Cancel" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>

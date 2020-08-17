<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddRole.aspx.cs"
    Inherits="TRAVELMART.AddRole" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="~/App_Themes/Default/Stylesheet.css" rel="stylesheet" type="text/css" />
</head>
</html>
<body>
    <form id="form1" runat="server">
    <div class="PageTitle">
        User Role
    </div>
    <hr />
    <table>
        <tr>
            <td colspan="2">
            </td>
        </tr>
        <tr>
            <td>
                Role Name :
            </td>
            <td>
                <asp:TextBox ID="uotextboxRoleName" runat="server" Width="300px" />
                <asp:RequiredFieldValidator ID="uorfvUname" runat="server" ControlToValidate="uotextboxRoleName"
                    ErrorMessage="Role Name is required." ToolTip="Role Name is required." ValidationGroup="Save">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                Role Description :
            </td>
            <td>
                <asp:TextBox ID="uotextboxRoleDesc" runat="server" Width="300px" 
                    TextMode="MultiLine" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td align="left">
                <asp:Button ID="uobuttonSave" runat="server" Text="Save" OnClick="uobuttonSave_Click"
                    Width="59px" ValidationGroup="Save" /><asp:HiddenField ID="uohiddenRoleId" runat="server" />
            </td>
        </tr>
    </table>
    </form>
</body>

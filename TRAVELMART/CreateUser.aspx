<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreateUser.aspx.cs" Inherits="TRAVELMART.CreateUser" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Travel Mart - Login</title>

    <script type="text/javascript">
        function ToUpper(ctrl) {
            var t = ctrl.value;
            ctrl.value = t.toUpperCase();
        }

    </script>

</head>
<body style="background-image: url('Images/gradient.jpg'); background-repeat: repeat-x;
    font-family: Arial, Helvetica, sans-serif; font-size: 12px; color: #333;">
    <div style="text-align: center; margin: auto;">
        <center>
            <br />
            <br />
            <br />
            <br />
            <asp:Panel ID="Panel3" runat="server" Style="margin: auto;">
                <form id="form1" runat="server">
                <div style="text-align: center; margin: auto;">
                    <div style="color: White; margin-left: 15px; padding-top: 5px; font-family: Arial;
                        font-weight: bold; font-size: 32px; margin-bottom: 15px">
                        <span style="color: #0099CC">Travel Mart</span><span style="color: #999"></span>
                    </div>
                    <asp:Panel ID="Panel2" runat="server" Style="color: #D8000C; margin: auto; margin-bottom: 10px;"
                        Visible="false">
                        <img src="Images/exclamation.png" />&nbsp;&nbsp;<asp:Label ID="LabelLoginErrorDetails"
                            runat="server" Text=""></asp:Label>
                    </asp:Panel>
                    <div>
                        <center>
                            <asp:Panel ID="Panel1" runat="server" Style="background-color: White; border: 1px solid #000000;"
                                Width="398px">
                                <div style="padding: 15px; text-align: center; margin-bottom: 5px;">
                                    <div style="padding: 0px 0px 10px 5px; font-size: 15px; font-weight: bold;">
                                        CREATE USER:</div>
                                    <div style="padding: 5px; margin-bottom: 5px">
                                        <table>
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="uclabelLName" runat="server" Text="Last Name:" />
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="uotextboxLName" runat="server" Width="220px" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="uclabelFName" runat="server" Text="First Name:" />
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="uotextboxFName" runat="server" Width="220px" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="uclabel" runat="server" Text="Email Address:" />
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="uotextboxEmail" runat="server" Width="220px" /><asp:RequiredFieldValidator
                                                        ID="uorfvEmail" runat="server" ControlToValidate="uotextboxEmail" ErrorMessage="Email is required."
                                                        ToolTip="Email is required." ValidationGroup="Create">*</asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="uoregexvalEmail" ControlToValidate="uotextboxEmail"
                                                        runat="server" ErrorMessage="*" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                        ValidationGroup="Create" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="uclabelRole" runat="server" Text="User Role:" />
                                                </td>
                                                <td align="left">
                                                    <asp:DropDownList ID="uodropdownlistRole" runat="server" Width="225px">
                                                        <asp:ListItem Value="" Text="[Select Role]" />
                                                        <asp:ListItem Value="administrator" Text="[Administrator]" />
                                                        <asp:ListItem Value="Hotel Specialist" Text="[Hotel Specialist]" />
                                                        <asp:ListItem Value="Port Specialist" Text="[Port Specialist]" />
                                                        <asp:ListItem Value="Vehicle Specialist" Text="[Vehicle Specialist]" />
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="uorfvRole" runat="server" ControlToValidate="uodropdownlistRole"
                                                        ErrorMessage="Role is required." ToolTip="Email is required." ValidationGroup="Create"
                                                        InitialValue="0">*</asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="uclabelUName" runat="server" Text="User Name:" />
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="uotextboxUName" runat="server" Width="220px" /><asp:RequiredFieldValidator
                                                        ID="uorfvUname" runat="server" ControlToValidate="uotextboxUName" ErrorMessage="User Name is required."
                                                        ToolTip="User Name is required." ValidationGroup="Create">*</asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="uclabelPWD" runat="server" Text="Password:" />
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="uotextboxPWD" runat="server" Width="220px" TextMode="Password" /><asp:RequiredFieldValidator
                                                        ID="uorfvPWD" runat="server" ControlToValidate="uotextboxPWD" ErrorMessage="Password is required."
                                                        ToolTip="Password is required." ValidationGroup="Create">*</asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="uclabelPWD1" runat="server" Text="Re-Type Password:" />
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="uotextboxPWD1" runat="server" Width="220px" TextMode="Password" /><asp:RequiredFieldValidator
                                                        ID="uorfvPWD1" runat="server" ControlToValidate="uotextboxPWD1" ErrorMessage="Please re-type password."
                                                        ToolTip="Please re-type password." ValidationGroup="Create">*</asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                </td>
                                            </tr>
                                        </table>
                                        <div style="text-align: right; padding: 0 15px 0 0">
                                            <asp:Button ID="uobuttonSave" runat="server" Text="Save" ValidationGroup="Create"
                                                Width="77px" OnClick="uobuttonSave_Click" />
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                        </center>
                    </div>
                </div>
                </form>
            </asp:Panel>
        </center>
    </div>
</body>
</html>

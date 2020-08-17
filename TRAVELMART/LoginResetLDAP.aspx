<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginResetLDAP.aspx.cs" Inherits="TRAVELMART.LoginResetLDAP" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Travel Mart - Login</title>

    <script type="text/javascript">
        function ToUpper(ctrl) {
            var t = ctrl.value;
            ctrl.value = t.toUpperCase();
        }

    </script>

    <style type="text/css">
        .style1
        {
            padding-left:3px;
            height: 30px;
        }
        .style2
        {
            padding-left:3px;
            height: 28px;
        }
        .style3
        {
            
            height: 19px;
        }
        .pageTitle
        {
            padding-left:3px;
            font-size:medium;
        }
        .ErrorMessage
        {
            padding-bottom:5px;
            padding-top:5px;
            text-align:center;
            color:Red;    
        }
    </style>
</head>
<body style="background-image: url('Images/gradient.jpg'); background-repeat: repeat-x;
    font-family: Arial, Helvetica, sans-serif; font-size: 12px; color: #333;">
    <center>
        <div style="text-align: center; margin: auto;">
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
                        <img alt="" src="Images/exclamation.png" /></asp:Panel>
                    <div style="text-align: center; margin: auto; width: 400PX">
                        
                        <%--<asp:ChangePassword ID="ChangePassword1" BackColor="White" Width="400px" runat="server"
                            EnableTheming="True" OnCancelButtonClick="ChangePassword1_CancelButtonClick"
                            OnContinueButtonClick="ChangePassword1_ContinueButtonClick">
                            <ChangePasswordTemplate>--%>
                                
                                <div style="padding: 10px 10px 10px 10px; font-size: 15px; font-weight: bold;" align="left">
                                    CHANGE PASSWORD</div>
                                <center>
                                    <table border="0" cellpadding="1" cellspacing="0" style="border-collapse: collapse;" runat="server" id="uoTableChangePassword">
                                        <tr>
                                            <td>
                                                <div style=" border: solid 2px gray;">
                                                <table  cellpadding="0">
                                                    <tr>
                                                        <td align="center" class="pageTitle" colspan="2">
                                                            <asp:Label runat="server" ID="uoLabelUserName"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" class="ErrorMessage">
                                                            * Note: Password must be at least 9 characters long and a combination of 
                                                            Alpha-Numeric with upper and lower case letters.
                                                        </td>
                                                    </tr>
                                                  <%--  <tr>
                                                        <td align="right" class="style1">
                                                            <asp:Label ID="CurrentPasswordLabel" runat="server" AssociatedControlID="CurrentPassword">Password:&nbsp;&nbsp;</asp:Label>
                                                        </td>
                                                        <td class="style1" align="left">
                                                            <asp:TextBox ID="CurrentPassword" runat="server" TextMode="Password"
                                                                Width="150px"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="CurrentPasswordRequired" runat="server" ControlToValidate="CurrentPassword"
                                                                ErrorMessage="Password is required." ToolTip="Password is required." ValidationGroup="ChangePassword1">*</asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>--%>
                                                    <tr>
                                                        <td align="right" class="style2">
                                                            <asp:Label ID="NewPasswordLabel" runat="server" AssociatedControlID="NewPassword">New Password:&nbsp;&nbsp;</asp:Label>
                                                        </td>
                                                        <td class="style2" align="left">
                                                            <asp:TextBox ID="NewPassword" runat="server" TextMode="Password"
                                                                Width="150px"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="NewPasswordRequired" runat="server" ControlToValidate="NewPassword"
                                                                ErrorMessage="New Password is required." ToolTip="New Password is required."
                                                                ValidationGroup="ChangePassword1">*</asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" class="style2">
                                                            <asp:Label ID="ConfirmNewPasswordLabel" runat="server" AssociatedControlID="ConfirmNewPassword">Confirm New Password:&nbsp;&nbsp;</asp:Label>
                                                        </td>
                                                        <td class="style2" align="left">
                                                            <asp:TextBox ID="ConfirmNewPassword" runat="server" TextMode="Password"
                                                                Width="150px"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="ConfirmNewPasswordRequired" runat="server" 
                                                                ControlToValidate="ConfirmNewPassword"
                                                                ErrorMessage="Confirm New Password is required." 
                                                                ToolTip="Confirm New Password is required." 
                                                                ValidationGroup="ChangePassword1">*</asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                   <%-- <tr>
                                                        <td align="right" class="style2">
                                                            <asp:Label ID="uoLabelEmail" runat="server">Email Address :&nbsp;&nbsp;</asp:Label>
                                                        </td>
                                                        <td class="style2" align="left">
                                                            <asp:TextBox ID="uoTextBoxEmail" runat="server"
                                                                Width="150px"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="uoTextBoxEmail"
                                                                ErrorMessage="Confirm New Password is required." ToolTip="Email address is reqiured."
                                                                ValidationGroup="ChangePassword1">*</asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>--%>
                                                    <tr>
                                                        <td align="center" colspan="2" class="style3">
                                                            <asp:CompareValidator ID="NewPasswordCompare" runat="server" 
                                                                ControlToCompare="NewPassword" 
                                                                ControlToValidate="ConfirmNewPassword" 
                                                                Display="Dynamic" ErrorMessage="The Confirm New Password must match the New Password entry."
                                                                ValidationGroup="ChangePassword1"></asp:CompareValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" colspan="2" class="style2">
                                                            <asp:Button ID="ChangePasswordPushButton" runat="server" CommandName="ChangePassword"
                                                                Text="Save" ValidationGroup="ChangePassword1" OnClick="ChangePasswordPushButton_Click"
                                                                Width="60px" />&nbsp;&nbsp;
                                                            <asp:Button ID="CancelPushButton" runat="server" CausesValidation="False" CommandName="Cancel"
                                                                Text="Cancel" OnClick="CancelPushButton_Click" />
                                                            &nbsp;&nbsp;&nbsp;&nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                </table>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </center>
                          <%--  </ChangePasswordTemplate>
                        </asp:ChangePassword>--%>
                    </div>
                </div>
                </form>
            </asp:Panel>
        </div>
    </center>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RecoverPassword.aspx.cs" Inherits="TRAVELMART.RecoverPassword" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
     <script type="text/javascript" src="/Menu/jquery-1.3.1.min.js"></script>
    <title>Travel Mart - Recover Password</title>

    <script type="text/javascript">
        function ToUpper(ctrl) {
            var t = ctrl.value;
            ctrl.value = t.toUpperCase();
        }

    </script>

    <style type="text/css">
        .style1
        {
            height: 40px;
            text-align:center;
            font-weight:bold;
            font-size:large;
        }
        .style2
        {
            height: 28px;
        }
        #progressBackgroundFilter
        {
            position: fixed;
            top: 0px;
            bottom: 0px;
            left: 0px;
            right: 0px;
            overflow: hidden;
            padding: 0;
            margin: 0;
            background-color: #000;
            filter: alpha(opacity=50);
            opacity: 0.5;
            z-index: 1000;
        }
        #processMessage
        {
            position: fixed;
            top: 30%;
            left: 43%;
            padding: 10px;
            width: 14%;
            z-index: 1001;
            background-color: #fff;
            border: solid 1px #000;
            text-align: center;
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
                <script type="text/javascript" language="javascript">
                    var ModalProgress = '<%= uoModalPopUp.ClientID %>';

                    Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(beginReq);
                    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(endReq);

                    // Function is called when the Asynchronous postback starts, used to SHOW the Modal Popup.
                    function beginReq(sender, args) {
                        $find(ModalProgress).show(); // shows the Popup.
                    }

                    // Function is called when the Asynchronous postback ends, used to HIDE the Modal Popup.
                    function endReq(sender, args) {
                        $find(ModalProgress).hide(); // shows the Popup.
                    }
                </script>
                
                <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
                <div style="text-align: center; margin: auto;">
                    <div style="color: White; margin-left: 15px; padding-top: 5px; font-family: Arial;
                        font-weight: bold; font-size: 32px; margin-bottom: 15px">
                        <span style="color: #0099CC">Travel Mart</span><span style="color: #999"></span>
                    </div>
                    <asp:Panel ID="Panel2" runat="server" Style="color: #D8000C; margin: auto; margin-bottom: 10px;"
                        Visible="false">
                        <img src="Images/exclamation.png" />
                        <asp:Label runat="server" ID="UserNameFailureText" Text="Invalid UserName."></asp:Label>
                    </asp:Panel>
                    <asp:UpdatePanel ID="uoPanelBody" runat="server">
                        <ContentTemplate>
                            <div style="text-align: center; margin: auto; width: 400PX">
                            <asp:PasswordRecovery ID="PasswordRecovery1" runat="server" 
                                BackColor="White" Width="400px" SuccessPageUrl="~/Login.aspx"
                                EnableTheming="True" 
                                UserNameFailureText="Invalid Username. Please try again." 
                                onsendmailerror="PasswordRecovery1_SendMailError">
                                <UserNameTemplate>
                                    <div style="text-align:center; margin:auto;font-weight:bold; height:35px; font-size:large;">
                                        Forgot your Password?
                                    </div>
                                    <div style="text-align:center; margin:auto;height:31px;">
                                        Enter your User Name to receive your password.
                                    </div>
                                    <table style="text-align:center;">
                                        <tr>
                                            <td class="style2" align="right">UserName :</td>
                                            <td class="style2" align="left">
                                                <asp:TextBox runat="server" ID="UserName" Width="200px"
                                                    ValidationGroup="Submit"></asp:TextBox>
                                                <asp:RequiredFieldValidator runat="server"
                                                    ID="UserNameFailureText" ControlToValidate="UserName"
                                                    ValidationGroup="Submit">*</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" align="right" style="padding-right:5px;">
                                                <asp:Button runat="server" ID="Submit"
                                                Text="Submit" onclick="Submit_Click"/> &nbsp;
                                                <asp:Button runat="server" ID="Cancel" 
                                                Text="Cancel" onclick="Cancel_Click"/>
                                            </td>
                                        </tr>
                                    </table>
                                </UserNameTemplate>                             
                            </asp:PasswordRecovery>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <asp:UpdateProgress runat="server" ID="uoUpdateProgress" DisplayAfter="0" AssociatedUpdatePanelID="uoPanelBody">
                        <ProgressTemplate>
                            <asp:Panel runat="server" ID="uoPanelProgress">
                                <div id="progressBackgroundFilter">
                                </div>
                                <div id="processMessage">
                                    <img alt="Loading" src="Images/Processing.gif" />
                                    &nbsp; Processing Request...
                                </div>
                            </asp:Panel>
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                    <cc1:ModalPopupExtender runat="server" TargetControlID="uoPanelProgress" PopupControlID="uoPanelProgress"
                        ID="uoModalPopUp">
                    </cc1:ModalPopupExtender>                                
                </div>
                </form>
            </asp:Panel>
        </div>
    </center>    
</body>
</html>

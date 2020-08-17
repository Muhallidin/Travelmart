<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="TRAVELMART.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>   


<html xmlns="http://www.w3.org/1999/xhtml">


<head id="Head1" runat="server">
    <title>Travel Mart - Login</title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge"> 
    <link href="CSS/style.css" rel="stylesheet" type="text/css" />
    <link href="CSS/bootstrap.css" rel="stylesheet" type="text/css" />
    <!-- HTML5 shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <script src="Script/html5shiv.js" type="text/javascript"></script>

    <script src="Script/respond.js" type="text/javascript"></script>

    <script type="text/javascript" src="/FBox/jquery-1.6.1.min.js"></script>

    <script type="text/javascript" src="/Menu/jquery-1.3.1.min.js"></script>
    
    <script type="text/javascript" src="/FBox/fancybox/jquery.mousewheel-3.0.4.pack.js"></script>
    <script type="text/javascript" src="/FBox/fancybox/jquery.fancybox-1.3.4.pack.js"></script>
    <link rel="stylesheet" type="text/css" href="FBox/fancybox/jquery.fancybox-1.3.4.css"
        media="screen" />
        
    <script type="text/javascript"  language="javascript" >
        function ToUpper(ctrl) {
            var t = ctrl.value;
            ctrl.value = t.toUpperCase();
        }                
    </script>

</head>
<body>

    
    
   <div style="color: White; margin-left: 15px; padding-top: 5px; font-family: Arial;
        font-weight: bold; font-size: 36px; margin-bottom: 0px; text-align:center">
        <span style="color: #0099CC">Travel Mart</span><span style="color: #999"></span>
    </div>
   <div class="container">
        <div class="row" style="padding: 10px;">
           <%-- <div class="col-lg-12 logo-alignment"> <img src="Images/Maintenance/Splash%20Page-TravelMart.png" /></div> --%>            
        </div>
     <div class="row" >
        
        <%--<div class="col-lg-12" style="border:4px solid;">
            <div class="col-lg-12" style="text-align:center;">
            <img style="width:25%; azimuth" src="Images/Maintenance/SHRSS-Logo.png" />
            </div>
            <div class="col-lg-12 maintenance-message-text">
            <p class="maintenance-msg">Greetings from the TravelMart Team.<br/>
            <br />
            Please note that we will be conducting scheduled system maintenance work this afternoon, <strong>July 1, 2014</strong>, from <strong>2:00PM onwards</strong>. 
            <br /> <br />Thank you for your patience as we strive to improve your online experience.   </p>         
            </div>                        
        </div>--%>
     </div>
      <div class="row">
        &nbsp;
      </div> 
       
    </div>
  <%--        <div class="container">
          <div class="row">
            <div class="col-lg-2">&nbsp;</div>
            <div class="col-lg-8 logo-alignment"><img class="img-logo" src="Images/Maintenance/SHRSS-Logo.png" /></div>
            <div class="col-lg-2">&nbsp;</div>                
            </div>
            <div class="row">
            <div class="col-lg-2">&nbsp;</div>
            <div class="col-lg-8 logo-alignment"><span class="travelmart" >Travel Mart</span></div>
            <div class="col-lg-2">&nbsp;</div>             
            </div>
        <div class="row">
            <div class="col-lg-2">&nbsp;</div>
            <div class="col-lg-8 logo-alignment"><span class="maintenance-warning">We will have a system maintenance <br><strong>on June 23rd,</strong><br> from <strong>1:00AM to 4:00AM(EST)</strong >.</span></div>
            <div class="col-lg-2">&nbsp;</div>
        </div>          
        </div> --%>  
    <form id="form1" runat="server" >
        <script type="text/javascript" language="javascript">
            $(document).ready(function() {
                showUpdate();
            });

            function showUpdate() {

                $("#anchorShowUpdate").fancybox(
                {
                    'centerOnScroll': false,
                    'width': '50%',
                    'height': '70%',
                    'scrolling': 'no',
                    'autoScale': false,
                    'autoSize': true,
                    'transitionIn': 'fadeIn',
                    'transitionOut': 'fadeOut',
                    'type': 'iframe',
                    'onClosed': function() {
                        $("#Login1_LoginButton").click();
                    }
                });
            }
        </script>   
        
    <div style="text-align: center; margin: auto;width:300px;">
       <div style="color:Red; font-size:18px; margin-left: 15px; padding-top: 5px; text-align:center">
           TEST SYSTEM
     	</div>
        <asp:Panel ID="Panel2" runat="server" Style="color: #D8000C; margin: auto; margin-bottom: 10px;"
            Visible="false">
            <img src="Images/exclamation.png" />&nbsp;&nbsp;<asp:Label ID="LabelLoginErrorDetails"
                runat="server" Text=""></asp:Label>
        </asp:Panel>
        <asp:Login ID="Login1" runat="server" Style="text-align: center; margin: auto;" DestinationPageUrl="LoginProcess.aspx"
            OnLoginError="Login1_LoginError" onloggedin="Login1_LoggedIn" 
            onloggingin="Login1_LoggingIn" >
            <LayoutTemplate>
                <%-- Literal 
                    <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal>
                    --%>
                <asp:Panel ID="Panel1" runat="server" Style="background-color: White; border: 1px solid #000000;">
                    <div style="padding: 10px; text-align: left; margin-bottom: 5px;">
                        <div style="padding: 0px 0px 10px 5px; font-size: 15px; font-weight: bold;">
                            LOGIN:</div>
                        <%-- UserName --%>
                        <div style="padding: 5px; margin-bottom: 5px" id="LoginDiv">
                            <div>
                                <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">User Name:</asp:Label>
                            </div>
                            <asp:TextBox ID="UserName" runat="server" Width="250px" TabIndex="1"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName"
                                ErrorMessage="User Name is required." ToolTip="User Name is required." ValidationGroup="Login1">*</asp:RequiredFieldValidator>
                        </div>
                        <%-- Password --%>
                        <div style="padding: 5px; margin-bottom: 5px">
                            <div>
                                <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">Password:</asp:Label>
                            </div>
                            <asp:TextBox ID="Password" runat="server" TextMode="Password" Width="250px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password"
                                ErrorMessage="Password is required." ToolTip="Password is required." ValidationGroup="Login1">*</asp:RequiredFieldValidator>
                        </div>
                        
                        <div style="margin-bottom: 5px; padding: 0 0 0 3px;">
                            <asp:CheckBox ID="RememberMe" runat="server" Text="Remember me next time." />
                        </div>
                        
                        <%-- Login --%>
                        <div style="text-align: right; padding: 0 15px 0 0">
                            <%--<a id="anchorShowUpdate" href="UnderMaintenance2.html"></a>--%>
                                <asp:Button ID="LoginButton" runat="server" CommandName="Login" Text="Log In" 
                                ValidationGroup="Login1" onclick="LoginButton_Click"/>
                            
                        </div>
                        
                         <div style="padding: 5px; margin-bottom: 5px">
                             <a id="uoHyperLinkRecoverPassword" runat="server" href="RecoverPassword.aspx">
                               <span style="color:#0099CC">Forgot your password ?</span>
                            </a>
                            <%--<a id="A1" href="PasswordRecovery.aspx" visible="false" runat="server">Forgot your password?</a>--%>
                        </div>
                    </div>
                </asp:Panel>
            </LayoutTemplate>
        </asp:Login>
    </div>
    <asp:HiddenField ID="HiddenField1" runat="server" value='0'/>
    
        <script type="text/javascript"  language="javascript" >

            $("#Login1_UserName").blur(function() {
                var username = document.getElementById("Login1_UserName").value;
                if (username != '') { 
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: "/PageMethods.aspx/GetActiveUserVendor",
                        data: "{'userID': '" + username + "' }",
                        dataType: "json",
                        success: function(data) {
                            if (data.d == 0) {
                                alert('There is no active contract connected in your account.');
                                document.getElementById("Login1_UserName").focus();
                            }
                        }
                    });
                }
            });
        </script>
        
        <%--<asp:Label ID="lblLDAPUser" runat="server" Text="lblLDAPUser sample only"></asp:Label>--%>
        
        <%--<div>
            <table>
                <tr>
                    <td style="width:200px">
                        <asp:Label ID="lblLDAPUser" runat="server" Text="lblLDAPUser"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblLDAPSID" runat="server" Text="lblLDAPSID"></asp:Label>                        
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblLDAPRedirectPage" runat="server" Text="lblLDAPRedirectPage"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblLDAPValidated" runat="server" Text="lblLDAPValidated"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>--%>
        
           
    </form>
    
</body>
</html>

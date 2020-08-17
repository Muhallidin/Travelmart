<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UnderMaintenance2.aspx.cs" Inherits="TRAVELMART.UnderMaintenance2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Travelmart Page</title>
    <style type="text/css">
        .tableClass
        {
             
             font-family:Calibri;
             font-size:14px;
             width:600px;
             box-shadow: 5px 5px 30px #888888;
             -khtml-box-shadow: 5px 5px 30px #888888; 
             -moz-box-shadow: 5px 5px 30px #888888;
             -webkit-box-shadow: 5px 5px 30px #888888;
        }
        .tableClassNoBox
        {
             
             font-family:Calibri;
             font-size:14px;
             width:600px;                 
        }
        body
        {
        	background-color:White;
        }
    </style>
</head>

<body>
    <form id="form1" runat="server" >
    <div>
        <table cellpadding="0" cellspacing="0" class="tableClassNoBox">
            <tr>
                <td style ="text-align:center">
                    <br />
                    <img src="Images/LDAP-logo.png" style="width: 243px; height: 69px" />
                    <br />
                </td>
            </tr>        
            <tr>
                <td style="background:#ef5f50; color:White; text-align:center; vertical-align:middle; height:30px"  >   
                    REQUIRED SYSTEM UPDATE: Single sign-on registration for HR Systems                     
                </td>
            </tr>
            <tr>
                <td style="padding:20px 20px 20px 30px">
                    <span style="color:#868789; font-size:16px">
                        Our new one-stop HR portal, MyRCLHome.com, is coming soon!
                    </span>
                    <br />
                    <br />
                    <span style="color:#58595b">
                        As part of the enhanced online experience, we’re introducing a new single sign-on feature which allows you to use one user ID and password to easily access your RCL HR tools. 
                    </span>
                    
                    <br />
                    <br />
                    
                    <span style="color:#58595b">
                        All users are required to register for the single sign-on to any HR System. 
                    </span>
                    
                   <%-- <br />
                    <br />
                    
                    <span style="color:#5e95ce">
                        For more information, click here. 
                    </span> --%>

                    
                    <br />
                    <br />
                    <a href="Help/LDAP%20Message%20for%20vendors%20-%20user_print_dec18.pdf">For more information, click here. 
                    </a>
                    <br />
                    <br />
                    <span style="color:#58595b">
                        We appreciate your cooperation and we look forward to sharing more exciting MyRCLHome developments in the coming weeks.
                    <br />
                    </span>                
                </td>
            </tr>        
            <%--<tr>
                <td style="text-align:right; padding-right:10px; padding-bottom:10px">
                    <img src="Images/LDAP-close.png" style="height: 20px; width: 20px"/>
                </td>
            </tr>--%>
        </table>
     </div>
    </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Message.aspx.cs" Inherits="TRAVELMART.Message" %>
<%@ Register src="TravelmartMenu.ascx" tagname="TravelmartMenu" tagprefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Travelmart</title>
        <link href="App_Themes/Default/Stylesheet.css" rel="stylesheet" type="text/css" />
        <link href="App_Themes/Default/LinkMenu.css" rel="stylesheet" type="text/css" />
    <style type="text/css">        
        table.full_height {
        height:100%;
        width:1220px;
        border:1px solid black;
        background-color: #ECDFC4;
        vertical-align:top;
         font-family: tahoma;
         font-size: small;
         padding-bottom: 15px;
        }
        
        table.full_height td.tdClass
        {
            
        }
    </style>
</head>
<body bgcolor="#82ABB8" >
    <form id="form2" runat="server">   
     <center>
      <br />
        <table class="full_height" width="1220px">
            <tr>
                <td  colspan="2" style="vertical-align:top; ">                    
                <%--    <table cellpadding="0" cellspacing = "0" width="100%" style="vertical-align:top">--%>
                <table bgcolor="#ECDFC4" runat="server" id="tblMenu" style="height:95%;" cellpadding="0" cellspacing = "0" width="100%">
                        <tr>
                            <td class="BlueBanner"></td>
                            <%--<td class="BannerLeft"></td>
                            <td class="BannerMiddle">&nbsp;</td>
                            <td class="BannerRight"></td>--%>
                        </tr>
                    </table>
                </td>
            </tr>            
            <tr>
                <td style="padding-left:20px; vertical-align:top; text-align:left;">                            
                        <h2>
                            Your request could not be processed at this time.</h2>
                        <hr />
                       
                            <b>Error encountered:</b> &quot;<asp:Label ID="errorMessage" runat="server" 
                                Font-Bold="true"></asp:Label>
                            &quot;. We apologize for any inconvenience this may have caused you.
                        <br />
                        <br />
                        <b>Error In: </b> <asp:Label ID="uoLabelErrorIn" runat="server" Text=""></asp:Label>
                        <br />
                        <br />        
                        <b>Stack Trace:</b> 
                        <asp:Label ID="uoLabelStackTrace" runat="server" Text=""></asp:Label>
                        <hr />                        
                        <%--Return to <a href="../../../../../../Login.aspx">Homepage</a>--%>                       
                    
                </td>    
            </tr>
            <tr runat="server" id="uoRowPreviosPage">
                <td style="padding-left:20px; vertical-align:top; text-align:left; " valign="top">
                     Return to 
                    <asp:HyperLink ID="uoHyperLinkBack" NavigateUrl="#" runat="server">Previous Page</asp:HyperLink>                                         
                </td>
            </tr>
            </table> 
        </center>                 
    </form>    
</body>
</html>

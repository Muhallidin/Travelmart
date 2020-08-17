<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CrewAssistCancelHotel.aspx.cs" Inherits="TRAVELMART.CrewAssistCancelHotel" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <link href="../App_Themes/Default/Stylesheet.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../FBox/jquery-1.6.1.min.js"></script> 
    <style type="text/css">
        .style6
        {
            width: 200px;
            white-space: nowrap;            
        }
                
    </style>
</head>
<body>
    <form id="form1" runat="server">
    
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div class="ViewTitlePadding">
           <table  cellpadding="0" width="100%" cellspacing="0" class="PageTitle">
                <tr>
                    <td>
                        <asp:Label ID="uoLabelTitle" runat="server" Text="Cancel Hotel Booking" CssClass="Title"></asp:Label>                    
                    </td>
                </tr>
            </table>
        </div>
        
        <hr/> 
        
        <table>
            
            <tr>
                <td>
                 <asp:Label runat="server" ID="Label1" Text="HotelName"></asp:Label>
                <%--<span lang="en-us">:</span>--%> </td>
                <td colspan="3">
                    <asp:Label runat="server" ID="uoLabelHotel" Text="HotelName"></asp:Label>
                </td>
            </tr>
            
            <tr>
                <td> Email To: </td>
                <td colspan="3" style=" ">
                    <asp:TextBox ID="txtEmailTo" runat="server" 
                        onblur="validateMultipleEmailsSemiColonSeparated(this,';');" 
                        ToolTip="Use a semicolon (;) with no spaces to separate multiple e-mail addresses."
                         width="99%">
                    </asp:TextBox>
                </td>
            </tr>
            
            <tr>
                <td> Email CC: </td>
                <td colspan="3">
                    <asp:TextBox ID="txtEmailCC" runat="server"  Width="99%"></asp:TextBox>
                </td>
            </tr>
            
            <tr>
                <td class="style6" >
                    Comment:
                </td>
                <td colspan="3">
                    <asp:TextBox runat="server" ID="uoTextBoxComment" Width="98%" 
                                 TextMode="MultiLine" Rows="2"></asp:TextBox>
                </td>
            </tr>
            
            <tr>
                <td>
                    Confirmed By:
                </td>
                <td colspan="3">
                    <asp:TextBox ID="uoTextBoxConfirmedBy" Width="99%" runat="server"></asp:TextBox>
                </td>
            </tr>
            
            <tr>
                <td class="style6" ></td>
                <td colspan="3" style="width:99%; text-align:right; padding-right:2px; padding-top:5px;">
                    <asp:Button ID="btnSaveCancelHotel" runat="server" Text="Cancel Hotel Booking & Email" CssClass="SmallButton" 
                        OnClientClick="return ValidateFields();"
                        OnClick="btnSaveCancelHotel_click"
                        
                       />
                </td>
            </tr>
            
            
        </table>
    
    <asp:HiddenField ID="uoHiddenFieldHotelID" runat="server" />
    <asp:HiddenField ID="uoHiddenFieldHIDBigint" runat="server" />
    <asp:HiddenField ID="uoHiddenFieldSeqNo" runat="server" />
    <asp:HiddenField ID="uoHiddenFieldHTravelRequestID" runat="server" />
    <asp:HiddenField ID="uoHiddenFieldCurrentDate" runat="server" />
    <asp:HiddenField ID="uoHiddenFieldTimeZone" runat="server" />
    <asp:HiddenField ID="uoHiddenFieldUser" runat="server" />
    <asp:HiddenField ID="uoHiddenFieldIsVehicle" runat="server" />
    <asp:HiddenField ID="uoHiddenFieldVendorType" runat="server" />
    
     
    <script type="text/javascript" language="javascript">
    
        function ValidateFields() {
      
           
           var ConfirmedBy = document.getElementById("<%= uoTextBoxConfirmedBy.ClientID %>").value;
           var Comment = document.getElementById("<%= uoTextBoxComment.ClientID %>").value;
           var EmailTo = document.getElementById("<%= txtEmailTo.ClientID %>").value;

           if (EmailTo == '') {
               alert('Email to Required!');
               return false;

           } else if (Comment == '') {
                alert('Comment Required!');
               return false;
           } else if (ConfirmedBy == '') {
                alert('Confirm by Required!');
               return false;
           }
           return true;
        }
    
    </script>
    
    </form>
</body>
</html>

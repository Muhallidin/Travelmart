<%@ Page Title="" Language="C#" MasterPageFile="~/TravelMartMaintenanceMaster.Master"
    AutoEventWireup="true" CodeBehind="UnderMaintenance.aspx.cs" Inherits="TRAVELMART.Settings.UnderMaintenance" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        Padding1
        {
            padding-left:16px;    
        }
        Padding2
        {
            padding-left:2px;
            }
        .style1
        {
            width: 296px;
        }
        .style2
        {
            width: 120px;
            vertical-align: middle;
            white-space:nowrap;
        }
        .style3
        {
            width: 120px;
        }
        .style4
        {
            width: 209px;
        }
        .style5
        {
            width: 54px;
        }
        .style6
        {
            width: 145px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" runat="server">   

    <script type="text/javascript" language="javascript">
        function confirmEmail() {
            if (confirm("Email will be sent to ALL Users. Do you want to continue?") == true)
                return true;
            else
                return false;
        }
        function confirmEmailImmigration() {
            var txtFrom = $("#<%=uoTextBoxFrom.ClientID %>");
            var txtTo = $("#<%=uoTextBoxTo.ClientID %>");
            var newPwd = $("#<%=uoTextBoxNewPassword.ClientID %>");

            if (txtFrom.val() == "" || txtFrom.val() == "MM/dd/yyyy") {
                alert("Invalid date range!");
                return false;
            }
            if (txtTo.val() == "" || txtTo.val() == "MM/dd/yyyy") {
                alert("Invalid date range!");
                return false;
            }

            if (newPwd.val().trim() == "") {
                alert("New password required");
                return false;
            }
            
            if (confirm("Immigration Officers with Alternate Email will be reset. Do you want to continue?") == true)
                return true;
            else
                return false;
        }
        function CheckFromDate(d) {
            var txtFromDate = $("#<%=uoTextBoxFrom.ClientID %>");
            var txtToDate = $("#<%=uoTextBoxTo.ClientID %>");
            var dt = d.value;

            if (ValidateDate(dt)) {
                if (txtToDate.val() != '') {

                    var fromDate = Date.parse(dt);
                    var toDate = Date.parse(txtToDate.val());

                    if (fromDate > toDate) {
                        alert("Invalid date range!");
                        txtToDate.val('');
                        return false;
                    }
                }
            }
            else {
                alert("Invalid date!");
                txtFromDate.val('');
                return false;
            }
            return true;
        }
        function CheckToDate(d) {
            var txtFromDate = $("#<%=uoTextBoxFrom.ClientID %>");
            var txtToDate = $("#<%=uoTextBoxTo.ClientID %>");
            var dt = d.value;

            if (ValidateDate(dt)) {
                if (txtFromDate.val() != '') {

                    var toDate = Date.parse(dt);
                    var fromDate = Date.parse(txtFromDate.val());
                    if (fromDate > toDate) {
                        alert("Invalid date range!");
                        txtFromDate.val('');
                        return false;
                    }
                }
            }
            else {
                alert("Invalid date!");
                txtToDate.val('');
                return false;
            }
            return true;
        }
        function ValidateDate(pDate) {
            try {
                var dt = Date.parse(pDate);
                return true;
            }
            catch (err) {
                return false;
            }
        }
    </script>
    <div class="PageTitle">
        <table width="100%">
            <tr class="PageTitle">
                <td class="LeftClass">
                    Under Maintenance Settings
                </td>                
            </tr>
        </table>
    </div>
    <table width="100%" style="text-align:left">
        <%--<tr align="center">
            <td>
                <span>User Role:</span>
                <asp:DropDownList ID="uoDropDownUserType" runat="server" Width="250px" AutoPostBack="True"
                    AppendDataBoundItems="True" DataTextField="RoleToAccess" DataValueField="RoleToAccess"
                    OnSelectedIndexChanged="uoDropDownUserType_SelectedIndexChanged">
                    <asp:ListItem Text="" Value="" />
                </asp:DropDownList>
            </td>
            <td>
                Name :
                <asp:TextBox ID="uoTextBoxSearchParam" runat="server" Font-Size="7.5pt" Width="180px" />
                <asp:Button ID="uoButtonSearch" runat="server" Text="Search" Font-Size="X-Small"
                    OnClick="uoButtonSearch_Click" />
            </td>
        </tr>--%>
        <tr>
            <td class="style1">
                <asp:Button ID="uoButtonEmail" runat="server" Text="Under Maintenance Email"  Visible="false"
                onclick="uoButtonEmail_Click" Width="250px" OnClientClick="return confirmEmail();" />
            </td>
        </tr>
         <tr>
            <td class="style1">
                <asp:Button ID="uoButtonResume" runat="server" Text="Service Resumed Email"  Visible="false"
                onclick="uoButtonResume_Click" Width="250px" OnClientClick="return confirmEmail();"/>
            </td>
        </tr>
        </table>
        
        <%--New Password--%>
        <table width="100%" style="text-align:left">
        <tr>
            <td class="style2">
                Date Range From: 
            </td>                  
            <td class="style6">
                <asp:TextBox ID="uoTextBoxFrom" runat="server" onchange="return CheckFromDate(this);" Width="120px"></asp:TextBox>
                 <cc1:textboxwatermarkextender ID="uoTextBoxFrom_TextBoxWatermarkExtender" runat="server"
                    Enabled="True" TargetControlID="uoTextBoxFrom" WatermarkCssClass="fieldWatermark"
                    WatermarkText="MM/dd/yyyy">
                </cc1:textboxwatermarkextender>
                <cc1:calendarextender ID="uoTextBoxFrom_CalendarExtender" runat="server" Enabled="True"
                    TargetControlID="uoTextBoxFrom"  Format="MM/dd/yyyy">
                </cc1:calendarextender >
                <cc1:maskededitextender ID="uoTextBoxFrom_MaskedEditExtender" runat="server"
                    CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                    CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                    CultureTimePlaceholder="" Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="uoTextBoxFrom"
                    UserDateFormat="MonthDayYear">
                </cc1:maskededitextender>
            </td>                            
            <td class="style5">
                To: 
             </td> 
             <td> 
                <asp:TextBox ID="uoTextBoxTo" runat="server" onchange="return CheckToDate(this);" Width="120px"></asp:TextBox><cc1:textboxwatermarkextender ID="Textboxwatermarkextender1" runat="server"
                    Enabled="True" TargetControlID="uoTextBoxTo" WatermarkCssClass="fieldWatermark"
                    WatermarkText="MM/dd/yyyy">
                </cc1:textboxwatermarkextender>
                <cc1:calendarextender ID="Calendarextender1" runat="server" Enabled="True"
                    TargetControlID="uoTextBoxTo"  Format="MM/dd/yyyy">
                </cc1:calendarextender >
                <cc1:maskededitextender ID="Maskededitextender1" runat="server"
                    CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                    CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                    CultureTimePlaceholder="" Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="uoTextBoxTo"
                    UserDateFormat="MonthDayYear">
                </cc1:maskededitextender>
            </td>
        </tr>
        <tr>
            <td class="style3" >
                New Password:
            </td>
            <td class="style4" colspan="3">
                <asp:TextBox runat="server" Width="330px" ID="uoTextBoxNewPassword"></asp:TextBox>                           
            </td>
        </tr>
        <tr>
            <td colspan="4">                
                 <asp:Button ID="uoButtonImmigration" runat="server" Text="Reset Password of Immigration Role with Alternate Email"  
                 Width="457px" OnClientClick="return confirmEmailImmigration();" 
                    onclick="uoButtonImmigration_Click"/>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="NaviPlaceHolder" runat="server">

    

    <script type="text/javascript">
        $(document).ready(function() {
            SetTRResolution();
           
        });

        function pageLoad(sender, args) {
            var isAsyncPostback = Sys.WebForms.PageRequestManager.getInstance().get_isInAsyncPostBack();
            if (isAsyncPostback) {
                SetTRResolution();
               
            }
        }

        function SetTRResolution() {
            var ht = $(window).height();
            var wd = $(window).width()*0.945;
            if (screen.height <= 600) {
                ht = ht * 0.20;
            }
            else if (screen.height <= 720) {
                ht = ht * 0.39;
            }
            else {
                ht = ht * 0.55;
            }

            $("#Bv").height(ht);
            
        }


        function divScrollL() {
            var Right = document.getElementById('Av');
            var Left = document.getElementById('Bv');
            Right.scrollLeft = Left.scrollLeft;

        }
        function CloseModal(strURL) {
            window.location = strURL;
        } 
    </script>
    <div align="center" visible="false">  
        <asp:Label ID="Label1" runat="server" Text="List of Users Emailed"></asp:Label>
           <div id="Av" style="overflow: auto; overflow-x: hidden; overflow-y: hidden;">
                <div style="padding-left:16px;">
                <asp:ListView runat="server" ID="ListView1">
                    <LayoutTemplate>
                    </LayoutTemplate>
                    <ItemTemplate>
                    </ItemTemplate>
                    <EmptyDataTemplate>
                        <table border="0" cellpadding="0" cellspacing="0" class="listViewTableClass" width="100%" >
                            <tr >
                                <th>
                                    <asp:Label runat="server" ID="uoLblEmail" Text="Email" Width="300px"></asp:Label>
                                </th>                                
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                </asp:ListView>
                </div>              
            </div>
            <div id="Bv" style="overflow: auto; overflow-x: auto; overflow-y: auto;"
                onscroll="divScrollL();">
                <div style="padding-left:16px;">
                <asp:ListView runat="server" ID="uoUserList" >
                    <LayoutTemplate>
                        <table border="0" cellpadding="0" cellspacing="0" class="listViewTableClass" width="100%" >
                            <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                        </table>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <tr>
                            <td class="leftAligned" style="white-space: normal;">
                                <asp:Label runat="server" ID="uoLblUName" Text='<%# Eval("sEmail")%>' Width="300px"></asp:Label>
                            </td>                            
                        </tr>
                    </ItemTemplate>
                    <EmptyDataTemplate>
                        <table border="0" cellpadding="0" cellspacing="0" class="listViewTable" width="100%">
                            <tr>
                                <td colspan="" class="leftAligned">
                                    No Record
                                </td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                </asp:ListView>      
        </div>        
     </div>
    
    <asp:HiddenField ID="uoHiddenFieldRole" runat="server" Value="" />
    <asp:HiddenField ID="uoHiddenFieldUser" runat="server" Value="" />
    </div>
</asp:Content>

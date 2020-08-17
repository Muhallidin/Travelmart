<%@ Page Title="" Language="C#" MasterPageFile="~/TravelMartMaintenanceMaster.Master"
    AutoEventWireup="true" CodeBehind="UserImmigrationList.aspx.cs" Inherits="TRAVELMART.Administration.UserImmigrationList" %>
    

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
            width: 120px;
        }
        .ErrorMessage
        {
        	padding-bottom:5px;
            padding-top:5px;
            text-align:center;
            color:Red; 
        }
        .style3
        {
            padding-bottom: 5px;
            padding-top: 5px;
            text-align: left;
            color: Red;            
        }
        .style4
        {
            width: 300px;
            white-space:nowrap;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" runat="server">    
    <div class="PageTitle">
        <table width="100%">
            <tr class="PageTitle">
                <td class="LeftClass">
                    Immigration with Alternate Email Change Password
                </td>                
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="NaviPlaceHolder" runat="server">
    <div>
        <table style="text-align:left" width="100%">
        <tr>
            <td class="style1">
                Username or Email:
            </td>
            <td class="style4">
                <asp:TextBox ID="uoTextBoxSearch" runat="server" Font-Size="7.5pt" Width="280px" />
            &nbsp;
                <asp:Button ID="uoButtonSearch" runat="server" Text="Search" 
                    Font-Size="X-Small" Width="100 px" onclick="uoButtonSearch_Click"/>
            </td>  
            <td></td>          
        </tr>
        <tr>
            <td class="style1">
                New Password:
            </td>
            <td class="style4">
                <asp:TextBox ID="uoTextBoxNewPassword" runat="server" Font-Size="7.5pt" Width="280px" /> &nbsp;
                <asp:Button ID="uoButtonChangePassword" runat="server" Text="Change Password" OnClientClick='return ValidateIfCheck();'
                    Font-Size="X-Small" Width="100 px" onclick="uoButtonChangePassword_Click"/>
            </td>
            <td></td>                  
        </tr>
        <tr>
            <td></td>
            <td  class="style3" colspan="2">
                * Note: Password must be at least 9 characters long and a combination of 
                  Alpha-Numeric with upper and lower case letters & special character.
            
            </td>
            
        </tr>
    </table>
    </div>
    <div id="Av" style="overflow: auto; overflow-x: hidden; overflow-y: hidden;">
        <div style="padding-left:16px;">
        <asp:ListView runat="server" ID="ListView1">
            <LayoutTemplate>
                <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
            </LayoutTemplate>
            <ItemTemplate>
            </ItemTemplate>
            <EmptyDataTemplate>                
                <table border="0" cellpadding="0" cellspacing="0" class="listViewTableClass" width="100%"  >
                        <th >
                            <asp:CheckBox runat="server" ID="uoCheckBoxSelectAll" Text="" Width="50px" OnClick="CheckUncheckAll(this);">
                            </asp:CheckBox>
                        </th>
                        <th >
                            <asp:LinkButton runat="server" ID="uoLinkButtonUname" CommandName="Uname" Text="Username" Width="200px">
                            </asp:LinkButton>
                        </th>
                        <th >
                            <asp:LinkButton runat="server" ID="LinkButton2" CommandName="FName" Text="Firstname" Width="200px">
                            </asp:LinkButton>
                        </th>
                        <th >                           
                            <asp:LinkButton runat="server" ID="LinkButton4" CommandName="LName" Text="Lastname" Width="100px">
                            </asp:LinkButton>
                        </th>                      
                         <th >
                            <asp:LinkButton runat="server" ID="LinkButton6" CommandName="Email" Text="Email" Width="200px">
                            </asp:LinkButton>
                        </th>
                        <th >
                            <asp:LinkButton runat="server" ID="LinkButton5" CommandName="AltEmail" Text="Alternate Email" Width="200px">
                            </asp:LinkButton>
                        </th>                       
                        <th runat="server"  id="Th1" >
                            <asp:Label runat="server" ID="uoLblScrollHdr" Text="" Width="10px"></asp:Label>
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
                <table border="0" cellpadding="0" cellspacing="0" class="listViewTableClass" width="100%" id="uoTableUser">
                    <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                </table>
            </LayoutTemplate>
            <ItemTemplate>
                <tr>
                    <td>
                       <asp:CheckBox runat="server" ID="uoCheckBoxSelect" Text="" Width="50px">
                        </asp:CheckBox>
                    </td>
                    <td class="leftAligned" style="white-space: normal;">
                        <asp:Label runat="server" ID="uoLblUName" Text='<%# Eval("UserName")%>' Width="204px"></asp:Label>
                    </td>
                    <td class="leftAligned" style="white-space: normal;">
                        <asp:Label runat="server" ID="uoLabelFName" Text='<%# Eval("Firstname")%>' Width="202px"></asp:Label>
                    </td>
                    <td class="leftAligned" style="white-space: normal;">
                        <asp:Label runat="server" ID="uoLabelLName" Text='<%# Eval("Lastname")%>' Width="103px"></asp:Label>
                    </td>
                    <td class="leftAligned" style="white-space: normal;">
                        <asp:Label runat="server" ID="uoLabelEmail" Text='<%# Eval("Email")%>' Width="203px"></asp:Label>
                    </td>
                    <td class="leftAligned" style="white-space: normal;">
                        <asp:Label runat="server" ID="uoLabelAltEmail" Text='<%# Eval("AlternateEmail")%>' Width="203px"></asp:Label>
                    </td>                                        
                </tr>
            </ItemTemplate>
            <EmptyDataTemplate>
                <table border="0" cellpadding="0" cellspacing="0" class="listViewTable" width="100%">
                    <tr>
                        <td colspan="4" class="leftAligned">
                            No Record
                        </td>
                    </tr>
                </table>
            </EmptyDataTemplate>
        </asp:ListView>
        <div class="LeftClass">
        <asp:DataPager ID="uoUserListPager" runat="server" PagedControlID="uoUserList" PageSize="20" 
            OnPreRender="uoUserListPager_PreRender">
            <Fields>
                <asp:NumericPagerField ButtonType="link" NumericButtonCssClass="PagerClass" />
            </Fields>
        </asp:DataPager>
        </div>
        
    </div>
    </div>
    
    <asp:HiddenField ID="uoHiddenFieldRole" runat="server" Value="" />
    <asp:HiddenField ID="uoHiddenFieldUser" runat="server" Value="" />
    <asp:HiddenField ID="uoHiddenFieldOrderBy" runat="server" Value="Uname" />
    

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
            var wd = $(window).width() * 0.945;

            
            ht = ht - 220;
            $("#Bv").height(ht);            
        }


        function divScrollL() {
            var Right = document.getElementById('Av');
            var Left = document.getElementById('Bv');
            Right.scrollLeft = Left.scrollLeft;

        }

        function CheckUncheckAll(obj) {
            $('input[name*="uoCheckBoxSelect"]').attr('checked', obj.checked);
        }
        function ValidateIfCheck() {
           
            var bIsCheck = false;
            $("#uoTableUser tr").each(function(i, element) {
                var $TDs = $(this).find('td');
                var chk = $('td:eq(0) input[name*="uoCheckBoxSelect"]', this);
                
                
                if (chk != null) {
                    if (chk.attr('checked')) {
                        bIsCheck = true;
                    }
                }
            });

            if (bIsCheck == false) {
                alert("No selected record!");
                return false;
            }
        }
        function CloseModal(strURL) {
            window.location = strURL;
        } 
    </script>
</asp:Content>
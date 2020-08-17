<%@ Page Title="" Language="C#" MasterPageFile="~/TravelMartMaintenanceMaster.Master"
    AutoEventWireup="true" CodeBehind="UserListWithToken.aspx.cs" Inherits="TRAVELMART.Settings.UserListWithToken" %>

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
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" runat="server">   
    <script type="text/javascript" language="javascript">
        
    </script>
    <div class="PageTitle">
        <table width="100%">
            <tr class="PageTitle">
                <td class="LeftClass">
                    User List with Token
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
            <td>
                <asp:Button ID="uoButtonUserList" runat="server" Text="Get User List"  CssClass="TextBoxInput"
                    Width="250px" onclick="uoButtonUserList_Click"  />
                    <br />
            </td>
        </tr>
      <%--  <tr>
            <td>
                <asp:TextBox ID="uoTextBoxExncrypted" runat="server" Width="250px" CssClass="TextBoxInput"></asp:TextBox>
                <asp:TextBox ID="uoTextBoxUserName" runat="server" Width="250px" CssClass="TextBoxInput"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                 <asp:Button ID="uoButtonDecrypt" runat="server" Text="Decrypt"  CssClass="TextBoxInput"
                    Width="250px" onclick="uoButtonDecrypt_Click"   />
                <asp:Label ID="uoLabelDecrypt" runat="server" Text="Decrypted Value"></asp:Label>
            </td>
        </tr>--%>
         <tr>
            <td>
                <asp:GridView ID="uoGridViewUsers" runat="server" AutoGenerateColumns="False" GridLines="Both"                                          
                    Width="750px"  CssClass="listViewTable">                                            
                    <Columns>                                           
                      <asp:BoundField DataField="sEnc" HeaderText="Token" ItemStyle-Width="100px" HeaderStyle-Width="100px" ItemStyle-CssClass="leftAligned" />
                      <asp:BoundField DataField="sUserName" HeaderText="User Name" ItemStyle-Width="100px" HeaderStyle-Width="100px" ItemStyle-CssClass="leftAligned" />                     
                      <asp:BoundField DataField="sUserEmail" HeaderText="Email" ItemStyle-Width="100px" HeaderStyle-Width="100px" ItemStyle-CssClass="leftAligned" />                     
                      <asp:BoundField DataField="dDateCreated" HeaderText="Date Created" 
                        DataFormatString="{0:dd-MMM-yyyy HHmm}"
                        ItemStyle-Width="100px" HeaderStyle-Width="100px" ItemStyle-CssClass="leftAligned" />                                            
                    </Columns>                                               
                </asp:GridView>
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
    </script>
    <div align="center">  
            
     </div>
    
    <asp:HiddenField ID="uoHiddenFieldRole" runat="server" Value="" />
    <asp:HiddenField ID="uoHiddenFieldUser" runat="server" Value="" />
    </div>
</asp:Content>
